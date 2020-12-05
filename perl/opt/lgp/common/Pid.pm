#!/usr/bin/perl -w

########################################################################################################
# @description Process Identifier ( pid ) Control Class ( Module )
#
# @author David O Neill
# @studentid 0813001
# @email dave@feeditout.com
# @supervisor J.J Collins
# @project Envisioning group policy for a linux heterogeneous environment
########################################################################################################

package Pid;


sub new 
{
	my( $class , $file  ) = @_;

	my $self =
	{
		_pidFile => undef,
		_pid => undef
	};
	
	bless( $self , $class );
	
	if( defined( $file ) )
	{
		$self->{ _pidFile } = "/tmp/" . $file;
	}
	
	$self->{ _pid } = $$;
	
	return $self;
}


sub create
{
    my( $self , $file ) = @_;
    
    if( defined( $self->{ _pidFile } ) )
    {
    	# pid file already defined
    }
    elsif( defined( $file ) )
    {
    	# user defined pid file
    	$self->{ _pidFile } = "/tmp/" . $file;
    }
    else
    {
    	# fall back create pid file based upon the file 'name.pl.pid'
    	$self->{ _pidFile } = "/tmp/" . $0 . ".pid";
    }
    
    $filename = $self->{ _pidFile };

	if( -e $filename ) 
 	{
 		$pid = `cat $filename`;
 		$pid =~ s/^\s+//gsm;
    	$pid =~ s/\s+$//gsm;
    	$pid =~ s/^\s+$//gsm;
    	$pid =~ s/\n\n/\n/gsm;
 		
		my $running = `ps -A | grep $pid`;

		if( $running =~ /^$pid(.*?)$/gm )
		{
		  	return 0;
		}
		else
		{
		  	$pid = `echo $self->{ _pid } > $filename`;
 			return 1;
		}
 		
 	} 
 	else
 	{
 		$pid = `echo $self->{ _pid } > $filename`;
 		return 1;
 	}
}

sub updatePid
{
    my( $self ) = @_;
    
    if( defined( $self->{ _pidFile } ) )
    {
    	# pid file already defined
    }
    else
    {
    	# fall back create pid file based upon the file 'name.pl.pid'
    	$self->{ _pidFile } = "/tmp/" . $0 . ".pid";
    }
    
    $filename = $self->{ _pidFile };

	$pid = `echo $self->{ _pid } > $filename`;
}


sub destroy
{
    my( $self ) = @_;
    
    my $del = `rm -rvf $self->{ _pidFile }`;
}

sub getPid
{
    my( $self ) = @_;
    
    return $self->{ _pid };
}

sub setPid
{
	my( $self , $pid ) = @_;
	
	$self->{ _pid } = $pid;
	
	$self->updatePid();
}

sub getRunningPid
{
    my( $self ) = @_;
    
    $filename = $self->{ _pidFile };
    
    $pid = `cat $filename`;
 	$pid =~ s/^\s+//gsm;
    $pid =~ s/\s+$//gsm;
    $pid =~ s/^\s+$//gsm;
    $pid =~ s/\n\n/\n/gsm;
    
    return $pid;
}

1;
