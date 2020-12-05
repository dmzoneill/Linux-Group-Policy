#region

using System.Collections.Generic;

#endregion

namespace LGP.Components.Factory.Interfaces.Network
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBroadcastListener
    {
        /// <summary>
        ///   Gets the list of servers detected
        /// </summary>
        /// <returns>List Server</returns>
        List< IServerInfo > GetServers();

        /// <summary>
        ///   Retarts the listener
        /// </summary>
        void RetartListener();

        /// <summary>
        ///   Stops the listener
        /// </summary>
        void StopListener();
    }
}