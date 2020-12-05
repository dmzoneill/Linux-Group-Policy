#region

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using LGP.Components.Factory.Interfaces.Network;

#endregion

namespace LGP.Components.Factory.Internal.ServerControl
{

    #region Delegates

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public delegate void DataReceived( string data );

    #endregion

    /// <summary>
    /// </summary>
    internal class ServerController : IServerController
    {
        private readonly int _clientPort;
        private readonly string _serverAddr;
        private readonly Thread _tcpListenerThread;

        private bool _connected;

        private TcpListener _receivingClient;
        private TcpClient _sendingClient;


        /// <summary>
        ///   Constructor
        /// </summary>
        /// <param name = "serverIp"></param>
        /// <param name = "clientId"></param>
        public ServerController( string serverIp , int clientId )
        {
            try
            {
                this._clientPort = clientId;

                this._serverAddr = serverIp;
                this._tcpListenerThread = new Thread( this.Listener )
                {
                    IsBackground = true
                };
                this._tcpListenerThread.Start();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #region IServerController Members

        /// <summary>
        /// 
        /// </summary>
        public event DataReceived OnDataReceived;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            return this._connected;
        }


        /// <summary>
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            try
            {
                this._sendingClient = new TcpClient( this._serverAddr , 50000 );
                this._connected = true;
                return this._connected;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                this._connected = false;
                return this._connected;
            }
        }


        /// <summary>
        /// </summary>
        /// <returns></returns>
        public bool Disconnect()
        {
            try
            {
                this._sendingClient.Close();
                this._connected = false;
                return !this._connected;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                this._connected = false;
                return this._connected;
            }
        }


        /// <summary>
        /// </summary>
        /// <param name = "message"></param>
        /// <returns></returns>
        public bool SendMesssage( List< KeyValuePair< string , string > > message )
        {
            try
            {
                var msg = "{";
                msg += "\"peerPort\" : " + "\"" + ( this._clientPort + 1 ) + "\",";

                for( var h = 0; h < message.Count - 1; h++ )
                {
                    msg += "\"" + message[ h ].Key + "\" : " + "\"" + message[ h ].Value + "\",";
                }
                msg += "\"" + message[ message.Count - 1 ].Key + "\" : " + "\"" + message[ message.Count - 1 ].Value + "\"";
                msg += "}";

                var data = Encoding.UTF8.GetBytes( Framework.Utils.Base64Encode( msg ) );
                var stream = this._sendingClient.GetStream();
                stream.Write( data , 0 , data.Length );
                stream.Flush();

                return true;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void SendPolicyUpdated()
        {
            try
            {
                var msg = "{";
                msg += "\"peerPort\" : " + "\"" + ( this._clientPort + 1 ) + "\",";
                msg += "\"admin\" : " + "\"" + ( this._clientPort + 1 ) + "\",";
                msg += "\"request\" : " + "\"updatepolicies\"";
                msg += "}";

                var data = Encoding.UTF8.GetBytes( Framework.Utils.Base64Encode( msg ) );
                var stream = this._sendingClient.GetStream();
                stream.Write( data , 0 , data.Length );
                stream.Flush();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void SendPushPolicies()
        {
            try
            {
                var msg = "{";
                msg += "\"peerPort\" : " + "\"" + ( this._clientPort + 1 ) + "\",";
                msg += "\"admin\" : " + "\"" + ( this._clientPort + 1 ) + "\",";
                msg += "\"request\" : " + "\"pushpolicies\"";
                msg += "}";

                var data = Encoding.UTF8.GetBytes( Framework.Utils.Base64Encode( msg ) );
                var stream = this._sendingClient.GetStream();
                stream.Write( data , 0 , data.Length );
                stream.Flush();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void SendPushModules()
        {
            try
            {
                var msg = "{";
                msg += "\"peerPort\" : " + "\"" + ( this._clientPort + 1 ) + "\",";
                msg += "\"admin\" : " + "\"" + ( this._clientPort + 1 ) + "\",";
                msg += "\"request\" : " + "\"pushmodules\"";
                msg += "}";

                var data = Encoding.UTF8.GetBytes( Framework.Utils.Base64Encode( msg ) );
                var stream = this._sendingClient.GetStream();
                stream.Write( data , 0 , data.Length );
                stream.Flush();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void FetchModules()
        {
            try
            {
                var msg = "{";
                msg += "\"peerPort\" : " + "\"" + ( this._clientPort + 1 ) + "\",";
                msg += "\"admin\" : " + "\"" + ( this._clientPort + 1 ) + "\",";
                msg += "\"request\" : " + "\"fetchmodules\"";
                msg += "}";

                var data = Encoding.UTF8.GetBytes( Framework.Utils.Base64Encode( msg ) );
                var stream = this._sendingClient.GetStream();
                stream.Write( data , 0 , data.Length );
                stream.Flush();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void SaveModule( string name , string contents )
        {
            try
            {
                var msg = "{";
                msg += "\"peerPort\" : " + "\"" + ( this._clientPort + 1 ) + "\",";
                msg += "\"admin\" : " + "\"" + ( this._clientPort + 1 ) + "\",";
                msg += "\"request\" : " + "\"savemodule\",";
                msg += "\"moduleName\" : \"" + name + "\",";
                msg += "\"moduleContents\" : \"" + Framework.Utils.Base64Encode( contents ) + "\"";
                msg += "}";

                var data = Encoding.UTF8.GetBytes( Framework.Utils.Base64Encode( msg ) );
                var stream = this._sendingClient.GetStream();
                stream.Write( data , 0 , data.Length );
                stream.Flush();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void DeleteModule( string name )
        {
            try
            {
                var msg = "{";
                msg += "\"peerPort\" : " + "\"" + ( this._clientPort + 1 ) + "\",";
                msg += "\"admin\" : " + "\"" + ( this._clientPort + 1 ) + "\",";
                msg += "\"request\" : " + "\"deletemodule\",";
                msg += "\"moduleName\" : \"" + name + "\"";
                msg += "}";

                var data = Encoding.UTF8.GetBytes( Framework.Utils.Base64Encode( msg ) );
                var stream = this._sendingClient.GetStream();
                stream.Write( data , 0 , data.Length );
                stream.Flush();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        /// </summary>
        public void Listener()
        {
            try
            {
                var localAddr = IPAddress.Parse( "0.0.0.0" );

                this._receivingClient = new TcpListener( localAddr , this._clientPort + 1 );
                this._receivingClient.Start();

                // Buffer for reading data
                var bytes = new Byte[1024];

                while( true )
                {
                    var client = this._receivingClient.AcceptTcpClient();
                    var stream = client.GetStream();

                    int i;
                    var data = "";


                    while( ( i = stream.Read( bytes , 0 , bytes.Length ) ) != 0 )
                    {
                        data += Encoding.ASCII.GetString( bytes , 0 , i );
                    }

                    data = Framework.Utils.Base64Decode( data );

                    this.OnDataReceived( data );

                    client.Close();
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Shuts down the listener
        /// </summary>
        public void Shutdown()
        {
            try
            {
                this._tcpListenerThread.Abort();
                this._receivingClient.Stop();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #endregion
    }
}