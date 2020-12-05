#region

using System;
using System.Collections.Generic;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;

#endregion

namespace LGP.Components.Database.Entities
{
    internal class Policy : IPolicy
    {
        private readonly int _policyid;
        private string _created;
        private string _dsl;
        private string _edited;
        private bool _enabled;
        private List< IPolicyObserver > _observers;
        private int _ouid;
        private string _version;

        public Policy( int pid , string created , string edited , int enabled , string dsl , int ouid , string version )
        {
            this._observers = new List< IPolicyObserver >();
            this._policyid = pid;
            this._ouid = ouid;
            this._created = created;
            this._edited = edited;
            this._enabled = ( enabled == 1 );
            this._dsl = Framework.Utils.Base64Decode( dsl );
            this._version = version;
        }

        #region Implementation of IPolicyObserver

        /// <summary>
        ///   Update this observer with a refernece to the ou
        /// </summary>
        /// <param name = "policy">IPolicy</param>
        /// <param name = "source">source observer</param>
        public void Update( IPolicy policy , IPolicyObserver source )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Attach objects that observer this IPolicy
        /// </summary>
        /// <param name = "obj">IPolicyObserver</param>
        public void Attach( IPolicyObserver obj )
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
        ///   Detach objects that observer this IPolicy
        /// </summary>
        /// <param name = "obj">IPolicyObserver</param>
        public void Detach( IPolicyObserver obj )
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
        ///   Notify observers of this IPolicy
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
        /// <param name = "obj">IPolicyObserver</param>
        public void Dispose( IPolicyObserver obj )
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

        #endregion

        #region Implementation of IPolicy

        /// <summary>
        ///   Gets the id of this policy
        /// </summary>
        /// <returns>int</returns>
        public int GetId()
        {
            return this._policyid;
        }

        /// <summary>
        ///   Gets the ou id
        /// </summary>
        /// <returns>int</returns>
        public int GetOuId()
        {
            return this._ouid;
        }

        /// <summary>
        ///   Sets the ou id
        /// </summary>
        /// <param name = "id">int</param>
        public void SetOuId( int id )
        {
            try
            {
                var sql = String.Format( "update policy set p_ou_id = '{0}' where p_id = '{1}' " , id , this._policyid );

                if( Framework.Database.IsConnected() )
                {
                    Framework.Database.ExecuteNonQuery( sql );
                }

                this._ouid = id;
                this.Notify();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Gets the created date
        /// </summary>
        /// <returns>string</returns>
        public string GetCreated()
        {
            return this._created;
        }

        /// <summary>
        ///   Sets the created date
        /// </summary>
        /// <param name = "created">string</param>
        public void SetCreated( string created )
        {
            try
            {
                var sql = String.Format( "update policy set p_created = '{0}' where p_id = '{1}' " , created , this._policyid );

                if( Framework.Database.IsConnected() )
                {
                    Framework.Database.ExecuteNonQuery( sql );
                }

                this._created = created;
                this.Notify();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Gets the edited date
        /// </summary>
        /// <returns>string</returns>
        public string GetEdited()
        {
            return this._edited;
        }

        /// <summary>
        ///   Sets the edited date
        /// </summary>
        /// <param name = "edited">string</param>
        public void SetEdited( string edited )
        {
            try
            {
                var sql = String.Format( "update policy set p_edited = '{0}' where p_id = '{1}' " , edited , this._policyid );

                if( Framework.Database.IsConnected() )
                {
                    Framework.Database.ExecuteNonQuery( sql );
                }

                this._edited = edited;
                this.Notify();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Gets the enabled status
        /// </summary>
        /// <returns>bool</returns>
        public bool IsEnabled()
        {
            return this._enabled;
        }

        /// <summary>
        ///   Sets the enabled status
        /// </summary>
        /// <param name = "enabled"></param>
        public void SetEnabled( bool enabled )
        {
            try
            {
                var b = ( enabled ) ? 1 : 0;
                var sql = String.Format( "update policy set p_enabled = '{0}' where p_id = '{1}' " , b , this._policyid );

                if( Framework.Database.IsConnected() )
                {
                    Framework.Database.ExecuteNonQuery( sql );
                }

                this._enabled = enabled;
                this.Notify();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Get the DSL or rules
        /// </summary>
        /// <returns></returns>
        public string GetDsl()
        {
            return this._dsl;
        }

        /// <summary>
        ///   Set the DSL rules
        /// </summary>
        /// <param name = "dsl"></param>
        public void SetDsl( string dsl )
        {
            try
            {
                var sql = String.Format( "update policy set p_dsl = '{0}' where p_id = '{1}' " , Framework.Utils.Base64Encode( dsl ) , this._policyid );

                if( Framework.Database.IsConnected() )
                {
                    Framework.Database.ExecuteNonQuery( sql );
                }

                this._dsl = dsl;
                this.Notify();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Gets the version
        /// </summary>
        /// <returns>string</returns>
        public string GetVersion()
        {
            return this._version;
        }

        /// <summary>
        ///   Sets the version
        /// </summary>
        /// <param name = "version">string</param>
        public void SetVersion( string version )
        {
            try
            {
                var sql = String.Format( "update policy set p_version = '{0}' where p_id = '{1}' " , version , this._policyid );

                if( Framework.Database.IsConnected() )
                {
                    Framework.Database.ExecuteNonQuery( sql );
                }

                this._version = version;
                this.Notify();
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
        ~Policy()
        {
            this._observers = null;
        }
    }
}