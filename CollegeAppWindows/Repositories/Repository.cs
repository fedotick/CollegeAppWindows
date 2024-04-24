using CollegeAppWindows.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CollegeAppWindows.Repositories
{
    public class Repository<T>
    {
        private bool isView;

        private static Repository<T>? instance;

        private Repository()
        {
             isView = IsView();
             CreateProcedures();
        }

        public static Repository<T> GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Repository<T>();
                }

                return instance;
            }
        }

        public int Add(T model, SqlTransaction? transaction = null)
        {
            int id = 0;
            string query = $"sp_Insert{typeof(T).Name}";

            SqlConnection connection = DataBase.GetInstance.GetConnection();

            DataBase.GetInstance.OpenConnection();

            using (SqlCommand command = transaction == null
                ? new SqlCommand(query, connection)
                : new SqlCommand(query, connection, transaction))
            {
                command.CommandType = CommandType.StoredProcedure;

                SqlUtil.AddValuesToParameters(command, model);
                id = Convert.ToInt32(command.ExecuteScalar());
            }

            if (transaction == null)
            {
                DataBase.GetInstance.CloseConnection();
            }

            return id;
        }

        public List<T> GetAll(SqlTransaction? transaction = null)
        {
            List<T> models = new();

            string query = $"sp_Select{typeof(T).Name}";

            SqlConnection connection = DataBase.GetInstance.GetConnection();

            DataBase.GetInstance.OpenConnection();

            using (SqlCommand command = transaction == null
                ? new SqlCommand(query, connection)
                : new SqlCommand(query, connection, transaction))
            {
                command.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T model = Activator.CreateInstance<T>();
                        SqlUtil.MapValuesFromDataReader(model, reader);
                        models.Add(model);
                    }
                }
            }

            if (transaction == null)
            {
                DataBase.GetInstance.CloseConnection();
            }

            return models;
        }

        public T? GetById(int id, SqlTransaction? transaction = null)
        {
            T model = Activator.CreateInstance<T>();

            string query = $"sp_Select{typeof(T).Name}ById";

            SqlConnection connection = DataBase.GetInstance.GetConnection();

            DataBase.GetInstance.OpenConnection();

            using (SqlCommand command = transaction == null
                ? new SqlCommand(query, connection)
                : new SqlCommand(query, connection, transaction))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        SqlUtil.MapValuesFromDataReader(model, reader);
                    }
                    else
                    {
                        return default;
                    }
                }
            }

            if (transaction == null)
            {
                DataBase.GetInstance.CloseConnection();
            }

            return model;
        }

        public void Update(T model, SqlTransaction? transaction = null)
        {
            string query = $"sp_Update{typeof(T).Name}";

            SqlConnection connection = DataBase.GetInstance.GetConnection();

            DataBase.GetInstance.OpenConnection();

            using (SqlCommand command = transaction == null
                ? new SqlCommand(query, connection)
                : new SqlCommand(query, connection, transaction))
            {
                command.CommandType = CommandType.StoredProcedure;
                SqlUtil.AddValuesToParameters(command, model, false);
                command.ExecuteNonQuery();
            }

            if (transaction == null)
            {
                DataBase.GetInstance.CloseConnection();
            }
        }

        public void DeleteById(int id, SqlTransaction? transaction = null)
        {
            string query = $"sp_Delete{typeof(T).Name}ById";

            SqlConnection connection = DataBase.GetInstance.GetConnection();

            DataBase.GetInstance.OpenConnection();

            using (SqlCommand command = transaction == null
                ? new SqlCommand(query, connection)
                : new SqlCommand(query, connection, transaction))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }

            if (transaction == null)
            {
                DataBase.GetInstance.CloseConnection();
            }
        }

        private bool IsView()
        {
            string tableType = string.Empty;

            string query = $@"
            SELECT TABLE_TYPE
            FROM INFORMATION_SCHEMA.TABLES
            WHERE TABLE_NAME = '{typeof(T).Name}';";

            DataBase.GetInstance.OpenConnection();

            using (SqlCommand command = new SqlCommand(query, DataBase.GetInstance.GetConnection()))
            {
                tableType = command.ExecuteScalar().ToString();
            }

            DataBase.GetInstance.CloseConnection();

            if (tableType == "VIEW")
            {
                return true;
            }

            return false;
        }

        private void CreateProcedures()
        {
            string selectProcedureQuery = SqlUtil.GetSelectProcedureQuery<T>();
            CreateProcedure(selectProcedureQuery);

            string selectProcedureQueryById = SqlUtil.GetSelectProcedureQueryById<T>();
            CreateProcedure(selectProcedureQueryById);

            if (!isView)
            {
                string insertProcedureQuery = SqlUtil.GetInsertProcedureQuery<T>();
                CreateProcedure(insertProcedureQuery);

                string updateProcedureQuery = SqlUtil.GetUpdateProcedureQuery<T>();
                CreateProcedure(updateProcedureQuery); 
            
                string deleteProcedureQueryById = SqlUtil.GetDeleteProcedureQueryById<T>();
                CreateProcedure(deleteProcedureQueryById);
            }
        }

        private void CreateProcedure(string query)
        {
            DataBase.GetInstance.OpenConnection();

            using(SqlCommand command = new SqlCommand(query, DataBase.GetInstance.GetConnection()))
            {
                command.ExecuteNonQuery();
            }

            DataBase.GetInstance.CloseConnection();
        }
    }
}
