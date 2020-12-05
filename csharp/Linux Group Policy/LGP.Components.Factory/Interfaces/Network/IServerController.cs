#region

using System.Collections.Generic;
using LGP.Components.Factory.Internal.ServerControl;

#endregion

namespace LGP.Components.Factory.Interfaces.Network
{
    /// <summary>
    /// 
    /// </summary>
    public interface IServerController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool IsConnected();

        /// <summary>
        /// </summary>
        /// <returns></returns>
        bool Connect();

        /// <summary>
        /// </summary>
        /// <returns></returns>
        bool Disconnect();

        /// <summary>
        /// </summary>
        /// <param name = "message"></param>
        /// <returns></returns>
        bool SendMesssage( List< KeyValuePair< string , string > > message );

        /// <summary>
        /// 
        /// </summary>
        void SendPolicyUpdated();

        /// <summary>
        /// 
        /// </summary>
        void SendPushPolicies();

        /// <summary>
        /// 
        /// </summary>
        void SendPushModules();

        /// <summary>
        /// 
        /// </summary>
        void FetchModules();

        /// <summary>
        /// 
        /// </summary>
        void SaveModule( string name , string contents );

        /// <summary>
        /// 
        /// </summary>
        void DeleteModule( string name );

        /// <summary>
        /// </summary>
        void Listener();

        /// <summary>
        ///   Shuts down the listener
        /// </summary>
        void Shutdown();

        /// <summary>
        /// 
        /// </summary>
        event DataReceived OnDataReceived;
    }
}