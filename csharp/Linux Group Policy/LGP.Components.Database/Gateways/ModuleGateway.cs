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
    internal class ModuleGateway : IModuleGateway
    {
        private static List< IModule > _modules;

        #region Implementation of IModuleGateway


        public IModule CreateModule( string modulename )
        {
            try
            {
                if( _modules == null )
                {
                    BuildModuleListing();
                }
                var sql = string.Format( "insert into modules values( null, '{0}'  )" , modulename );
                if( Framework.Database.IsConnected() == false )
                {
                    return null;
                }

                if( Framework.Database.ExecuteNonQuery( sql ) > 0 )
                {
                    sql = string.Format( "select * from modules where id = ( select MAX( id ) from modules )" );
                    var ds = Framework.Database.ExecuteQuery( sql );
                    var ouTable = ds.Tables[ 0 ];
                    if( ouTable.Rows.Count == 0 )
                    {
                        return null;
                    }
                    var row = ouTable.Rows[ 0 ];
                    var module = new Module( ( int ) row[ "id" ] , ( string ) row[ "name" ] );
                    _modules.Add( module );
                    return _modules[ _modules.Count - 1 ];
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
        ///   Deletes an Module
        /// </summary>
        /// <param name = "modulename">the module to delete</param>
        public void DeleteModule( string modulename )
        {
            try
            {
                if( _modules == null )
                {
                    BuildModuleListing();
                }

                var sql1 = string.Format( "delete from moduleGrammerWords where m_id = '{0}'" , this.GetModuleByName( modulename ).GetModuleId() );
                var sql2 = string.Format( "delete from modules where id = '{0}'" , this.GetModuleByName( modulename ).GetModuleId() );

                if( Framework.Database.IsConnected() )
                {
                    Framework.Database.ExecuteNonQuery( sql1 );
                    Framework.Database.ExecuteNonQuery( sql2 );


                    for( var y = 0; y < _modules.Count; y++ )
                    {
                        if( _modules[ y ].GetModuleName().CompareTo( modulename ) == 0 )
                        {
                            _modules[ y ].Dispose( _modules[ y ] );
                            _modules.RemoveAt( y );
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
        ///   Gets an IModule given a name
        /// </summary>
        /// <param name = "modulename">the module name</param>
        /// <returns>IModule</returns>
        public IModule GetModuleByName( string modulename )
        {
            try
            {
                if( _modules == null )
                {
                    BuildModuleListing();
                }

                for( var y = 0; y < _modules.Count; y++ )
                {
                    if( _modules[ y ].GetModuleName().CompareTo( modulename ) == 0 )
                    {
                        return _modules[ y ];
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
        ///   Get the list of modules in the database
        /// </summary>
        /// <returns>List IModule</returns>
        public List< IModule > GetModules()
        {
            if( _modules == null )
            {
                BuildModuleListing();
            }

            return _modules;
        }

        /// <summary>
        /// refreshes the list
        /// </summary>
        public void Refresh()
        {
            try
            {
                var sql = string.Format( "select * from modules" );

                if( Framework.Database.IsConnected() == false )
                {
                    return;
                }

                var modulesds = Framework.Database.ExecuteQuery( sql );

                var modulesTable = modulesds.Tables[ 0 ];

                if( modulesTable.Rows.Count == 0 )
                {
                    return;
                }

                foreach( DataRow moduleRow in modulesTable.Rows )
                {
                    var newDbEntry = true;
                    var modid = ( int ) moduleRow[ "id" ];

                    for( var y = 0; y < _modules.Count; y++ )
                    {
                        if( _modules[ y ].GetModuleId() != modid )
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

                    var module = new Module( modid , ( string ) moduleRow[ "name" ] );
                    _modules.Add( module );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #endregion

        private static void BuildModuleListing()
        {
            _modules = new List< IModule >();

            try
            {
                var sql = string.Format( "select * from modules" );

                if( Framework.Database.IsConnected() == false )
                {
                    return;
                }

                var modulesds = Framework.Database.ExecuteQuery( sql );

                var modulesTable = modulesds.Tables[ 0 ];

                if( modulesTable.Rows.Count == 0 )
                {
                    return;
                }

                foreach( DataRow moduleRow in modulesTable.Rows )
                {
                    var module = new Module( ( int ) moduleRow[ "id" ] , ( string ) moduleRow[ "name" ] );
                    _modules.Add( module );
                }
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }
    }
}