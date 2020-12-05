#region

using System;
using System.Collections.Generic;
using System.Data;
using LGP.Components.Database.Entities;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;

#endregion

namespace LGP.Components.Database.Gateways
{
    internal class GrammerGateway : IGrammerGateway
    {
        private static List< IGrammer > _grammer;

        private static void BuildGrammerListing()
        {
            _grammer = new List< IGrammer >();

            try
            {
                var sql = string.Format( "select * from moduleGrammerWords" );

                if( Framework.Database.IsConnected() == false )
                {
                    return;
                }

                var ds = Framework.Database.ExecuteQuery( sql );

                var clientTable = ds.Tables[ 0 ];

                if( clientTable.Rows.Count == 0 )
                {
                    return;
                }

                foreach( DataRow row in clientTable.Rows )
                {
                    var id = ( int ) row[ "id" ];
                    var mid = ( int ) row[ "m_id" ];
                    var gkey = ( string ) row[ "grammerkey" ];
                    var gvalue = ( string ) row[ "grammerval" ];

                    var grammer = new Grammer( id , mid , gkey , gvalue );

                    _grammer.Add( grammer );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #region Implementation of IGrammerGateway

        /// <summary>
        ///   Adds a grammer given a moduleid or returns existing Grammer
        /// </summary>
        /// <param name = "moduleId">int</param>
        /// <param name = "key">string</param>
        /// <param name = "val">string</param>
        /// <returns>IGrammer</returns>
        public IGrammer CreateGrammer( int moduleId , string key , string val )
        {
            try
            {
                if( _grammer == null )
                {
                    BuildGrammerListing();
                }

                for( var y = 0; y < _grammer.Count; y++ )
                {
                    if( _grammer[ y ].GetModuleId() == moduleId && _grammer[ y ].GetKey().CompareTo( key ) == 0 )
                    {
                        return _grammer[ y ];
                    }
                }

                var sql = string.Format( "insert into moduleGrammerWords values( null , '{0}' , '{1}' ,'{2}' )" , moduleId , key , val );

                if( Framework.Database.IsConnected() == false )
                {
                    return null;
                }

                Framework.Database.ExecuteNonQuery( sql );

                var sql2 = string.Format( "select * from moduleGrammerWords where id = ( select MAX( id ) from moduleGrammerWords)" );

                var ds = Framework.Database.ExecuteQuery( sql2 );

                var grammerTable = ds.Tables[ 0 ];
                var row = grammerTable.Rows[ 0 ];
                var id = ( int ) row[ "id" ];

                var grammer = new Grammer( id , moduleId , key , val );
                _grammer.Add( grammer );

                return _grammer[ _grammer.Count - 1 ];
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return null;
            }
        }

        /// <summary>
        ///   Deletes a grammer entry given key and moduleid
        /// </summary>
        /// <param name = "mid">int</param>
        /// <param name = "key">string</param>
        public void DeleteGrammer( int mid , string key )
        {
            try
            {
                if( _grammer == null )
                {
                    BuildGrammerListing();
                }

                for( var y = 0; y < _grammer.Count; y++ )
                {
                    if( _grammer[ y ].GetModuleId() == mid && _grammer[ y ].GetKey().CompareTo( key ) == 0 )
                    {
                        _grammer.RemoveAt( y );
                    }
                }

                var sql = string.Format( "delete from moduleGrammerWords where m_id= '{0}' and grammerkey = '{1}' " , mid , key );

                if( Framework.Database.IsConnected() == false )
                {
                    return;
                }

                Framework.Database.ExecuteNonQuery( sql );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Deletes all grammer given key the moduleid
        /// </summary>
        /// <param name = "mid">int</param>
        public void DeleteGrammer( int mid )
        {
            try
            {
                if( _grammer == null )
                {
                    BuildGrammerListing();
                }

                for( var y = 0; y < _grammer.Count; y++ )
                {
                    if( _grammer[ y ].GetModuleId() == mid )
                    {
                        _grammer.RemoveAt( y );
                    }
                }

                var sql = string.Format( "delete from moduleGrammerWords where m_id= '{0}'" , mid );

                if( Framework.Database.IsConnected() == false )
                {
                    return;
                }

                Framework.Database.ExecuteNonQuery( sql );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Gets a Grammer given a the moduleid and key
        /// </summary>
        /// <param name = "mid">int</param>
        /// <param name = "key">string</param>
        /// <returns>IGrammer</returns>
        public IGrammer GetGrammer( int mid , string key )
        {
            try
            {
                if( _grammer == null )
                {
                    BuildGrammerListing();
                }

                for( var y = 0; y < _grammer.Count; y++ )
                {
                    if( _grammer[ y ].GetModuleId() == mid && _grammer[ y ].GetKey().CompareTo( key ) == 0 )
                    {
                        return _grammer[ y ];
                    }
                }

                return null;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return null;
            }
        }

        /// <summary>
        ///   Gets a Grammer given a the moduleid and id
        /// </summary>
        /// <param name = "mid">int</param>
        /// <param name = "id">int</param>
        /// <returns>IGrammer</returns>
        public IGrammer GetGrammer( int mid , int id )
        {
            try
            {
                if( _grammer == null )
                {
                    BuildGrammerListing();
                }

                for( var y = 0; y < _grammer.Count; y++ )
                {
                    if( _grammer[ y ].GetModuleId() == mid && _grammer[ y ].GetId() == id )
                    {
                        return _grammer[ y ];
                    }
                }

                return null;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return null;
            }
        }

        /// <summary>
        ///   Gets all Grammer given a the moduleid
        /// </summary>
        /// <param name = "mid">int</param>
        /// <returns>IGrammer list</returns>
        public List< IGrammer > GetGrammer( int mid )
        {
            var found = new List< IGrammer >();
            try
            {
                if( _grammer == null )
                {
                    BuildGrammerListing();
                }

                for( var y = 0; y < _grammer.Count; y++ )
                {
                    if( _grammer[ y ].GetModuleId() == mid )
                    {
                        found.Add( _grammer[ y ] );
                    }
                }

                return found;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return found;
            }
        }

        /// <summary>
        ///   Gets all grammers
        /// </summary>
        /// <returns>IGrammer List</returns>
        public List< IGrammer > GetGrammer()
        {
            try
            {
                if( _grammer == null )
                {
                    BuildGrammerListing();
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
            return _grammer;
        }

        /// <summary>
        /// Refreshes the list
        /// </summary>
        public void Refresh()
        {
            try
            {
                var sql = string.Format( "select * from moduleGrammerWords" );

                if( Framework.Database.IsConnected() == false )
                {
                    return;
                }

                var ds = Framework.Database.ExecuteQuery( sql );

                var clientTable = ds.Tables[ 0 ];

                if( clientTable.Rows.Count == 0 )
                {
                    return;
                }

                foreach( DataRow row in clientTable.Rows )
                {
                    var id = ( int ) row[ "id" ];
                    var mid = ( int ) row[ "m_id" ];
                    var gkey = ( string ) row[ "grammerkey" ];
                    var gvalue = ( string ) row[ "grammerval" ];

                    var newDbEntry = true;

                    for( var y = 0; y < _grammer.Count; y++ )
                    {
                        if( _grammer[ y ].GetId() != id )
                        {
                            continue;
                        }

                        newDbEntry = false;
                        break;
                    }

                    if( !newDbEntry )
                    {
                        continue;
                    }

                    var grammer = new Grammer( id , mid , gkey , gvalue );

                    _grammer.Add( grammer );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #endregion
    }
}