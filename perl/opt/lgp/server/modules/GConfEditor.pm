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
	
	my $loggedinusers = `who | awk '{ print \$1 }'`;	
	my @userarr = split( /\n/ , $loggedinusers );
	
	my %seen = ();
	
	# https://bugs.launchpad.net/ubuntu/+source/gconf/+bug/290647
	
	foreach my $user( @userarr ) 
	{
        if( not exists( $seen{ $user } ) ) 
		{		
		
            my $cmd = "cat /etc/passwd | grep $user | awk -F: '{ print \$6 }'";
            my $home = `$cmd`;
            $home =~ s/^\s+//;
	        $home =~ s/\s+$//;
            
            $cmd = "cat /var/lib/dbus/machine-id";
            my $machineid = `$cmd`;
            $machineid =~ s/^\s+//;
	        $machineid =~ s/\s+$//;
            
            $cmd = "grep -v \"^#\" " . $home . "/.dbus/session-bus/" . $machineid . "-0";
            my $dbusBus = `$cmd`;
            $dbusBus =~ s/^\s+//;
	        $dbusBus =~ s/\s+$//;
	        $dbusBus =~ s/\s+/ /g;
            
            $seen{ $user } = $dbusBus;            
            
        }
    }	
	
	$self->{ _users } = \%seen;
	
	return $self;
}


sub registerGrammer
{
	my( $self ) = @_;
	
	$self->addGrammer( "GCONF_SET_INT" , 
	"\\s*												# spaces may or may not exist
		( \\/[a-zA-Z0-9@\\._\\-]+ )+        			# gconf path
		\\s+											# followed by space
		( [0-9]+ ) 										# value	
	");
	
	$self->addGrammer( "GCONF_SET_BOOL" , 
	"\\s*												# spaces may or may not exist
		( \\/[a-zA-Z0-9@\\._\\-]+ )+        			# gconf path
		\\s+											# followed by space
		( 0 | 1 | true | false ) 						# value	
	");
	
	
	$self->addGrammer( "GCONF_SET_FLOAT" , 
	"\\s*												# spaces may or may not exist
		( \\/[a-zA-Z0-9@\\._\\-]+ )+        			# gconf path
		\\s+											# followed by space
		( [0-9]+\\.[0-9]+ ) 							# value	
	");
	
	$self->addGrammer( "GCONF_SET_STRING" , 
	"\\s*												# spaces may or may not exist
		( \\/[a-zA-Z0-9@\\._\\-]+ )+        			# gconf path
		\\s+											# followed by space
		( '.*?' )	                                    # value	
	");
	
	$self->addGrammer( "GCONF_SET_LIST" , 
	"\\s*												# spaces may or may not exist
		( \\/[a-zA-Z0-9@\\._\\-]+ )+        			# gconf path
		\\s+											# followed by space
		( \\[ ( [ a-zA-Z0-9\\.\\-@_\\/\\\\ ]+ ) \\] ) 	# value	
	");
	
}


sub gconfSetInt
{
	my( $self ) = @_;
			
	my $options = $self->{ actionEvent }->getEventOptions();
	$options =~ s/^\s+//;
	$options =~ s/\s+$//;
	
	my @parts = split( /\s+/ , $options );		
	my $exec = "--type int --set " . $parts[ 0 ] . " " . $parts[ 1 ];
	
	$self->ExecAsLoggedinUsers( $exec );
}


sub gconfSetBool
{
	my( $self ) = @_;
			
	my $options = $self->{ actionEvent }->getEventOptions();
	$options =~ s/^\s+//;
	$options =~ s/\s+$//;
	
	my @parts = split( /\s+/ , $options );		
	my $exec = "--type bool --set " . $parts[ 0 ] . " " . $parts[ 1 ];
	
	$self->ExecAsLoggedinUsers( $exec );
}


sub gconfSetFloat
{
	my( $self ) = @_;
			
	my $options = $self->{ actionEvent }->getEventOptions();
	$options =~ s/^\s+//;
	$options =~ s/\s+$//;
	
	my @parts = split( /\s+/ , $options );		
	my $exec = "--type float --set " . $parts[ 0 ] . " " . $parts[ 1 ];
	
	$self->ExecAsLoggedinUsers( $exec );
}


sub gconfSetString
{
	my( $self ) = @_;
			
	my $options = $self->{ actionEvent }->getEventOptions();
	$options =~ s/^\s+//;
	$options =~ s/\s+$//;
	
	my @parts = split( /'/ , $options );	
	my $exec = "--type string --set " . $parts[ 0 ] . " '" . $parts[ 1 ] . "'";
	
	$self->ExecAsLoggedinUsers( $exec );
}


sub gconfSetList
{
	my( $self ) = @_;
			
	my $options = $self->{ actionEvent }->getEventOptions();
	$options =~ s/^\s+//;
	$options =~ s/\s+$//;
	
	my @parts = split( /\s+/ , $options );		
	my $exec = "--type list --set " . $parts[ 0 ] . " " . $parts[ 1 ];
	
	$self->ExecAsLoggedinUsers( $exec );
}


sub ExecAsLoggedinUsers
{
	my( $self , $exec ) = @_;
		
	my $globalexec = "gconftool-2 --direct --config-source xml:readwrite:/etc/gconf/gconf.xml.mandatory $exec";
	$self->{ _log }->log( 'info' , $self->{ actionEvent }->getEventCode() . ' : ' . $globalexec );
	##$globalexec#
	
	# https://bugs.launchpad.net/ubuntu/+source/gconf/+bug/290647
	
	foreach my $user ( keys %{ $self->{ _users } } )
    {
        my $dbus = $self->{ _users }->{ $user }; 
        my $userexec = "sudo -u $user $dbus gconftool-2 $exec"; 
		$self->{ _log }->log( 'info' , $self->{ actionEvent }->getEventCode() . ' : ' . $userexec );
		`$userexec 2>&1 >/dev/null`;
    }
		
}


sub update
{
    my( $self , $actionEvent , $coordinator ) = @_; 
    
    $self->{ coordinator } = $coordinator;
    $self->{ actionEvent } = $actionEvent;
    	
	if( $self->{ actionEvent }->getEventCode() eq "GCONF_SET_INT" )
	{	
		$self->gconfSetInt();	
	}
	
	elsif( $self->{ actionEvent }->getEventCode() eq "GCONF_SET_BOOL" )
	{	
		$self->gconfSetBool();	
	}
	
	elsif( $self->{ actionEvent }->getEventCode() eq "GCONF_SET_FLOAT" )
	{	
		$self->gconfSetFloat();	
	}
	
	elsif( $self->{ actionEvent }->getEventCode() eq "GCONF_SET_STRING" )
	{	
		$self->gconfSetString();	
	}
	
	elsif( $self->{ actionEvent }->getEventCode() eq "GCONF_SET_LIST" )
	{	
		$self->gconfSetList();	
	}
}



1;