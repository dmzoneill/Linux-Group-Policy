use Filter::Simple;
 
FILTER
{    
	$_ = processText( $_ );
};

1;