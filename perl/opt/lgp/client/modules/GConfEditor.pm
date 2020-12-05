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

package GConfEditor;

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
	
	$self->addGrammer( "GCONF_DELETE" , "test" );
	$self->addGrammer( "GCONF_ADD" , "" );
	$self->addGrammer( "GCONF_UPDATE" , "" );
	$self->addGrammer( "GCONF_REFRESH" , "" );
	$self->addGrammer( "GCONF_REFRESH2" , "" );
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