#!/usr/bin/perl -w

########################################################################################################
# @description Client Boundary Class ( Module ) - Net IO
#
# @author David O Neill
# @studentid 0813001
# @email dave@feeditout.com
# @supervisor J.J Collins
# @project Envisioning group policy for a linux heterogeneous environment
########################################################################################################

package LGPClient;

use threads;
use threads::shared;
use Thread::Queue;
use Thread::Semaphore;
use strict;
use warnings;
use IO::Socket;
use Net::hostent;
use utf8;
use English;
use POSIX;
use Digest::MD5 qw( md5 );
use MIME::Base64;
use JSON;
use Try::Tiny;
use Computer;
use LGPSyslog;
use Time::HiRes qw( usleep );

my $criticalSection = Thread::Semaphore->new();

sub new 
{
	my( $class ) = @_;

	my $self = {};
	my $_continue : shared = 1;
	my $_sender_Socket : shared;
	my $_receiver_Socket : shared;
	my @_request_handlers : shared = ();
	my @_sequenced_requests : shared = ();
	my $_in_queue : shared = Thread::Queue->new;
	my $_updating : shared = 0;

	$self =
	{
		_protocol => undef,								# not shared implicitly
		_port => undef, 								# not shared implicitly
		_host => undef, 								# not shared implicitly
		_clientport => undef, 							# not shared implicitly
		_update_working => \$_updating,					# sender is processing update
		_sender_Socket => \$_sender_Socket,				# thread will assign handle
		_receiver_Socket => \$_receiver_Socket,			# thread will assign handle
		_sender_Thread => undef, 						# owned by parent
		_queue_Thread => undef, 						# owned by parent
		_receiver_Thread => undef, 						# owned by parent
		_incoming_queue => \$_in_queue,					# thread will assign messages
		_request_handlers => \@_request_handlers,		# thread will push handles
		_log => undef,									# thread will push messages
		_pipe => undef,									# not shared implicitly
		_continue => \$_continue,						# used to kill loops globally
		_clientversion => "1.0",						# client version
		_cached_policy => undef,						# cached policy, reduce disk checking
		_cached_policy_hash => undef					# hash of policy for comparison on update
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
	$self->{ _clientport } = $port + 1;
	$self->{ _log } = \LGPSyslog::getInstance();
	
	if( defined( ${ $self->{ _log } } ) )
	{
		$criticalSection->down();
		${ $self->{ _log } }->log( 'info' , "Starting ..." );
		$criticalSection->up();
	}

	$self->{ _queue_Thread } = threads->create( "queuehandler" , $self );
	$self->{ _receiver_Thread } = threads->create( "receiver" , $self );
	$self->{ _sender_Thread } = threads->create( "sender" , $self );

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
            elsif( $input eq 'update' )
            {
            	if( ${ $self->{ _update_working } } == 0 )
            	{
            		$self->{ _sender_Thread }->kill( 'KILL' )->detach();
            		sleep 2;
            		$self->{ _sender_Thread } = threads->create( "sender" , $self );
            	}
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
		${ $self->{ _log } }->log( 'info' ,  "Shutting down ..." );
		$criticalSection->up();
	}
	
	${ $self->{ _continue } } = -1;
	
	if( defined( ${ $self->{ _sender_Socket } } ) )
	{
		local $SIG{ __WARN__ } = sub{};
		close( ${ $self->{ _sender_Socket } } );
	}
	
	if( defined( ${ $self->{ _receiver_Socket } } ) )
	{
		local $SIG{ __WARN__ } = sub{};
		close( ${ $self->{ _receiver_Socket } } );
	}
	
	if( defined( $self->{ _receiver_Thread } ) )
	{
		$self->{ _receiver_Thread }->kill( 'KILL' )->detach();
	}
	
	if( defined( $self->{ _sender_Thread } ) )
	{
		$self->{ _sender_Thread }->kill( 'KILL' )->detach();
	}
	
	if( defined( $self->{ _queue_Thread } ) )
	{
		$self->{ _queue_Thread }->kill( 'KILL' )->detach();
	}
	
	# cleanup any dead threads
    for( my $i = 0; $i < @{ $self->{ _request_handlers} }; $i++ )
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
	my $updatecount = 0;
	
	while( ${ $self->{ _continue } } != -1 ) 
	{
	    my $socket = IO::Socket::INET->new
	    ( 
	    	PeerAddr => $self->{ _host }, 
	    	PeerPort => $self->{ _port }, 
	    	Proto => $self->{ _protocol }, 
	    	Type => SOCK_STREAM
	    );
	    
	    if( !defined( $socket ) )
	    {
			if( defined( ${ $self->{ _log } } ) )
			{
				$criticalSection->down();
				${ $self->{ _log } }->log( 'err' ,  "Couldn't connect to $self->{ _host }:$self->{ _port } : $@" );
				$criticalSection->up();
			}
			
	    	${ $self->{ _continue } } = -1;
	    }
	    else
	    {
	    	${ $self->{ _sender_Socket } } = fileno $socket; 
	
			if( defined( ${ $self->{ _log } } ) )
			{
				$criticalSection->down();
				${ $self->{ _log } }->log( 'info' , "Client created " . $self->{ _protocol } . " connection on " . $self->{ _host } . ":" . $self->{ _port } );
				$criticalSection->up();
			}

			${ $self->{ _update_working } } = 1;
			
			my $computer = Computer::getInstance( $self->{ _log } );
			
			my $request;
			
			# update modules every 6 hours
			if( $updatecount > 0 && $updatecount % 12 == 0 )
			{
				$request = encode_base64( "{ 
					
					\"peerPort\" :   \"" . $self->{ _clientport } . "\" , 
					\"request\"  :   \"updatemodules\" ,
					\"guid\"  :   \"" . $computer->getGuid() . "\" ,
					\"os\"  :   \"" . $computer->getOs() . "\" ,
					\"version\"  :   \"" . $computer->getVersion() . "\" ,
					\"architecture\"  :   \"" . $computer->getArchitecture() . "\" ,
					\"kernel\"  :   \"" . $computer->getKernel() . "\" ,
					\"psuedoName\"  :   \"" . $computer->getPsuedoName() . "\" ,
					\"distribution\"  :   \"" . $computer->getDistribution() . "\" ,
					\"distroVersion\"  :   \"" . $computer->getDistroVersion() . "\" ,
					\"hostname\"  :   \"" . $computer->getHostname() . "\" ,
					\"clientVersion\" :    \"" . $self->{ _clientversion } . "\" 
				
				}" );
				
				$criticalSection->down();
				${ $self->{ _log } }->log( 'info' , "Sending request for modules update" );
				$criticalSection->up();
			}
			else
			{
			
				$request = encode_base64( "{ 
					
					\"peerPort\" :   \"" . $self->{ _clientport } . "\" , 
					\"request\"  :   \"update\" ,
					\"guid\"  :   \"" . $computer->getGuid() . "\" ,
					\"os\"  :   \"" . $computer->getOs() . "\" ,
					\"version\"  :   \"" . $computer->getVersion() . "\" ,
					\"architecture\"  :   \"" . $computer->getArchitecture() . "\" ,
					\"kernel\"  :   \"" . $computer->getKernel() . "\" ,
					\"psuedoName\"  :   \"" . $computer->getPsuedoName() . "\" ,
					\"distribution\"  :   \"" . $computer->getDistribution() . "\" ,
					\"distroVersion\"  :   \"" . $computer->getDistroVersion() . "\" ,
					\"hostname\"  :   \"" . $computer->getHostname() . "\" ,
					\"clientVersion\" :    \"" . $self->{ _clientversion } . "\" 
				
				}" );
				
				$criticalSection->down();
				${ $self->{ _log } }->log( 'info' , "Sending request for policy update" );
				$criticalSection->up();
			}			
			
			$socket->send( $request );
			
			${ $self->{ _update_working } } = 0;
			
			close( $socket );
			
			$updatecount++;
	    } 
  
	    sleep 1800;
	}
}


sub queuehandler
{
	$SIG{'KILL'} = sub { threads->exit(); };

	my $self = shift;
    my $queue = ${ $self->{ _incoming_queue } };

	while( ${ $self->{ _continue } } != -1 ) 
    {
        while( $queue->pending ) 
        {
            my $data = $queue->dequeue;

			$data = decode_base64( $data->{ request } );
			my $json = new JSON;
			my $jsonmsg;

			try
			{
				my $start = index( $data , '{' );
				my $end = rindex( $data , '}' );
				my $trimmedData = substr( $data , $start , $end + 1 );	
				
				$jsonmsg = $json->allow_nonref->decode( $trimmedData );

				if( defined( $jsonmsg->{ policy } ) )
				{
					$criticalSection->down();
					${ $self->{ _log } }->log( 'info' , "Policy update received" );
					$criticalSection->up();

					my $policy = "#!/usr/bin/perl -w\nuse lib \"" . $ENV{'lgppath'} . "/client/includes/\";\nuse Interpreter;\n";
					$policy .= $jsonmsg->{ policy };

					my $policyMd5 = md5( $policy );
					
					my $policyFile = "./policies/policy.dsl";

					if( -e $policyFile )
					{
						if( !defined( ${ $self->{ _cached_policy } } ) )
						{
							${ $self->{ _cached_policy } } = `cat $policyFile`;
							${ $self->{ _cached_policy_hash } } = md5( ${ $self->{ _cached_policy } } );
						}

						if( ${ $self->{ _cached_policy_hash } } eq $policyMd5 )
						{
							$criticalSection->down();
							${ $self->{ _log } }->log( 'info' , "No change in policy" );
							$criticalSection->up();
						}
						else
						{
							if( open( my $fp , ">" , $policyFile ) )
							{
								${ $self->{ _cached_policy } } = $policy;
								${ $self->{ _cached_policy_hash } } = $policyMd5;
								
								$criticalSection->down();
								${ $self->{ _log } }->log( 'info' , "New policy saved into polcies folder. executing policy" );
								$criticalSection->up();
								print $fp $policy;
								close( $fp );	
								`perl $policyFile`;
							}
							else
							{
								$criticalSection->down();
								${ $self->{ _log } }->log( 'err' , "Unable to save policy, updates will not be applied" );
								$criticalSection->up();
							}
						}
					}
					else
					{
						if( open( my $fp , ">" , $policyFile ) )
						{
							${ $self->{ _cached_policy } } = $policy;
							${ $self->{ _cached_policy_hash } } = $policyMd5;
							
							$criticalSection->down();
							${ $self->{ _log } }->log( 'info' , "Policy saved into polcies folder. executing policy" );
							$criticalSection->up();
							
							print $fp $policy;
							close( $fp );	
							`perl $policyFile`;
						}
						else
						{
							$criticalSection->down();
							${ $self->{ _log } }->log( 'err' , "Unable to save policy, updates will not be applied" );
							$criticalSection->up();
						}
					}
				}
				elsif( defined( $jsonmsg->{ modules } ) )
				{					
					my $modulespath = $ENV{'lgppath'} . "/client/modules/";
					
					# clean out the old modules
					my $cleanup = "rm -rvf " . $modulespath . "*";
					`$cleanup`;
					
					while( my ( $key , $value ) = each %{ $jsonmsg->{ modules } } ) 
					{
						my $file = $modulespath . $key;
						if( open( FILE , "+>" , $file ) )
						{
							print FILE decode_base64( $value );
							close( FILE )
						}
					}	
					
					$criticalSection->down();
					${ $self->{ _log } }->log( 'info' , "Modules updated received" );		
					$criticalSection->up();					
				}
	        }
			catch
			{
				if( defined( ${ $self->{ _log } } ) )
				{
					$criticalSection->down();
					${ $self->{ _log } }->log( 'err' , "Invalid server message : " . $_ );
					$criticalSection->up();
				}
				return;
			}
		
		}
		
		usleep 500000;
	}
}


sub receiver
{
	$SIG{'KILL'} = sub { threads->exit(); };
	
	my $self = shift;
	
	my $localport = $self->{ _clientport };
	
	while( ${ $self->{ _continue } } != -1 ) 
	{
		my $socket = IO::Socket::INET->new
		( 
			Proto => 'tcp', 
			LocalAddr => '0.0.0.0', 
			LocalPort => $localport, 
			Listen => SOMAXCONN, 
			Reuse => 1
		);
		
		if( !defined( $socket ) ) 
		{
			if( defined( ${ $self->{ _log } } ) )
			{
				$criticalSection->down();
				${ $self->{ _log } }->log( 'err' , "Client Couldn't bind to 0.0.0.0:" . $localport . " : $@" );
				$criticalSection->up();
			}
			
			${ $self->{ _continue } } = -1;
		}
		else
		{
			${ $self->{ _receiver_Socket } } = fileno $socket;
			
			while( my $connection = $socket->accept() ) 
			{  
				if( defined( ${ $self->{ _log } } ) )
				{
					$criticalSection->down();
					${ $self->{ _log } }->log( 'info' , "Received connection from " . $connection->peerhost() );
					$criticalSection->up();
				}
				
				push( @{ $self->{ _request_handlers } } , threads->create( "handleRequest" , $self , $connection )->detach() );
			}
		}
		
		usleep 500000;
	}
}


sub handleRequest 
{
	$SIG{'KILL'} = sub { threads->exit(); };
	
    my( $self , $socket ) = @_; 

	my $requestData;
	my $requestReceived = 0;
    
    while( <$socket> ) 
    {
    	$requestData .= $_;
		$requestReceived = 1;
	}
	
	if( $requestReceived == 1 )
	{
		my %request :shared = 
        (
        	request => $requestData
        );

        ${ $self->{ _incoming_queue } }->enqueue( \%request );
	}
	
    close( $socket );
    
    # cleanup any dead threads
    for( my $i = 0; $i < @{ $self->{ _request_handlers } }; $i++ )
    {
    	if( ${ $self->{ _request_handlers } }[ $i ]->is_running() == 0 )
    	{
    		splice( @{ $self->{ _request_handlers } } , $i , 1 );
    	}
    }
    
}


1;
