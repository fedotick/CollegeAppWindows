using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Windows;

namespace CollegeAppWindows.Utilities
{
    public class SqlUtil
    {
        public class COLUMN
        {
            public string COLUMN_NAME { get; set; }
            public string DATA_TYPE { get; set; }
            public int? CHARACTER_MAXIMUM_LENGTH { get; set; }
        }

        public static List<COLUMN> GetColumns<T>()
        {
            List<COLUMN> columns = new List<COLUMN>();

            string query = $@"
            SELECT 
                COLUMN_NAME,
                DATA_TYPE,
                CHARACTER_MAXIMUM_LENGTH
            FROM INFORMATION_SCHEMA.COLUMNS 
            WHERE TABLE_NAME = '{typeof(T).Name}';";

            DataBase.GetInstance.OpenConnection();

            try
            {
                using (SqlCommand command = new SqlCommand(query, DataBase.GetInstance.GetConnection()))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            COLUMN column = new COLUMN();
                            MapValuesFromDataReader(column, reader);
                            columns.Add(column);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            DataBase.GetInstance.CloseConnection();

            return columns;
        }

        public static string GetParameters(List<COLUMN> columns)
        {
            StringBuilder parameters = new StringBuilder();

            foreach (COLUMN column in columns)
            {
                string dataType = column.DATA_TYPE;
                if (dataType == "varchar")
                {
                    dataType += $"({column.CHARACTER_MAXIMUM_LENGTH})";
                }
                parameters.Append($"@{column.COLUMN_NAME} {dataType}, ");
            }

            parameters.Length -= 2;

            return parameters.ToString();
        }

        public static string GetInsertQuery<T>()
        {
            StringBuilder columns = new StringBuilder();
            StringBuilder values = new StringBuilder();

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property != properties[0])
                {
                    columns.Append($"[{property.Name}], ");
                    values.Append($"@{property.Name}, ");
                }
            }

            columns.Length -= 2;
            values.Length -= 2;

            return $@"
            INSERT INTO [{typeof(T).Name}] ({columns}) 
            VALUES ({values}); 
            SELECT SCOPE_IDENTITY();";
        }

        public static string GetSelectQuery<T>()
        {
            return $@"
            SELECT * 
            FROM [{typeof(T).Name}];";
        }

        public static string GetSelectQueryById<T>()
        {
            return $@"
            SELECT * 
            FROM [{typeof(T).Name}]
            WHERE Id = @Id;";
        }

        public static string GetUpdateQuery<T>()
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

            return $@"
            UPDATE [{typeof(T).Name}] 
            SET {values} 
            WHERE Id = @Id";
        }

        public static string GetDeleteQueryById<T>()
        {
            return $@"
            DELETE 
            FROM [{typeof(T).Name}] 
            WHERE Id = @Id;";
        }

        public static string GetInsertProcedureQuery<T>()
        {
            List<COLUMN> columns = GetColumns<T>();
            columns.RemoveAt(0);

            string procedureName = $"Insert{typeof(T).Name}";
            string parameters = GetParameters(columns);
            string insertQuery = GetInsertQuery<T>();

            string query = GetCreateOrAlterProcedureQuery(procedureName, parameters, insertQuery);

            return query;
        }

        public static string GetSelectProcedureQuery<T>()
        {
            string procedureName = $"Select{typeof(T).Name}";
            string parameters = "";
            string selectQuery = GetSelectQuery<T>();

            string query = GetCreateOrAlterProcedureQuery(procedureName,parameters, selectQuery);
            
            return query;
        }

        public static string GetSelectProcedureQueryById<T>()
        {
            string procedureName = $"Select{typeof(T).Name}ById";
            string parameters = "@Id INT";
            string selectQuery = GetSelectQueryById<T>();

            string query = GetCreateOrAlterProcedureQuery(procedureName, parameters, selectQuery);

            return query;
        }

        public static string GetUpdateProcedureQuery<T>()
        {
            List<COLUMN> columns = GetColumns<T>();

            string procedureName = $"Update{typeof(T).Name}";
            string parameters = GetParameters(columns);
            string updateQuery = GetUpdateQuery<T>();

            string query = GetCreateOrAlterProcedureQuery(procedureName, parameters, updateQuery);

            return query;
        }

        public static string GetDeleteProcedureQueryById<T>()
        {
            string procedureName = $"Delete{typeof(T).Name}ById";
            string parameters = "@Id INT";
            string deleteQueryById = GetDeleteQueryById<T>();

            string query = GetCreateOrAlterProcedureQuery(procedureName, parameters, deleteQueryById);

            return query;
        }

        public static string GetCreateOrAlterProcedureQuery(string procedureName, string parameters, string statements)
        {
            string query = $@"
            CREATE OR ALTER PROCEDURE sp_{procedureName}
                {parameters}
            AS
            BEGIN
                {statements}
            END;";

            return query;
        }

        public static void AddValuesToParameters<T>(SqlCommand command, T model, bool isInsert = true)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property != properties[0] || !isInsert)
                {
                    command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(model));
                }
            }
        }

        public static void MapValuesFromDataReader<T>(T model, SqlDataReader reader)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                int columnIndex = reader.GetOrdinal(propertyName);
                object? value = reader.GetValue(columnIndex);

                if (Convert.IsDBNull(value)) value = null;

                property.SetValue(model, value);
            }
        }

        public static bool SaveBackUp(string path)
        {
            string initialCatalog = DataBase.GetInstance.GetInitialCatalog();

            string query = $@"
            BACKUP DATABASE [{initialCatalog}] 
            TO DISK = N'{path}' 
            WITH NOFORMAT, NOINIT,  
            NAME = N'{initialCatalog}-Full Database Backup', 
            SKIP, NOREWIND, NOUNLOAD,  STATS = 10";

            DataBase.GetInstance.OpenConnection();

            using (SqlCommand command = new SqlCommand(query, DataBase.GetInstance.GetConnection()))
            {
                try
                {
                    command.ExecuteNonQuery();
                    return true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
                finally
                {
                    DataBase.GetInstance.CloseConnection();
                }
            }
        }

        public static bool LoadBackUp(string path)
        {
            string initialCatalog = DataBase.GetInstance.GetInitialCatalog();

            string query = $@"
            USE master;
            ALTER DATABASE [{initialCatalog}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
            RESTORE DATABASE [{initialCatalog}] 
            FROM DISK = N'{path}' 
            WITH REPLACE;
            ALTER DATABASE [{initialCatalog}] SET MULTI_USER;";

            DataBase.GetInstance.OpenConnection();

            try
            {
                SqlCommand command = new SqlCommand(query, DataBase.GetInstance.GetConnection());
                command.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                DataBase.GetInstance.CloseConnection();
            }
        }
    }
}
