#region

using System;
using System.Collections.Generic;
using System.Data;

#endregion

namespace LGP.Components.Factory.Interfaces.Database
{
    /// <summary>
    ///   Database interface
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// Change the strategy type
        /// </summary>
        /// <param name="type"></param>
        IDatabase ChangeStrategy(Type type);

        /// <summary>
        ///   Connect method for database interface
        /// </summary>
        /// <param name = "user">Username to use in connection</param>
        /// <param name = "pass">Password to use in connection</param>
        /// <param name = "host">Hostname to use in connection</param>
        /// <param name = "dbname">Database to use in connection</param>
        /// <returns></returns>
        bool Connect( string user , string pass , string host , string dbname );


        /// <summary>
        ///   Disconnect method for the interface realizer
        /// </summary>
        /// <returns>bool disconnected</returns>
        bool Disconnect();


        /// <summary>
        ///   Registers a database type
        /// </summary>
        /// <param name = "type"></param>
        void AddDatabaseType( Type type );


        /// <summary>
        ///   Gets the registered DB Types
        /// </summary>
        List< Type > GetDatabaseTypes();


        /// <summary>
        ///   Executes a query provided by the appropriate gateway
        /// </summary>
        /// <param name = "sql">sql query</param>
        /// <returns></returns>
        DataSet ExecuteQuery( string sql );


        /// <summary>
        ///   Executes a query provided by the appropriate gateway
        /// </summary>
        /// <param name = "sql">sql query</param>
        /// <returns>bool</returns>
        int ExecuteNonQuery( string sql );


        /// <summary>
        ///   Check to see if the database connector is connected
        /// </summary>
        /// <returns>bool</returns>
        bool IsConnected();


        /// <summary>
        ///   Gets a new Ou Gateway interface
        /// </summary>
        /// <returns>IOuGateway</returns>
        IOuGateway CreateOuGateway();


        /// <summary>
        ///   Gets a new Client Gateway interface
        /// </summary>
        /// <returns>IClientGateway</returns>
        IClientGateway CreateClientGateway();


        /// <summary>
        ///   Gets a new Module gateway interface
        /// </summary>
        /// <returns>IModuleGateway</returns>
        IModuleGateway CreateModuleGateway();


        /// <summary>
        ///   Gets a new grammer Gateway interface
        /// </summary>
        /// <returns>IGrammerGateway</returns>
        IGrammerGateway CreateGrammerGateway();


        /// <summary>
        ///   Gets a new policy gateway interface
        /// </summary>
        /// <returns>IPolicyGateway</returns>
        IPolicyGateway CreatePolicyGateway();
    }
}