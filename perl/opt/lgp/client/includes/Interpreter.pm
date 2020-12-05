#!/usr/bin/perl -w

########################################################################################################
# @description Interpretor Boundary 
#
# @author David O Neill
# @studentid 0813001
# @email dave@feeditout.com
# @supervisor J.J Collins
# @project Envisioning group policy for a linux heterogeneous environment
########################################################################################################

package Interpreter;



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

use InterpreterHelper;
use Filter::Simple;
use strict;

 
FILTER
{    
	$_ = processDSLExternal( $_ );

    s/^\s+//gsm;
    s/\s+$//gsm;
    s/^\s+$//gsm;
    s/\n\n/\n/gsm;
    
    $_ = processDSLInternal( $_ );
	$_ = "print q{Completed\n};";
};



########################################################################################################
# @description Process the grammer of the DSL
#
# @param The DSL script
#
# @return return the result of the interpretation
########################################################################################################

sub processDSLExternal
{
	my $_ = shift;
	
	my $regex_blocks = '#(\w+)(\s+:|:)(.*?):(\w+)#';    
    my $interchange = "";    
    
    while( $_ =~ /$regex_blocks/gis )
    {      
        if( lc( $1 ) eq 'configuration' )
	    {
            $interchange = InterpreterHelper::getInstance()->interpretDslConfiguration( $3 );
        }
        elsif( lc( $1 ) eq 'action' )
   	    {
            $interchange = InterpreterHelper::getInstance()->interpretDslActions( $3 );
        }     
        
        s/$regex_blocks/$interchange/sm;
    }
	
	return $_;
}



########################################################################################################
# @description Process the embedded Perl in the DSL
#
# @param The DSL script
#
# @return return the perl embedded code to be executed
########################################################################################################

sub processDSLInternal
{
	my $_ = shift;
	my $result = ''; 
	      
    my $regex_code = "<%|%>";      
    my $i = 0;
    
    for( split( /$regex_code/ , $_ ) )
    {
        if( $i++ % 2 == 0 )
        {
            s/[{}]/\\$&/g;
            $result .= "print q{". $_ ."};";
        }
        else
        {
            $result .= $_;
        }
    }
	
	return $result;
}


1;


