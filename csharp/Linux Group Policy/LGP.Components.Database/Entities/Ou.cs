#region

using System;
using System.Collections.Generic;
using System.Windows.Controls;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;

#endregion

namespace LGP.Components.Database.Entities
{
    internal class Ou : IOu
    {
        private List< IOuObserver > _observers;
        private int _ouId;
        private string _ouName;
        private int _ouParentId;
        private string _oupolicyGroup;


        public Ou( int id , string name , int parentid , string group )
        {
            this._observers = new List< IOuObserver >();
            this._ouId = id;
            this._ouName = name;
            this._ouParentId = parentid;
            this._oupolicyGroup = group;
        }

        #region IOu Members

        /// <summary>
        ///   Sets the ou Id
        /// </summary>
        /// <param name = "val">int</param>
        public void SetOuId( int val )
        {
            try
            {
                var sql = String.Format( "update ous set ou_id = '{0}' where ou_id = '{1}' " , val , this._ouId );

                if( Framework.Database.IsConnected() )
                {
                    Framework.Database.ExecuteNonQuery( sql );
                }

                this._ouId = val;
                this.Notify();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Gets the ou Id
        /// </summary>
        /// <returns>int</returns>
        public int GetOuId()
        {
            return this._ouId;
        }


        /// <summary>
        ///   Sets the ou name
        /// </summary>
        /// <param name = "val">string</param>
        public void SetName( string val )
        {
            try
            {
                var sql = String.Format( "update ous set ou_name = '{0}' where ou_id = '{1}' " , val , this._ouId );

                if( Framework.Database.IsConnected() )
                {
                    Framework.Database.ExecuteNonQuery( sql );
                }

                this._ouName = val;
                this.Notify();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Gets the ou name
        /// </summary>
        /// <returns>string</returns>
        public string GetName()
        {
            return this._ouName;
        }


        /// <summary>
        ///   Sets the parent ou Id
        /// </summary>
        /// <param name = "val">int</param>
        public void SetParentOuId( int val )
        {
            try
            {
                var sql = String.Format( "update ous set ou_parent_id = '{0}' where ou_id = '{1}' " , val , this._ouId );

                if( Framework.Database.IsConnected() )
                {
                    Framework.Database.ExecuteNonQuery( sql );
                }

                this._ouParentId = val;
                this.Notify();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Gets the parent ou Id
        /// </summary>
        /// <returns>int</returns>
        public int GetParentOuId()
        {
            return this._ouParentId;
        }


        /// <summary>
        ///   Sets the ou policy group
        /// </summary>
        /// <param name = "val"></param>
        public void SetPolicyGroup( string val )
        {
            try
            {
                var sql = String.Format( "update ous set ou_policygroup = '{0}' where ou_id = '{1}' " , val , this._ouId );

                if( Framework.Database.IsConnected() )
                {
                    Framework.Database.ExecuteNonQuery( sql );
                }

                this._oupolicyGroup = val;
                this.Notify();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Gets the ou policy group
        /// </summary>
        /// <returns></returns>
        public string GetPolicyGroup()
        {
            return this._oupolicyGroup;
        }


        /// <summary>
        ///   Gets the ou image
        /// </summary>
        /// <param name = "size">int</param>
        /// <returns>image</returns>
        public Image GetOuImage( int size )
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

                return Framework.Images.GetImage( "workgroup" , sizeFolder );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return null;
            }
        }


        /// <summary>
        ///   Update this observer with a refernece to the ou
        /// </summary>
        /// <param name = "ou">Iou</param>
        /// <param name = "source">source observer</param>
        public void Update( IOu ou , IOuObserver source )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Attach objewcts that observer this IOu
        /// </summary>
        /// <param name = "obj">IOuObserver</param>
        public void Attach( IOuObserver obj )
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
        ///   Detach objewcts that observer this IOu
        /// </summary>
        /// <param name = "obj">IOuObserver</param>
        public void Detach( IOuObserver obj )
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
        ///   Notify observers of this IOu
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
        /// <param name = "obj">IOuObserver</param>
        public void Dispose( IOuObserver obj )
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

        /// <summary>
        ///   Allows an <see cref = "T:System.Object" /> to attempt to free resources and perform other cleanup operations before the <see
        ///    cref = "T:System.Object" /> is reclaimed by garbage collection.
        /// </summary>
        ~Ou()
        {
            this._observers = null;
        }
    }
}