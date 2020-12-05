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
	
	$self->addGrammer( "PACKAGE_INSTALL" , 	
	"\\s*												# spaces may or may not exist
		(.*\\s?)+        								# package names
	");
	
	$self->addGrammer( "PACKAGE_REMOVE" , 	
	"\\s*												# spaces may or may not exist
		(.*\\s?)+        								# package names
	");
	
	$self->addGrammer( "PACKAGE_GROUP_INSTALL" , 	
	"\\s*												# spaces may or may not exist
		('.*?'\\s?)+        							# package names
	");
	
	$self->addGrammer( "PACKAGE_GROUP_REMOVE" , 	
	"\\s*												# spaces may or may not exist
		('.*?'\\s?)+        							# package names
	");
	
	$self->addGrammer( "PACKAGE_UPDATE" , 	
	"\\s*												# spaces may or may not exist
		(.*\\s?)+        								# package names
	");
	
	$self->addGrammer( "PACKAGE_UPDATE_ALL" , "(.*?)" );
	
	$self->addGrammer( "PACKAGE_SYNC_REPO" , "(.*?)" );
	
}


sub installPackage
{
	my( $self ) = @_;
	
	my $computer = Computer::getInstance();
	my $distro = $computer->getDistro();
	my $packageManager = $distro->getPackageManager();
	my $exec;
	
	if( $packageManager eq "yum" )
	{
	
	}
	elsif( $packageManager eq "apt-get" )
	{
	
	}
	elsif( $packageManager eq "emerge" )
	{
	
	}
	elsif( $packageManager eq "zypper" )
	{
	
	}
}


sub removePackage
{
	my( $self ) = @_;
	
	my $computer = Computer::getInstance();
	my $distro = $computer->getDistro();
	my $packageManager = $distro->getPackageManager();
	my $exec;
		
	if( $packageManager eq "yum" )
	{
	
	}
	elsif( $packageManager eq "apt-get" )
	{
	
	}
	elsif( $packageManager eq "emerge" )
	{
	
	}
	elsif( $packageManager eq "zypper" )
	{
	
	}
}


sub installGroup
{
	my( $self ) = @_;
	
	my $computer = Computer::getInstance();
	my $distro = $computer->getDistro();
	my $packageManager = $distro->getPackageManager();
	my $exec;
		
	if( $packageManager eq "yum" )
	{
	
	}
	elsif( $packageManager eq "apt-get" )
	{
	
	}
	elsif( $packageManager eq "emerge" )
	{
	
	}
	elsif( $packageManager eq "zypper" )
	{
	
	}
}


sub removeGroup
{
	my( $self ) = @_;
	
	my $computer = Computer::getInstance();
	my $distro = $computer->getDistro();
	my $packageManager = $distro->getPackageManager();
	my $exec;
		
	if( $packageManager eq "yum" )
	{
	
	}
	elsif( $packageManager eq "apt-get" )
	{
	
	}
	elsif( $packageManager eq "emerge" )
	{
	
	}
	elsif( $packageManager eq "zypper" )
	{
	
	}
}


sub updatePackage
{
	my( $self ) = @_;
	
	my $computer = Computer::getInstance();
	my $distro = $computer->getDistro();
	my $packageManager = $distro->getPackageManager();
	my $exec;
		
	if( $packageManager eq "yum" )
	{
	
	}
	elsif( $packageManager eq "apt-get" )
	{
	
	}
	elsif( $packageManager eq "emerge" )
	{
	
	}
	elsif( $packageManager eq "zypper" )
	{
	
	}
}


sub updateAllPackages
{
	my( $self ) = @_;
		
	my $computer = Computer::getInstance();
	my $distro = $computer->getDistro();
	my $packageManager = $distro->getPackageManager();
	my $exec;
		
	if( $packageManager eq "yum" )
	{
		$exec = "yum -y update";
	}
	elsif( $packageManager eq "apt-get" )
	{
		$exec = "apt-get -y update";
	}
	elsif( $packageManager eq "emerge" )
	{
	   $exec = "emerge -uDN world";
	}
	elsif( $packageManager eq "zypper" )
	{
		$exec = "zypper -n update";
	}
	
	$self->{ _log }->log( 'info' , $self->{ actionEvent }->getEventCode() . ' : ' . $exec );
	`$exec  2>&1 >/dev/null`;
}


sub syncRepositories
{
	my( $self ) = @_;
	
	my $computer = Computer::getInstance();
	my $distro = $computer->getDistro();
	my $packageManager = $distro->getPackageManager();
	my $exec;
		
	if( $packageManager eq "yum" )
	{
		$exec = "yum check-update";
	}
	elsif( $packageManager eq "apt-get" )
	{
		$exec = "apt-get -d update";
	}
	elsif( $packageManager eq "emerge" )
	{
	   $exec = "emerge --sync";
	}
	elsif( $packageManager eq "zypper" )
	{
		$exec = "zypper refresh";
	}
	
	$self->{ _log }->log( 'info' , $self->{ actionEvent }->getEventCode() . ' : ' . $exec );
	`$exec  2>&1 >/dev/null`;
}


sub update
{
    my( $self , $actionEvent , $coordinator ) = @_; 
    
    $self->{ coordinator } = $coordinator;
    $self->{ actionEvent } = $actionEvent;
    	
	if( $self->{ actionEvent }->getEventCode() eq "PACKAGE_INSTALL" )
	{	
		$self->installPackage();	
	}
	
	elsif( $self->{ actionEvent }->getEventCode() eq "PACKAGE_REMOVE" )
	{	
		$self->removePackage();	
	}
	
	elsif( $self->{ actionEvent }->getEventCode() eq "PACKAGE_GROUP_INSTALL" )
	{	
		$self->installGroup();	
	}
	
	elsif( $self->{ actionEvent }->getEventCode() eq "PACKAGE_GROUP_REMOVE" )
	{	
		$self->removeGroup();	
	}
	
	elsif( $self->{ actionEvent }->getEventCode() eq "PACKAGE_UPDATE" )
	{	
		$self->updatePackage();	
	}
	
	elsif( $self->{ actionEvent }->getEventCode() eq "PACKAGE_UPDATE_ALL" )
	{	
		$self->updateAllPackages();	
	}
	
	elsif( $self->{ actionEvent }->getEventCode() eq "PACKAGE_SYNC_REPO" )
	{	
		$self->syncRepositories();	
	}
}


1;