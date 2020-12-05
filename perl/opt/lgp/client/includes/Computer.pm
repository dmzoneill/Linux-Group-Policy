#!/usr/bin/perl -w

########################################################################################################
# @description Computer Boundary Class ( Module )
#
# @author David O Neill
# @studentid 0813001
# @email dave@feeditout.com
# @supervisor J.J Collins
# @project Envisioning group policy for a linux heterogeneous environment
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

package Computer;

use Data::UUID;

use Bsd;
use Darwin;
use Netbsd;
use Freebsd;

use Redhat;
use Fedora;
use Centos;

use Debian;
use Ubuntu;
use Mint;

use Suse;
use Gentoo;

use LGPSyslog;


########################################################################################################
# Static Variables
########################################################################################################

my $singletonInstance = undef;


sub new 
{	
	if( !defined( $singletonInstance ) )
	{
		my( $class ) = @_;

		my $self =
		{
			_distro => undef,
			_os => undef,
			_version => undef,
			_architecture => undef,
			_kernel => undef,
			_psuedoName => undef,
			_distribution => undef,
			_distroVersion => undef,
			_guid => undef,
			_hostname => undef,
			_log => undef
		};
			
		$self->{ _log } = \LGPSyslog::getInstance();
	
		bless( $self , $class );
		
		$singletonInstance = $self;
	
		$self->detectDistro();
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
		$singletonInstance = new Computer();
	}
	
	return $singletonInstance;
}


