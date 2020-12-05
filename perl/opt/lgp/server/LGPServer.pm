#!/usr/bin/perl -w

########################################################################################################
# @description Server Boundary Class ( Module ) - Net IO
#
# @author David O Neill
# @studentid 0813001
# @email dave@feeditout.com
# @supervisor J.J Collins
# @project Envisioning group policy for a linux heterogeneous environment
########################################################################################################

package LGPServer;

use threads;
use threads::shared;
use Thread;
use Thread::Queue;
use Thread::Semaphore;
use strict;
use warnings;
use IO::Socket;
use IO::Socket::INET;
use Net::hostent;
use utf8;
use English;
use POSIX;
use LGPMysqlDBI;
use MIME::Base64;
use JSON;
use Try::Tiny;
use LGPSyslog;
use Time::HiRes qw( usleep );
use diagnostics;
use List::Util qw(sum);
use ModuleHandler;

my $criticalSection = Thread::Semaphore->new();

sub new 
{
	my( $class ) = @_;
	
	
	my $self = {};
	my $_continue : shared = 1;
	my $_loghandler : shared;
	my $_sender_Socket : shared;
	my $_receiver_Socket : shared;
	my $_announcer_Socket : shared;
	my @_request_handlers : shared = ();
	my $_out_queue : shared = Thread::Queue->new;
	my $_in_queue : shared = Thread::Queue->new;
	my $_policies_updating : shared = 0;
	my %_clients : shared;
	my %_policy_cache : shared;
	my $_policy_cache_size : shared = 1;
	my $_cpuusage : shared = 0;
	my $_rxkb : shared = 0;
	my $_txkb : shared = 0;
	
	$self =
	{
		_protocol => undef,								# not shared implicitly
		_port => undef,									# not shared implicitly
		_host => undef,									# not shared implicitly
		_receiver_socket => undef,						# thread will assign handle
		_sender_socket => undef,						# thread will assign handle
		_announcer_socket => undef,						# thread will assign handle
		_outgoing_queue => \$_out_queue,				# thread will assign messages
		_incoming_queue => \$_in_queue,					# thread will assign messages
		_sending_thread => undef,						# owned by parent
		_receive_thread => undef,						# owned by parent
		_announcer_thread => undef,						# owned by parent
		_queue_thread => undef,							# owned by parent
		_policy_update_thread => undef,					# owned by parent
		_monitor_thread => undef,						# owned by parent
		_request_handlers => \@_request_handlers,		# thread will push handleS
		_log => \$_loghandler,							# thread will push messages
		_pipe => undef,									# not shared implicitly
		_continue => \$_continue,						# used to kill loops globally
		_mysql_connection => undef,                     # mysql connection
		_policies_updating => \$_policies_updating,     # when polcies are being updated, clients will have to wait
		_clients_list => \%_clients,					# client guid to ou mapping
		_cpu_usage => \$_cpuusage,						# monitor cpu
		_tx_usage => \$_txkb,							# monitor cpu
		_rx_usage => \$_rxkb,							# monitor cpu
		_policy_cache => \%_policy_cache,				# cache ou policy to improve performance, reduce disk access
		_policy_cache_size => \$_policy_cache_size		# policy cache size ( num of ou policies )
	};
	
	bless( $self , $class );
	
	return $self;
}


sub start
{
	my( $self , $host , $port , $protocol ) = @_;

	$self->{ _protocol } = $protocol;
	$self->{ _port } = $port;
	$self->{ _host } = $host;
	$self->{ _log } = \LGPSyslog::getInstance();
	
	if( defined( ${ $self->{ _log } } ) )
	{
		$criticalSection->down();
		${ $self->{ _log } }->log( 'info' , "Starting ..." );
		$criticalSection->up();
	}
	
	$self->{ _policy_update_thread } = threads->create( "policyupdater" , $self );
	$self->{ _announcer_thread } = threads->create( "announcer" , $self );
	$self->{ _receive_thread } = threads->create( "receiver" , $self );
	$self->{ _sending_thread } = threads->create( "sender" , $self );
	$self->{ _queue_thread } = threads->create( "queueHandler" , $self );
	$self->{ _monitor_thread } = threads->create( "monitor" , $self );
	
	$self->{ _pipe } = "/tmp/" . $0 . ".pipe";

	if( -e $self->{ _pipe } )
	{
		system( "rm -rf " . $self->{ _pipe } );
	}

	system( "mkfifo " . $self->{ _pipe } );	

	if( sysopen( FIFO , $self->{ _pipe } , O_RDWR ) ) 
	{	
		while( ${ $self->{ _continue } } != -1 ) 
		{
	        my $input = <FIFO>;
	        chomp( $input );

	        if( $input eq 'stop' )
	        {
	           	${ $self->{ _continue } } = -1;
	        	last;
	        }
	    }

		close(FIFO);
	} 
	else 
	{
		if( defined( ${ $self->{ _log } } ) )
		{
			$criticalSection->down();
			${ $self->{ _log } }->log( 'err' , "ERROR: Reading $self->{ _pipe }: $!" );
			$criticalSection->up();
		}
	}
	
	$self->stop();
}


