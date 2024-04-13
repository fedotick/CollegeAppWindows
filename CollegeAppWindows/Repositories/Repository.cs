using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace CollegeAppWindows.Repositories
{
    public class Repository<T>
    {
        public Repository() { }

        public int Add(T model, SqlTransaction? transaction = null)
        {
            int id = 0;
            string query = GetQueryInsert();
            
            SqlConnection connection = DataBase.Instance.GetConnection();

            DataBase.Instance.OpenConnection();

            using (SqlCommand command = transaction == null
                ? new SqlCommand(query, connection)
                : new SqlCommand(query, connection, transaction))
            {
                AddParameters(command, model);
                id = Convert.ToInt32(command.ExecuteScalar());
            }

            if (transaction == null)
            {
                DataBase.Instance.CloseConnection();
            }

            return id;
        }

        public T? GetById(int id, SqlTransaction? transaction = null)
        {
            T model = Activator.CreateInstance<T>();

            string query = $"SELECT * FROM [{typeof(T).Name}] WHERE Id = @Id";

            SqlConnection connection = DataBase.Instance.GetConnection();

            DataBase.Instance.OpenConnection();

            using (SqlCommand command = transaction == null 
                ? new SqlCommand(query, connection)
                : new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        MapPropertiesFromDataReader(model, reader);
                    }
                    else
                    {
                        return default;
                    }
                }
            }

            if (transaction == null)
            {
                DataBase.Instance.CloseConnection();
            }

            return model;
        }

        public List<T> GetAll(SqlTransaction? transaction = null)
        {
            List<T> models = new();

            string query = $"SELECT * FROM [{typeof(T).Name}]";

            SqlConnection connection = DataBase.Instance.GetConnection();

            DataBase.Instance.OpenConnection();

            using (SqlCommand command = transaction == null
                ? new SqlCommand(query, connection)
                : new SqlCommand(query, connection, transaction))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T model = Activator.CreateInstance<T>();
                        MapPropertiesFromDataReader(model, reader);
                        models.Add(model);
                    }
                }
            }

            if (transaction == null)
            {
                DataBase.Instance.CloseConnection();
            }

            return models;
        }

        public void Update(T model, SqlTransaction? transaction = null)
        {
            string query = GetQueryUpdate();
            
            SqlConnection connection = DataBase.Instance.GetConnection();

            DataBase.Instance.OpenConnection();

            using (SqlCommand command = transaction == null
                ? new SqlCommand(query, connection)
                : new SqlCommand(query, connection, transaction))
            {
                AddParameters(command, model);
                command.ExecuteNonQuery();
            }

            if (transaction == null)
            {
                DataBase.Instance.CloseConnection();
            }
        }

        public void Delete(int id, SqlTransaction? transaction = null)
        {
            string query = $"DELETE FROM [{typeof(T).Name}] WHERE Id = @Id";

            SqlConnection connection = DataBase.Instance.GetConnection();

            DataBase.Instance.OpenConnection();

            using (SqlCommand command = transaction == null
                ? new SqlCommand(query, connection)
                : new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }

            if (transaction == null)
            {
                DataBase.Instance.CloseConnection();
            }
        }

        private string GetQueryInsert()
        {
            StringBuilder columns = new StringBuilder();
            StringBuilder values = new StringBuilder();

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property != properties[0])
                {
                    columns.Append($"{property.Name}, ");
                    values.Append($"@{property.Name}, ");
                }
            }

            columns.Length -= 2;
            values.Length -= 2;

            return $"INSERT INTO [{typeof(T).Name}] ({columns}) VALUES ({values}); SELECT SCOPE_IDENTITY();";
        }

        private string GetQueryUpdate()
        {
            StringBuilder values = new StringBuilder();

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property != properties[0])
                {
                    values.Append($"{property.Name} = @{property.Name}, ");
                }
            }

            values.Length -= 2;

            return $"UPDATE [{typeof(T).Name}] SET {values} WHERE Id = @Id";
        }

        private void AddParameters(SqlCommand command, T model)
        {
            Type modelType = typeof(T);
            PropertyInfo[] properties = modelType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(model));
            }
        }

        private void MapPropertiesFromDataReader(T model, SqlDataReader reader)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                int columnIndex = reader.GetOrdinal(propertyName);
                object value = reader.GetValue(columnIndex);

                property.SetValue(model, value);
            }
        }
    }
}
