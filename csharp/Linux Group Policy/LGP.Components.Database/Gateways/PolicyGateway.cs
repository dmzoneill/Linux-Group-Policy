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
    internal class PolicyGateway : IPolicyGateway
    {
        private static readonly IOuGateway OuGateway;
        private static List< IPolicy > _policies;

        static PolicyGateway()
        {
            OuGateway = Framework.Database.CreateOuGateway();
        }

        #region Implementation of IPolicyGateway

        /// <summary>
        ///   Create a policy for a given ou
        /// </summary>
        /// <param name = "ou">the associated ou</param>
        /// <param name="defaultPolicy">default policy</param>
        /// <returns>IPolicy</returns>
        public IPolicy CreatePolicy( IOu ou , string defaultPolicy )
        {
            try
            {
                if( _policies == null )
                {
                    BuildPolicyListing();
                }

                var now = DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss" );

                var sql = string.Format( "insert into policy values( null, '{1}' , '{1}' ,'1' ,'{2}' ,'{0}' ,''  )" , ou.GetOuId() , now , Framework.Utils.Base64Encode( defaultPolicy ) );

                if( Framework.Database.IsConnected() == false )
                {
                    return null;
                }

                if( Framework.Database.ExecuteNonQuery( sql ) > 0 )
                {
                    var sql2 = string.Format( "select * from policy where p_id = ( select MAX(p_id) from policy )" );

                    var policies = Framework.Database.ExecuteQuery( sql2 );

                    var policiesTable = policies.Tables[ 0 ];

                    if( policiesTable.Rows.Count == 0 )
                    {
                        return null;
                    }

                    var row = policiesTable.Rows[ 0 ];

                    var pid = ( int ) row[ "p_id" ];
                    var created = ( string ) row[ "p_created" ];
                    var edited = ( string ) row[ "p_edited" ];
                    const int enabled = 1;
                    var dsl = defaultPolicy;
                    var ouid = ou.GetOuId();
                    const string version = "";
                    var policy = new Policy( pid , created , edited , enabled , dsl , ouid , version );
                    _policies.Add( policy );

                    return _policies[ _policies.Count - 1 ];
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
        ///   Deletes a policy
        /// </summary>
        /// <param name = "ou">IOu</param>
        public void DeletePolicy( IOu ou )
        {
            try
            {
                if( _policies == null )
                {
                    BuildPolicyListing();
                }

                var sql = string.Format( "delete from policy where p_ou_id='{0}'" , ou.GetOuId() );

                if( Framework.Database.IsConnected() == false )
                {
                    return;
                }

                Framework.Database.ExecuteNonQuery( sql );

                for( var h = 0; h < _policies.Count; h++ )
                {
                    if( _policies[ h ].GetOuId() == ou.GetOuId() )
                    {
                        _policies.RemoveAt( h );
                        return;
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Deletes a policy
        /// </summary>
        /// <param name = "policy">IPolicy</param>
        public void DeletePolicy( IPolicy policy )
        {
            try
            {
                if( _policies == null )
                {
                    BuildPolicyListing();
                }

                var sql = string.Format( "delete from policy where p_id='{0}'" , policy.GetId() );

                if( Framework.Database.IsConnected() == false )
                {
                    return;
                }

                Framework.Database.ExecuteNonQuery( sql );

                _policies.Remove( policy );
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Deletes a policy
        /// </summary>
        /// <param name = "policyid">policy id</param>
        public void DeletePolicy( int policyid )
        {
            try
            {
                if( _policies == null )
                {
                    BuildPolicyListing();
                }

                for( var h = 0; h < _policies.Count; h++ )
                {
                    if( _policies[ h ].GetId() == policyid )
                    {
                        var sql = string.Format( "delete from policy where p_id='{0}'" , policyid );

                        if( Framework.Database.IsConnected() == false )
                        {
                            return;
                        }

                        Framework.Database.ExecuteNonQuery( sql );

                        _policies.RemoveAt( h );
                        return;
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        /// <summary>
        ///   Gets a policy
        /// </summary>
        /// <param name = "id">policy id</param>
        /// <returns>IPolicy</returns>
        public IPolicy GetPolicy( int id )
        {
            try
            {
                if( _policies == null )
                {
                    BuildPolicyListing();
                }

                for( var h = 0; h < _policies.Count; h++ )
                {
                    if( _policies[ h ].GetId() == id )
                    {
                        return _policies[ h ];
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }

            return null;
        }

        /// <summary>
        ///   Gets a policy
        /// </summary>
        /// <param name = "ou">IOu</param>
        /// <returns>IPolicy</returns>
        public IPolicy GetPolicy( IOu ou )
        {
            try
            {
                if( _policies == null )
                {
                    BuildPolicyListing();
                }

                for( var h = 0; h < _policies.Count; h++ )
                {
                    if( _policies[ h ].GetOuId() == ou.GetOuId() )
                    {
                        return _policies[ h ];
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }

            return null;
        }

        /// <summary>
        ///   Gets an predecessor policy
        /// </summary>
        /// <param name = "policy">IPolicy</param>
        /// <returns>IPolicy</returns>
        public IPolicy GetPredecessorPolicy( IPolicy policy )
        {
            try
            {
                if( _policies == null )
                {
                    BuildPolicyListing();
                }

                var children = OuGateway.GetChildren( policy.GetOuId() );

                if( children == null )
                {
                    return null;
                }

                foreach( var iou in children )
                {
                    for( var h = 0; h < _policies.Count; h++ )
                    {
                        if( _policies[ h ].GetOuId() == iou.GetOuId() )
                        {
                            return _policies[ h ];
                        }
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }

            return null;
        }

        /// <summary>
        ///   Gets an predecessor policy
        /// </summary>
        /// <param name = "ou">IOu</param>
        /// <returns>IPolicy</returns>
        public IPolicy GetPredecessorPolicy( IOu ou )
        {
            try
            {
                if( _policies == null )
                {
                    BuildPolicyListing();
                }

                var children = OuGateway.GetChildren( ou.GetOuId() );

                if( children == null )
                {
                    return null;
                }

                foreach( var iou in children )
                {
                    for( var h = 0; h < _policies.Count; h++ )
                    {
                        if( _policies[ h ].GetOuId() == iou.GetOuId() )
                        {
                            return _policies[ h ];
                        }
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }

            return null;
        }

        /// <summary>
        ///   Gets an ancestor policy
        /// </summary>
        /// <param name = "policy">IPolicy</param>
        /// <returns>IPolicy</returns>
        public IPolicy GetAncestorPolicy( IPolicy policy )
        {
            try
            {
                if( _policies == null )
                {
                    BuildPolicyListing();
                }

                var parentOu = OuGateway.GetOuParentById( policy.GetOuId() );

                if( parentOu == null )
                {
                    return null;
                }

                for( var h = 0; h < _policies.Count; h++ )
                {
                    if( _policies[ h ].GetOuId() == parentOu.GetOuId() )
                    {
                        return _policies[ h ];
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }

            return null;
        }

        /// <summary>
        ///   Gets an ancestor policy
        /// </summary>
        /// <param name = "ou">IOu</param>
        /// <returns>IPolicy</returns>
        public IPolicy GetAncestorPolicy( IOu ou )
        {
            try
            {
                if( _policies == null )
                {
                    BuildPolicyListing();
                }

                var parentou = ou.GetParentOuId();

                for( var h = 0; h < _policies.Count; h++ )
                {
                    if( _policies[ h ].GetOuId() == parentou )
                    {
                        return _policies[ h ];
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }

            return null;
        }

        /// <summary>
        ///   Gets a list of all policies
        /// </summary>
        /// <returns>List IPolicy</returns>
        public List< IPolicy > GetPolicies()
        {
            if( _policies == null )
            {
                BuildPolicyListing();
            }

            return _policies;
        }

        /// <summary>
        ///   Get the policy in a given ou
        /// </summary>
        /// <param name = "ou">IOu</param>
        /// <returns>IPolicy</returns>
        public IPolicy GetPolicyByOu( IOu ou )
        {
            try
            {
                if( _policies == null )
                {
                    BuildPolicyListing();
                }

                for( var h = 0; h < _policies.Count; h++ )
                {
                    if( _policies[ h ].GetOuId() == ou.GetOuId() )
                    {
                        return _policies[ h ];
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }

            return null;
        }

        /// <summary>
        /// Refreshes the list
        /// </summary>
        public void Refresh()
        {
            try
            {
                var sql = string.Format( "select * from policy" );

                if( Framework.Database.IsConnected() == false )
                {
                    return;
                }

                var policies = Framework.Database.ExecuteQuery( sql );

                var policiesTable = policies.Tables[ 0 ];

                if( policiesTable.Rows.Count == 0 )
                {
                    return;
                }
                foreach( DataRow policyRow in policiesTable.Rows )
                {
                    var pid = ( int ) policyRow[ "p_id" ];
                    var created = ( string ) policyRow[ "p_created" ];
                    var edited = ( string ) policyRow[ "p_edited" ];
                    var enabled = ( int ) policyRow[ "p_enabled" ];
                    var dsl = ( string ) policyRow[ "p_dsl" ];
                    var ouid = ( int ) policyRow[ "p_ou_id" ];
                    var version = ( string ) policyRow[ "p_version" ];


                    var newDbEntry = true;

                    for( var y = 0; y < _policies.Count; y++ )
                    {
                        if( _policies[ y ].GetId() != pid )
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

                    var policy = new Policy( pid , created , edited , enabled , dsl , ouid , version );
                    _policies.Add( policy );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }


        private static void BuildPolicyListing()
        {
            _policies = new List< IPolicy >();

            try
            {
                var sql = string.Format( "select * from policy" );

                if( Framework.Database.IsConnected() == false )
                {
                    return;
                }

                var policies = Framework.Database.ExecuteQuery( sql );

                var policiesTable = policies.Tables[ 0 ];

                if( policiesTable.Rows.Count == 0 )
                {
                    return;
                }

                foreach( DataRow policyRow in policiesTable.Rows )
                {
                    var pid = ( int ) policyRow[ "p_id" ];
                    var created = ( string ) policyRow[ "p_created" ];
                    var edited = ( string ) policyRow[ "p_edited" ];
                    var enabled = ( int ) policyRow[ "p_enabled" ];
                    var dsl = ( string ) policyRow[ "p_dsl" ];
                    var ouid = ( int ) policyRow[ "p_ou_id" ];
                    var version = ( string ) policyRow[ "p_version" ];
                    var policy = new Policy( pid , created , edited , enabled , dsl , ouid , version );
                    _policies.Add( policy );
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