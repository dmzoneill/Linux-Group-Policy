# where $self->{ _pipe } is /directory/filename

if( -e $self->{ _pipe } )
{
	system( "rm -rf " . $self->{ _pipe } );
}

system( "mkfifo " . $self->{ _pipe } );	

if( sysopen( FIFO , $self->{ _pipe } , O_RDWR ) ) 
{	
	while( ${ $self->{ _continue } } != -1 ) 
	{
		my $input = <FIFO>;
		chomp( $input );

		if( $input eq 'stop' )
		{
			${ $self->{ _continue } } = -1;
			last;
		}
	}

	close(FIFO);
} 