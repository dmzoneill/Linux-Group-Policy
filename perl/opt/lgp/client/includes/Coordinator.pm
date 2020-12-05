#!/usr/bin/perl -w

########################################################################################################
# @description Control Class For Observers and Computer Boundary Class
#
# @author David O Neill
# @studentid 0813001
# @email dave@feeditout.com
# @supervisor J.J Collins
# @project Envisioning group policy for a linux heterogeneous environment
########################################################################################################

package Coordinator;

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
use base 'Observer';
use Computer;


sub new 
{
	my( $class ) = @_;
	my $self = Observer->new( $class );
	bless( $self , $class );

	$self->{ _computer } = Computer::getInstance();
	$self->{ _events } = ();
	$self->{ _modules } = ();
	
	$self->loadModules();
	
	return $self;
}


sub addActionEvent
{
    my( $self , $event ) = @_;
	
    if( !defined( $self->{ _events } ) )
    {
    	$self->{ _events } = ();		
    }
    
   	push( @{ $self->{ _events } } , $event );
   	
   	$self->notify( $event );
}


sub loadModules
{
    my( $self ) = @_; 
	
	my $modulespath = $ENV{'lgppath'} . "/client/modules/";
	
	my $previousModule = "";
	
	if( opendir( DIR , $modulespath ) )
	{
		my @modules = readdir( DIR );
		close( DIR );
	
		foreach my $module ( @modules ) 
		{
			if( $module =~ /(.*)\.pm/s )
			{
				my $moduleName = $1;
			
				if( $previousModule ne $moduleName )
				{
					eval
					{
	        			require "$module";
	        		};
					
	        		my $reference = $moduleName->new();
					
	        		$reference->registerGrammer();
					
					$self->attach( \$reference );
				
					$previousModule = $moduleName;
				}
			}
		} 
	}
	else
	{
		#unable to open path
	}
}


sub getComputer
{
    my( $self ) = @_;
    
    return $self->{ _computer };
}

1;