sub stop
{
	my( $self ) = @_;
	
	if( defined( ${ $self->{ _log } } ) )
	{
		$criticalSection->down();
		${ $self->{ _log } }->log( 'info' , "Shutting down ..." );
		$criticalSection->up();
	}
	
	${ $self->{ _continue } } = -1;
	
	if( defined( ${ $self->{ _sender_socket } } ) )
	{
		local $SIG{ __WARN__ } = sub{};
		close( ${ $self->{ _sender_socket } } );
	}
	
	if( defined( ${ $self->{ _receiver_socket } } ) )
	{
		local $SIG{ __WARN__ } = sub{};
		close( ${ $self->{ _receiver_socket } } );
	}
	
	if( defined( ${ $self->{ _announcer_socket } } ) )
	{
		local $SIG{ __WARN__ } = sub{};
		close( ${ $self->{ _announcer_socket } } );
	}
	
	if( defined( $self->{ _receive_thread } ) )
	{
		$self->{ _receive_thread }->kill( 'KILL' )->detach();
	}
	
	if( defined( $self->{ _sending_thread } ) )
	{
		$self->{ _sending_thread }->kill( 'KILL' )->detach();
	}
	
	if( defined( $self->{ _announcer_thread } ) )
	{
		$self->{ _announcer_thread }->kill( 'KILL' )->detach();
	}
	
	if( defined( $self->{ _policy_update_thread } ) )
	{
		$self->{ _policy_update_thread }->kill( 'KILL' )->detach();
	}
	
	if( defined( $self->{ _queue_thread } ) )
	{
		$self->{ _queue_thread }->kill( 'KILL' )->detach();
	}
	
	if( defined( $self->{ _monitor_thread } ) )
	{
		$self->{ _monitor_thread }->kill( 'KILL' )->detach();
	}
	
	# cleanup any dead threads
    for( my $i = 0; $i < @{ $self->{ _request_handlers } }; $i++ )
    {
    	$self->{ _request_handlers }->[ $i ]->kill( 'KILL' )->detach();
    }
    
    if( defined( $self->{ _pipe } ) )
	{
		system( "rm -rf " . $self->{ _pipe } );
	} 
	
	exit(0);
}


sub sender
{
	$SIG{'KILL'} = sub { threads->exit(); };
	
	my $self = shift;
    my $queue = ${ $self->{ _outgoing_queue } };
	
	while( ${ $self->{ _continue } } != -1 ) 
	{
		my $socket;
		
		if( ${ $self->{ _policies_updating } } == 1 )
		{
			usleep 500000;
		}
		else
		{
			while( $queue->pending ) 
	        {
	            my $data = $queue->dequeue;
				my $peer = $data->{ peer };
				my $responcePort = 50001;
				
				my $json = new JSON;
				my $jsonresp = $json->allow_nonref->decode( $data->{ request } );

				if( $jsonresp->{ peerPort } =~ /([0-9]+)/ )
				{
					$responcePort = $1;
				}
				else
				{
					$responcePort = 50001;
				}

				$socket = IO::Socket::INET->new
		        ( 
		           	PeerAddr => $peer, 
		           	PeerPort => $responcePort, 
		           	Proto => "tcp", 
		           	Type => SOCK_STREAM
		        );
				
			    if( !defined( $socket ) )
			    {					
					if( defined( ${ $self->{ _log } } ) )
					{
						$criticalSection->down();
						${ $self->{ _log } }->log( 'err' , "Couldn't connect to $peer:$responcePort : $@" );
						$criticalSection->up();
					}

			    	if( defined( $data->{ attempts } ) )
	            	{
	            		$data->{ attempts } = $data->{ attempts }  + 1;
	            	}
	            	else
	            	{
	            		$data->{ attempts } = 1;
	            	}

	            	if( $data->{ attempts } < 3 )
	            	{
	             		$queue->enqueue( $data );
	            	}
			    }
			    else
			    {					
			    	${ $self->{ _sender_Socket } } = fileno $socket; 

					$socket->send( encode_base64( $data->{ response } ) );
					
					if( defined( ${ $self->{ _log } } ) )
					{
						$criticalSection->down();
						${ $self->{ _log } }->log( 'info' , "Sent response to client : " . $peer );
						$criticalSection->up();
					}

					close( $socket );
			    } 
	        }
		}
		
	    usleep 500000;
	}      
}


