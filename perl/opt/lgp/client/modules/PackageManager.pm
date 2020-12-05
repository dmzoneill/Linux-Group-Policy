#!/usr/bin/perl -w

########################################################################################################
# @description Concrete Module Class ( Module ) extends Module
#
# @author David O Neill
# @studentid 0813001
# @email dave@feeditout.com
# @supervisor J.J Collins
# @project Envisioning group policy for a linux heterogeneous environment
########################################################################################################

package PackageManager;

BEGIN 
{
    unshift( @INC, $ENV{'lgppath'} . "/client/config" );
	unshift( @INC, $ENV{'lgppath'} . "/client/distribution/" );
    unshift( @INC, $ENV{'lgppath'} . "/client/includes/" );
    unshift( @INC, $ENV{'lgppath'} . "/client/modules/" );
	unshift( @INC, $ENV{'lgppath'} . "/client/policies/" );
    unshift( @INC, $ENV{'lgppath'} . "/common/" );
}

use strict;
use base 'Module';

sub new 
{
	my( $class ) = @_;
	my $self = Module->new( @_ );
	bless( $self , $class );
	
	return $self;
}


sub registerGrammer
{
	my( $self ) = @_;
	
	$self->addGrammer( "PKG_UPDATE" , "" );
	$self->addGrammer( "PKG_INSTALL" , "" );
	$self->addGrammer( "PKG_GROUP" , "" );
}


sub serviceControl
{
	my( $self ) = @_;
	
	#print $self->{ actionEvent }->getEventOptions() . "\n";
}


sub update
{
    my( $self , $actionEvent , $coordinator ) = @_; 
    
    $self->{ coordinator } = $coordinator;
    $self->{ actionEvent } = $actionEvent;
    
    #print "   ServiceController : Received update event\n";
	
	if( $self->{ actionEvent }->getEventCode() eq "SERVICE_CONTROL" )
	{	
		$self->serviceControl();	
	}
}


1;