sub detectDistro
{
    my( $self ) = @_; 
    
    my @parts = ();
    my $releaseFile = `ls /etc/*-release 2> /dev/null | head -n 1`;
    
    if( $releaseFile ne '' )
    {
		chomp( $releaseFile );
		my @subparts = split( /\// , $releaseFile ); 
		@parts = split( /-|_/ , $subparts[2] );
    }

    my $os = `uname -s`;
    my $version = `uname -r`;
	my $architecture = `uname -m`;
	my $kernel = '';
	my $psuedoName = '';
	my $distribution = '';
	my $distroVersion = '';
	my $hostname = '';
	
	$os =~ s/\n//gsm;
	$version =~ s/\n//gsm;
	$architecture =~ s/\n//gsm;
	
	if( lc( $os ) eq "linux" )
	{
		if( defined( ${ $self->{ _log } } ) )
		{
			${ $self->{ _log } }->log( 'info' , "Client detected as linux host" );
		}
		
		$kernel = $version;
		$distribution = lc( $parts[0] );
				
		if( $distribution eq "suse" )
		{
			my $releasedata = `cat $releaseFile`;
			my @releaseparts = split( /\// , $releasedata ); 
			
			foreach my $releasepart ( @releaseparts )
			{				
				if( $releasepart =~ m/VERSION = (.*)/i )
				{
					$distroVersion = lc( $1 );
				}
				
				if( $releasepart =~ m/CODENAME = (.*)/i )
				{
					$psuedoName = lc( $1 );
				}				
			}	
			$hostname = `hostname`;
			$self->{ _distro } = new Suse;
		}
		elsif( $distribution eq "lsb" )
		{
			my $lsbFile = `cat $releaseFile`;
			my @lsbparts = split( /\// , $lsbFile ); 
			
			foreach my $lsbpart( @lsbparts )
			{
				if( $lsbpart =~ m/DISTRIB_ID=(.*)/i )
				{
					$distribution = lc( $1 );
				}
				
				if( $lsbpart =~ m/DISTRIB_RELEASE=(.*)/i )
				{
					$distroVersion = lc( $1 );
				}
				
				if( $lsbpart =~ m/DISTRIB_CODENAME=(.*)/i )
				{
					$psuedoName = lc( $1 );
				}				
			}	
			$hostname = `hostname`;
			
			if( $distribution eq "ubuntu" )
			{
				$self->{ _distro } = new Ubuntu;
			}
		}
		elsif( $distribution eq "redhat" )
		{
			#to do
			$self->{ _distro } = new Redhat;
		}
		elsif( $distribution eq "slackware" )
		{
			#to do
			$self->{ _distro } = new Slackware;
		}
		elsif( $distribution eq "fedora" )
		{
			$psuedoName = `cat $releaseFile | sed s/.*\\(// | sed s/\\)//`;
			$distroVersion = `cat $releaseFile | sed s/.*release\\ // | sed s/\\ .*//`;
			$hostname = `hostname`;
			$self->{ _distro } = new Fedora;
		}
		elsif( $distribution eq "debian" )
		{
			#to do
			$self->{ _distro } = new Debian;
		}
		elsif( $distribution eq "mandrake" )
		{
			#to do
			$self->{ _distro } = new Mandrake;
		}
		elsif( $distribution eq "gentoo" )
		{
			$psuedoName = `cat $releaseFile`;
			
			if( $psuedoName =~ /.*(\d\.\d\.\d).*/ )
			{
				$distroVersion = lc( $1 );
			}
			
			if( $psuedoName =~ /(.*)\d\.\d\.\d.*/ )
			{
				$psuedoName = lc( $1 );
			}
			
			$hostname = `hostname`;
			$self->{ _distro } = new Gentoo;
		}
		
		if( defined( ${ $self->{ _log } } ) )
		{
			${ $self->{ _log } }->log( 'info' , "$distribution distribution detected" );
		}
		
		$psuedoName =~ s/\n//gsm;
		$distroVersion =~ s/\n//gsm;
	}
	elsif( lc( $os ) eq "darwin" )
	{
		#mac
		$distroVersion = `sw_vers -productVersion | tr -d '\n'`;
		$kernel = $version;
		$version = `sw_vers -buildVersion | tr -d '\n'`;
		$distribution = `sw_vers -productName | tr -d '\n'`;
		
		@codeVersionParts = split( /\./ , $distroVersion );

		if( $codeVersionParts[1] eq "0" )
		{
			$psuedoName = "Cheetah";
		}
		elsif( $codeVersionParts[1] eq "1" )
		{
			$psuedoName = "Puma";
		}
		elsif( $codeVersionParts[1] eq "2" )
		{
			$psuedoName = "Jaguar";
		}
		elsif( $codeVersionParts[1] eq "3" )
		{
			$psuedoName = "Panther";
		}
		elsif( $codeVersionParts[1] eq "4" )
		{
			$psuedoName = "Tiger";
		}
		elsif( $codeVersionParts[1] eq "5" )
		{
			$psuedoName = "Leopard";
		}
		elsif( $codeVersionParts[1] eq "6" )
		{
			$psuedoName = "Snow Leopard";
		}
		elsif( $codeVersionParts[1] eq "7" )
		{
			$psuedoName = "Lion";
		}
		else
		{
			$psuedoName = "unknown";
		}
		
		$self->{ _distro } = new Darwin;		
	}	
	elsif( lc( $os ) eq "freebsd" )
	{
		#to do
		$self->{ _distro } = new Freebsd;
	}
	elsif( lc( $os ) eq "netbsd" )
	{
		#to do
		$self->{ _distro } = new Netbsd;
	}
	
	$hostname =~ s/\n//gsm;
	
	my $UUIDsalt =  "$os - $version - $architecture - $kernel - $psuedoName - $distribution - $distroVersion\n";
	my $uuid = new Data::UUID;
	
   	$self->{ _guid } = $uuid->create_from_name_str( NameSpace_X500 , $UUIDsalt );
	$self->{ _os } = $os;
	$self->{ _version } = $version;
	$self->{ _architecture } = $architecture;
	$self->{ _kernel } = $kernel;
	$self->{ _psuedoName } = $psuedoName;
	$self->{ _distribution } = $distribution;
	$self->{ _distroVersion } = $distroVersion;
	$self->{ _hostname } = $hostname;
	
	if( defined( ${ $self->{ _log } } ) )
	{
		${ $self->{ _log } }->log( 'info' , "Client uuid " . $self->{ _guid } );
	}	
}


sub getDistro
{
    my( $self ) = @_; 
	    
    return $self->{ _distro };
}


sub getGuid
{
    my( $self ) = @_;
    
    return $self->{ _guid };
}


sub getOs
{
    my( $self ) = @_;
	    
    return $self->{ _os };
}


sub getVersion
{
    my( $self ) = @_;
	
    return $self->{ _version };
}


sub getArchitecture
{
    my( $self ) = @_;
	
    return $self->{ _architecture };
}


sub getKernel
{
    my( $self ) = @_;
	
    return $self->{ _kernel };
}


sub getPsuedoName
{
    my( $self ) = @_;
	
    return $self->{ _psuedoName };
}


sub getDistribution
{
    my( $self ) = @_;
	
    return $self->{ _distribution };
}


sub getDistroVersion
{
    my( $self ) = @_;
	
    return $self->{ _distroVersion };
}


sub getHostname
{
    my( $self ) = @_;
	
    return $self->{ _hostname };
}

1;
