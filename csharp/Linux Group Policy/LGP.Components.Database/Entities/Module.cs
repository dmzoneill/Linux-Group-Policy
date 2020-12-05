#region

using System;
using System.Collections.Generic;
using LGP.Components.Database.Gateways;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;

#endregion

namespace LGP.Components.Database.Entities
{
    internal class Module : IModule
    {
        private static readonly IGrammerGateway GrammerGateway;
        private readonly int _moduleid;
        private string _moduleName;
        private List< IModuleObserver > _observers;

        static Module()
        {
            GrammerGateway = new GrammerGateway();
        }

        public Module( int moduleid , string modulename )
        {
            this._observers = new List< IModuleObserver >();
            this._moduleid = moduleid;
            this._moduleName = modulename;
        }

        #region Implementation of IModuleObserver

        /// <summary>
        ///   Update this observer with a refernece to the module
        /// </summary>
        /// <param name = "module">IModule</param>
        /// <param name = "source">IModuleObserver</param>
        public void Update( IModule module , IModuleObserver source )
        {
            throw new NotImplementedException();
        }


        /// <summary>
        ///   Attach objewcts that observer this module
        /// </summary>
        /// <param name = "obj">IModuleObserver</param>
        public void Attach( IModuleObserver obj )
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
        ///   Detach objewcts that observer this module
        /// </summary>
        /// <param name = "obj">IModuleObserver</param>
        public void Detach( IModuleObserver obj )
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
        ///   Notify observers of this module
        /// </summary>
        public void Notify()
        {
            try
            {
                for( var h = 0; h < this._observers.Count; h++ )
                {
                    this._observers[ h ].Update( this , this );
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
        /// <param name = "obj">IModuleObserver</param>
        public void Dispose( IModuleObserver obj )
        {
            try
            {
                for( var h = 0; h < this._observers.Count; h++ )
                {
                    this._observers[ h ].Dispose( this );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   The file contents when used by the module editor
        /// </summary>
        public string FileContents
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        /// <summary>
        ///   Returns the module id
        /// </summary>
        /// <returns>int</returns>
        public int GetModuleId()
        {
            return this._moduleid;
        }

        /// <summary>
        ///   Gets the name of the module
        /// </summary>
        /// <returns>string</returns>
        public string GetModuleName()
        {
            return this._moduleName;
        }

        /// <summary>
        ///   Sets the module name
        /// </summary>
        /// <param name = "name"></param>
        public void SetModuleName( string name )
        {
            try
            {
                var sql = String.Format( "update modules set name = '{0}' where id = '{1}' " , name , this._moduleid );

                if( Framework.Database.IsConnected() )
                {
                    Framework.Database.ExecuteNonQuery( sql );
                }

                this._moduleName = name;
                this.Notify();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Adds a grammer to this module
        /// </summary>
        /// <param name = "key">string</param>
        /// <param name = "value">string</param>
        /// <returns>IGrammer</returns>
        public IGrammer AddGrammer( string key , string value )
        {
            try
            {
                var grammer = GrammerGateway.CreateGrammer( this._moduleid , key , value );
                this.Notify();
                return grammer;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return null;
            }
        }


        /// <summary>
        ///   Deletes a grammer given a key
        /// </summary>
        /// <param name = "key"></param>
        public void DeleteGrammer( string key )
        {
            try
            {
                GrammerGateway.DeleteGrammer( this._moduleid , key );
                this.Notify();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Dletes all the grammers associated with this module
        /// </summary>
        public void DeleteAllGrammer()
        {
            try
            {
                GrammerGateway.DeleteGrammer( this._moduleid );
                this.Notify();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Gets all the grammers for this module
        /// </summary>
        /// <returns>IGrammer</returns>
        public List< IGrammer > GetAllGrammers()
        {
            return GrammerGateway.GetGrammer( this._moduleid );
        }

        /// <summary>
        ///   Gets a grammer given a key
        /// </summary>
        /// <param name = "key">grammer lvale</param>
        /// <returns>KeyValuePair</returns>
        public IGrammer GetGrammer( string key )
        {
            return GrammerGateway.GetGrammer( this._moduleid , key );
        }

        /// <summary>
        ///   Gets a grammer given an id
        /// </summary>
        /// <param name = "id">grammer id</param>
        /// <returns>IGrammer</returns>
        public IGrammer GetGrammer( int id )
        {
            return GrammerGateway.GetGrammer( this._moduleid , id );
        }

        /// <summary>
        ///   Get the has of the module
        /// </summary>
        /// <returns>int</returns>
        public int GetHash()
        {
            return 0;
        }

        /// <summary>
        ///   Gets the module location
        /// </summary>
        /// <returns>string</returns>
        public string GetModuleLocation()
        {
            return "";
        }

        #endregion

        ~Module()
        {
            this._observers = null;
        }
    }
}