sub receiver
{
	$SIG{'KILL'} = sub { threads->exit(); };
	
	my $self = shift;
	
	while( ${ $self->{ _continue } } != -1 ) 
	{
		my $socket = IO::Socket::INET->new
		( 
			Proto => $self->{ _protocol }, 
			LocalAddr => $self->{ _host }, 
			LocalPort => $self->{ _port }, 
			Listen => SOMAXCONN, 
			Reuse => 1
		);
		
		if( !defined( $socket ) ) 
		{
			if( defined( ${ $self->{ _log } } ) )
			{
				$criticalSection->down();
				${ $self->{ _log } }->log( 'err' , "Client Couldn't bind to 0.0.0.0:" . $self->{ _port } . " : $@" );
				$criticalSection->up();
			}
			
			${ $self->{ _continue } } = -1;
		}
		else
		{
			${ $self->{ _receiver_socket } } = fileno $socket;
			
			if( defined( ${ $self->{ _log } } ) )
			{
				$criticalSection->down();
				${ $self->{ _log } }->log( 'info' , "Server accepting " . $self->{ _protocol } . " connections on " . $self->{ _host } . ":" . $self->{ _port } );
				$criticalSection->up();
			}
			
			while( my $connection = $socket->accept() ) 
			{  
				if( defined( ${ $self->{ _log } } ) )
				{
					$criticalSection->down();
					${ $self->{ _log } }->log( 'info' , "Received connection from " . $connection->peerhost() );
					$criticalSection->up();
				}
				
		    	push( @{ $self->{ _request_handler } } , threads->create( "handleRequest" , $self , $connection )->detach() );
			}
		}
		
		usleep 500000;
	}
}


sub announcer
{
	$SIG{'KILL'} = sub { threads->exit(); };
	
	my $self = shift;
	
	my $socket = IO::Socket::INET->new
	( 
		Proto => 'udp', 
		PeerAddr => '255.255.255.255' , 
		PeerPort => '50005',
		LocalAddr => '0.0.0.0',
		Broadcast => 1
	);
	
	$socket->sockopt( SO_BROADCAST , 1 );
	
	if( !defined( $socket ) ) 
	{
		if( defined( ${ $self->{ _log } } ) )
		{
			$criticalSection->down();
			${ $self->{ _log } }->log( 'err' , "Couldn't bind to 255.255.255.255:" . $self->{ _port } . " : $@ " );
			$criticalSection->up();
		}
		${ $self->{ _continue } } = -1;
	}
	else
	{
		${ $self->{ _announcer_socket } } = fileno $socket;
		
		if( defined( ${ $self->{ _log } } ) )
		{
			$criticalSection->down();
			${ $self->{ _log } }->log( 'info' , "Server announce broadcasting " . $self->{ _protocol } . " 255.255.255.255:" . '50005' );
			$criticalSection->up();
		}	
		
		while( ${ $self->{ _continue } } != -1 ) 
		{
			my $message = "{
				\"tx\" : " . ${ $self->{ _tx_usage } } . " ,
				\"rx\" : " . ${ $self->{ _rx_usage } } . " ,
				\"outgoing\" : " . ${ $self->{ _outgoing_queue } }->pending() . " ,
				\"incoming\" : " . ${ $self->{ _incoming_queue } }->pending() . " ,
				\"cpuusage\" : \"" . ${ $self->{ _cpu_usage } } . "\"
			}";
			$socket->send( $message );
			sleep 1;
		}
	}
}


sub policyupdater
{
	$SIG{'KILL'} = sub { threads->exit(); };
	
	my $self = shift;
	
	my %request :shared = 
    (
    	peer => "0.0.0.0",
    	request => "{ 
						\"peerPort\" : \"0\" ,
						\"admin\" : \"0\" ,
						\"request\" : \"updatepolicies\"
					}"
    );

	while( ${ $self->{ _continue } } != -1 ) 
    {
		${ $self->{ _incoming_queue } }->enqueue( \%request );
		
		sleep 600;
	}
}


