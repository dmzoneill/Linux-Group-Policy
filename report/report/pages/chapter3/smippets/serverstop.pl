sub stop
{   
	if( defined( $self->{ _receive_thread } ) )
	{
		$self->{ _receive_thread }->kill( 'KILL' )->detach();
	}
	
	exit(0);
}