#region

using System.Data;

#endregion

namespace LGP.Components.Factory.Interfaces.Database
{
    /// <summary>
    ///   Database interface
    /// </summary>
    public interface IDatabaseModule
    {
        /// <summary>
        ///   Connect method for database interface
        /// </summary>
        /// <param name = "user">Username to use in connection</param>
        /// <param name = "pass">Password to use in connection</param>
        /// <param name = "host">Hostname to use in connection</param>
        /// <param name = "dbname">Datbase to use in connection</param>
        /// <returns></returns>
        bool Connect( string user , string pass , string host , string dbname );


        /// <summary>
        ///   Disconnect method for the interface realizer
        /// </summary>
        /// <returns>bool disconnected</returns>
        bool Disconnect();


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
    }
}