sub monitor
{
	$SIG{'KILL'} = sub { threads->exit(); };
	
	my $self = shift;

	my ( $previdle , $prevtotal ) = qw(0 0);
	my $totalRX = 0;
	my $prevTotalRX = 0;
	my $totalTX = 0;
	my $prevTotalTX = 0;
	my $temp;
	
	while( ${ $self->{ _continue } } != -1 ) 
	{
		if( open( STAT , '/proc/stat') )
		{
			while(<STAT>) 
			{
				next unless /^cpu\s+[0-9]+/;
				my @cpu = split( /\s+/ , $_ );
				shift @cpu;

				my $idle = $cpu[ 3 ];
				my $total = sum( @cpu );

				my $diffidle = $idle - $previdle;
				my $difftotal = $total - $prevtotal;
				my $diffusage = 100 * ( $difftotal - $diffidle ) / $difftotal;

				$previdle = $idle;
				$prevtotal = $total;

				${ $self->{ _cpu_usage } } = sprintf( "%d" , $diffusage );
			}
			close STAT;			
		}
		
		if( open( RXSTAT , '/sys/class/net/eth0/statistics/rx_bytes' ) )
		{			
			while(<RXSTAT>) 
			{
				$temp = $_;
				$temp =~ s/^\s+//;
				$temp =~ s/\s+$//;            
			}
		
			$prevTotalRX = $totalRX;
			$totalRX = int( $temp ); 
			
			${ $self->{ _rx_usage } } = sprintf( "%d" , ( ( $totalRX - $prevTotalRX ) / 1024 ) );
		
			close RXSTAT;
		}
	
		if( open( TXSTAT , '/sys/class/net/eth0/statistics/tx_bytes' ) )
		{	
			while(<TXSTAT>) 
			{
				$temp = $_;
				$temp =~ s/^\s+//;
				$temp =~ s/\s+$//;
			}		
		
			$prevTotalTX = $totalTX;					
			$totalTX = int( $temp ); 
			
			${ $self->{ _tx_usage } } = sprintf( "%d" , ( ( $totalTX - $prevTotalTX ) / 1024 ) );
		
			close TXSTAT;
		}
		
		usleep 500000;
	}	
}


sub policycacher
{
	my ( $self , $ou ) = @_;
	
	my $policy;
	my $size = scalar( keys( %{ $self->{ _policy_cache } } ) );
	
	my %entry : shared = (
		_lastaccessed => undef,
		_policy => undef
	);
	
	if( !defined( $self->{ _policy_cache }->{ $ou } ) )
	{
		$policy = "./policies/" . $ou . ".dsl";
		$policy = `cat $policy`;
		$policy =~ s/\r|\r\n|\n/\\n/g;
		$policy =~ s/\t/ /g;
		
		if( $size >= ${ $self->{ _policy_cache_size } } )
		{
			my $firstaccessed = undef;
			
			foreach my $key ( keys( %{ $self->{ _policy_cache } } ) )
			{
				if( !defined( $firstaccessed ) )
				{
					$firstaccessed = $key;
				}
				else
				{
					my %current = %{ $self->{ _policy_cache }->{ $key } };
					my %last = %{ $self->{ _policy_cache }->{ $firstaccessed } };
			
					if( $current{ _lastaccessed } < $last{ _lastaccessed } )
					{
						$firstaccessed = $key;
					}
				}
			}
			
			delete( $self->{ _policy_cache }->{ $firstaccessed } );
			
			$entry{ _lastaccessed } = time;
			$entry{ _policy } = $policy;
			
			$self->{ _policy_cache }->{ $ou } = \%entry;
		}
		else
		{
			$entry{ _lastaccessed } = time;
			$entry{ _policy } = $policy;
			
			$self->{ _policy_cache }->{ $ou } = \%entry;
		}
	}
	else
	{
		%entry = %{ $self->{ _policy_cache }->{ $ou } };
		$entry{ _lastaccessed } = time;
		$policy = $entry{ _policy };
		
		$self->{ _policy_cache }->{ $ou } = \%entry;
	}
	
	return $policy;
}


