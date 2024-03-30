using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeAppWindows
{
    /// <summary>
    /// Class for working with the database.
    /// </summary>
    internal class DataBase
    {
        private SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-L2NIP6G\MSSQLSERVER01;Initial Catalog=CollegeDB;Integrated Security=true");

        /// <summary>
        /// Opens the connection to the database if it's closed.
        /// </summary>
        public void OpenConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        /// <summary>
        /// Closes the connection to the database if it's open.
        /// </summary>
        public void CloseConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        /// <summary>
        /// Returns the SqlConnection object for working with the database.
        /// </summary>
        /// <returns>The SqlConnection object.</returns>
        public SqlConnection GetConnection()
        {
            return sqlConnection;
        }
    }
}
