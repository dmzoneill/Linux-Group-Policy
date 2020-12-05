#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using LGP.Components.Database.Gateways;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;

#endregion

namespace LGP.Components.Database
{
    /// <summary>
    ///   Creates and manages the strategy context object given type
    /// </summary>
    public class DatabaseHandler : IDatabase
    {
        private static readonly List< Type > DatabaseConnectors;
        private IDatabaseModule _strategyContext;


        static DatabaseHandler()
        {
            DatabaseConnectors = new List< Type >();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DatabaseHandler()
        {
            try
            {
                Framework.ShutDown += this.Terminate;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }
        }

        #region IDatabase Members

        /// <summary>
        ///   Connect method for database interface
        /// </summary>
        /// <param name = "user">Username to use in connection</param>
        /// <param name = "pass">Password to use in connection</param>
        /// <param name = "host">Hostname to use in connection</param>
        /// <param name = "dbname">Datbase to use in connection</param>
        /// <returns></returns>
        public bool Connect( string user , string pass , string host , string dbname )
        {
            return this._strategyContext.Connect( user , pass , host , dbname );
        }


        /// <summary>
        ///   Disconnect method for the interface realizer
        /// </summary>
        /// <returns>bool disconnected</returns>
        public bool Disconnect()
        {
            return this._strategyContext.Disconnect();
        }


        /// <summary>
        ///   Registers a database type
        /// </summary>
        /// <param name = "type"></param>
        public void AddDatabaseType( Type type )
        {
            DatabaseConnectors.Add( type );
        }


        /// <summary>
        ///   Gets the registered DB Types
        /// </summary>
        public List< Type > GetDatabaseTypes()
        {
            return DatabaseConnectors;
        }


        /// <summary>
        ///   Executes a query provided by the appropriate gateway
        /// </summary>
        /// <param name = "sql">sql query</param>
        /// <returns></returns>
        public DataSet ExecuteQuery( string sql )
        {
            return this._strategyContext.ExecuteQuery( sql );
        }


        /// <summary>
        ///   Executes a query provided by the appropriate gateway
        /// </summary>
        /// <param name = "sql">sql query</param>
        /// <returns>bool</returns>
        public int ExecuteNonQuery( string sql )
        {
            return this._strategyContext.ExecuteNonQuery( sql );
        }


        /// <summary>
        ///   Check to see if the database connector is connected
        /// </summary>
        /// <returns>bool</returns>
        public bool IsConnected()
        {
            if( this._strategyContext == null )
            {
                return false;
            }

            return this._strategyContext.IsConnected();
        }


        /// <summary>
        ///   Gets a new Ou Gateway interface
        /// </summary>
        /// <returns>IOuGateway</returns>
        public IOuGateway CreateOuGateway()
        {
            return new OuGateway();
        }


        /// <summary>
        ///   Gets a new client Gateway interface
        /// </summary>
        /// <returns>IOuGateway</returns>
        public IClientGateway CreateClientGateway()
        {
            return new ClientGateway();
        }


        /// <summary>
        ///   Gets a new Module gateway interface
        /// </summary>
        /// <returns>IModuleGateway</returns>
        public IModuleGateway CreateModuleGateway()
        {
            return new ModuleGateway();
        }


        /// <summary>
        ///   Gets a new grammer gateway interface
        /// </summary>
        /// <returns>IGrammerGateway</returns>
        public IGrammerGateway CreateGrammerGateway()
        {
            return new GrammerGateway();
        }


        /// <summary>
        ///   Gets a new policy gateway interface
        /// </summary>
        /// <returns>IGrammerGateway</returns>
        public IPolicyGateway CreatePolicyGateway()
        {
            return new PolicyGateway();
        }

        /// <summary>
        /// Change the strategy type
        /// </summary>
        /// <param name="type"></param>
        public IDatabase ChangeStrategy( Type type )
        {
            try
            {
                var obj = Activator.CreateInstance( type );
                this._strategyContext = ( IDatabaseModule ) obj;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
            }

            return this;
        }

        #endregion

        private void Terminate( object sender , CancelEventArgs args )
        {
            try
            {
                this.Disconnect();
            }
            catch( Exception )
            {
            }
        }
    }
}