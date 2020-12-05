#!/usr/bin/perl -w

########################################################################################################
# @description Mysql Control Class ( Module )
#
# @author David O Neill
# @studentid 0813001
# @email dave@feeditout.com
# @supervisor J.J Collins
# @project Envisioning group policy for a linux heterogeneous environment
########################################################################################################

package LGPMysql;

use threads;
use threads::shared;
use strict;
use IO::Socket;
use Net::hostent;
use utf8;
use English;
use DBI;


sub new 
{
	my( $class , $log ) = @_;
	
	if( !defined( $log ) )
    {
        print "LGPMysql : No logging mechanism specified\n";
    }

	my $self =
	{
	    _connection => undef,
		_host => undef,
		_database => undef,
		_username => undef,
		_password => undef,
		_port => undef,
		_log => $log
	};
	
	bless( $self , $class );
	
	return $self;
}


sub connect
{
    my( $self , $host , $username , $password ) = @_;
        
    if( !defined( $host ) )
    {
        print "LGPMysql : No host defined for connection\n";
        
        if( defined( $self->{ _log } ) )
        {
            $self->{ _log }->log( 'error' , "No host was defined for the mysql connection, please check configuration" );	
        }
        
        return 0;
    }    
    
    if( !defined( $username ) )
    {
        print "LGPMysql : No username defined for connection\n";
        
        if( defined( $self->{ _log } ) )
        {
            $self->{ _log }->log( 'error' , "No username was defined for the mysql connection, please check configuration" );	
        }
        
        return 0;
    }
    
    if( !defined( $password ) )
    {
        print "LGPMysql : No password defined for connection\n";
        
        if( defined( $self->{ _log } ) )
        {
            $self->{ _log }->log( 'error' , "No password was defined for the mysql connection, please check configuration" );	
        }
        
        return 0;
    }
    
    $self->{ host } = $host;    
    $self->{ username } = $username;
    $self->{ password } = $password;
    
    $self->{ connection } = Mysql->connect( $self->{ host } , $self->{ username }, $self->{ password } );
    
    if( defined( $self->{ connection } ) )
    {
        if( defined( $self->{ _log } ) )
        {
            $self->{ _log }->log( 'info' , "LPGServer Mysql connection made successfully" );	
        }
        return 1;
    }
    
    if( defined( $self->{ _log } ) )
    {
        $self->{ _log }->log( 'error' , "No password was defined for the mysql connection, please check configuration" );	
    }
    
    return 0;
}


sub disconnect
{
    my( $self ) = @_;
    
    undef( $self->{ connection } );
    
    return 1;
}


sub selectdb
{
    my( $self , $database ) = @_;
    
    if( !defined( $database ) )
    {
        print "LGPMysql : No database defined for connection\n";
    
        if( defined( $self->{ _log } ) )
        {
            $self->{ _log }->log( 'error' , "No database was defined for the mysql select db, please check configuration" );	
        }
    
        return 0;
    }
    
    $self->{ database } = $database;

    if( $self->{ connection }->SelectDB( $self->{ database } ) )
    {
        return 1;
    }
    else
    {
        print "LGPMysql : Unable to select database $database, check configuration\n";
        return 0;
    }
}


sub query
{
    my( $self , $sql , $type ) = @_;
    
    if( !defined( $self->{ connection } ) )
    {
        print "LGPMysql : Query failed, no mysql connection!\n";
    
        if( defined( $self->{ _log } ) )
        {
            $self->{ _log }->log( 'error' , "Query failed, no mysql connection!" );	
        }
    
        return 0;
    }
    
    if( !defined( $sql ) )
    {
        print "LGPMysql : Query failed, no sql statement! eh?\n";
        
        return 0;
    }
    
    if( !defined( $type ) )
    {
        print "LGPMysql : No type selected!\n";
        print "LGPMysql : insert = insert return nothing\n";
        print "LGPMysql : one = return single value as string scalar\n";
        print "LGPMysql : row = return entire row as array\n";
        print "LGPMysql : array = return rows as array of arrays\n";
        return 0;
    }         
    
    my $resultSet = $self->{ connection }->Query( $sql );
    
    if( $type eq 'insert' )
    {
        return;
    }    
    
    if( $type eq 'one' )
    {
        my @arr = $resultSet->FetchRow();
        return $arr[ 0 ];
    }
    
    if( $type eq 'row' )
    {
        return $resultSet->FetchRow();
    }
    
    if( $type eq 'array' )
    {
        my @result = ();
        
        while( my @record = $resultSet->FetchRow() ) 
        {
            push( @result , @record );
        }
        
        return @result;
    }
        
    return;       
}


sub listDbs
{
    my( $self ) = @_;
    
    if( !defined( $self->{ connection } ) )
    {
        print "LGPMysql : List Dbs failed, no mysql connection!\n";
    
        if( defined( $self->{ _log } ) )
        {
            $self->{ _log }->log( 'error' , "Query failed, no mysql connection!" );	
        }
    
        return 0;
    }    
        
    my $resultSet = $self->{ connection }->ListDBs();
            
    return $resultSet;       
}


sub listTables
{
    my( $self ) = @_;
    
    if( !defined( $self->{ connection } ) )
    {
        print "LGPMysql : List tables failed, no mysql connection!\n";
    
        if( defined( $self->{ _log } ) )
        {
            $self->{ _log }->log( 'error' , "Query failed, no mysql connection!" );	
        }
    
        return 0;
    }    
        
    my $resultSet = $self->{ connection }->ListTables();
            
    return $resultSet;       
}


sub clean
{
    my( $self , $value ) = @_;
    
    if( !defined( $self->{ connection } ) )
    {
        print "LGPMysql : Clean failed, no mysql connection!\n";
    
        if( defined( $self->{ _log } ) )
        {
            $self->{ _log }->log( 'error' , "Query failed, no mysql connection!" );	
        }
    
        return 0;
    }        
    
    # sanitisation to be done!   
    # add some more cleaning!
    
    return $self->{ connection }->quote( $value );
}


1;
