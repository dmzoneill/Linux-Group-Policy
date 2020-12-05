#!/usr/bin/perl -w

########################################################################################################
# @description Concrete Distribution Entity Class  
#
# @author David O Neill
# @studentid 0813001
# @email dave@feeditout.com
# @supervisor J.J Collins
# @project Envisioning group policy for a linux heterogeneous environment
########################################################################################################

package Bsd;

use strict;
use base 'Distribution';

sub new 
{
	my( $class ) = shift;

	my $self = Distribution->new( @_ );
	
	bless( $self , $class );
		
	$self->registerServiceNames();
	
	return $self;
}


1;
