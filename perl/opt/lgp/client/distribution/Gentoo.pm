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


package Gentoo;

use strict;
use base 'Distribution';
use diagnostics -verbose;

sub new 
{
	my( $class ) = shift;

	my $self = Distribution->new( @_ );
	
	$self->{ _serviceController } = "rc-service";
	$self->{ _serviceModifier } = "rc-update";
	$self->{ _packageManager } = "emerge";
	
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
	$self->{ _distrosServices }->{ "acpi-support" } = undef;
	$self->{ _distrosServices }->{ "after.local" } = undef;
	$self->{ _distrosServices }->{ "alsa-mixer-save" } = undef;
	$self->{ _distrosServices }->{ "alsasound" } = undef;
	$self->{ _distrosServices }->{ "anacron" } = undef;
	$self->{ _distrosServices }->{ "apparmor" } = undef;
	$self->{ _distrosServices }->{ "apport" } = undef;
	$self->{ _distrosServices }->{ "atd" } = undef;
	$self->{ _distrosServices }->{ "auditd" } = "auditd"; 
	$self->{ _distrosServices }->{ "autofs" } = "autofs"; 
	$self->{ _distrosServices }->{ "autoyast" } = undef;
	$self->{ _distrosServices }->{ "avahi-daemon" } = undef;
	$self->{ _distrosServices }->{ "avahi-dnsconfd" } = undef;
	$self->{ _distrosServices }->{ "before.local" } = undef;
	$self->{ _distrosServices }->{ "binfmt-support" } = undef;
	$self->{ _distrosServices }->{ "bluetooth" } = "bluetooth"; 
	$self->{ _distrosServices }->{ "bluez-coldplug" } = undef;
	$self->{ _distrosServices }->{ "bootmisc" } = "bootmisc"; 
	$self->{ _distrosServices }->{ "brld" } = undef;
	$self->{ _distrosServices }->{ "brltty" } = undef;
	$self->{ _distrosServices }->{ "cgconfig" } = undef;
	$self->{ _distrosServices }->{ "cgred" } = undef;
	$self->{ _distrosServices }->{ "cifs" } = undef;
	$self->{ _distrosServices }->{ "conman" } = undef;
	$self->{ _distrosServices }->{ "console-setup" } = undef;
	$self->{ _distrosServices }->{ "cpufreq" } = undef;
	$self->{ _distrosServices }->{ "cpuspeed" } = undef;
	$self->{ _distrosServices }->{ "cron" } = undef;
	$self->{ _distrosServices }->{ "crond" } = undef;
	$self->{ _distrosServices }->{ "cups" } = undef;
	$self->{ _distrosServices }->{ "cupsd" } = "cupsd"; 
	$self->{ _distrosServices }->{ "dbus" } = "dbus"; 
	$self->{ _distrosServices }->{ "devfs" } = "devfs"; 
	$self->{ _distrosServices }->{ "dhcpcd" } = "dhcpcd"; 
	$self->{ _distrosServices }->{ "dmesg" } = "dmesg"; 
	$self->{ _distrosServices }->{ "dnsmasq" } = "dnsmasq"; 
	$self->{ _distrosServices }->{ "dund" } = undef;
	$self->{ _distrosServices }->{ "dvb" } = undef;
	$self->{ _distrosServices }->{ "earlysyslog" } = undef;
	$self->{ _distrosServices }->{ "earlyxdm" } = undef;
	$self->{ _distrosServices }->{ "ermencoding" } = "ermencoding"; 
	$self->{ _distrosServices }->{ "failsafe-x" } = undef;
	$self->{ _distrosServices }->{ "fancontrol" } = undef;
	$self->{ _distrosServices }->{ "fbcondecor" } = "fbcondecor"; 
	$self->{ _distrosServices }->{ "fbset" } = undef;
	$self->{ _distrosServices }->{ "fcoe" } = undef;
	$self->{ _distrosServices }->{ "firstboot" } = undef;
	$self->{ _distrosServices }->{ "fsck" } = "fsck"; 
	$self->{ _distrosServices }->{ "gdm" } = undef;
	$self->{ _distrosServices }->{ "gpm" } = undef;
	$self->{ _distrosServices }->{ "gpsd" } = undef;
	$self->{ _distrosServices }->{ "grub-common" } = undef;
	$self->{ _distrosServices }->{ "haldaemon" } = undef;
	$self->{ _distrosServices }->{ "hidd" } = undef;
	$self->{ _distrosServices }->{ "hostname" } = "hostname"; 
	$self->{ _distrosServices }->{ "httpd" } = undef;
	$self->{ _distrosServices }->{ "hwclock" } = "hwclock"; 
	$self->{ _distrosServices }->{ "hwclock-save" } = undef;
	$self->{ _distrosServices }->{ "inputattach" } = undef;
	$self->{ _distrosServices }->{ "ip6tables" } = undef;
	$self->{ _distrosServices }->{ "ipmi" } = undef;
	$self->{ _distrosServices }->{ "iptables" } = undef;
	$self->{ _distrosServices }->{ "irda" } = undef;
	$self->{ _distrosServices }->{ "irqbalance" } = undef;
	$self->{ _distrosServices }->{ "iscsi" } = undef;
	$self->{ _distrosServices }->{ "iscsid" } = undef;
	$self->{ _distrosServices }->{ "joystick" } = undef;
	$self->{ _distrosServices }->{ "kbd" } = undef;
	$self->{ _distrosServices }->{ "kerneloops" } = undef;
	$self->{ _distrosServices }->{ "keymaps" } = "keymaps"; 
	$self->{ _distrosServices }->{ "killprocs" } = "killprocs"; 
	$self->{ _distrosServices }->{ "kudzu" } = undef;
	$self->{ _distrosServices }->{ "lirc" } = undef;
	$self->{ _distrosServices }->{ "lircd" } = "lircd"; 
	$self->{ _distrosServices }->{ "livesys" } = undef;
	$self->{ _distrosServices }->{ "livesys-late" } = undef;
	$self->{ _distrosServices }->{ "lldpad" } = undef;
	$self->{ _distrosServices }->{ "lm-sensors" } = undef;
	$self->{ _distrosServices }->{ "local" } = "local"; 
	$self->{ _distrosServices }->{ "lvm2-monitor" } = undef;
	$self->{ _distrosServices }->{ "mcstrans" } = undef;
	$self->{ _distrosServices }->{ "mdadmd" } = undef;
	$self->{ _distrosServices }->{ "mdmonitor" } = undef;
	$self->{ _distrosServices }->{ "mdmpd" } = undef;
	$self->{ _distrosServices }->{ "messagebus" } = undef;
	$self->{ _distrosServices }->{ "microcode.ctl" } = undef;
	$self->{ _distrosServices }->{ "microcode_ctl" } = undef;
	$self->{ _distrosServices }->{ "module-init-tools" } = undef;
	$self->{ _distrosServices }->{ "modules" } = "modules"; 
	$self->{ _distrosServices }->{ "mount-ro" } = "mount-ro"; 
	$self->{ _distrosServices }->{ "mtab" } = "mtab"; 
	$self->{ _distrosServices }->{ "multipathd" } = undef;
	$self->{ _distrosServices }->{ "mysqld" } = undef;
	$self->{ _distrosServices }->{ "net.eth0" } = "net.eth0"; 
	$self->{ _distrosServices }->{ "net.lo" } = "net.lo"; 
	$self->{ _distrosServices }->{ "netconsole" } = undef;
	$self->{ _distrosServices }->{ "netfs" } = undef;
	$self->{ _distrosServices }->{ "netmount" } = "netmount"; 
	$self->{ _distrosServices }->{ "netplugd" } = undef;
	$self->{ _distrosServices }->{ "network" } = undef;
	$self->{ _distrosServices }->{ "network-interface" } = undef;
	$self->{ _distrosServices }->{ "network-interface-security" } = undef;
	$self->{ _distrosServices }->{ "NetworkManager" } = undef;
	$self->{ _distrosServices }->{ "network-remotefs" } = undef;
	$self->{ _distrosServices }->{ "nfs" } = "nfs"; 
	$self->{ _distrosServices }->{ "nfslock" } = undef;
	$self->{ _distrosServices }->{ "nfsserver" } = undef;
	$self->{ _distrosServices }->{ "nmb" } = undef;
	$self->{ _distrosServices }->{ "nmbd" } = undef;
	$self->{ _distrosServices }->{ "nscd" } = "nscd"; 
	$self->{ _distrosServices }->{ "ntp" } = undef;
	$self->{ _distrosServices }->{ "ntpd" } = "ntpd"; 
	$self->{ _distrosServices }->{ "ntpdate" } = undef;
	$self->{ _distrosServices }->{ "ocalmount" } = "ocalmount"; 
	$self->{ _distrosServices }->{ "oddjobd" } = undef;
	$self->{ _distrosServices }->{ "ondemand" } = undef;
	$self->{ _distrosServices }->{ "openvpn" } = "openvpn"; 
	$self->{ _distrosServices }->{ "pand" } = undef;
	$self->{ _distrosServices }->{ "pcmciautils" } = undef;
	$self->{ _distrosServices }->{ "pcscd" } = undef;
	$self->{ _distrosServices }->{ "plymouth" } = undef;
	$self->{ _distrosServices }->{ "plymouth-log" } = undef;
	$self->{ _distrosServices }->{ "plymouth-splash" } = undef;
	$self->{ _distrosServices }->{ "plymouth-stop" } = undef;
	$self->{ _distrosServices }->{ "pm-profiler" } = undef;
	$self->{ _distrosServices }->{ "portmap" } = undef;
	$self->{ _distrosServices }->{ "portreserve" } = undef;
	$self->{ _distrosServices }->{ "postfix" } = "postfix"; 
	$self->{ _distrosServices }->{ "powerd" } = undef;
	$self->{ _distrosServices }->{ "pppd-dns" } = undef;
	$self->{ _distrosServices }->{ "procfs" } = "procfs"; 
	$self->{ _distrosServices }->{ "procps" } = undef;
	$self->{ _distrosServices }->{ "psacct" } = undef;
	$self->{ _distrosServices }->{ "pulseaudio" } = undef;
	$self->{ _distrosServices }->{ "random" } = undef;
	$self->{ _distrosServices }->{ "raw" } = undef;
	$self->{ _distrosServices }->{ "rawdevices" } = undef;
	$self->{ _distrosServices }->{ "rdisc" } = undef;
	$self->{ _distrosServices }->{ "readahead_early" } = undef;
	$self->{ _distrosServices }->{ "readahead_later" } = undef;
	$self->{ _distrosServices }->{ "readahead-list" } = "readahead-list"; 
	$self->{ _distrosServices }->{ "readahead-list-early" } = "readahead-list-early"; 
	$self->{ _distrosServices }->{ "resolvconf" } = undef;
	$self->{ _distrosServices }->{ "restorecond" } = undef;
	$self->{ _distrosServices }->{ "rhnsd" } = undef;
	$self->{ _distrosServices }->{ "root" } = "root"; 
	$self->{ _distrosServices }->{ "rpc.idmapd" } = "rpc.idmapd"; 
	$self->{ _distrosServices }->{ "rpc.statd" } = "rpc.statd"; 
	$self->{ _distrosServices }->{ "rpcbind" } = "rpcbind"; 
	$self->{ _distrosServices }->{ "rpcgssd" } = undef;
	$self->{ _distrosServices }->{ "rpcidmapd" } = undef;
	$self->{ _distrosServices }->{ "rpcsvcgssd" } = undef;
	$self->{ _distrosServices }->{ "rpmconfigcheck" } = undef;
	$self->{ _distrosServices }->{ "rsync" } = undef;
	$self->{ _distrosServices }->{ "rsyncd" } = "rsyncd"; 
	$self->{ _distrosServices }->{ "rsyslog" } = "rsyslog"; 
	$self->{ _distrosServices }->{ "samba" } = "samba"; 
	$self->{ _distrosServices }->{ "saned" } = undef;
	$self->{ _distrosServices }->{ "saslauthd" } = undef;
	$self->{ _distrosServices }->{ "savecache" } = "savecache"; 
	$self->{ _distrosServices }->{ "sbl" } = undef;
	$self->{ _distrosServices }->{ "sendmail" } = undef;
	$self->{ _distrosServices }->{ "setserial" } = undef;
	$self->{ _distrosServices }->{ "skeleton.compat" } = undef;
	$self->{ _distrosServices }->{ "smartd" } = undef;
	$self->{ _distrosServices }->{ "smb" } = undef;
	$self->{ _distrosServices }->{ "smbd" } = undef;
	$self->{ _distrosServices }->{ "smolt" } = undef;
	$self->{ _distrosServices }->{ "smpppd" } = undef;
	$self->{ _distrosServices }->{ "speech-dispatcher" } = undef;
	$self->{ _distrosServices }->{ "splash" } = undef;
	$self->{ _distrosServices }->{ "splash_early" } = undef;
	$self->{ _distrosServices }->{ "sshd" } = "sshd"; 
	$self->{ _distrosServices }->{ "sssd" } = undef;
	$self->{ _distrosServices }->{ "stoppreload" } = undef;
	$self->{ _distrosServices }->{ "SuSEfirewall2_init" } = undef;
	$self->{ _distrosServices }->{ "SuSEfirewall2_setup" } = undef;
	$self->{ _distrosServices }->{ "svnserve" } = "svnserve"; 
	$self->{ _distrosServices }->{ "swap" } = "swap"; 
	$self->{ _distrosServices }->{ "sysctl" } = "sysctl"; 
	$self->{ _distrosServices }->{ "sysfs" } = "sysfs"; 
	$self->{ _distrosServices }->{ "syslog" } = undef;
	$self->{ _distrosServices }->{ "syslog-ng" } = "syslog-ng"; 
	$self->{ _distrosServices }->{ "tcsd" } = undef;
	$self->{ _distrosServices }->{ "udev" } = "udev"; 
	$self->{ _distrosServices }->{ "udev-finish" } = undef;
	$self->{ _distrosServices }->{ "udevmonitor" } = undef;
	$self->{ _distrosServices }->{ "udev-mount" } = "udev-mount"; 
	$self->{ _distrosServices }->{ "udev-post" } = undef;
	$self->{ _distrosServices }->{ "udev-postmount" } = "udev-postmount"; 
	$self->{ _distrosServices }->{ "udevtrigger" } = undef;
	$self->{ _distrosServices }->{ "ufw" } = undef;
	$self->{ _distrosServices }->{ "unattended-upgrades" } = undef;
	$self->{ _distrosServices }->{ "unscd" } = "unscd"; 
	$self->{ _distrosServices }->{ "urandom" } = "urandom"; 
	$self->{ _distrosServices }->{ "vboxadd" } = undef;
	$self->{ _distrosServices }->{ "vixie-cron" } = "vixie-cron"; 
	$self->{ _distrosServices }->{ "vmtoolsd" } = undef;
	$self->{ _distrosServices }->{ "vmware-tools" } = undef;
	$self->{ _distrosServices }->{ "vncserver" } = undef;
	$self->{ _distrosServices }->{ "wdaemon" } = undef;
	$self->{ _distrosServices }->{ "wpa_supplicant" } = undef;
	$self->{ _distrosServices }->{ "x11-common" } = undef;
	$self->{ _distrosServices }->{ "xdm" } = "xdm"; 
	$self->{ _distrosServices }->{ "xdm.orig" } = undef;
	$self->{ _distrosServices }->{ "xdm-setup" } = "xdm-setup"; 
	$self->{ _distrosServices }->{ "xfs" } = undef;
	$self->{ _distrosServices }->{ "xinetd" } = "xinetd"; 
	$self->{ _distrosServices }->{ "ypbind" } = "ypbind"; 
	$self->{ _distrosServices }->{ "yum-updatesd" } = undef;
}


1;

