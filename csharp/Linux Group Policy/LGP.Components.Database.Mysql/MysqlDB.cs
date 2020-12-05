#region

using System;
using System.Data;
using LGP.Components.Factory;
using LGP.Components.Factory.Interfaces.Database;
using MySql.Data.MySqlClient;

#endregion

namespace LGP.Components.Database.Mysql
{
    /// <summary>
    ///   Mysql Wrapper
    /// </summary>
    public class MysqlDb : IDatabaseModule
    {
        private MySqlConnection _connection;

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
            this._connection = new MySqlConnection
            {
                ConnectionString = string.Format( "Server={0};Database={3};Uid={1};Pwd={2};" , host , user , pass , dbname )
            };
            try
            {
                this._connection.Open();
                return true;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return false;
            }
        }


        /// <summary>
        ///   Disconnect method for the interface realizer
        /// </summary>
        /// <returns>bool disconnected</returns>
        public bool Disconnect()
        {
            try
            {
                this._connection.Close();
                return true;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return false;
            }
        }


        /// <summary>
        ///   Executes a query provided by the appropriate gateway
        /// </summary>
        /// <param name = "sql">sql query</param>
        /// <returns></returns>
        public DataSet ExecuteQuery( string sql )
        {
            try
            {
                var ds = new DataSet();
                var myAdapter = new MySqlDataAdapter( sql , this._connection );
                myAdapter.Fill( ds );
                return ds;
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return null;
            }
        }


        /// <summary>
        ///   Executes a query provided by the appropriate gateway
        /// </summary>
        /// <param name = "sql">sql query</param>
        /// <returns>int num rows affected</returns>
        public int ExecuteNonQuery( string sql )
        {
            try
            {
                var command = new MySqlCommand( sql , this._connection );
                return command.ExecuteNonQuery();
            }
            catch( Exception error )
            {
                Framework.EventBus.Publish( error );
                return -1;
            }
        }


        /// <summary>
        ///   Check to see if the database is connected
        /// </summary>
        /// <returns>bool</returns>
        public bool IsConnected()
        {
            if( this._connection != null )
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}