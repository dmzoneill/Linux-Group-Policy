#!/usr/bin/perl -w

########################################################################################################
# @description Syslog Control Class ( Module )
#
# @author David O Neill
# @studentid 0813001
# @email dave@feeditout.com
# @supervisor J.J Collins
# @project Envisioning group policy for a linux heterogeneous environment
########################################################################################################

package LGPSyslog;

use strict;
use English;
use Sys::Syslog qw( :DEFAULT setlogsock);

# LOG_EMERG - system is unusable
# LOG_ALERT - action must be taken immediately
# LOG_CRIT - critical conditions
# LOG_ERR - error conditions
# LOG_WARNING - warning conditions
# LOG_NOTICE - normal, but significant, condition
# LOG_INFO - informational message
# LOG_DEBUG - debug-level message

########################################################################################################
# Static Variables
########################################################################################################

my $singletonInstance = undef;


sub new 
{
	if( !defined( $singletonInstance ) )
	{
		my( $class ) = @_;

		my $self =
		{
			_ident => $0,
			_logopt => 'ndelay,nofatal,nowait,perror,pid',
			_facility => 'local6'
		};
	
		bless( $self , $class );
	
		$singletonInstance = $self;
	}
	
	return $singletonInstance;
}


########################################################################################################
# @description getInstance Singleton
#
# @return the same instance
########################################################################################################

sub getInstance 
{
	if( !defined( $singletonInstance ) )
	{
		$singletonInstance = new LGPSyslog();
	}
	
	return $singletonInstance;
}


sub log
{
    my( $self , $level , $message ) = @_;
    openlog( $self->{ _ident } , $self->{ _logopt } ,  $self->{ _facility } );
	syslog( $level , $message );
	closelog();
}

    
1;
