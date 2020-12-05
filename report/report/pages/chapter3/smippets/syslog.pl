use Sys::Syslog qw( :DEFAULT setlogsock);

sub log
{
	my( $self , $level , $message ) = @_;
	openlog( 'cmain.pl' , 'ndelay,nofatal,nowait,perror,pid' ,  'local6' );
	syslog( $level , $message );
	closelog();
}

# $level = 
# LOG_ALERT - action must be taken immediately
# LOG_CRIT - critical conditions
# LOG_ERR - error conditions
# LOG_WARNING - warning conditions
# LOG_NOTICE - normal, but significant, condition
# LOG_INFO - informational message
