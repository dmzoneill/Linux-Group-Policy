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


package Suse;

use strict;
use base 'Distribution';

sub new 
{
	my( $class ) = shift;

	my $self = Distribution->new( @_ );
	
	$self->{ _serviceController } = "service";
	$self->{ _serviceModifier } = "chkconfig";
	$self->{ _packageManager } = "zypper";
	
	bless( $self , $class );
	
	$self->registerServiceNames();
	
	return $self;
}


sub registerServiceNames
{
	my( $self ) = @_;
	
	$self->{ _distrosServices }->{ "aaeventd" } = "aaeventd"; 
	$self->{ _distrosServices }->{ "abrtd" } = undef;
	$self->{ _distrosServices }->{ "acpid" } = "acpid";
	$self->{ _distrosServices }->{ "acpi-support" } = undef;
	$self->{ _distrosServices }->{ "after.local" } = "after.local";
	$self->{ _distrosServices }->{ "alsa-mixer-save" } = undef;
	$self->{ _distrosServices }->{ "alsasound" } = "alsasound"; 
	$self->{ _distrosServices }->{ "anacron" } = "cron";
	$self->{ _distrosServices }->{ "apparmor" } = undef;
	$self->{ _distrosServices }->{ "apport" } = "apport"; 
	$self->{ _distrosServices }->{ "atd" } = "atd"; 
	$self->{ _distrosServices }->{ "auditd" } = "auditd"; 
	$self->{ _distrosServices }->{ "autofs" } = "autofs";
	$self->{ _distrosServices }->{ "autoyast" } = "autoyast"; 
	$self->{ _distrosServices }->{ "avahi-daemon" } = "avahi-daemon";
	$self->{ _distrosServices }->{ "avahi-dnsconfd" } = "avahi-dnsconfd";
	$self->{ _distrosServices }->{ "before.local" } = "before.local";
	$self->{ _distrosServices }->{ "binfmt-support" } = undef; 
	$self->{ _distrosServices }->{ "bluetooth" } = "bluez-coldplug";
	$self->{ _distrosServices }->{ "bluez-coldplug" } = "bluez-coldplug"; 
	$self->{ _distrosServices }->{ "bootmisc" } = undef;
	$self->{ _distrosServices }->{ "brld" } = "brld";
	$self->{ _distrosServices }->{ "brltty" } = undef;
	$self->{ _distrosServices }->{ "cgconfig" } = undef; 
	$self->{ _distrosServices }->{ "cgred" } = "cgred";
	$self->{ _distrosServices }->{ "cifs" } = "cifs"; 
	$self->{ _distrosServices }->{ "conman" } = undef;
	$self->{ _distrosServices }->{ "console-setup" } = undef;
	$self->{ _distrosServices }->{ "cpufreq" } = "cpufreq"; 
	$self->{ _distrosServices }->{ "cpuspeed" } = "cpufreq"; 
	$self->{ _distrosServices }->{ "cron" } = "cron"; 
	$self->{ _distrosServices }->{ "crond" } = "cron"; 
	$self->{ _distrosServices }->{ "cups" } = "cups"; 
	$self->{ _distrosServices }->{ "cupsd" } = "cups"; 
	$self->{ _distrosServices }->{ "dbus" } = "dbus"; 
	$self->{ _distrosServices }->{ "devfs" } = undef;
	$self->{ _distrosServices }->{ "dhcpcd" } = "dhcpd"; 
	$self->{ _distrosServices }->{ "dmesg" } = undef;
	$self->{ _distrosServices }->{ "dnsmasq" } = "dnsmasq"; 
	$self->{ _distrosServices }->{ "dund" } = "bluez-compat"; 
	$self->{ _distrosServices }->{ "dvb" } = "dvb"; 
	$self->{ _distrosServices }->{ "earlysyslog" } = "earlysyslog"; 
	$self->{ _distrosServices }->{ "earlyxdm" } = "earlyxdm"; 
	$self->{ _distrosServices }->{ "ermencoding" } = undef;
	$self->{ _distrosServices }->{ "failsafe-x" } = undef;
	$self->{ _distrosServices }->{ "fancontrol" } = undef;
	$self->{ _distrosServices }->{ "fbcondecor" } = undef;
	$self->{ _distrosServices }->{ "fbset" } = "fbset"; 
	$self->{ _distrosServices }->{ "fcoe" } = undef;
	$self->{ _distrosServices }->{ "firstboot" } = undef;
	$self->{ _distrosServices }->{ "fsck" } = undef;
	$self->{ _distrosServices }->{ "gdm" } = "xdm"; 
	$self->{ _distrosServices }->{ "gpm" } = "gpm"; 
	$self->{ _distrosServices }->{ "gpsd" } = undef;
	$self->{ _distrosServices }->{ "grub-common" } = undef;
	$self->{ _distrosServices }->{ "haldaemon" } = undef;
	$self->{ _distrosServices }->{ "hidd" } = undef;
	$self->{ _distrosServices }->{ "hostname" } = undef;
	$self->{ _distrosServices }->{ "httpd" } = undef;
	$self->{ _distrosServices }->{ "hwclock" } = undef;
	$self->{ _distrosServices }->{ "hwclock-save" } = undef;
	$self->{ _distrosServices }->{ "inputattach" } = "inputattach"; 
	$self->{ _distrosServices }->{ "ip6tables" } = undef;
	$self->{ _distrosServices }->{ "ipmi" } = undef;
	$self->{ _distrosServices }->{ "iptables" } = undef;
	$self->{ _distrosServices }->{ "irda" } = "irda"; 
	$self->{ _distrosServices }->{ "irqbalance" } = undef;
	$self->{ _distrosServices }->{ "iscsi" } = undef;
	$self->{ _distrosServices }->{ "iscsid" } = undef;
	$self->{ _distrosServices }->{ "joystick" } = "joystick"; 
	$self->{ _distrosServices }->{ "kbd" } = "kbd"; 
	$self->{ _distrosServices }->{ "kerneloops" } = undef;
	$self->{ _distrosServices }->{ "keymaps" } = undef;
	$self->{ _distrosServices }->{ "killprocs" } = undef;
	$self->{ _distrosServices }->{ "kudzu" } = undef;
	$self->{ _distrosServices }->{ "lirc" } = "lirc"; 
	$self->{ _distrosServices }->{ "lircd" } = undef;
	$self->{ _distrosServices }->{ "livesys" } = undef;
	$self->{ _distrosServices }->{ "livesys-late" } = undef;
	$self->{ _distrosServices }->{ "lldpad" } = undef;
	$self->{ _distrosServices }->{ "lm-sensors" } = undef;
	$self->{ _distrosServices }->{ "local" } = undef;
	$self->{ _distrosServices }->{ "lvm2-monitor" } = undef;
	$self->{ _distrosServices }->{ "mcstrans" } = undef;
	$self->{ _distrosServices }->{ "mdadmd" } = "mdadmd"; 
	$self->{ _distrosServices }->{ "mdmonitor" } = undef;
	$self->{ _distrosServices }->{ "mdmpd" } = undef;
	$self->{ _distrosServices }->{ "messagebus" } = undef;
	$self->{ _distrosServices }->{ "microcode.ctl" } = "microcode.ctl"; 
	$self->{ _distrosServices }->{ "microcode_ctl" } = undef;
	$self->{ _distrosServices }->{ "module-init-tools" } = undef;
	$self->{ _distrosServices }->{ "modules" } = undef;
	$self->{ _distrosServices }->{ "mount-ro" } = undef;
	$self->{ _distrosServices }->{ "mtab" } = undef;
	$self->{ _distrosServices }->{ "multipathd" } = "multipathd"; 
	$self->{ _distrosServices }->{ "mysqld" } = undef;
	$self->{ _distrosServices }->{ "net.eth0" } = undef;
	$self->{ _distrosServices }->{ "net.lo" } = undef;
	$self->{ _distrosServices }->{ "netconsole" } = undef;
	$self->{ _distrosServices }->{ "netfs" } = undef;
	$self->{ _distrosServices }->{ "netmount" } = undef;
	$self->{ _distrosServices }->{ "netplugd" } = undef;
	$self->{ _distrosServices }->{ "network" } = "network"; 
	$self->{ _distrosServices }->{ "network-interface" } = undef;
	$self->{ _distrosServices }->{ "network-interface-security" } = undef;
	$self->{ _distrosServices }->{ "NetworkManager" } = undef;
	$self->{ _distrosServices }->{ "network-remotefs" } = "network-remotefs"; 
	$self->{ _distrosServices }->{ "nfs" } = "nfs"; 
	$self->{ _distrosServices }->{ "nfslock" } = undef;
	$self->{ _distrosServices }->{ "nfsserver" } = "nfsserver";
	$self->{ _distrosServices }->{ "nmb" } = undef;
	$self->{ _distrosServices }->{ "nmbd" } = undef;
	$self->{ _distrosServices }->{ "nscd" } = "nscd"; 
	$self->{ _distrosServices }->{ "ntp" } = "ntp"; 
	$self->{ _distrosServices }->{ "ntpd" } = undef;
	$self->{ _distrosServices }->{ "ntpdate" } = undef;
	$self->{ _distrosServices }->{ "ocalmount" } = undef;
	$self->{ _distrosServices }->{ "oddjobd" } = undef;
	$self->{ _distrosServices }->{ "ondemand" } = undef;
	$self->{ _distrosServices }->{ "openvpn" } = "openvpn";  
	$self->{ _distrosServices }->{ "pand" } = undef;
	$self->{ _distrosServices }->{ "pcmciautils" } = undef;
	$self->{ _distrosServices }->{ "pcscd" } = "pcscd"; 
	$self->{ _distrosServices }->{ "plymouth" } = undef;
	$self->{ _distrosServices }->{ "plymouth-log" } = undef;
	$self->{ _distrosServices }->{ "plymouth-splash" } = undef;
	$self->{ _distrosServices }->{ "plymouth-stop" } = undef;
	$self->{ _distrosServices }->{ "pm-profiler" } = "pm-profiler";  
	$self->{ _distrosServices }->{ "portmap" } = undef;
	$self->{ _distrosServices }->{ "portreserve" } = undef;
	$self->{ _distrosServices }->{ "postfix" } = "postfix"; 
	$self->{ _distrosServices }->{ "powerd" } = undef;
	$self->{ _distrosServices }->{ "pppd-dns" } = undef;
	$self->{ _distrosServices }->{ "procfs" } = undef;
	$self->{ _distrosServices }->{ "procps" } = undef;
	$self->{ _distrosServices }->{ "psacct" } = undef;
	$self->{ _distrosServices }->{ "pulseaudio" } = undef;
	$self->{ _distrosServices }->{ "random" } = "random"; 
	$self->{ _distrosServices }->{ "raw" } = "raw";
	$self->{ _distrosServices }->{ "rawdevices" } = undef;
	$self->{ _distrosServices }->{ "rdisc" } = undef;
	$self->{ _distrosServices }->{ "readahead_early" } = undef;
	$self->{ _distrosServices }->{ "readahead_later" } = undef;
	$self->{ _distrosServices }->{ "readahead-list" } = undef;
	$self->{ _distrosServices }->{ "readahead-list-early" } = undef;
	$self->{ _distrosServices }->{ "resolvconf" } = undef;
	$self->{ _distrosServices }->{ "restorecond" } = undef;
	$self->{ _distrosServices }->{ "rhnsd" } = undef;
	$self->{ _distrosServices }->{ "root" } = undef;
	$self->{ _distrosServices }->{ "rpc.idmapd" } = undef;
	$self->{ _distrosServices }->{ "rpc.statd" } = undef;
	$self->{ _distrosServices }->{ "rpcbind" } = "rpcbind"; 
	$self->{ _distrosServices }->{ "rpcgssd" } = undef;
	$self->{ _distrosServices }->{ "rpcidmapd" } = undef;
	$self->{ _distrosServices }->{ "rpcsvcgssd" } = undef;
	$self->{ _distrosServices }->{ "rpmconfigcheck" } = "rpmconfigcheck"; 
	$self->{ _distrosServices }->{ "rsync" } = "rsyncd";
	$self->{ _distrosServices }->{ "rsyncd" } = "rsyncd"; 
	$self->{ _distrosServices }->{ "rsyslog" } = "syslog"; 
	$self->{ _distrosServices }->{ "samba" } = "smb";
	$self->{ _distrosServices }->{ "saned" } = undef;
	$self->{ _distrosServices }->{ "saslauthd" } = undef;
	$self->{ _distrosServices }->{ "savecache" } = undef;
	$self->{ _distrosServices }->{ "sbl" } = "sbl"; 
	$self->{ _distrosServices }->{ "sendmail" } = "sendmail";
	$self->{ _distrosServices }->{ "setserial" } = undef;
	$self->{ _distrosServices }->{ "skeleton.compat" } = "skeleton.compat"; 
	$self->{ _distrosServices }->{ "smartd" } = "smartd"; 
	$self->{ _distrosServices }->{ "smb" } = "smb"; 
	$self->{ _distrosServices }->{ "smbd" } = "smb"; 
	$self->{ _distrosServices }->{ "smolt" } = "smolt"; 
	$self->{ _distrosServices }->{ "smpppd" } = "smpppd";
	$self->{ _distrosServices }->{ "speech-dispatcher" } =  undef;
	$self->{ _distrosServices }->{ "splash" } = "splash";
	$self->{ _distrosServices }->{ "splash_early" } = "splash_early";
	$self->{ _distrosServices }->{ "sshd" } = "sshd"; 
	$self->{ _distrosServices }->{ "sssd" } = "sssd"; 
	$self->{ _distrosServices }->{ "stoppreload" } = "stoppreload"; 
	$self->{ _distrosServices }->{ "svnserve" } = "svnserve"; 
	$self->{ _distrosServices }->{ "swap" } = undef;
	$self->{ _distrosServices }->{ "sysctl" } = undef;
	$self->{ _distrosServices }->{ "sysfs" } = undef;
	$self->{ _distrosServices }->{ "syslog" } = "syslog";
	$self->{ _distrosServices }->{ "syslog-ng" } = "syslog"; 
	$self->{ _distrosServices }->{ "tcsd" } = undef;
	$self->{ _distrosServices }->{ "udev" } = undef;
	$self->{ _distrosServices }->{ "udev-finish" } = undef;
	$self->{ _distrosServices }->{ "udevmonitor" } = undef;
	$self->{ _distrosServices }->{ "udev-mount" } = undef;
	$self->{ _distrosServices }->{ "udev-post" } = undef;
	$self->{ _distrosServices }->{ "udev-postmount" } = undef;
	$self->{ _distrosServices }->{ "udevtrigger" } = undef;
	$self->{ _distrosServices }->{ "ufw" } = "SuSEfirewall2_init";  
	$self->{ _distrosServices }->{ "unattended-upgrades" } = undef;
	$self->{ _distrosServices }->{ "unscd" } = "unscd"; 
	$self->{ _distrosServices }->{ "urandom" } = undef;
	$self->{ _distrosServices }->{ "vboxadd" } = "vboxadd"; 
	$self->{ _distrosServices }->{ "vixie-cron" } = "cron"; 
	$self->{ _distrosServices }->{ "vmtoolsd" } = "vmtoolsd";
	$self->{ _distrosServices }->{ "vmware-tools" } = "vmtoolsd";
	$self->{ _distrosServices }->{ "vncserver" } =  undef;
	$self->{ _distrosServices }->{ "wdaemon" } =  undef;
	$self->{ _distrosServices }->{ "wpa_supplicant" } = undef;
	$self->{ _distrosServices }->{ "x11-common" } = undef;
	$self->{ _distrosServices }->{ "xdm" } = "xdm";
	$self->{ _distrosServices }->{ "xdm.orig" } = "xdm.orig";
	$self->{ _distrosServices }->{ "xdm-setup" } = undef;
	$self->{ _distrosServices }->{ "xfs" } = "xfs"; 
	$self->{ _distrosServices }->{ "xinetd" } = "xinetd"; 
	$self->{ _distrosServices }->{ "ypbind" } = "ypbind"; 
	$self->{ _distrosServices }->{ "yum-updatesd" } = undef; 
}

1;

