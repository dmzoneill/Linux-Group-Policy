# Linux Group Policy

A final year project that brings Windows-style Group Policy management to
Linux and BSD machines. A central server stores policies and agents on
each machine apply them.

## Components

- `perl`: the agent and server daemons.
  - `opt/lgp/client`: the client daemon (`cmain.pl`) with modules
    for GConf settings, software installation, package management and
    service control, plus per-distribution support for Debian, Ubuntu,
    Mint, Fedora, Red Hat, CentOS, SUSE, Gentoo, FreeBSD, NetBSD and
    Darwin.
  - `opt/lgp/server`: the server daemon (`smain.pl`) and its module
    handler.
  - `opt/lgp/common`: shared syslog, PID file and MySQL helpers.
  - `etc`: an init script and sysconfig file for the `lgpd` service.
- `csharp`: the Windows management console, `Linux Group Policy.sln`.
  It is built from modules for an organisation unit explorer, a policy
  editor, a module editor, server control and statistics, with database
  back ends for MySQL, PostgreSQL, Microsoft SQL Server and Oracle.
- `report`: the LaTeX project report and the presentation.

## Tech

- Perl for the daemons
- C# and WPF with Prism on .NET Framework 4.0 for the console
- MySQL and other relational databases
- LaTeX for the report

## Building

Open `csharp/Linux Group Policy/Linux Group Policy.sln` in Visual Studio
to build the console. The Perl files are laid out to be copied to `/etc`
and `/opt` on the target machine; `perl/install.sh` is still a
placeholder.

## License

Licensed under the Apache License 2.0. See [LICENSE](LICENSE).
