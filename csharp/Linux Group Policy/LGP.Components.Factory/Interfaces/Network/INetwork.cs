#region

using System.Collections.Generic;
using LGP.Components.Factory.Internal.ServerControl;

#endregion

namespace LGP.Components.Factory.Interfaces.Network
{
    /// <summary>
    ///   Interface for the server connection handler
    /// </summary>
    public interface INetwork
    {
        /// <summary>
        ///   Gets the list of servers detected
        /// </summary>
        /// <returns>List Server</returns>
        List< IServerInfo > GetServers();

        /// <summary>
        ///   Gets the list of clients
        /// </summary>
        /// <returns>List Server</returns>
        List< IServerController > GetClients();


        /// <summary>
        ///   Sets the server ip address
        /// </summary>
        /// <param name = "ipaddr">ip address</param>
        void SetServerAddress( string ipaddr );


        /// <summary>
        ///   Returns the server ip address
        /// </summary>
        /// <returns>ip address</returns>
        string GetServerAddress();


        /// <summary>
        ///   Get the list of clients
        /// </summary>
        /// <returns>Clients array</returns>
        IServerController CreateClient();


        /// <summary>
        ///   Restart the service listener
        /// </summary>
        void RestartServerListener();
    }
}