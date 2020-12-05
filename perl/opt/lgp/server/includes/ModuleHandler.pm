#!/usr/bin/perl -w

########################################################################################################
# @description Server Boundary Class ( Module ) - Net IO
#
# @author David O Neill
# @studentid 0813001
# @email dave@feeditout.com
# @supervisor J.J Collins
# @project Envisioning group policy for a linux heterogeneous environment
########################################################################################################

package ModuleHandler;

use MIME::Base64;


sub new 
{
	my( $class ) = @_;

	my $self = {};
	my @_fileNames : shared = ();
	
	$self =
	{
		_fileNames => \@_fileNames,								# not shared implicitly
	};
	
	bless( $self , $class );
	
	$self->readModules();
	
	return $self;
}


sub readModules
{
	my( $self ) = @_;
	
	my $modulespath = $ENV{'lgppath'} . "/server/modules/";
	
	my $previousModule = "";
	
	if( opendir( DIR , $modulespath ) )
	{
		my @modules = readdir( DIR );
		close( DIR );
	
		foreach my $module ( @modules ) 
		{
			if( $module =~ /(.*)\.pm/s )
			{			
				push( @{ $self->{ _fileNames } } , $module );
			}
		} 
	}
}


sub getModules
{
	my( $self ) = @_;
	
	my $modulespath = $ENV{'lgppath'} . "/server/modules/";
	
	my $files = "{";
	
	my $size = @{ $self->{ _fileNames } };

	for (my $i = 0; $i < $size; $i++) 
	{
		my $modulename = @{ $self->{ _fileNames } }[ $i ];
		my $filepath = $modulespath . $modulename;
		
		if( open( FILE , $filepath ) )
		{
			my @lines = <FILE>;
			close( FILE );
			
			my $fileData = "";
			
			foreach my $line ( @lines )
			{
				$fileData .= $line;
			}
	
			$files .= " \"" . $modulename . "\" : \"" . encode_base64( $fileData ) . "\"";
			
			if( $i < $size - 1 )
			{
				$files .= ",";
			}			
		}
	}
		
	$files .= "}";
	
	return $files;
}

sub saveModule
{
	my( $self , $moduleFile, $moduleContents ) = @_;
	
	my $modulespath = $ENV{'lgppath'} . "/server/modules/";
	my $filepath = $modulespath . $moduleFile;
	
	if( open( FILE , "+>", $filepath ) )
	{
		push( @{ $self->{ _fileNames } } , $module );
		print FILE $moduleContents;
		close( FILE );		
	}	
}


sub deleteModule
{
	my( $self , $moduleFile ) = @_;
	
	my $modulespath = $ENV{'lgppath'} . "/server/modules/";
	my $filepath = $modulespath . $moduleFile;
	unlink $filepath;
	
	my $size = @{ $self->{ _fileNames } };

	for (my $i = 0; $i < $size; $i++) 
	{
		my $modulename = @{ $self->{ _fileNames } }[ $i ];
		
		if( $modulename eq $moduleFile )
		{
			delete( @{ $self->{ _fileNames } }[ $i ] );
			return;
		}
	}
}


1;