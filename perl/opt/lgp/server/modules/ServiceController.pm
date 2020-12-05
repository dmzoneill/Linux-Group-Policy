#!/usr/bin/perl -w

########################################################################################################
# @description Concrete Module Class ( Module ) extends Module
#
# @author David O Neill
# @studentid 0813001
# @email dave@feeditout.com
# @supervisor J.J Collins
# @project Envisioning group policy for a linux heterogeneous environment
########################################################################################################

package ServiceController;

BEGIN 
{
    unshift( @INC, $ENV{'lgppath'} . "/client/config" );
	unshift( @INC, $ENV{'lgppath'} . "/client/distribution/" );
    unshift( @INC, $ENV{'lgppath'} . "/client/includes/" );
    unshift( @INC, $ENV{'lgppath'} . "/client/modules/" );
	unshift( @INC, $ENV{'lgppath'} . "/client/policies/" );
    unshift( @INC, $ENV{'lgppath'} . "/common/" );
}

use strict;
use base 'Module';


sub new 
{
	my( $class ) = @_;
	
	my $self = Module->new( @_ );
	
	bless( $self , $class );	
	
	return $self;
}


sub registerGrammer
{
	my( $self ) = @_;
		
	$self->addGrammer( "SERVICE_STATUS" , "" );
	$self->addGrammer( "SERVICE_CONTROL" , 
	"\\s*								# spaces may or may not exist
		(								# the group of
			(
				aaeventd |
				abrtd |
				acpid |
				acpi\\-support |
				after\\.local |
				alsa\\-mixer\\-save |
				alsasound |
				anacron |
				apparmor |
				apport |
				atd |
				auditd |
				autofs |
				autoyast |
				avahi\\-daemon |
				avahi\\-dnsconfd |
				before\\.local |
				binfmt\\-support |
				bluetooth |
				bluez\\-coldplug |
				bootmisc |
				brld |
				brltty |
				cgconfig |
				cgred |
				cifs |
				conman |
				console\\-setup |
				cpufreq |
				cpuspeed |
				cron |
				crond |
				cups |
				cupsd |
				dbus |
				devfs |
				dhcpcd |
				dmesg |
				dnsmasq |
				dund |
				dvb |
				earlysyslog |
				earlyxdm |
				ermencoding |
				failsafe\\-x |
				fancontrol |
				fbcondecor |
				fbset |
				fcoe |
				firstboot |
				fsck |
				gdm |
				gpm |
				gpsd |
				grub\\-common |
				haldaemon |
				hidd |
				hostname |
				httpd |
				hwclock |
				hwclock\\-save |
				inputattach |
				ip6tables |
				ipmi |
				iptables |
				irda |
				irqbalance |
				iscsi |
				iscsid |
				joystick |
				kbd |
				kerneloops |
				keymaps |
				killprocs |
				kudzu |
				lirc |
				lircd |
				livesys |
				livesys\\-late |
				lldpad |
				lm\\-sensors |
				local |
				lvm2\\-monitor |
				mcstrans |
				mdadmd |
				mdmonitor |
				mdmpd |
				messagebus |
				microcode\\.ctl |
				microcode_ctl |
				module\\-init\\-tools |
				modules |
				mount\\-ro |
				mtab |
				multipathd |
				mysqld |
				net\\.eth0 |
				net\\.lo |
				netconsole |
				netfs |
				netmount |
				netplugd |
				network |
				network\\-interface |
				network\\-interface\\-security |
				NetworkManager |
				network\\-remotefs |
				nfs |
				nfslock |
				nfsserver |
				nmb |
				nmbd |
				nscd |
				ntp |
				ntpd |
				ntpdate |
				ocalmount |
				oddjobd |
				ondemand |
				openvpn |
				pand |
				pcmciautils |
				pcscd |
				plymouth |
				plymouth\\-log |
				plymouth\\-splash |
				plymouth\\-stop |
				pm\\-profiler |
				portmap |
				portreserve |
				postfix |
				powerd |
				pppd\\-dns |
				procfs |
				procps |
				psacct |
				pulseaudio |
				random |
				raw |
				rawdevices |
				rdisc |
				readahead_early |
				readahead_later |
				readahead\\-list |
				readahead\\-list\\-early |
				resolvconf |
				restorecond |
				rhnsd |
				root |
				rpc\\.idmapd |
				rpc\\.statd |
				rpcbind |
				rpcgssd |
				rpcidmapd |
				rpcsvcgssd |
				rpmconfigcheck |
				rsync |
				rsyncd |
				rsyslog |
				samba |
				saned |
				saslauthd |
				savecache |
				sbl |
				sendmail |
				setserial |
				skeleton\\.compat |
				smartd |
				smb |
				smbd |
				smolt |
				smpppd |
				speech\\-dispatcher |
				splash |
				splash_early |
				sshd |
				sssd |
				stoppreload |
				SuSEfirewall2_init |
				SuSEfirewall2_setup |
				svnserve |
				swap |
				sysctl |
				sysfs |
				syslog |
				syslog\\-ng |
				tcsd |
				udev |
				udev\\-finish |
				udevmonitor |
				udev\\-mount |
				udev\\-post |
				udev\\-postmount |
				udevtrigger |
				ufw |
				unattended\\-upgrades |
				unscd |
				urandom |
				vboxadd |
				vixie\\-cron |
				vmtoolsd |
				vmware\\-tools |
				vncserver |
				wdaemon |
				wpa_supplicant |
				x11\\-common |
				xdm |
				xdm\\.orig |
				xdm\\-setup |
				xfs |
				xinetd |
				ypbind |
				yum\\-updatesd 
			)							# any of these
			\\s*						# followed by optional spaces
		)+								# one or more times		
		\\s+							# followed by at least on spaces
		(on|off) 						# followed by on or off
	");
					
	$self->addGrammer( "SERVICE_INIT" , 
	"\\s*								# spaces may or may not exist
		(								# the group of
			(
				aaeventd |
				abrtd |
				acpid |
				acpi\\-support |
				after\\.local |
				alsa\\-mixer\\-save |
				alsasound |
				anacron |
				apparmor |
				apport |
				atd |
				auditd |
				autofs |
				autoyast |
				avahi\\-daemon |
				avahi\\-dnsconfd |
				before\\.local |
				binfmt\\-support |
				bluetooth |
				bluez\\-coldplug |
				bootmisc |
				brld |
				brltty |
				cgconfig |
				cgred |
				cifs |
				conman |
				console\\-setup |
				cpufreq |
				cpuspeed |
				cron |
				crond |
				cups |
				cupsd |
				dbus |
				devfs |
				dhcpcd |
				dmesg |
				dnsmasq |
				dund |
				dvb |
				earlysyslog |
				earlyxdm |
				ermencoding |
				failsafe\\-x |
				fancontrol |
				fbcondecor |
				fbset |
				fcoe |
				firstboot |
				fsck |
				gdm |
				gpm |
				gpsd |
				grub\\-common |
				haldaemon |
				hidd |
				hostname |
				httpd |
				hwclock |
				hwclock\\-save |
				inputattach |
				ip6tables |
				ipmi |
				iptables |
				irda |
				irqbalance |
				iscsi |
				iscsid |
				joystick |
				kbd |
				kerneloops |
				keymaps |
				killprocs |
				kudzu |
				lirc |
				lircd |
				livesys |
				livesys\\-late |
				lldpad |
				lm\\-sensors |
				local |
				lvm2\\-monitor |
				mcstrans |
				mdadmd |
				mdmonitor |
				mdmpd |
				messagebus |
				microcode\\.ctl |
				microcode_ctl |
				module\\-init\\-tools |
				modules |
				mount\\-ro |
				mtab |
				multipathd |
				mysqld |
				net\\.eth0 |
				net\\.lo |
				netconsole |
				netfs |
				netmount |
				netplugd |
				network |
				network\\-interface |
				network\\-interface\\-security |
				NetworkManager |
				network\\-remotefs |
				nfs |
				nfslock |
				nfsserver |
				nmb |
				nmbd |
				nscd |
				ntp |
				ntpd |
				ntpdate |
				ocalmount |
				oddjobd |
				ondemand |
				openvpn |
				pand |
				pcmciautils |
				pcscd |
				plymouth |
				plymouth\\-log |
				plymouth\\-splash |
				plymouth\\-stop |
				pm\\-profiler |
				portmap |
				portreserve |
				postfix |
				powerd |
				pppd\\-dns |
				procfs |
				procps |
				psacct |
				pulseaudio |
				random |
				raw |
				rawdevices |
				rdisc |
				readahead_early |
				readahead_later |
				readahead\\-list |
				readahead\\-list\\-early |
				resolvconf |
				restorecond |
				rhnsd |
				root |
				rpc\\.idmapd |
				rpc\\.statd |
				rpcbind |
				rpcgssd |
				rpcidmapd |
				rpcsvcgssd |
				rpmconfigcheck |
				rsync |
				rsyncd |
				rsyslog |
				samba |
				saned |
				saslauthd |
				savecache |
				sbl |
				sendmail |
				setserial |
				skeleton\\.compat |
				smartd |
				smb |
				smbd |
				smolt |
				smpppd |
				speech\\-dispatcher |
				splash |
				splash_early |
				sshd |
				sssd |
				stoppreload |
				SuSEfirewall2_init |
				SuSEfirewall2_setup |
				svnserve |
				swap |
				sysctl |
				sysfs |
				syslog |
				syslog\\-ng |
				tcsd |
				udev |
				udev\\-finish |
				udevmonitor |
				udev\\-mount |
				udev\\-post |
				udev\\-postmount |
				udevtrigger |
				ufw |
				unattended\\-upgrades |
				unscd |
				urandom |
				vboxadd |
				vixie\\-cron |
				vmtoolsd |
				vmware\\-tools |
				vncserver |
				wdaemon |
				wpa_supplicant |
				x11\\-common |
				xdm |
				xdm\\.orig |
				xdm\\-setup |
				xfs |
				xinetd |
				ypbind |
				yum\\-updatesd 
			)							# any of these
			\\s+?						# followed by optional spaces
		)+								# one or more times		
		\\s+							# followed by at least on spaces
		([0-6]+\\s)?					# run levels
		(on|off) 						# followed by on or off
	");
}


sub serviceControl
{
	my( $self ) = @_;
			
	my $computer = Computer::getInstance();
	my $distro = $computer->getDistro();
	my $serviceCommand = $distro->getServiceController();
		
	my $options = $self->{ actionEvent }->getEventOptions();
	$options =~ s/^\s+//;
	$options =~ s/\s+$//;
	
	my @parts = split( /\s+/ , $options );
	my $actionOnOff = $parts[ @parts - 1 ];
	
	for( my $y = 0; $y < @parts - 1; $y++ )
	{		
		my $opt;
		my $exec;
		my $service = $distro->getDistributionSpecificService( $parts[ $y ] );
				
		if( !defined( $service ) )
		{
			next;
		}
			
		if( $serviceCommand eq "service" )
		{
			$opt = ( $actionOnOff eq "on" ) ? "start" : "stop";
			$exec = "$serviceCommand $service $opt\n";			
		}
		
		if( $serviceCommand eq "rc-service" )
		{
			$opt = ( $actionOnOff eq "on" ) ? "start" : "stop";
			$exec = "$serviceCommand --ifexists $service $opt\n";			
		}
		
		$self->{ _log }->log( 'info' , $self->{ actionEvent }->getEventCode() . ' : ' . $exec );
		`$exec  2>&1 >/dev/null`;
	}	
}


sub serviceInit
{
	my( $self ) = @_;
	
	my $computer = Computer::getInstance();
	my $distro = $computer->getDistro();
	my $serviceCommand = $distro->getServiceModifier();
	
	my $options = $self->{ actionEvent }->getEventOptions();
	$options =~ s/^\s+//;
	$options =~ s/\s+$//;
	
	my @parts = split( /\s+/ , $options );
	my $actionOnOff = $parts[ @parts - 1 ];
	my $runlevels = -1;
		
	if( $options =~ m/\s+([0-6]+)\s+/ )
	{
		$runlevels = $1;
	}
	
	my $loopend = ( $runlevels != -1 ) ? @parts - 2 : @parts - 1;
	
	for( my $y = 0; $y < $loopend; $y++ )
	{
		my $exec;
		my $opt = $actionOnOff;
		my $runlevel = ( $runlevels != -1 ) ? "--level $runlevels" : "";
		my $service = $distro->getDistributionSpecificService( $parts[ $y ] );
		
		if( !defined( $service ) )
		{
			next;
		}
				
		if( $serviceCommand eq "chkconfig" )
		{
			$exec = "chkconfig $runlevel $service $actionOnOff\n";			
		}		
		
		if( $serviceCommand eq "rc-update" )
		{
			$opt = ( $actionOnOff eq "on" ) ? "add" : "del";
			$exec = "$serviceCommand $opt $service\n";			
		}
		
		if( $serviceCommand eq "update-rc.d" )
		{
			$opt = ( $actionOnOff eq "on" ) ? "defaults" : "remove";
			$exec = "$serviceCommand $service $opt\n";				
		}
		
		$self->{ _log }->log( 'info' , $self->{ actionEvent }->getEventCode() . ' : ' . $exec );
		`$exec  2>&1 >/dev/null`;
	}
}
	


sub update
{
    my( $self , $actionEvent , $coordinator ) = @_; 
    
    $self->{ coordinator } = $coordinator;
    $self->{ actionEvent } = $actionEvent;
	
	if( $self->{ actionEvent }->getEventCode() eq "SERVICE_CONTROL" )
	{	
		$self->serviceControl();	
	}
	
	elsif( $self->{ actionEvent }->getEventCode() eq "SERVICE_INIT" )
	{	
		$self->serviceInit();	
	}
}


1;