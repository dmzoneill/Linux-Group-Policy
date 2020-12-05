#!/usr/bin/perl -w

########################################################################################################
# @description Interpretor Control Class ( Module )
#
# @author David O Neill
# @studentid 0813001
# @email dave@feeditout.com
# @supervisor J.J Collins
# @project Envisioning group policy for a linux heterogeneous environment
########################################################################################################

package InterpreterHelper;



########################################################################################################
# @description Required Libraries & modules
########################################################################################################

BEGIN 
{
    unshift( @INC, $ENV{'lgppath'} . "/client/config" );
	unshift( @INC, $ENV{'lgppath'} . "/client/distribution/" );
    unshift( @INC, $ENV{'lgppath'} . "/client/includes/" );
    unshift( @INC, $ENV{'lgppath'} . "/client/modules/" );
	unshift( @INC, $ENV{'lgppath'} . "/client/policies/" );
    unshift( @INC, $ENV{'lgppath'} . "/common/" );
}

use Coordinator;
use ActionEvent;
use strict;


########################################################################################################
# Static Variables
########################################################################################################

my $singletonInstance = undef;


########################################################################################################
# @description Constructor
#
# @param The name of the Module which will be blessed 'instance of the modules name'
#
# @return the blessed module instance
########################################################################################################

sub new 
{
	if( !defined( $singletonInstance ) )
	{
		my( $class ) = @_;

		my %config = ();
		my @additionalGrammer = ();

		my $self = {
			_config => \%config,
			_additionalGrammer => \@additionalGrammer,
			_coordinator => undef
		};

		bless( $self , $class );

		$singletonInstance = $self;

		$self->{ _coordinator } = new Coordinator();

	}
	
	return $singletonInstance;
}


########################################################################################################
# @description getInstance Singleton
#
# @return the same instance
########################################################################################################

sub getInstance 
{
	if( !defined( $singletonInstance ) )
	{
		$singletonInstance = new InterpreterHelper();
	}
	
	return $singletonInstance;
}



########################################################################################################
# @description Adds grammer provided by the modules into the provided Grammer array
#
# @param Reference to the current object
# @param The new grammer provided by modules 
########################################################################################################

sub addProvidedModuleGrammer
{
	my( $self , $providedGrammer ) = @_;
	
	push( @{ $self->{ _additionalGrammer } }  , $providedGrammer );
}



########################################################################################################
# @description Interprets the DSL actions area using the grammer provided by the modules
#              Creates actionEvents to be passed back to the modules for interpreted execution
#
# @param Reference to the current object
# @param Actions in the DSL to be interpreted 
#
# @return ""
########################################################################################################

sub interpretDslActions
{
	my( $self , $data ) = @_;
    my $result = ''; 
    
	foreach my $grammer ( @{ $self->{ _additionalGrammer } } )
	{
		my $deref = $$grammer;
		my $keyWord = $deref->getKeyCode();
		my $keyOptions = $deref->getEventParser();
		
		my @dataLines = split( /\n/ , $data );
		
		foreach my $line( @dataLines )
		{
			if( $line =~ /^(\s*$keyWord)(\s*:)(.*?)$/)
			{
				my $rvalue = $3;
				$rvalue =~ s/^\s+//gsm;
	    		$rvalue =~ s/\s+$//gsm;
	    		$rvalue =~ s/^\s+$//gsm;
	    		$rvalue =~ s/\n\n/\n/gsm;

				if( $rvalue =~ qr/$keyOptions/x )
				{				
					$self->{ _coordinator }->addActionEvent( new ActionEvent( $keyWord , $rvalue ) );
				}
			}
		}
	}
    
    return $result;
}



########################################################################################################
# @description Interprets the configuration area 
#
# @param Reference to the current object
# @param Configuration data for the DSL for later use
#
# @return "" 
########################################################################################################

sub interpretDslConfiguration
{
	my( $self , $data ) = @_;
    my $result = "";         
    my %pairs = %{ $self->prep_key_value_pairs( $data ) };    
    
    while( my ( $key , $value ) = each( %pairs ) )
    { 
        my @vals = split( /\s+/ , $value );
        
        if( $key eq 'NAME' )
        {
        	$self->{ _config }->{ $key } = $value;	
        }
        elsif( $key eq 'TYPE' )
        {
        	$self->{ _config }->{ $key } = $value;
        }
        elsif( $key eq 'REPORTING' )
        {
        	$self->{ _config }->{ $key } = $value;
        }
        elsif( $key eq 'LOGGING' )
        {
        	$self->{ _config }->{ $key } = $value;
        }
        elsif( $key eq 'LOGVERBOSITY' )
        {
        	$self->{ _config }->{ $key } = $value;
        }
        else
        {
        	$self->{ _config }->{ $key } = ();
        	my @values = split( /\s+/ , $value );
        	
        	foreach my $arrVal ( @values )
        	{
        		push( @{ $self->{ _config }->{ $key } } , $arrVal ); 
        	}
        }
    }
        
    return $result;
}



########################################################################################################
# @description Splits the Lvalue : Rvalur syntax of the DSL into a hash lookup
#
# @param Reference to the current object
# @param The data to be split and put into the hash as key / value pairs
#
# @return The key / value hash
########################################################################################################

sub prep_key_value_pairs
{
	my( $self , $data ) = @_;
        
    $data =~ s/^\s+//gsm;
    $data =~ s/\s+$//gsm;
    $data =~ s/^\s+$//gsm;
    $data =~ s/\n\n/\n/gsm;
    
    my @lines = split( '\n' , $data );   
    
    my %keypairhash = ();
    
    for my $line( @lines )
    {	
	    my @pairs = split( /:/ , $line );
	    
	    if( @pairs == 1 )
	    {
	    	next;
	    }
	    	    
	    $pairs[ 1 ] .= "";
	    
	    $pairs[ 0 ] =~ s/\*//;
	    $pairs[ 0 ] =~ s/^\s+//;
	    $pairs[ 0 ] =~ s/\s+$//;
	    
	    $pairs[ 1 ] =~ s/^\s+//;
	    $pairs[ 1 ] =~ s/\s+$//;
	    
	    $keypairhash{ $pairs[ 0 ] } = $pairs[ 1 ];
    }
    
    return \%keypairhash;
}

1;