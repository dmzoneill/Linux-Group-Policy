#region

using System;
using System.Collections.Generic;
using LGP.Components.Factory.Interfaces.Network;

#endregion

namespace LGP.Components.Factory.Internal.ServerControl
{
    /// <summary>
    ///   Holds server information
    /// </summary>
    public class ServerInfo : IServerInfo
    {
        private readonly List< int > _cpu;
        private readonly List< int > _incoming;
        private readonly List< int > _outgoing;
        private readonly List< int > _rx;
        private readonly List< int > _tx;

        /// <summary>
        /// 
        /// </summary>
        public ServerInfo()
        {
            this._incoming = new List< int >();
            this._outgoing = new List< int >();
            this._rx = new List< int >();
            this._tx = new List< int >();
            this._cpu = new List< int >();
        }

        #region IServerInfo Members

        /// <summary>
        ///   The server ip address
        /// </summary>
        public string ServerAddress
        {
            get;
            set;
        }


        /// <summary>
        ///   The time it was last seen
        /// </summary>
        public DateTime LastSeen
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List< int > GetCpu()
        {
            return this._cpu;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void AddCpu( int value )
        {
            if( this._cpu.Count > 29 )
            {
                this._cpu.RemoveAt( 0 );
            }
            this._cpu.Add( value );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List< int > GetIncoming()
        {
            return this._incoming;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void AddIncoming( int value )
        {
            if( this._incoming.Count > 29 )
            {
                this._incoming.RemoveAt( 0 );
            }
            this._incoming.Add( value );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List< int > GetOutgoing()
        {
            return this._outgoing;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void AddOutgoing( int value )
        {
            if( this._outgoing.Count > 29 )
            {
                this._outgoing.RemoveAt( 0 );
            }
            this._outgoing.Add( value );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void AddTx( int value )
        {
            if( this._tx.Count > 29 )
            {
                this._tx.RemoveAt( 0 );
            }
            this._tx.Add( value );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List< int > GetTx()
        {
            return this._tx;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void AddRx( int value )
        {
            if( this._rx.Count > 29 )
            {
                this._rx.RemoveAt( 0 );
            }
            this._rx.Add( value );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List< int > GetRx()
        {
            return this._rx;
        }


        /// <summary>
        ///   Overrides to string, for listbox usage
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return this.ServerAddress;
        }

        #endregion
    }
}