$SIG{ HUP } = \&signal_interrupt; # hang up signal
$SIG{ INT } = \&signal_interrupt; # interrupt signal eg. ctrl ^ c
$SIG{ KILL } = \&signal_interrupt; # terminate immediately
$SIG{ TERM } = \&signal_interrupt; # Termination (request to terminate)

sub signal_interrupt
{
	use vars qw( %options $pid $server );
	
	$SIG{ INT } = \&signal_interrupt;   	
	$server->stop();
	$pid->destroy();
	
	sleep 1;
	exit(0);
}