#region

using System;
using System.Collections.Generic;
using System.Windows.Controls;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;

#endregion

namespace LGP.Components.Database.Entities
{
    internal class Client : IClient
    {
        private string _arch;
        private string _binded;
        private string _clientversion;
        private string _distribution;
        private string _distributionVersion;
        private string _guid;
        private int _id;
        private string _ipaddress;
        private string _kernel;
        private string _lastseen;
        private string _name;
        private List< IClientObserver > _observers;
        private string _os;
        private int _ouId;
        private string _port;
        private string _pseudoName;
        private string _version;

        public Client( int id , int ouid , string guid , string name , string os , string arch , string kernel , string pseudoName , string distro , string distroVersion , string lastseen , string binded , string clientVersion , string ip , string port )
        {
            this._observers = new List< IClientObserver >();
            this._id = id;
            this._ouId = ouid;
            this._guid = guid;
            this._name = name;
            this._os = os;
            this._arch = arch;
            this._kernel = kernel;
            this._pseudoName = pseudoName;
            this._distribution = distro;
            this._distributionVersion = distroVersion;
            this._lastseen = lastseen;
            this._binded = binded;
            this._clientversion = clientVersion;
            this._ipaddress = ip;
            this._port = port;
        }

        #region IClient Members

        /// <summary>
        ///   Sets the client id
        /// </summary>
        /// <param name = "val">int</param>
        public void SetId( int val )
        {
            this._id = val;
            this.Notify();
        }


        /// <summary>
        ///   Gets the client id
        /// </summary>
        /// <returns>int</returns>
        public int GetId()
        {
            return this._id;
        }


        /// <summary>
        ///   Sets the client ou id
        /// </summary>
        /// <param name = "val">int</param>
        public void Setouid( int val )
        {
            this._ouId = val;
            this.Notify();
        }


        /// <summary>
        ///   Gets the client ou id
        /// </summary>
        /// <returns>int</returns>
        public int GetouId()
        {
            return this._ouId;
        }


        /// <summary>
        ///   Sets the client guid
        /// </summary>
        /// <param name = "val">string</param>
        public void SetGuid( string val )
        {
            this._guid = val;
            this.Notify();
        }


        /// <summary>
        ///   Gets the client guid
        /// </summary>
        /// <returns>string</returns>
        public string GetGuid()
        {
            return this._guid;
        }


        /// <summary>
        ///   Sets the client name
        /// </summary>
        /// <param name = "val">string</param>
        public void SetName( string val )
        {
            this._name = val;
            this.Notify();
        }


        /// <summary>
        ///   Gets the client name
        /// </summary>
        /// <returns>string</returns>
        public string GetName()
        {
            return this._name;
        }


        /// <summary>
        ///   Sets the client OS
        /// </summary>
        /// <param name = "val">string</param>
        public void SetOs( string val )
        {
            this._os = val;
            this.Notify();
        }


        /// <summary>
        ///   Gets the client OS
        /// </summary>
        /// <returns>string</returns>
        public string GetOs()
        {
            return this._os;
        }


        /// <summary>
        ///   Sets the client architecture
        /// </summary>
        /// <param name = "val">string</param>
        public void SetArch( string val )
        {
            this._arch = val;
            this.Notify();
        }


        /// <summary>
        ///   Gets the client architecture
        /// </summary>
        /// <returns>string</returns>
        public string GetArch()
        {
            return this._arch;
        }


        /// <summary>
        ///   Sets the client kernel
        /// </summary>
        /// <param name = "val">string</param>
        public void SetKernel( string val )
        {
            this._kernel = val;
            this.Notify();
        }


        /// <summary>
        ///   Gets the client kernel
        /// </summary>
        /// <returns>string</returns>
        public string GetKernel()
        {
            return this._kernel;
        }


        /// <summary>
        ///   Sets the client psuedoname
        /// </summary>
        /// <param name = "val">string</param>
        public void SetPseudoName( string val )
        {
            this._pseudoName = val;
            this.Notify();
        }


        /// <summary>
        ///   Gets the client psuedoname
        /// </summary>
        /// <returns>string</returns>
        public string GetPseudoName()
        {
            return this._pseudoName;
        }


        /// <summary>
        ///   Sets the client Distribution
        /// </summary>
        /// <param name = "val">string</param>
        public void SetDistribution( string val )
        {
            this._distribution = val;
            this.Notify();
        }


        /// <summary>
        ///   Gets the client Distribution
        /// </summary>
        /// <returns>string</returns>
        public string GetDistribution()
        {
            return this._distribution;
        }


        /// <summary>
        ///   Sets the client Distribution version
        /// </summary>
        /// <param name = "val">string</param>
        public void SetDistributionVersion( string val )
        {
            this._distributionVersion = val;
            this.Notify();
        }


