#!/usr/bin/perl -w

########################################################################################################
# @description Mysql DBI Control Class ( Module )
#
# @author David O Neill
# @studentid 0813001
# @email dave@feeditout.com
# @supervisor J.J Collins
# @project Envisioning group policy for a linux heterogeneous environment
########################################################################################################

package LGPMysqlDBI;

use threads;
use threads::shared;
use strict;
use IO::Socket;
use Net::hostent;
use utf8;
use English;
use DBI;
use MIME::Base64;


sub new 
{
	my( $class) = @_;

	my $self =
	{
	    _connection => undef,
	    _connectionString => undef,
		_host => undef,
		_database => undef,
		_username => undef,
		_password => undef,
		_port => undef,
		_log => undef,
		_connected => 0
	};
	
	bless( $self , $class );
	
	$self->{ _log } = LGPSyslog::getInstance();
	
	return $self;
}


sub Connect
{
    my( $self , $host , $database , $username , $password ) = @_;
        
    if( !defined( $host ) )
	{
        if( defined( $self->{ _log } ) )
        {
            $self->{ _log }->log( 'err' , "No host was defined for the mysql connection, please check configuration" );	
        }

        $self->{ _connected } = 0;

        return 0;
    }    
    
    if( !defined( $username ) )
    {
        print "LGPMysql : No username defined for connection\n";
        
        if( defined( $self->{ _log } ) )
        {
            $self->{ _log }->log( 'err' , "No username was defined for the mysql connection, please check configuration" );	
        }

		$self->{ _connected } = 0;
        
        return 0;
    }
    
    if( !defined( $password ) )
    {
        print "LGPMysql : No password defined for connection\n";
        
        if( defined( $self->{ _log } ) )
        {
            $self->{ _log }->log( 'err' , "No password was defined for the mysql connection, please check configuration" );	
        }

		$self->{ _connected } = 0;
        
        return 0;
    }
    
    if( !defined( $database ) )
    {
        print "LGPMysql : No database defined for connection\n";
    
        if( defined( $self->{ _log } ) )
        {
            $self->{ _log }->log( 'err' , "No database was defined for the connection string, please check configuration" );	
        }

		$self->{ _connected } = 0;
    
        return 0;
    }
    
    $self->{ host } = $host;    
    $self->{ username } = $username;
    $self->{ password } = $password;
    $self->{ database } = $database;
    
    $self->{ connectionString } = 'DBI:mysql:DBNAME;host=DBHOST;mysql_auto_reconnect=1';
    $self->{ connectionString } =~ s/DBNAME/$self->{ database }/m;
    $self->{ connectionString } =~ s/DBHOST/$self->{ host }/m;
    
    $self->{ connection } = DBI->connect( $self->{ connectionString } , $self->{ username }, $self->{ password } );
    
    if( defined( $self->{ connection } ) )
    {
        if( defined( $self->{ _log } ) )
        {
            $self->{ _log }->log( 'info' , "LPGServer Mysql connection made successfully" );	
        }

		$self->{ _connected } = 1;

        return 1;
    }
    else
    {
        if( defined( $self->{ _log } ) )
        {
            $self->{ _log }->log( 'err' , "LPGServer Mysql connection failed" );	
        }

		$self->{ _connected } = 0;

        return 0;
    }    
}


sub Disconnect
{
    my( $self ) = @_;
    
    $self->{ connection }->disconnect();;
    
    return 1;
}


sub Query
{
    my( $self , $sql , $type ) = @_;
    
    if( !defined( $self->{ connection } ) )
    {
        if( defined( $self->{ _log } ) )
        {
            $self->{ _log }->log( 'err' , "Query failed, no mysql connection!" );	
        }
    
        return 0;
    }
    
    if( !defined( $sql ) )
    {
		$self->{ _log }->log( 'err' , "Query failed, sql statement blank" );
        
        return 0;
    }
    
    if( !defined( $type ) )
    {
        $self->{ _log }->log( 'err' , "Query failed, sql statement type unknown" );

        return 0;
    }         
    
    my @result;
    my $qry = $self->{ connection }->prepare( $sql );
    $qry->execute();
    
    if( $type eq 'insert' )
    {
        return;
    }    
    
    if( $type eq 'one' )
    {
        @result = $qry->fetchrow_array();
        return $result[ 0 ];
    }
    
    if( $type eq 'row' )
    {
        return $qry->fetchrow_array();
    }
    
    if( $type eq 'array' )
    {
        my @result = ();
		my $i = 0; 
        
        while( my @record = $qry->fetchrow_array() ) 
        {
            push( @{ $result[ $i ] } , @record );
			$i++;
        }
        
        return @result;
    }
        
    return;       
}


sub ListTables
{
    my( $self ) = @_;
    
    if( !defined( $self->{ connection } ) )
    {
        if( defined( $self->{ _log } ) )
        {
            $self->{ _log }->log( 'err' , "Query failed, no mysql connection!" );	
        }
    
        return 0;
    }    
        
    my $query = $self->{ connection }->prepare( 'show tables' );
    
    my @resultSet;
    
    while( my @row = $query->fetchrow_array() )
    {
        push( @resultSet , $row[ 0 ] );
    }
            
    return @resultSet;       
}


sub IsConnected
{
	my( $self ) = @_;
	
	return $self->{ _connected };
}


sub DownloadPolicies
{
	my( $self ) = @_;
	
	$self->{ _log }->log( 'info' , "LGPServer : Mysql downloading policies" );	
	
	my $sql = "select * from policy where p_enabled='1'";
	my @policies = $self->Query( $sql , "array" );
	
	`rm -rvf ./policies/*`;
	
	for( my $i = 0; $i < @policies; $i++ )
	{
		my @cols = @{ $policies[ $i ] };
		my $policy = "./policies/" . $cols[ 5 ] . ".dsl";
		
		if( open( my $fp , ">" , $policy ) )
		{
			print $fp decode_base64( $cols[ 4 ] );
			close( $fp );	
		}
	}
}


sub UpdateClient
{
	my( $self , $hostname , $os , $guid , $kernel , $architecture , $distribution , $distroVersion , $version , $clientVersion , $psuedoname , $peer , $peerport ) = @_;
	
	my $sql = "select count(*) from clients where c_guid='" . $guid . "'";
	my $exists = $self->Query( $sql , "one" );
	
	if( $exists == 1 )
	{
		$sql = "update clients set c_lastseen='" . time . "', c_clientversion='" . $clientVersion . "', c_port='" . $peerport . "' where c_guid='" . $guid . "'";
		$self->Query( $sql , "insert" );
	}
	else
	{
		$sql = "insert into clients values( null , '1' , '$guid' , '$hostname' , '$os' , '$architecture' , '$kernel' , 
											'$psuedoname' , '$distribution' , '$distroVersion' , '" . time . "' , '" . time . "' , '$clientVersion' , '$peer' , '$peerport' )";
		$self->Query( $sql , "insert" );
	}
}


sub GetClients
{
	my( $self ) = @_;
	
	my $sql = "select c_ou_id, c_ipaddress, c_port from clients where c_lastseen > '0'";
	
	return $self->Query( $sql , "array" );
}


sub GetOu
{
	my( $self , $guid ) = @_;

	my $sql = "select c_ou_id from clients where c_guid='" . $guid . "'";
	my $ou = $self->Query( $sql , "one" );
	
	if( $ou > 0 )
	{
		return $ou;
	}
	else
	{
		return -1;
	}
}

1;
