#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using LGP.Components.Factory.Interfaces.Network;
using Newtonsoft.Json;

#endregion

namespace LGP.Components.Factory.Internal.ServerControl
{
    internal class BroadcastListener : IBroadcastListener
    {
        private const int UdpLocalPort = 50006;
        private const int UdpRemotePort = 50005;
        private static IBroadcastListener _instance;
        private readonly List< IServerInfo > _servers;

        private IPEndPoint _receiveUdpGroup;

        private UdpClient _receivingUdpClient;
        private UdpClient _sendingUdpClient;
        private IPEndPoint _targetUdpGroup;
        private Thread _udplistenerThread;


        public BroadcastListener()
        {
            try
            {
                this._servers = new List< IServerInfo >();
                this._udplistenerThread = new Thread( this.ListenForAnnounces )
                {
                    IsBackground = true
                };
                this._udplistenerThread.Start();
                Framework.ShutDown += this.Terminate;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private void Terminate( object sender , CancelEventArgs args )
        {
            try
            {
                this.StopListener();
            }
            catch( Exception )
            {
            }
        }

        private void ListenForAnnounces()
        {
            try
            {
                this._sendingUdpClient = new UdpClient( UdpLocalPort , AddressFamily.InterNetwork );
                this._targetUdpGroup = new IPEndPoint( IPAddress.Broadcast , UdpRemotePort );
                this._sendingUdpClient.Connect( this._targetUdpGroup );

                var fdfd = Encoding.UTF8.GetBytes( "requestannounce" );

                this._sendingUdpClient.Send( fdfd , fdfd.Length );
                this._sendingUdpClient.Close();

                this._receiveUdpGroup = new IPEndPoint( IPAddress.Any , 0 );
                this._receivingUdpClient = new UdpClient( UdpRemotePort );

                while( true )
                {
                    var received = this._receivingUdpClient.Receive( ref this._receiveUdpGroup );

                    var returnData = Encoding.ASCII.GetString( received );
                    var values = JsonConvert.DeserializeObject< Dictionary< string , int > >( returnData );

                    var found = false;
                    IServerInfo server = null;

                    foreach( var t in this._servers )
                    {
                        if( t.ServerAddress.CompareTo( this._receiveUdpGroup.Address.ToString() ) == 0 )
                        {
                            server = t;
                            found = true;
                        }
                    }

                    if( found == false )
                    {
                        var t = new ServerInfo
                        {
                            ServerAddress = this._receiveUdpGroup.Address.ToString() , LastSeen = DateTime.Now
                        };
                        t.AddIncoming( values[ "incoming" ] );
                        t.AddOutgoing( values[ "outgoing" ] );
                        t.AddRx( values[ "rx" ] );
                        t.AddTx( values[ "tx" ] );
                        t.AddOutgoing( values[ "outgoing" ] );
                        t.AddCpu( values[ "cpuusage" ] );
                        this._servers.Add( t );
                    }
                    else
                    {
                        server.AddIncoming( values[ "incoming" ] );
                        server.AddOutgoing( values[ "outgoing" ] );
                        server.AddRx( values[ "rx" ] );
                        server.AddTx( values[ "tx" ] );
                        server.AddCpu( values[ "cpuusage" ] );
                    }
                }
            }

            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Gets the list of servers detected
        /// </summary>
        /// <returns>List Server</returns>
        public List< IServerInfo > GetServers()
        {
            return this._servers;
        }


        /// <summary>
        ///   Retarts the listener
        /// </summary>
        public void RetartListener()
        {
            if( this._udplistenerThread.IsAlive )
            {
                try
                {
                    this._udplistenerThread.Abort();
                    this._udplistenerThread = new Thread( this.ListenForAnnounces )
                    {
                        IsBackground = true
                    };
                    this._udplistenerThread.Start();
                }
                catch( Exception error )
                {
                    Framework.EventBus.Publish( error );
                }
            }
        }


        /// <summary>
        ///   Stops the listener
        /// </summary>
        public void StopListener()
        {
            if( this._udplistenerThread.IsAlive )
            {
                try
                {
                    this._udplistenerThread.Abort();
                }
                catch( Exception error )
                {
                    Framework.EventBus.Publish( error );
                }
            }
        }


        /// <summary>
        ///   Gets the instance of the server handler
        /// </summary>
        /// <returns></returns>
        public static IBroadcastListener GetInstance()
        {
            return _instance ?? ( _instance = new BroadcastListener() );
        }
    }
}