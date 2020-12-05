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
    internal class OuGateway : IOuGateway
    {
        private static List< IOu > _ous;

        #region IOuGateway Members

        /// <summary>
        ///   Adds a Ou to a given parent ou
        /// </summary>
        /// <param name = "ouName">The new ou name</param>
        /// <param name = "parentId">The new parent id</param>
        /// <returns>IOu</returns>
        public IOu CreateOu( string ouName , int parentId )
        {
            try
            {
                if( _ous == null )
                {
                    BuildOuListing();
                }

                var sql = string.Format( "insert into ous values( null, '{0}' , '{1}' , '' )" , ouName , parentId );

                if( Framework.Database.IsConnected() == false )
                {
                    return null;
                }

                if( Framework.Database.ExecuteNonQuery( sql ) > 0 )
                {
                    sql = string.Format( "select * from ous where ou_id = ( select MAX( ou_id ) from ous )" );

                    var ds = Framework.Database.ExecuteQuery( sql );

                    var ouTable = ds.Tables[ 0 ];

                    if( ouTable.Rows.Count == 0 )
                    {
                        return null;
                    }

                    var row = ouTable.Rows[ 0 ];

                    int parentou;

                    try
                    {
                        parentou = ( int ) row[ "ou_parent_id" ];
                    }
                    catch( Exception error )
                    {
                        Framework.EventBus.Publish( error );
                        parentou = -1;
                    }

                    var ou = new Ou( ( int ) row[ "ou_id" ] , ( string ) row[ "ou_name" ] , parentou , ( string ) row[ "ou_policygroup" ] );

                    _ous.Add( ou );

                    return _ous[ _ous.Count - 1 ];
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
        ///   Deletes an Ou to a given parent ou
        /// </summary>
        /// <param name = "ouid">the ou id to delete</param>
        public void DeleteOu( int ouid )
        {
            try
            {
                if( _ous == null )
                {
                    BuildOuListing();
                }

                var sql = string.Format( "delete from ous where ou_id = '{0}'" , ouid );

                if( Framework.Database.IsConnected() )
                {
                    Framework.Database.ExecuteNonQuery( sql );

                    for( var y = 0; y < _ous.Count; y++ )
                    {
                        if( _ous[ y ].GetOuId() == ouid )
                        {
                            _ous[ y ].Dispose( _ous[ y ] );
                            _ous.RemoveAt( y );
                            break;
                        }
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        /// <summary>
        ///   Gets an Iou given an id
        /// </summary>
        /// <param name = "ouid">int id</param>
        /// <returns>IOu</returns>
        public IOu GetOuById( int ouid )
        {
            try
            {
                if( _ous == null )
                {
                    BuildOuListing();
                }

                for( var y = 0; y < _ous.Count; y++ )
                {
                    if( _ous[ y ].GetOuId() == ouid )
                    {
                        return _ous[ y ];
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
        ///   Gets a parent Iou given an id
        /// </summary>
        /// <param name = "ouid">int id</param>
        /// <returns>IOu</returns>
        public IOu GetOuParentById( int ouid )
        {
            try
            {
                if( _ous == null )
                {
                    BuildOuListing();
                }

                for( var y = 0; y < _ous.Count; y++ )
                {
                    if( _ous[ y ].GetOuId() == ouid )
                    {
                        return this.GetOuById( _ous[ y ].GetParentOuId() );
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
        ///   Gets a listing of Ous
        /// </summary>
        /// <returns>IOu List</returns>
        public List< IOu > GetOuListing()
        {
            if( _ous == null )
            {
                BuildOuListing();
            }

            return _ous;
        }


        /// <summary>
        ///   Get the children of a ou
        /// </summary>
        /// <param name = "ouid">int outid</param>
        /// <returns>IOu List</returns>
        public List< IOu > GetChildren( int ouid )
        {
            try
            {
                if( _ous == null )
                {
                    BuildOuListing();
                }

                var children = new List< IOu >();

                for( var y = 0; y < _ous.Count; y++ )
                {
                    if( _ous[ y ].GetParentOuId() == ouid && _ous[ y ].GetOuId() != _ous[ y ].GetParentOuId() )
                    {
                        children.Add( _ous[ y ] );
                    }
                }

                return children;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return null;
            }
        }


        /// <summary>
        ///   Get the parents with no parents
        /// </summary>
        /// <returns>IOu List</returns>
        public List< IOu > GetRoots()
        {
            try
            {
                if( _ous == null )
                {
                    BuildOuListing();
                }

                var roots = new List< IOu >();

                for( var y = 0; y < _ous.Count; y++ )
                {
                    if( _ous[ y ].GetOuId() == _ous[ y ].GetParentOuId() )
                    {
                        roots.Add( _ous[ y ] );
                    }
                }

                return roots;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return null;
            }
        }

        /// <summary>
        ///   Refreshes the ou list
        /// </summary>
        public void Refresh()
        {
            BuildOuListing();
        }

        #endregion

        private static void BuildOuListing()
        {
            var refresh = true;
            if( _ous == null )
            {
                _ous = new List< IOu >();
                refresh = false;
            }

            try
            {
                var sql = string.Format( "select * from ous" );

                if( Framework.Database.IsConnected() == false )
                {
                    return;
                }

                var ds = Framework.Database.ExecuteQuery( sql );

                var ouTable = ds.Tables[ 0 ];

                if( ouTable.Rows.Count == 0 )
                {
                    return;
                }

                foreach( DataRow row in ouTable.Rows )
                {
                    int parentou;

                    try
                    {
                        parentou = ( int ) row[ "ou_parent_id" ];
                    }
                    catch( Exception error )
                    {
                        Framework.EventBus.Publish( error );
                        parentou = -1;
                    }

                    var ou = new Ou( ( int ) row[ "ou_id" ] , ( string ) row[ "ou_name" ] , parentou , ( string ) row[ "ou_policygroup" ] );

                    if( refresh )
                    {
                        if( Contains( ou ) == false )
                        {
                            _ous.Add( ou );
                        }
                    }
                    else
                    {
                        _ous.Add( ou );
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private static bool Contains( Ou ou )
        {
            foreach( Ou cou in _ous )
            {
                if( cou.GetOuId() == ou.GetOuId() )
                {
                    return true;
                }
            }
            return false;
        }
    }
}