        /// <summary>
        ///   Gets the client Distribution version
        /// </summary>
        /// <returns>string</returns>
        public string GetDistributionVersion()
        {
            return this._distributionVersion;
        }


        /// <summary>
        ///   Sets the client version
        /// </summary>
        /// <param name = "val">string</param>
        public void SetVersion( string val )
        {
            this._version = val;
            this.Notify();
        }


        /// <summary>
        ///   Gets the client version
        /// </summary>
        /// <returns>string</returns>
        public string GetVersion()
        {
            return this._version;
        }


        /// <summary>
        ///   Sets the client last seen time
        /// </summary>
        /// <param name = "val">string</param>
        public void SetLastseen( string val )
        {
            this._lastseen = val;
            this.Notify();
        }


        /// <summary>
        ///   Gets the client last seen time
        /// </summary>
        /// <returns>string</returns>
        public string GetLastseen()
        {
            return this._lastseen;
        }


        /// <summary>
        ///   Sets the client binded state
        /// </summary>
        /// <param name = "val">string</param>
        public void SetBinded( string val )
        {
            this._binded = val;
            this.Notify();
        }


        /// <summary>
        ///   Gets the client binded state
        /// </summary>
        /// <returns>string</returns>
        public string GetBinded()
        {
            return this._binded;
        }


        /// <summary>
        ///   Sets the clients version
        /// </summary>
        /// <param name = "val">string</param>
        public void SetClientVersion( string val )
        {
            this._clientversion = val;
            this.Notify();
        }


        /// <summary>
        ///   Gets the clients version
        /// </summary>
        /// <returns>string</returns>
        public string GetClientVersion()
        {
            return this._clientversion;
        }


        /// <summary>
        ///   Sets the clients ip address
        /// </summary>
        /// <param name = "val">string</param>
        public void SetIpaddress( string val )
        {
            this._ipaddress = val;
            this.Notify();
        }


        /// <summary>
        ///   Gets the clients ip address
        /// </summary>
        /// <returns>string</returns>
        public string GetIpaddress()
        {
            return this._ipaddress;
        }


        /// <summary>
        ///   Sets the clients port
        /// </summary>
        /// <param name = "val">string</param>
        public void SetPort( string val )
        {
            this._port = val;
            this.Notify();
        }


        /// <summary>
        ///   Gets the clients port
        /// </summary>
        /// <returns>string</returns>
        public string GetPort()
        {
            return this._port;
        }


        /// <summary>
        ///   Gets the client distro image
        /// </summary>
        /// <param name = "size">int</param>
        /// <returns>image</returns>
        public Image GetDistroImage( int size )
        {
            try
            {
                var sizeFolder = "distros/";

                switch( size )
                {
                    case 32 :
                        sizeFolder += "32";
                        break;

                    case 48 :
                        sizeFolder += "48";
                        break;

                    case 64 :
                        sizeFolder += "64";
                        break;

                    case 128 :
                        sizeFolder += "128";
                        break;

                    default :
                        sizeFolder += "32";
                        break;
                }

                return Framework.Images.GetImage( this._distribution , sizeFolder );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return null;
            }
        }

        #endregion

        #region Implementation of IClientObserver

        /// <summary>
        ///   Update this observer with a refernece to the client
        /// </summary>
        /// <param name = "client">IClient</param>
        /// <param name = "source">source observer</param>
        public void Update( IClient client , IClientObserver source )
        {
            try
            {
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Attach objewcts that observer this client
        /// </summary>
        /// <param name = "obj">IOuObserver</param>
        public void Attach( IClientObserver obj )
        {
            try
            {
                if( !this._observers.Contains( obj ) )
                {
                    this._observers.Add( obj );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Detach objewcts that observer this client
        /// </summary>
        /// <param name = "obj">IOuObserver</param>
        public void Detach( IClientObserver obj )
        {
            try
            {
                this._observers.Remove( obj );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Notify observers of this client
        /// </summary>
        public void Notify()
        {
            try
            {
                int i = 0;
                var h = this._observers.Count;
                while( i < h )
                {
                    this._observers[ i ].Update( this , this );
                    h = this._observers.Count;
                    i++;
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Dispose this object and all the observers
        /// </summary>
        /// <param name = "obj">IOuObserver</param>
        public void Dispose( IClientObserver obj )
        {
            try
            {
                var h = this._observers.Count - 1;
                while( h > -1 )
                {
                    this._observers[ h ].Dispose( this );
                    h = this._observers.Count - 1;
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #endregion

        /// <summary>
        ///   Allows an <see cref = "T:System.Object" /> to attempt to free resources and perform other cleanup operations before the <see
        ///    cref = "T:System.Object" /> is reclaimed by garbage collection.
        /// </summary>
        ~Client()
        {
            this._observers = null;
        }
    }
}