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


package Redhat;

use strict;
use base 'Distribution';
use diagnostics -verbose;


sub new 
{
	my( $class ) = shift;

	my $self = Distribution->new( @_ );
		
	bless( $self , $class );
	
	$self->{ _serviceController } = "service";
	$self->{ _serviceModifier } = "chkconfig";
	$self->{ _packageManager } = "yum";
	
	$self->registerServiceNames();
	
	return $self;
}


sub registerServiceNames
{
	my( $self ) = @_;	
	
	$self->{ _distrosServices }->{ "aaeventd" } = undef;
	$self->{ _distrosServices }->{ "abrtd" } = "abrtd"; 
	$self->{ _distrosServices }->{ "acpid" } = "acpid"; 
	$self->{ _distrosServices }->{ "acpi-support" } = undef;
	$self->{ _distrosServices }->{ "after.local" } = undef;
	$self->{ _distrosServices }->{ "alsa-mixer-save" } = undef;
	$self->{ _distrosServices }->{ "alsasound" } = undef;
	$self->{ _distrosServices }->{ "anacron" } = undef;
	$self->{ _distrosServices }->{ "apparmor" } = undef;
	$self->{ _distrosServices }->{ "apport" } = undef;
	$self->{ _distrosServices }->{ "atd" } = "atd"; 
	$self->{ _distrosServices }->{ "auditd" } = "auditd"; 
	$self->{ _distrosServices }->{ "autofs" } = "autofs"; 
	$self->{ _distrosServices }->{ "autoyast" } = undef;
	$self->{ _distrosServices }->{ "avahi-daemon" } = "avahi-daemon"; 
	$self->{ _distrosServices }->{ "avahi-dnsconfd" } = "avahi-dnsconfd"; 
	$self->{ _distrosServices }->{ "before.local" } = undef;
	$self->{ _distrosServices }->{ "binfmt-support" } = undef;
	$self->{ _distrosServices }->{ "bluetooth" } = "bluetooth"; 
	$self->{ _distrosServices }->{ "bluez-coldplug" } = undef;
	$self->{ _distrosServices }->{ "bootmisc" } = undef;
	$self->{ _distrosServices }->{ "brld" } = undef;
	$self->{ _distrosServices }->{ "brltty" } = undef;
	$self->{ _distrosServices }->{ "cgconfig" } = "cgconfig"; 
	$self->{ _distrosServices }->{ "cgred" } = "cgred"; 
	$self->{ _distrosServices }->{ "cifs" } = undef;
	$self->{ _distrosServices }->{ "conman" } = undef;
	$self->{ _distrosServices }->{ "console-setup" } = undef;
	$self->{ _distrosServices }->{ "cpufreq" } = undef;
	$self->{ _distrosServices }->{ "cpuspeed" } = "cpuspeed"; 
	$self->{ _distrosServices }->{ "cron" } = undef;
	$self->{ _distrosServices }->{ "crond" } = "crond"; 
	$self->{ _distrosServices }->{ "cups" } = "cups"; 
	$self->{ _distrosServices }->{ "cupsd" } = undef;
	$self->{ _distrosServices }->{ "dbus" } = undef;
	$self->{ _distrosServices }->{ "devfs" } = undef;
	$self->{ _distrosServices }->{ "dhcpcd" } = undef;
	$self->{ _distrosServices }->{ "dmesg" } = undef;
	$self->{ _distrosServices }->{ "dnsmasq" } = "dnsmasq"; 
	$self->{ _distrosServices }->{ "dund" } = undef;
	$self->{ _distrosServices }->{ "dvb" } = undef;
	$self->{ _distrosServices }->{ "earlysyslog" } = undef;
	$self->{ _distrosServices }->{ "earlyxdm" } = undef;
	$self->{ _distrosServices }->{ "ermencoding" } = undef;
	$self->{ _distrosServices }->{ "failsafe-x" } = undef;
	$self->{ _distrosServices }->{ "fancontrol" } = undef;
	$self->{ _distrosServices }->{ "fbcondecor" } = undef;
	$self->{ _distrosServices }->{ "fbset" } = undef;
	$self->{ _distrosServices }->{ "fcoe" } = "fcoe"; 
	$self->{ _distrosServices }->{ "firstboot" } = "firstboot"; 
	$self->{ _distrosServices }->{ "fsck" } = undef;
	$self->{ _distrosServices }->{ "gdm" } = undef;
	$self->{ _distrosServices }->{ "gpm" } = "gpm"; 
	$self->{ _distrosServices }->{ "gpsd" } = "gpsd"; 
	$self->{ _distrosServices }->{ "grub-common" } = undef;
	$self->{ _distrosServices }->{ "haldaemon" } = "haldaemon"; 
	$self->{ _distrosServices }->{ "hidd" } = undef;
	$self->{ _distrosServices }->{ "hostname" } = undef;
	$self->{ _distrosServices }->{ "httpd" } = "httpd"; 
	$self->{ _distrosServices }->{ "hwclock" } = undef;
	$self->{ _distrosServices }->{ "hwclock-save" } = undef;
	$self->{ _distrosServices }->{ "inputattach" } = undef;
	$self->{ _distrosServices }->{ "ip6tables" } = "ip6tables"; 
	$self->{ _distrosServices }->{ "ipmi" } = undef;
	$self->{ _distrosServices }->{ "iptables" } = "iptables"; 
	$self->{ _distrosServices }->{ "irda" } = "irda"; 
	$self->{ _distrosServices }->{ "irqbalance" } = "irqbalance"; 
	$self->{ _distrosServices }->{ "iscsi" } = "iscsi"; 
	$self->{ _distrosServices }->{ "iscsid" } = "iscsid"; 
	$self->{ _distrosServices }->{ "joystick" } = undef;
	$self->{ _distrosServices }->{ "kbd" } = undef;
	$self->{ _distrosServices }->{ "kerneloops" } = undef;
	$self->{ _distrosServices }->{ "keymaps" } = undef;
	$self->{ _distrosServices }->{ "killprocs" } = undef;
	$self->{ _distrosServices }->{ "kudzu" } = undef;
	$self->{ _distrosServices }->{ "lirc" } = "lirc"; 
	$self->{ _distrosServices }->{ "lircd" } = undef;
	$self->{ _distrosServices }->{ "livesys" } = "livesys"; 
	$self->{ _distrosServices }->{ "livesys-late" } = "livesys-late"; 
	$self->{ _distrosServices }->{ "lldpad" } = "lldpad"; 
	$self->{ _distrosServices }->{ "lm-sensors" } = undef;
	$self->{ _distrosServices }->{ "local" } = undef;
	$self->{ _distrosServices }->{ "lvm2-monitor" } = "lvm2-monitor"; 
	$self->{ _distrosServices }->{ "mcstrans" } = undef;
	$self->{ _distrosServices }->{ "mdadmd" } = undef;
	$self->{ _distrosServices }->{ "mdmonitor" } = "mdmonitor"; 
	$self->{ _distrosServices }->{ "mdmpd" } = undef;
	$self->{ _distrosServices }->{ "messagebus" } = "messagebus"; 
	$self->{ _distrosServices }->{ "microcode.ctl" } = undef;
	$self->{ _distrosServices }->{ "microcode_ctl" } = undef;
	$self->{ _distrosServices }->{ "module-init-tools" } = undef;
	$self->{ _distrosServices }->{ "modules" } = undef;
	$self->{ _distrosServices }->{ "mount-ro" } = undef;
	$self->{ _distrosServices }->{ "mtab" } = undef;
	$self->{ _distrosServices }->{ "multipathd" } = "multipathd"; 
	$self->{ _distrosServices }->{ "mysqld" } = "mysqld"; 
	$self->{ _distrosServices }->{ "net.eth0" } = undef;
	$self->{ _distrosServices }->{ "net.lo" } = undef;
	$self->{ _distrosServices }->{ "netconsole" } = "netconsole"; 
	$self->{ _distrosServices }->{ "netfs" } = "netfs"; 
	$self->{ _distrosServices }->{ "netmount" } = undef;
	$self->{ _distrosServices }->{ "netplugd" } = undef;
	$self->{ _distrosServices }->{ "network" } = "network"; 
	$self->{ _distrosServices }->{ "network-interface" } = undef;
	$self->{ _distrosServices }->{ "network-interface-security" } = undef;
	$self->{ _distrosServices }->{ "NetworkManager" } = "NetworkManager"; 
	$self->{ _distrosServices }->{ "network-remotefs" } = undef;
	$self->{ _distrosServices }->{ "nfs" } = "nfs"; 
	$self->{ _distrosServices }->{ "nfslock" } = "nfslock"; 
	$self->{ _distrosServices }->{ "nfsserver" } = undef;
	$self->{ _distrosServices }->{ "nmb" } = undef;
	$self->{ _distrosServices }->{ "nmbd" } = undef;
	$self->{ _distrosServices }->{ "nscd" } = "nscd"; 
	$self->{ _distrosServices }->{ "ntp" } = undef;
	$self->{ _distrosServices }->{ "ntpd" } = "ntpd"; 
	$self->{ _distrosServices }->{ "ntpdate" } = "ntpdate"; 
	$self->{ _distrosServices }->{ "ocalmount" } = undef;
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
	$self->{ _distrosServices }->{ "portreserve" } = "portreserve"; 
	$self->{ _distrosServices }->{ "postfix" } = undef;
	$self->{ _distrosServices }->{ "powerd" } = undef;
	$self->{ _distrosServices }->{ "pppd-dns" } = undef;
	$self->{ _distrosServices }->{ "procfs" } = undef;
	$self->{ _distrosServices }->{ "procps" } = undef;
	$self->{ _distrosServices }->{ "psacct" } = "psacct"; 
	$self->{ _distrosServices }->{ "pulseaudio" } = undef;
	$self->{ _distrosServices }->{ "random" } = undef;
	$self->{ _distrosServices }->{ "raw" } = undef;
	$self->{ _distrosServices }->{ "rawdevices" } = undef;
	$self->{ _distrosServices }->{ "rdisc" } = "rdisc"; 
	$self->{ _distrosServices }->{ "readahead_early" } = undef;
	$self->{ _distrosServices }->{ "readahead_later" } = undef;
	$self->{ _distrosServices }->{ "readahead-list" } = undef;
	$self->{ _distrosServices }->{ "readahead-list-early" } = undef;
	$self->{ _distrosServices }->{ "resolvconf" } = undef;
	$self->{ _distrosServices }->{ "restorecond" } = "restorecond"; 
	$self->{ _distrosServices }->{ "rhnsd" } = undef;
	$self->{ _distrosServices }->{ "root" } = undef;
	$self->{ _distrosServices }->{ "rpc.idmapd" } = undef;
	$self->{ _distrosServices }->{ "rpc.statd" } = undef;
	$self->{ _distrosServices }->{ "rpcbind" } = "rpcbind"; 
	$self->{ _distrosServices }->{ "rpcgssd" } = "rpcgssd"; 
	$self->{ _distrosServices }->{ "rpcidmapd" } = "rpcidmapd"; 
	$self->{ _distrosServices }->{ "rpcsvcgssd" } = "rpcsvcgssd"; 
	$self->{ _distrosServices }->{ "rpmconfigcheck" } = undef;
	$self->{ _distrosServices }->{ "rsync" } = undef;
	$self->{ _distrosServices }->{ "rsyncd" } = undef;
	$self->{ _distrosServices }->{ "rsyslog" } = "rsyslog"; 
	$self->{ _distrosServices }->{ "samba" } = undef;
	$self->{ _distrosServices }->{ "saned" } = undef;
	$self->{ _distrosServices }->{ "saslauthd" } = "saslauthd"; 
	$self->{ _distrosServices }->{ "savecache" } = undef;
	$self->{ _distrosServices }->{ "sbl" } = undef;
	$self->{ _distrosServices }->{ "sendmail" } = "sendmail"; 
	$self->{ _distrosServices }->{ "setserial" } = undef;
	$self->{ _distrosServices }->{ "skeleton.compat" } = undef;
	$self->{ _distrosServices }->{ "smartd" } = undef;
	$self->{ _distrosServices }->{ "smb" } = undef;
	$self->{ _distrosServices }->{ "smbd" } = undef;
	$self->{ _distrosServices }->{ "smolt" } = "smolt"; 
	$self->{ _distrosServices }->{ "smpppd" } = undef;
	$self->{ _distrosServices }->{ "speech-dispatcher" } = undef;
	$self->{ _distrosServices }->{ "splash" } = undef;
	$self->{ _distrosServices }->{ "splash_early" } = undef;
	$self->{ _distrosServices }->{ "sshd" } = "sshd"; 
	$self->{ _distrosServices }->{ "sssd" } = "sssd"; 
	$self->{ _distrosServices }->{ "stoppreload" } = undef;
	$self->{ _distrosServices }->{ "SuSEfirewall2_init" } = undef;
	$self->{ _distrosServices }->{ "SuSEfirewall2_setup" } = undef;
	$self->{ _distrosServices }->{ "svnserve" } = "svnserve"; 
	$self->{ _distrosServices }->{ "swap" } = undef;
	$self->{ _distrosServices }->{ "sysctl" } = undef;
	$self->{ _distrosServices }->{ "sysfs" } = undef;
	$self->{ _distrosServices }->{ "syslog" } = undef;
	$self->{ _distrosServices }->{ "syslog-ng" } = undef;
	$self->{ _distrosServices }->{ "tcsd" } = undef;
	$self->{ _distrosServices }->{ "udev" } = undef;
	$self->{ _distrosServices }->{ "udev-finish" } = undef;
	$self->{ _distrosServices }->{ "udevmonitor" } = undef;
	$self->{ _distrosServices }->{ "udev-mount" } = undef;
	$self->{ _distrosServices }->{ "udev-post" } = "udev-post"; 
	$self->{ _distrosServices }->{ "udev-postmount" } = undef;
	$self->{ _distrosServices }->{ "udevtrigger" } = undef;
	$self->{ _distrosServices }->{ "ufw" } = undef;
	$self->{ _distrosServices }->{ "unattended-upgrades" } = undef;
	$self->{ _distrosServices }->{ "unscd" } = undef;
	$self->{ _distrosServices }->{ "urandom" } = undef;
	$self->{ _distrosServices }->{ "vboxadd" } = undef;
	$self->{ _distrosServices }->{ "vixie-cron" } = undef;
	$self->{ _distrosServices }->{ "vmtoolsd" } = undef;
	$self->{ _distrosServices }->{ "vmware-tools" } = undef;
	$self->{ _distrosServices }->{ "vncserver" } = undef;
	$self->{ _distrosServices }->{ "wdaemon" } = undef;
	$self->{ _distrosServices }->{ "wpa_supplicant" } = "wpa_supplicant"; 
	$self->{ _distrosServices }->{ "x11-common" } = undef;
	$self->{ _distrosServices }->{ "xdm" } = undef;
	$self->{ _distrosServices }->{ "xdm.orig" } = undef;
	$self->{ _distrosServices }->{ "xdm-setup" } = undef;
	$self->{ _distrosServices }->{ "xfs" } = "xfs"; 
	$self->{ _distrosServices }->{ "xinetd" } = "xinetd"; 
	$self->{ _distrosServices }->{ "ypbind" } = "ypbind"; 
	$self->{ _distrosServices }->{ "yum-updatesd" } = undef;
}


1;
