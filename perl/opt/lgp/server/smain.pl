#!/usr/bin/perl -w

########################################################################################################
# @description Command Line Controller for the Server Boundary Class
#
# @author David O Neill
# @studentid 0813001
# @email dave@feeditout.com
# @supervisor J.J Collins
# @project Envisioning group policy for a linux heterogeneous environment
########################################################################################################

$| = 1;

BEGIN 
{
    unshift( @INC, $ENV{'lgppath'} . "/server/" );
	unshift( @INC, $ENV{'lgppath'} . "/server/includes/" );
    unshift( @INC, $ENV{'lgppath'} . "/common/" );
}

use strict;
use warnings;
use LGPServer;
use Getopt::Std;
use Pid;
use POSIX;


my %options = ();
my $pid = new Pid;
my $server = new LGPServer;


$SIG{ HUP } = \&signal_interrupt; # hang up signal
$SIG{ INT } = \&signal_interrupt; # interrupt signal eg. ctrl ^ c
$SIG{ QUIT } = \&signal_interrupt; # core dump signal
$SIG{ ILL } = \&signal_interrupt; # illegal instruction
$SIG{ KILL } = \&signal_interrupt; # terminate immediately
$SIG{ QUIT } = \&signal_interrupt; # quit and core dump
$SIG{ STOP } = \&signal_interrupt; # Stop executing temporarily
$SIG{ TERM } = \&signal_interrupt; # Termination (request to terminate)


sub signal_interrupt
{
	use vars qw( %options $pid $server );
	$SIG{ INT } = \&signal_interrupt;      
	$server->stop();
	$pid->destroy();
	sleep 1;
	print "sleep\n";
	sleep 1;
    exit(0);
}


sub startServer
{
	use vars qw( %options $pid $server );
	$server->start( '0.0.0.0' , '50000' , 'tcp' );
	$pid->destroy();
}


sub usage()
{
	print STDERR "usage: $0 [-vd] [-vf] [-p pidFile] [-c configFile] [h] [s]\n";
    print STDERR "  -h           		: this (help) message\n";
    print STDERR "  -v           		: verbose output\n";
    print STDERR "  -d           		: daemon mode\n";
    print STDERR "  -p pidfile   		: pidfile for the daemon\n";
    print STDERR "  -c config    		: config file for the daemon\n";
    print STDERR "  -f           		: run in foreground\n";
    print STDERR "  -s [command]        : send command, where command is [update|stop]\n";
    print STDERR "example: $0 -v -d\n";
    
	exit;
}


sub init()
{
	use vars qw( %options $pid );
    getopts( "vdfp:c:hs:", \%options ) or usage();
    
    if( $options{h} )
    {
    	usage();
    }
    elsif( $options{d} )
    {
    	if( $pid->create() == 0 )
		{
			print "Server is already running : [ " . $pid->getRunningPid() . " ]\n";	
		}
		else
		{	
			print "Starting server ...\n";
			chdir '/' or die "Can't chdir to /: $!";
			umask 0;
			open STDIN, '/dev/null' or die "Can't read /dev/null: $!";
			open STDOUT, '>/dev/null' or die "Can't write to /dev/null: $!";
			open STDERR, '>/dev/null' or die "Can't write to /dev/null: $!";
			defined( my $child = fork ) or die "Can't fork: $!";
			exit if $child;
			my $newPid = setsid() or die "Can't start a new session: $!";
			$pid->setPid( $newPid );
			startServer();
    		
		} 
    }
    elsif( $options{f} )
    {
    	if( $pid->create() == 0 )
		{
			print "Server is already running : [ " . $pid->getRunningPid() . " ]\n";	
		}
		else
		{
    		startServer();
		}
    }
    elsif( $options{s} )
    {
    	if( -e "/tmp/$0.pipe" )
    	{
    		if( $options{s} eq 'stop' )
    		{
    			print "Shutting down the server ...\n";
    			my $cmdPipe = "echo stop > /tmp/$0.pipe";
    			`$cmdPipe`;
    		}
    		elsif( $options{s} eq 'update' )
    		{
    			print "Server requesting update ...\n";
    			my $cmdPipe = "echo update > /tmp/$0.pipe";
    			`$cmdPipe`;
    		}
    		else
    		{
    			print "Unkown command '" . $options{s} . "' in send option\n";
    		}
    		sleep 2;
    	}
    	else
    	{
    		print "I can't find the server, is it running? unable to send kill\n";
    	}
    }
    else
    {
    	usage();
    }
}


init();




