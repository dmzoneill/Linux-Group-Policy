#region

using System.Data;
using LGP.Components.Factory.Interfaces.Database;

#endregion

namespace LGP.Components.Database.Postgresql
{
    /// <summary>
    ///   Postgresql wrapper
    /// </summary>
    public class PostgreDb : IDatabaseModule
    {
        #region IDatabaseModule Members

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
            return true;
        }


        /// <summary>
        ///   Disconnect method for the interface realizer
        /// </summary>
        /// <returns>bool disconnected</returns>
        public bool Disconnect()
        {
            return true;
        }


        /// <summary>
        ///   Executes a query provided by the appropriate gateway
        /// </summary>
        /// <param name = "sql">sql query</param>
        /// <returns></returns>
        public DataSet ExecuteQuery( string sql )
        {
            return null;
        }


        /// <summary>
        ///   Executes a query provided by the appropriate gateway
        /// </summary>
        /// <param name = "sql">sql query</param>
        /// <returns>int num rows affected</returns>
        public int ExecuteNonQuery( string sql )
        {
            return -1;
        }


        /// <summary>
        ///   Check to see if the database is connected
        /// </summary>
        /// <returns>bool</returns>
        public bool IsConnected()
        {
            return false;
        }

        #endregion
    }
}