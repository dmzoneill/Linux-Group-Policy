$request = encode_base64( "{ 
	
	\"peerPort\" :   \"" . $self->{ _clientport } . "\" , 
	\"request\"  :   \"update\" ,
	\"guid\"  :   \"" . $computer->getGuid() . "\" ,
	\"os\"  :   \"" . $computer->getOs() . "\" ,
	\"version\"  :   \"" . $computer->getVersion() . "\" ,
	\"architecture\"  :   \"" . $computer->getArchitecture() . "\" ,
	\"kernel\"  :   \"" . $computer->getKernel() . "\" ,
	\"psuedoName\"  :   \"" . $computer->getPsuedoName() . "\" ,
	\"distribution\"  :   \"" . $computer->getDistribution() . "\" ,
	\"distroVersion\"  :   \"" . $computer->getDistroVersion() . "\" ,
	\"hostname\"  :   \"" . $computer->getHostname() . "\" ,
	\"clientVersion\" :    \"" . $self->{ _clientversion } . "\" 

}" );