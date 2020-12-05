#region

using System.Collections.Generic;
using LGP.Components.Factory.Interfaces.Network;

#endregion

namespace LGP.Components.Factory.Internal.ServerControl
{
    /// <summary>
    ///   Connection Handler to the server
    /// </summary>
    internal class NetworkController : INetwork
    {
        private static List< IServerController > _clients;
        private static INetwork _instance;
        private int _clientsCount;
        private string _serverAddress;


        private NetworkController()
        {
            _clients = new List< IServerController >();
        }

        #region INetwork Members

        /// <summary>
        ///   Gets the list of servers detected
        /// </summary>
        /// <returns>List Server</returns>
        public List< IServerInfo > GetServers()
        {
            return BroadcastListener.GetInstance().GetServers();
        }


        /// <summary>
        ///   Gets the list of clients
        /// </summary>
        /// <returns>List Server</returns>
        public List< IServerController > GetClients()
        {
            return _clients;
        }


        /// <summary>
        ///   Sets the server ip address
        /// </summary>
        /// <param name = "ipaddr">ip address</param>
        public void SetServerAddress( string ipaddr )
        {
            this._serverAddress = ipaddr;
        }


        /// <summary>
        ///   REturns the server ip address
        /// </summary>
        /// <returns>ip address</returns>
        public string GetServerAddress()
        {
            return this._serverAddress;
        }


        /// <summary>
        ///   Get the list of clients
        /// </summary>
        /// <returns>Clients array</returns>
        public IServerController CreateClient()
        {
            if( this._serverAddress == null )
            {
                return null;
            }

            this._clientsCount += 2;
            var client = new ServerController( this._serverAddress , this._clientsCount + 50000 );
            _clients.Add( client );
            return client;
        }


        /// <summary>
        ///   Restart the service listener
        /// </summary>
        public void RestartServerListener()
        {
            BroadcastListener.GetInstance().RetartListener();
        }

        #endregion

        /// <summary>
        ///   Gets the instance of the server handler
        /// </summary>
        /// <returns></returns>
        public static INetwork GetInstance()
        {
            return _instance ?? ( _instance = new NetworkController() );
        }
    }
}