sub queueHandler
{
	$SIG{'KILL'} = sub { threads->exit(); };
	
    my $self = shift;
    my $queue = ${ $self->{ _incoming_queue } };

	my $_mysql = new LGPMysqlDBI();
	
	if( $_mysql->Connect( "192.168.1.10" , "LGP" , "root" , "bjc4200" ) == 1 )
	{
		${ $self->{ _mysql_connection } } = $_mysql;
		
		if( defined( ${ $self->{ _log } } ) )
		{
			$criticalSection->down();
			${ $self->{ _log } }->log( 'info' , "Mysql connection successfull" );
			$criticalSection->up();
		}
	}
	else
	{
		if( defined( ${ $self->{ _log } } ) )
		{
			$criticalSection->down();
			${ $self->{ _log } }->log( 'err' , "Mysql connection unsuccessfull" );
			$criticalSection->up();
		}
	}
    
    while( ${ $self->{ _continue } } != -1 ) 
    {
        while( $queue->pending ) 
        {
            my $data = $queue->dequeue;
			
			my $json = new JSON;
			my $jsonresp = $json->allow_nonref->decode( $data->{ request } );
			my $response = "{ \"\" : \"\" }";
			
			if( defined( ${ $self->{ _mysql_connection } } ) )
			{
				if( ${ $self->{ _mysql_connection } }->IsConnected() == 1 )
				{
					if( !defined( $jsonresp->{ 'admin' } ) )
					{
						my $peer = $data->{ peer };
						my $peerport = $jsonresp->{ 'peerPort' };
						my $guid = $jsonresp->{ 'guid' };
						my $os = $jsonresp->{ 'os' };
						my $version = $jsonresp->{ 'version' };
						my $architecture = $jsonresp->{ 'architecture' };
						my $kernel = $jsonresp->{ 'kernel' };
						my $psuedoname = $jsonresp->{ 'psuedoName' };
						my $distribution = $jsonresp->{ 'distribution' };
						my $distroVersion = $jsonresp->{ 'distroVersion' };
						my $hostname = $jsonresp->{ 'hostname' };
						my $clientVersion = $jsonresp->{ 'clientVersion' };

						${ $self->{ _mysql_connection } }->UpdateClient
						( 
							$hostname , $os , $guid , $kernel , $architecture , $distribution , $distroVersion , $version , $clientVersion , $psuedoname , $peer , $peerport 
						);
					
						if( !defined( $self->{ _clients_list }->{ $guid } ) )
						{
							$self->{ _clients_list }->{ $guid } = ${ $self->{ _mysql_connection } }->GetOu( $guid );
						}
						
						if( $jsonresp->{ request } eq "update" )
						{
							my $guid = $jsonresp->{ guid };
							if( defined( $self->{ _clients_list }->{ $guid } ) )
							{
								my $ou = $self->{ _clients_list }->{ $guid };
								if( $ou > 0 )
								{
									my $policyFile = $self->policycacher( $ou );
									$response = "{ \"policy\" : \"" . $policyFile . "\" }";
								}
							}
						}
						
						if( $jsonresp->{ request } eq "updatemodules" )
						{
							my $modulesObj = new ModuleHandler();
							my $moduleFiles = $modulesObj->getModules();
							$moduleFiles =~ s/\r|\r\n|\n/\\n/g;
							$moduleFiles =~ s/\t/ /g;
							
							$response = "{ \"modules\" : " . $moduleFiles . " }";
						}

			            my %response :shared = 
				        (
				        	peer => $data->{ peer },
							request => $data->{ request },
				        	response => $response
				        );

				        ${ $self->{ _outgoing_queue } }->enqueue( \%response );
						
					}
					else
					{					
						if( $jsonresp->{ request } eq "updatepolicies" )
						{
							if( defined( ${ $self->{ _log } } ) )
							{
								$criticalSection->down();
								${ $self->{ _log } }->log( 'info' , "Requested policy update" );
								$criticalSection->up();
							}
							
							${ $self->{ _policies_updating } } = 1;
							${ $self->{ _mysql_connection } }->DownloadPolicies();
							$self->{ _policy_cache } = undef;
							${ $self->{ _policies_updating } } = 0;
						}
						elsif( $jsonresp->{ request } eq "pushpolicies" )
						{
							if( defined( ${ $self->{ _log } } ) )
							{
								$criticalSection->down();
								${ $self->{ _log } }->log( 'info' , "Push policies to all known clients" );
								$criticalSection->up();
							}
							
							my @clients = ${ $self->{ _mysql_connection } }->GetClients();
							
							for( my $i = 0; $i < @clients; $i++ )
							{
								my @cols = @{ $clients[ $i ] };
								my $peer = $cols[ 1 ];
																
								my $policyFile = $self->policycacher( $cols[ 0 ] );
								$response = "{ \"policy\" : \"" . $policyFile . "\" }";
								my $request = "{ \"peerPort\" : \"" . $cols[ 2 ] . "\" }";
								
								my %response :shared = 
						        (
						        	peer => $cols[ 1 ],
									request => $request,
						        	response => $response
						        );

						        ${ $self->{ _outgoing_queue } }->enqueue( \%response );
							}
						}
						elsif( $jsonresp->{ request } eq "pushmodules" )
						{
							if( defined( ${ $self->{ _log } } ) )
							{
								$criticalSection->down();
								${ $self->{ _log } }->log( 'info' , "Push modules to all known clients" );
								$criticalSection->up();
							}
							
							my @clients = ${ $self->{ _mysql_connection } }->GetClients();
							
							my $modulesObj = new ModuleHandler();
							my $moduleFiles = $modulesObj->getModules();
							$moduleFiles =~ s/\r|\r\n|\n/\\n/g;
							$moduleFiles =~ s/\t/ /g;
							
							for( my $i = 0; $i < @clients; $i++ )
							{
								my @cols = @{ $clients[ $i ] };
								my $peer = $cols[ 1 ];
								
								$response = "{ \"modules\" : " . $moduleFiles . " }";
								my $request = "{ \"peerPort\" : \"" . $cols[ 2 ] . "\" }";
								
								my %response :shared = 
						        (
						        	peer => $cols[ 1 ],
									request => $request,
						        	response => $response
						        );

						        ${ $self->{ _outgoing_queue } }->enqueue( \%response );								
							}
						}
						elsif( $jsonresp->{ request } eq "fetchmodules" )
						{
							if( defined( ${ $self->{ _log } } ) )
							{
								$criticalSection->down();
								${ $self->{ _log } }->log( 'info' , "Admin requested modules" );
								$criticalSection->up();
							}
							
							my $modulesObj = new ModuleHandler();						
							
							my %response :shared = 
						    (
								peer => $data->{ peer },
								request => $data->{ request },
						        response => $modulesObj->getModules()
						    );

						    ${ $self->{ _outgoing_queue } }->enqueue( \%response );
						}
						elsif( $jsonresp->{ request } eq "savemodule" )
						{
							if( defined( ${ $self->{ _log } } ) )
							{
								$criticalSection->down();
								${ $self->{ _log } }->log( 'info' , "Admin created / modified module" );
								$criticalSection->up();
							}
							
							my $modulesObj = new ModuleHandler();	
							$modulesObj->saveModule( $jsonresp->{ moduleName } , decode_base64( $jsonresp->{ moduleContents } ) );							
						}
						
						elsif( $jsonresp->{ request } eq "deletemodule" )
						{
							if( defined( ${ $self->{ _log } } ) )
							{
								$criticalSection->down();
								${ $self->{ _log } }->log( 'info' , "Admin deleted module" );
								$criticalSection->up();
							}
							
							my $modulesObj = new ModuleHandler();	
							$modulesObj->deleteModule( $jsonresp->{ moduleName } );							
						}
					}
				}
			}
        }
        usleep 500000;
    }
}


