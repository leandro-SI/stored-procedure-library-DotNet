using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaSP.API.Extensão
{
    public class DatabaseHelper
    {
        private readonly string _connectionString; // Defina sua string de conexão aqui

        public DatabaseHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BaseConnection");
        }

        public async Task<IList<T>> ExecuteStoredProcedureAsync<T>(string storedProcedureName, IDictionary<string, object> parameters = null) where T : new()
        {
            IList<T> resultList = new List<T>();

            using (DbConnection connection = CreateConnection())
            {
                await connection.OpenAsync();

                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = storedProcedureName;
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        foreach (KeyValuePair<string, object> parameter in parameters)
                        {
                            DbParameter dbParameter = command.CreateParameter();
                            dbParameter.ParameterName = parameter.Key;
                            dbParameter.Value = parameter.Value;
                            command.Parameters.Add(dbParameter);
                        }
                    }

                    using (DbDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            T item = new T();
                            PopulateObject(reader, item);
                            resultList.Add(item);
                        }
                    }
                }

                connection.Close();
            }

            return resultList;
        }

        private DbConnection CreateConnection()
        {
            // Substitua "System.Data.SqlClient" pelo provedor de banco de dados adequado
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection connection = factory.CreateConnection();
            connection.ConnectionString = _connectionString;
            return connection;
        }

        private void PopulateObject<T>(DbDataReader reader, T item) where T : new()
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string columnName = reader.GetName(i);
                if (reader.IsDBNull(i))
                {
                    continue;
                }

                object value = reader.GetValue(i);
                SetProperty(item, columnName, value);
            }
        }

        private void SetProperty<T>(T item, string propertyName, object value)
        {
            Type itemType = item.GetType();
            var property = itemType.GetProperty(propertyName);
            if (property != null && value != DBNull.Value)
            {
                property.SetValue(item, value);
            }
        }


    }
}
