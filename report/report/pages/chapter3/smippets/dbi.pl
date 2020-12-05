sub Connect
{
	my( $self , $dbtype , $host , $database , $username , $password ) = @_;

	$self->{ connectionString } = 'DBI:DBTYPE:DBNAME;host=DBHOST';
	$self->{ connectionString } =~ s/DBNAME/$database/m;
	$self->{ connectionString } =~ s/DBHOST/$host/m;
	$self->{ connectionString } =~ s/DBTYPE/$dbtype/m;
	$self->{ connection } = DBI->connect( $self->{ connectionString } , $username, $password );

	if( defined( $self->{ connection } ) ) {
		$self->{ _log }->log( 'info' , "LPGServer $dbtype connection made successfully" );
		return 1;
	}
	$self->{ _log }->log( 'err' , "LPGServer $dbtype connection failed" );
	return 0;
}