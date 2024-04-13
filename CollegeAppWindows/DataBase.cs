using System.Data.SqlClient;

namespace CollegeAppWindows
{
    /// <summary>
    /// Class for working with the database.
    /// </summary>
    internal class DataBase
    {
        private static DataBase? instance;

        private SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-L2NIP6G\MSSQLSERVER01;Initial Catalog=CollegeDB;Integrated Security=true");

        private DataBase() { }

        /// <summary>
        /// Public static method for obtaining a single instance of a class.
        /// </summary>
        public static DataBase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataBase();
                }

                return instance;
            }
        }

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
