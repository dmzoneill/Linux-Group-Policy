#!/usr/bin/perl -w

########################################################################################################
# @description Observer Abstract Class ( Module / Interface )
#
# @author David O Neill
# @studentid 0813001
# @email dave@feeditout.com
# @supervisor J.J Collins
# @project Envisioning group policy for a linux heterogeneous environment
########################################################################################################

package Observer;

BEGIN 
{
    unshift( @INC, $ENV{'lgppath'} . "/client/config" );
	unshift( @INC, $ENV{'lgppath'} . "/client/distribution/" );
    unshift( @INC, $ENV{'lgppath'} . "/client/includes/" );
    unshift( @INC, $ENV{'lgppath'} . "/client/modules/" );
	unshift( @INC, $ENV{'lgppath'} . "/client/policies/" );
    unshift( @INC, $ENV{'lgppath'} . "/common/" );
}

use LGPSyslog;

sub new 
{
	my( $class ) = @_;
	
	my $self =
	{
		_observers => undef,
		_log => undef
	};
	
	$self->{ _log } = LGPSyslog::getInstance();
	
	bless( $self , $class );
	
	return $self;
}


sub update
{
    my( $self  ) = @_; 

}


sub attach
{
    my( $self , $observer ) = @_; 
    
    if( !defined( $self->{ _observers } ) )
    {
    	$self->{ _observers } = ();		
    }
    
   	push( @{ $self->{ _observers } } , $observer );

}


sub detach
{
    my( $self , $observer ) = @_; 
    
    if( !defined( $self->{ _observers } ) )
    {
    	return;	
    }
	
	my $counter = 0;
	
	while( $counter <= $#{ $self->{ _observers } } ) 
	{
		if( $self->{ _observers }[ $counter ]  eq $observer ) 
		{
			splice( @{ $self->{ _observers } } , $counter , 1 );
		}
		else 
		{
			$counter++;
		}
	}
}


sub notify
{
    my( $self , $message ) = @_; 
    
    foreach my $obj ( @{ $self->{ _observers } } )
    {	
    	$$obj->update( $message , $self );
    }

}


1;