sub handleRequest 
{
	$SIG{'KILL'} = sub { threads->exit(); };
	
    my( $self , $socket ) = @_;
	my $responceData = "";
	my $responceReceived = 0;
	my $peer;
	
	while( <$socket> ) 
	{
		$peer = $socket->peerhost();
		$responceData .= $_;
		$responceReceived = 1;
	}
	close $socket;

	if( $responceReceived == 1 )
	{
		my $data = decode_base64( $responceData );
		my $json = new JSON;
		my $jsonmsg;
			
		my $start = index( $data , "{" );
		my $end = rindex( $data , "}" );
		my $trimmedData = substr( $data , $start , $end + 1 );	

		$jsonmsg = $json->allow_nonref->decode( $trimmedData );
			
	    my %request :shared = 
	    (
	        peer => $peer,
	        request => $trimmedData
	    );

	    ${ $self->{ _incoming_queue } }->enqueue( \%request );
    }

	try
	{
    	# cleanup any dead threads
    	for( my $i = 0; $i < @{ $self->{ _request_handlers } }; $i++ )
    	{
    		if( ${ $self->{ _request_handlers } }[ $i ]->is_running() == 0 )
    		{
    			splice( @{ $self->{ _request_handlers } } , $i , 1 );
    		}
    	}
	}
	catch
	{
		if( defined( ${ $self->{ _log } } ) )
		{
			$criticalSection->down();
			${ $self->{ _log } }->log( 'err' , "Thread cleanup : " . $_ );
			$criticalSection->up();
		}
	}
}


1;
