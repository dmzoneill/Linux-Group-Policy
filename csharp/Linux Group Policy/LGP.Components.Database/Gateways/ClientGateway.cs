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
    internal class ClientGateway : IClientGateway
    {
        private static List< IClient > _clients;

        #region IClientGateway Members

        /// <summary>
        ///   Gets a client by id
        /// </summary>
        /// <param name = "id">client id</param>
        /// <returns>IClient</returns>
        public IClient GetClientById( int id )
        {
            try
            {
                if( _clients == null )
                {
                    BuildClientListing();
                }

                for( var y = 0; y < _clients.Count; y++ )
                {
                    if( _clients[ y ].GetId() == id )
                    {
                        return _clients[ y ];
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
        ///   Get clients from a given ou
        /// </summary>
        /// <param name = "ouid">id of the ou</param>
        /// <returns>List of clients</returns>
        public List< IClient > GetOuClients( int ouid )
        {
            try
            {
                if( _clients == null )
                {
                    BuildClientListing();
                }

                var children = new List< IClient >();

                for( var y = 0; y < _clients.Count; y++ )
                {
                    if( _clients[ y ].GetouId() == ouid )
                    {
                        children.Add( _clients[ y ] );
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
        ///   Refresh the client listing
        /// </summary>
        public void Refresh()
        {
            BuildClientListing();
        }

        #endregion

        private static void BuildClientListing()
        {
            var refresh = true;
            if( _clients == null )
            {
                _clients = new List< IClient >();
                refresh = false;
            }

            try
            {
                var sql = string.Format( "select * from clients" );

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
                    var cid = ( int ) row[ "c_id" ];
                    var cou = ( int ) row[ "c_ou_id" ];
                    var guid = ( string ) row[ "c_guid" ];
                    var name = ( string ) row[ "c_name" ];
                    var os = ( string ) row[ "c_os" ];
                    var arch = ( string ) row[ "c_arch" ];
                    var kernel = ( string ) row[ "c_kernel" ];
                    var pseudoName = ( string ) row[ "c_psuedoname" ];
                    var distro = ( string ) row[ "c_distribution" ];
                    var distroVersion = ( string ) row[ "c_distributionVersion" ];
                    var lastseen = ( string ) row[ "c_lastseen" ];
                    var binded = ( string ) row[ "c_binded" ];
                    var clientVersion = ( string ) row[ "c_clientversion" ];
                    var ip = ( string ) row[ "c_ipaddress" ];
                    var port = ( string ) row[ "c_port" ];

                    var client = new Client( cid , cou , guid , name , os , arch , kernel , pseudoName , distro , distroVersion , lastseen , binded , clientVersion , ip , port );

                    if( refresh )
                    {
                        if( Contains( client ) == false )
                        {
                            _clients.Add( client );
                        }
                    }
                    else
                    {
                        _clients.Add( client );
                    }
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        private static bool Contains( IClient client )
        {
            foreach( Client cli in _clients )
            {
                if( cli.GetId() == client.GetId() )
                {
                    return true;
                }
            }
            return false;
        }
    }
}