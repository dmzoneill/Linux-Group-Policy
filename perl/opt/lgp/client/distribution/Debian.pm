#!/usr/bin/perl -w

########################################################################################################
# @description Concrete Distribution Entity Class  
#
# @author David O Neill
# @studentid 0813001
# @email dave@feeditout.com
# @supervisor J.J Collins
# @project Envisioning group policy for a linux heterogeneous environment
########################################################################################################


package Debian;

use strict;
use base 'Distribution';

sub new 
{
	my( $class ) = shift;

	my $self = Distribution->new( @_ );
	
	$self->{ _serviceController } = "service";
	$self->{ _serviceModifier } = "update-rc.d";
	$self->{ _packageManager } = "apt-get";
	
	bless( $self , $class );
	
	$self->registerServiceNames();
	
	return $self;
}


sub registerServiceNames
{
	my( $self ) = @_;
	
	$self->{ _distrosServices }->{ "aaeventd" } = undef;
	$self->{ _distrosServices }->{ "abrtd" } = undef;
	$self->{ _distrosServices }->{ "acpid" } = "acpid"; 
	$self->{ _distrosServices }->{ "acpi-support" } = "acpi-support"; 
	$self->{ _distrosServices }->{ "after.local" } = undef;
	$self->{ _distrosServices }->{ "alsa-mixer-save" } = "alsa-mixer-save"; 
	$self->{ _distrosServices }->{ "alsasound" } = undef;
	$self->{ _distrosServices }->{ "anacron" } = "anacron"; 
	$self->{ _distrosServices }->{ "apparmor" } = "apparmor"; 
	$self->{ _distrosServices }->{ "apport" } = "apport"; 
	$self->{ _distrosServices }->{ "atd" } = "atd"; 
	$self->{ _distrosServices }->{ "auditd" } = undef;
	$self->{ _distrosServices }->{ "autofs" } = "autofs"; 
	$self->{ _distrosServices }->{ "autoyast" } = undef;
	$self->{ _distrosServices }->{ "avahi-daemon" } = "avahi-daemon"; 
	$self->{ _distrosServices }->{ "avahi-dnsconfd" } = "avahi-dnsconfd"; 
	$self->{ _distrosServices }->{ "before.local" } = undef;
	$self->{ _distrosServices }->{ "binfmt-support" } = "binfmt-support"; 
	$self->{ _distrosServices }->{ "bluetooth" } = "bluetooth"; 
	$self->{ _distrosServices }->{ "bluez-coldplug" } = undef;
	$self->{ _distrosServices }->{ "bootmisc" } = undef;
	$self->{ _distrosServices }->{ "brld" } = undef;
	$self->{ _distrosServices }->{ "brltty" } = "brltty"; 
	$self->{ _distrosServices }->{ "cgconfig" } = undef;
	$self->{ _distrosServices }->{ "cgred" } = undef;
	$self->{ _distrosServices }->{ "cifs" } = undef;
	$self->{ _distrosServices }->{ "conman" } = undef;
	$self->{ _distrosServices }->{ "console-setup" } = "console-setup"; 
	$self->{ _distrosServices }->{ "cpufreq" } = undef;
	$self->{ _distrosServices }->{ "cpuspeed" } = undef;
	$self->{ _distrosServices }->{ "cron" } = "cron"; 
	$self->{ _distrosServices }->{ "crond" } = undef;
	$self->{ _distrosServices }->{ "cups" } = "cups"; 
	$self->{ _distrosServices }->{ "cupsd" } = undef;
	$self->{ _distrosServices }->{ "dbus" } = "dbus"; 
	$self->{ _distrosServices }->{ "devfs" } = undef;
	$self->{ _distrosServices }->{ "dhcpcd" } = undef;
	$self->{ _distrosServices }->{ "dmesg" } = "dmesg"; 
	$self->{ _distrosServices }->{ "dnsmasq" } = undef;
	$self->{ _distrosServices }->{ "dund" } = undef;
	$self->{ _distrosServices }->{ "dvb" } = undef;
	$self->{ _distrosServices }->{ "earlysyslog" } = undef;
	$self->{ _distrosServices }->{ "earlyxdm" } = undef;
	$self->{ _distrosServices }->{ "ermencoding" } = undef;
	$self->{ _distrosServices }->{ "failsafe-x" } = "failsafe-x"; 
	$self->{ _distrosServices }->{ "fancontrol" } = "fancontrol"; 
	$self->{ _distrosServices }->{ "fbcondecor" } = undef;
	$self->{ _distrosServices }->{ "fbset" } = undef;
	$self->{ _distrosServices }->{ "fcoe" } = undef;
	$self->{ _distrosServices }->{ "firstboot" } = undef;
	$self->{ _distrosServices }->{ "fsck" } = undef;
	$self->{ _distrosServices }->{ "gdm" } = "gdm"; 
	$self->{ _distrosServices }->{ "gpm" } = "gpm"; 
	$self->{ _distrosServices }->{ "gpsd" } = undef;
	$self->{ _distrosServices }->{ "grub-common" } = "grub-common"; 
	$self->{ _distrosServices }->{ "haldaemon" } = undef;
	$self->{ _distrosServices }->{ "hidd" } = undef;
	$self->{ _distrosServices }->{ "hostname" } = "hostname"; 
	$self->{ _distrosServices }->{ "httpd" } = undef;
	$self->{ _distrosServices }->{ "hwclock" } = "hwclock"; 
	$self->{ _distrosServices }->{ "hwclock-save" } = "hwclock-save"; 
	$self->{ _distrosServices }->{ "inputattach" } = undef;
	$self->{ _distrosServices }->{ "ip6tables" } = undef;
	$self->{ _distrosServices }->{ "ipmi" } = undef;
	$self->{ _distrosServices }->{ "iptables" } = undef;
	$self->{ _distrosServices }->{ "irda" } = undef;
	$self->{ _distrosServices }->{ "irqbalance" } = "irqbalance"; 
	$self->{ _distrosServices }->{ "iscsi" } = undef;
	$self->{ _distrosServices }->{ "iscsid" } = undef;
	$self->{ _distrosServices }->{ "joystick" } = "joystick"; 
	$self->{ _distrosServices }->{ "kbd" } = undef;
	$self->{ _distrosServices }->{ "kerneloops" } = "kerneloops"; 
	$self->{ _distrosServices }->{ "keymaps" } = undef;
	$self->{ _distrosServices }->{ "killprocs" } = undef;
	$self->{ _distrosServices }->{ "kudzu" } = undef;
	$self->{ _distrosServices }->{ "lirc" } = "lirc"; 
	$self->{ _distrosServices }->{ "lircd" } = undef;
	$self->{ _distrosServices }->{ "livesys" } = undef;
	$self->{ _distrosServices }->{ "livesys-late" } = undef;
	$self->{ _distrosServices }->{ "lldpad" } = undef;
	$self->{ _distrosServices }->{ "lm-sensors" } = "lm-sensors"; 
	$self->{ _distrosServices }->{ "local" } = undef;
	$self->{ _distrosServices }->{ "lvm2-monitor" } = undef;
	$self->{ _distrosServices }->{ "mcstrans" } = undef;
	$self->{ _distrosServices }->{ "mdadmd" } = undef;
	$self->{ _distrosServices }->{ "mdmonitor" } = undef;
	$self->{ _distrosServices }->{ "mdmpd" } = undef;
	$self->{ _distrosServices }->{ "messagebus" } = undef;
	$self->{ _distrosServices }->{ "microcode.ctl" } = "microcode.ctl"; 
	$self->{ _distrosServices }->{ "microcode_ctl" } = undef;
	$self->{ _distrosServices }->{ "module-init-tools" } = "module-init-tools"; 
	$self->{ _distrosServices }->{ "modules" } = undef;
	$self->{ _distrosServices }->{ "mount-ro" } = undef;
	$self->{ _distrosServices }->{ "mtab" } = undef;
	$self->{ _distrosServices }->{ "multipathd" } = undef;
	$self->{ _distrosServices }->{ "mysqld" } = undef;
	$self->{ _distrosServices }->{ "net.eth0" } = undef;
	$self->{ _distrosServices }->{ "net.lo" } = undef;
	$self->{ _distrosServices }->{ "netconsole" } = undef;
	$self->{ _distrosServices }->{ "netfs" } = undef;
	$self->{ _distrosServices }->{ "netmount" } = undef;
	$self->{ _distrosServices }->{ "netplugd" } = undef;
	$self->{ _distrosServices }->{ "network" } = undef;
	$self->{ _distrosServices }->{ "network-interface" } = "network-interface"; 
	$self->{ _distrosServices }->{ "network-interface-security" } = "network-interface-security"; 
	$self->{ _distrosServices }->{ "NetworkManager" } = undef;
	$self->{ _distrosServices }->{ "network-remotefs" } = undef;
	$self->{ _distrosServices }->{ "nfs" } = undef;
	$self->{ _distrosServices }->{ "nfslock" } = undef;
	$self->{ _distrosServices }->{ "nfsserver" } = undef;
	$self->{ _distrosServices }->{ "nmb" } = undef;
	$self->{ _distrosServices }->{ "nmbd" } = "nmbd"; 
	$self->{ _distrosServices }->{ "nscd" } = "nscd"; 
	$self->{ _distrosServices }->{ "ntp" } = "ntp"; 
	$self->{ _distrosServices }->{ "ntpd" } = undef;
	$self->{ _distrosServices }->{ "ntpdate" } = undef;
	$self->{ _distrosServices }->{ "ocalmount" } = undef;
	$self->{ _distrosServices }->{ "oddjobd" } = undef;
	$self->{ _distrosServices }->{ "ondemand" } = "ondemand"; 
	$self->{ _distrosServices }->{ "openvpn" } = "openvpn"; 
	$self->{ _distrosServices }->{ "pand" } = undef;
	$self->{ _distrosServices }->{ "pcmciautils" } = "pcmciautils"; 
	$self->{ _distrosServices }->{ "pcscd" } = "pcscd"; 
	$self->{ _distrosServices }->{ "plymouth" } = "plymouth"; 
	$self->{ _distrosServices }->{ "plymouth-log" } = "plymouth-log"; 
	$self->{ _distrosServices }->{ "plymouth-splash" } = "plymouth-splash"; 
	$self->{ _distrosServices }->{ "plymouth-stop" } = "plymouth-stop"; 
	$self->{ _distrosServices }->{ "pm-profiler" } = undef;
	$self->{ _distrosServices }->{ "portmap" } = undef;
	$self->{ _distrosServices }->{ "portreserve" } = undef;
	$self->{ _distrosServices }->{ "postfix" } = "postfix"; 
	$self->{ _distrosServices }->{ "powerd" } = undef;
	$self->{ _distrosServices }->{ "pppd-dns" } = "pppd-dns"; 
	$self->{ _distrosServices }->{ "procfs" } = undef;
	$self->{ _distrosServices }->{ "procps" } = "procps"; 
	$self->{ _distrosServices }->{ "psacct" } = undef;
	$self->{ _distrosServices }->{ "pulseaudio" } = "pulseaudio"; 
	$self->{ _distrosServices }->{ "random" } = undef;
	$self->{ _distrosServices }->{ "raw" } = undef;
	$self->{ _distrosServices }->{ "rawdevices" } = undef;
	$self->{ _distrosServices }->{ "rdisc" } = undef;
	$self->{ _distrosServices }->{ "readahead_early" } = undef;
	$self->{ _distrosServices }->{ "readahead_later" } = undef;
	$self->{ _distrosServices }->{ "readahead-list" } = undef;
	$self->{ _distrosServices }->{ "readahead-list-early" } = undef;
	$self->{ _distrosServices }->{ "resolvconf" } = "resolvconf"; 
	$self->{ _distrosServices }->{ "restorecond" } = undef;
	$self->{ _distrosServices }->{ "rhnsd" } = undef;
	$self->{ _distrosServices }->{ "root" } = undef;
	$self->{ _distrosServices }->{ "rpc.idmapd" } = undef;
	$self->{ _distrosServices }->{ "rpc.statd" } = undef;
	$self->{ _distrosServices }->{ "rpcbind" } = "rpcbind"; 
	$self->{ _distrosServices }->{ "rpcgssd" } = undef;
	$self->{ _distrosServices }->{ "rpcidmapd" } = undef;
	$self->{ _distrosServices }->{ "rpcsvcgssd" } = undef;
	$self->{ _distrosServices }->{ "rpmconfigcheck" } = undef;
	$self->{ _distrosServices }->{ "rsync" } = "rsync"; 
	$self->{ _distrosServices }->{ "rsyncd" } = undef;
	$self->{ _distrosServices }->{ "rsyslog" } = "rsyslog"; 
	$self->{ _distrosServices }->{ "samba" } = undef;
	$self->{ _distrosServices }->{ "saned" } = "saned"; 
	$self->{ _distrosServices }->{ "saslauthd" } = undef;
	$self->{ _distrosServices }->{ "savecache" } = undef;
	$self->{ _distrosServices }->{ "sbl" } = undef;
	$self->{ _distrosServices }->{ "sendmail" } = undef;
	$self->{ _distrosServices }->{ "setserial" } = "setserial"; 
	$self->{ _distrosServices }->{ "skeleton.compat" } = undef;
	$self->{ _distrosServices }->{ "smartd" } = undef;
	$self->{ _distrosServices }->{ "smb" } = undef;
	$self->{ _distrosServices }->{ "smbd" } = "smbd"; 
	$self->{ _distrosServices }->{ "smolt" } = undef;
	$self->{ _distrosServices }->{ "smpppd" } = undef;
	$self->{ _distrosServices }->{ "speech-dispatcher" } = "speech-dispatcher"; 
	$self->{ _distrosServices }->{ "splash" } = undef;
	$self->{ _distrosServices }->{ "splash_early" } = undef;
	$self->{ _distrosServices }->{ "sshd" } = undef;
	$self->{ _distrosServices }->{ "sssd" } = "sssd"; 
	$self->{ _distrosServices }->{ "stoppreload" } = undef;
	$self->{ _distrosServices }->{ "SuSEfirewall2_init" } = undef;
	$self->{ _distrosServices }->{ "SuSEfirewall2_setup" } = undef;
	$self->{ _distrosServices }->{ "svnserve" } = undef;
	$self->{ _distrosServices }->{ "swap" } = undef;
	$self->{ _distrosServices }->{ "sysctl" } = undef;
	$self->{ _distrosServices }->{ "sysfs" } = undef;
	$self->{ _distrosServices }->{ "syslog" } = undef;
	$self->{ _distrosServices }->{ "syslog-ng" } = undef;
	$self->{ _distrosServices }->{ "tcsd" } = undef;
	$self->{ _distrosServices }->{ "udev" } = "udev"; 
	$self->{ _distrosServices }->{ "udev-finish" } = "udev-finish"; 
	$self->{ _distrosServices }->{ "udevmonitor" } = "udevmonitor"; 
	$self->{ _distrosServices }->{ "udev-mount" } = undef;
	$self->{ _distrosServices }->{ "udev-post" } = undef;
	$self->{ _distrosServices }->{ "udev-postmount" } = undef;
	$self->{ _distrosServices }->{ "udevtrigger" } = "udevtrigger"; 
	$self->{ _distrosServices }->{ "ufw" } = "ufw"; 
	$self->{ _distrosServices }->{ "unattended-upgrades" } = "unattended-upgrades"; 
	$self->{ _distrosServices }->{ "unscd" } = undef;
	$self->{ _distrosServices }->{ "urandom" } = undef;
	$self->{ _distrosServices }->{ "vboxadd" } = undef;
	$self->{ _distrosServices }->{ "vixie-cron" } = undef;
	$self->{ _distrosServices }->{ "vmtoolsd" } = undef;
	$self->{ _distrosServices }->{ "vmware-tools" } = undef;
	$self->{ _distrosServices }->{ "vncserver" } = undef;
	$self->{ _distrosServices }->{ "wdaemon" } = undef;
	$self->{ _distrosServices }->{ "wpa_supplicant" } = undef;
	$self->{ _distrosServices }->{ "x11-common" } = "x11-common"; 
	$self->{ _distrosServices }->{ "xdm" } = "xdm"; 
	$self->{ _distrosServices }->{ "xdm.orig" } = undef;
	$self->{ _distrosServices }->{ "xdm-setup" } = undef;
	$self->{ _distrosServices }->{ "xfs" } = "xfs"; 
	$self->{ _distrosServices }->{ "xinetd" } = "xinetd"; 
	$self->{ _distrosServices }->{ "ypbind" } = undef;
	$self->{ _distrosServices }->{ "yum-updatesd" } = undef;
}


1;
