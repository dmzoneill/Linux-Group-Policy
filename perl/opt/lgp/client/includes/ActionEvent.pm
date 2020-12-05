#!/usr/bin/perl -w

########################################################################################################
# @description Entity  - Observer Payload Update Message
#
# @author David O Neill
# @studentid 0813001
# @email dave@feeditout.com
# @supervisor J.J Collins
# @project Envisioning group policy for a linux heterogeneous environment
########################################################################################################

package ActionEvent;

sub new 
{
	my( $class , $eventCode , $eventOptions ) = @_;

	my $self =
	{
		_eventCode => $eventCode,
		_eventOptions => $eventOptions
	};
	
	bless( $self , $class );
	
	return $self;
}


sub getEventCode
{
    my( $self ) = @_;
    
    return $self->{ _eventCode };
}

sub getEventOptions
{
    my( $self ) = @_;
    
    return $self->{ _eventOptions };
}

1;
