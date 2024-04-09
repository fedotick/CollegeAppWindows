using CollegeAppWindows.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CollegeAppWindows.Repositories
{
    public class Repository<T>
    {
        private readonly SqlConnection _connection;

        public Repository(SqlConnection connection)
        {
            _connection = connection;
        }

        public int Add(T entity)
        {
            int id = 0;
            string query = GetQueryInsert();
            
            try
            {
                using (SqlCommand command = new SqlCommand(query, _connection))
                {
                    AddParameters(command, entity);
                    _connection.Open();
                    id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error adding entry: {e.Message}");
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }

            return id;
        }

        public T GetById(int id)
        {
            T entity = Activator.CreateInstance<T>();

            string query = $"SELECT * FROM [{typeof(T).Name}] WHERE Id = @Id";

            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    _connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            MapPropertiesFromDataReader(entity, reader);
                        }
                        else
                        {
                            return default(T);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Error receiving record: {e.Message}");
                }
                finally
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }

            return entity;
        }

        public List<T> GetAll()
        {
            List<T> entities = new List<T>();

            string query = $"SELECT * FROM [{typeof(T).Name}]";

            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                try
                {
                    _connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            T entity = Activator.CreateInstance<T>();

                            MapPropertiesFromDataReader(entity, reader);

                            entities.Add(entity);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Error retrieving records: {e.Message}");
                }
                finally
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }

            return entities;
        }

        public void Update(T entity)
        {
            string query = GetQueryUpdate();

            try
            {
                using (SqlCommand command = new SqlCommand(query, _connection))
                {
                    AddParameters(command, entity);
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error updating record: {e.Message}");
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        public void Delete(int id)
        {

            string query = $"DELETE FROM [{typeof(T).Name}] WHERE Id = @Id";

            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Error deleting entry: {e.Message}");
                }
                finally
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
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

        private void AddParameters(SqlCommand command, T entity)
        {
            Type entityType = typeof(T);
            PropertyInfo[] properties = entityType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(entity));
            }
        }

        private void MapPropertiesFromDataReader(T entity, SqlDataReader reader)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                int columnIndex = reader.GetOrdinal(propertyName);
                object value = reader.GetValue(columnIndex);

                property.SetValue(entity, value);
            }
        }
    }
}
