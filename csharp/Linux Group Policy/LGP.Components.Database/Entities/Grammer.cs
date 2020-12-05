#region

using System;
using System.Collections.Generic;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;

#endregion

namespace LGP.Components.Database.Entities
{
    internal class Grammer : IGrammer
    {
        private readonly int _rowid;
        private string _key;
        private int _moduleid;
        private List< IGrammerObserver > _observers;
        private string _value;

        public Grammer( int rowid , int moduleid , string key , string value )
        {
            this._observers = new List< IGrammerObserver >();
            this._key = key;
            this._moduleid = moduleid;
            this._rowid = rowid;
            this._value = value;
        }

        #region IGrammer Members

        /// <summary>
        ///   Gets the grammer rowid
        /// </summary>
        /// <returns>int</returns>
        public int GetId()
        {
            return this._rowid;
        }


        /// <summary>
        ///   Gets the grammer moduleid
        /// </summary>
        /// <returns>int</returns>
        public int GetModuleId()
        {
            return this._moduleid;
        }


        /// <summary>
        ///   Gets the grammer key
        /// </summary>
        /// <returns>string</returns>
        public string GetKey()
        {
            return this._key;
        }


        /// <summary>
        ///   Gets the grammer value
        /// </summary>
        /// <returns>string</returns>
        public string GetValue()
        {
            return this._value;
        }

        /// <summary>
        ///   Sets the grammer moduleid
        /// </summary>
        /// <param name = "val">int</param>
        public void SetModuleId( int val )
        {
            try
            {
                var sql = String.Format( "update moduleGrammerWords set m_id = '{0}' where id = '{1}' " , val , this._rowid );

                if( Framework.Database.IsConnected() )
                {
                    Framework.Database.ExecuteNonQuery( sql );
                }

                this._moduleid = val;
                this.Notify();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Sets the grammer key
        /// </summary>
        /// <param name = "val">string</param>
        public void SetKey( string val )
        {
            try
            {
                var sql = String.Format( "update moduleGrammerWords set grammerkey = '{0}' where id = '{1}' " , val , this._rowid );

                if( Framework.Database.IsConnected() )
                {
                    Framework.Database.ExecuteNonQuery( sql );
                }

                this._key = val;
                this.Notify();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Sets the grammer value
        /// </summary>
        /// <param name = "val">string</param>
        public void SetValue( string val )
        {
            try
            {
                var sql = String.Format( "update moduleGrammerWords set grammerval = '{0}' where id = '{1}' " , val , this._rowid );

                if( Framework.Database.IsConnected() )
                {
                    Framework.Database.ExecuteNonQuery( sql );
                }

                this._value = val;
                this.Notify();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #endregion

        #region Implementation of IGrammerObserver

        /// <summary>
        ///   Update this observer with a refernece to the grammer
        /// </summary>
        /// <param name = "grammer">IGrammer</param>
        /// <param name = "source">IGrammerObserver</param>
        public void Update( IGrammer grammer , IGrammerObserver source )
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
        ///   Attach objewcts that observer this grammer
        /// </summary>
        /// <param name = "obj">IGrammerObserver</param>
        public void Attach( IGrammerObserver obj )
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
        ///   Detach objewcts that observer this grammer
        /// </summary>
        /// <param name = "obj">IGrammerObserver</param>
        public void Detach( IGrammerObserver obj )
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
        ///   Notify observers of this grammer
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
        /// <param name = "obj">IGrammerObserver</param>
        public void Dispose( IGrammerObserver obj )
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

        ~Grammer()
        {
            this._observers = null;
        }


        public override string ToString()
        {
            return this.GetKey();
        }
    }
}