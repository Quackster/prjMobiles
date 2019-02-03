using log4net;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;

namespace Squirtle.Storage
{
    class Database
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static Database _database;

        /// <summary>
        /// Constructor for Database.
        /// </summary>
        public Database()
        {

        }

        /// <summary>
        /// Retrieves a MySQL connection
        /// </summary>
        /// <returns>returns the mysql connection</returns>
        public IDbConnection GetConnection(bool openConnection = true)
        {
            MySqlConnectionStringBuilder connectionString = new MySqlConnectionStringBuilder();
            connectionString.Server = "localhost";
            connectionString.UserID = "root";
            connectionString.Password = File.ReadAllText("password.db");
            connectionString.Database = "squirtle";

            var dbConnection = new MySqlConnection(connectionString.ToString());

            if (openConnection)
                dbConnection.Open();

            return dbConnection;
        }

        /// <summary>
        /// Get whether the connection is successful
        /// </summary>
        /// <returns>true, if successful</returns>
        public bool HasConnection(ref Exception exception)
        {
            try
            {
                GetConnection(true).Close();
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        /// <summary>
        /// Invoke the singleton instance
        /// </summary>
        public static Database Instance()
        {
            if (_database == null)
                _database = new Database();

            return _database;
        }
    }
}
