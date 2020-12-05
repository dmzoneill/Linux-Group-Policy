using System;
using System.Collections.Generic;

namespace LGP.Components.Factory.Interfaces.Network
{
    /// <summary>
    /// 
    /// </summary>
    public interface IServerInfo
    {
        /// <summary>
        ///   The server ip address
        /// </summary>
        string ServerAddress
        {
            get;
            set;
        }

        /// <summary>
        ///   The time it was last seen
        /// </summary>
        DateTime LastSeen
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List< int > GetCpu();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        void AddCpu( int value );

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List< int > GetIncoming();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        void AddIncoming( int value );

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List< int > GetOutgoing();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        void AddOutgoing( int value );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        void AddTx( int value );

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List< int > GetTx();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        void AddRx( int value );

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List< int > GetRx();

        /// <summary>
        ///   Overrides to string, for listbox usage
        /// </summary>
        /// <returns>string</returns>
        string ToString();
    }
}