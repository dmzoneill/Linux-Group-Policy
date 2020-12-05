#!/usr/bin/perl -w

########################################################################################################
# @description Entity Class for Additional Grammer 
#
# @author David O Neill
# @studentid 0813001
# @email dave@feeditout.com
# @supervisor J.J Collins
# @project Envisioning group policy for a linux heterogeneous environment
########################################################################################################

package Grammer;


sub new 
{
	my( $class , $keyCode , $eventParser ) = @_;

	my $self =
	{
		_keyCode => $keyCode,
		_eventParser => $eventParser
	};
	
	bless( $self , $class );
	
	return $self;
}


sub getKeyCode
{
    my( $self ) = @_;
    
    return $self->{ _keyCode };
}

sub getEventParser
{
    my( $self ) = @_;
    
    return $self->{ _eventParser };
}

1;
