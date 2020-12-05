#!/usr/bin/perl -w

########################################################################################################
# @description Module Abstract Class ( Module ) extends Observer
#
# @author David O Neill
# @studentid 0813001
# @email dave@feeditout.com
# @supervisor J.J Collins
# @project Envisioning group policy for a linux heterogeneous environment
########################################################################################################

package Module;

use strict;
use base 'Observer';
use Grammer;
use InterpreterHelper;

sub new 
{
	my( $class , $module ) = @_;
	my $self = Observer->new( $class );
	
	bless( $self , $class );
	
	return $self;
}


sub addGrammer
{
	my( $self , $eventCode , $eventOptions ) = @_;

	InterpreterHelper::getInstance()->addProvidedModuleGrammer( \Grammer->new( $eventCode , $eventOptions ) );
}


sub registerGrammer
{
	my( $self , $eventCode , $eventOptions ) = @_;
}

1;
