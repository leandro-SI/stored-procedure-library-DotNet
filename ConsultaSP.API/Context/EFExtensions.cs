using ConsultaSP.API.Extensão;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ConsultaSP.API.Context
{
    public static class EFExtensions
    {
        public static DbCommand LoadStoredProcedure( this DbContext context, string storedProcedureName, bool prependDefaultSchema = true, DbTransaction transactionObject = null)
        {
            DbCommand dbCommand = transactionObject == null ? context.Database.GetDbConnection().CreateCommand() : transactionObject.Connection.CreateCommand();
            dbCommand.CommandTimeout = 0;
            if (prependDefaultSchema)
            {
                string defaultSchema = context.Model.GetDefaultSchema();
                if (defaultSchema != null)
                    storedProcedureName = defaultSchema + "." + storedProcedureName;
            }
            dbCommand.CommandText = storedProcedureName;
            dbCommand.CommandType = CommandType.StoredProcedure;
            if (transactionObject != null)
                dbCommand.Transaction = transactionObject;
            return dbCommand;
        }

        public static DbCommand LoadSQL( this DbContext context, string sql, bool prependDefaultSchema = true, DbTransaction transactionObject = null)
        {
            DbCommand dbCommand = transactionObject == null ? context.Database.GetDbConnection().CreateCommand() : transactionObject.Connection.CreateCommand();
            dbCommand.CommandTimeout = 0;
            dbCommand.CommandText = sql;
            dbCommand.CommandType = CommandType.Text;
            if (transactionObject != null)
                dbCommand.Transaction = transactionObject;
            return dbCommand;
        }

        public static DbCommand WithParameterIn( this DbCommand command, string parameterName, object parameterValue, Action<DbParameter> configureParam = null)
        {
            if (string.IsNullOrEmpty(command.CommandText) && command.CommandType != CommandType.StoredProcedure)
                throw new InvalidOperationException("Execute LoadStoredProc antes deste método");
            DbParameter parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            if (parameterValue != null)
                parameter.Value = parameterValue;
            if (configureParam != null)
                configureParam(parameter);
            command.Parameters.Add((object)parameter);
            return command;
        }

        public static DbCommand WithParameterIn( this DbCommand command, string parameterName, Action<DbParameter> configureParam = null)
        {
            return command.WithParameterIn(parameterName, (object)null, configureParam);
        }

        private static DbType getDbType(Type type)
        {
            string name = type.Name;
            DbType dbType = DbType.String;
            try
            {
                dbType = (DbType)Enum.Parse(typeof(DbType), name, true);
            }
            catch (Exception ex)
            {
            }
            return dbType;
        }

        public static DbCommand WithParameterOut( this DbCommand command, string parameterName, Type type, int size = 0, byte precision = 0, byte scale = 0, Action<DbParameter> configureParam = null)
        {
            if (string.IsNullOrEmpty(command.CommandText) && command.CommandType != CommandType.StoredProcedure)
                throw new InvalidOperationException("Execute LoadStoredProc antes deste método");
            DbParameter parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Direction = ParameterDirection.Output;
            parameter.DbType = EFExtensions.getDbType(type);
            if (size != 0)
                parameter.Size = size;
            if (precision != (byte)0)
                parameter.Precision = precision;
            if (scale != (byte)0)
                parameter.Scale = scale;
            if (configureParam != null)
                configureParam(parameter);
            command.Parameters.Add((object)parameter);
            return command;
        }

        private static IList<T> MapToList<T>(this DbDataReader dr)
        {
            List<T> list = new List<T>();
            IEnumerable<PropertyInfo> props = typeof(T).GetRuntimeProperties();
            Dictionary<string, DbColumn> dictionary = dr.GetColumnSchema().Where<DbColumn>((Func<DbColumn, bool>)(x => props.Any<PropertyInfo>((Func<PropertyInfo, bool>)(y => y.Name.ToLower() == x.ColumnName.ToLower())))).ToDictionary<DbColumn, string>((Func<DbColumn, string>)(key => key.ColumnName.ToLower()));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    T instance = Activator.CreateInstance<T>();
                    foreach (PropertyInfo propertyInfo in props)
                    {
                        object obj = dr.GetValue(dictionary[propertyInfo.Name.ToLower()].ColumnOrdinal.Value);
                        propertyInfo.SetValue((object)instance, obj == DBNull.Value ? (object)null : obj);
                    }
                    list.Add(instance);
                }
            }
            return (IList<T>)list;
        }

        public static IList<T> Execute<T>(this DbCommand command)
        {
            using (command)
            {
                if (command.Connection.State == ConnectionState.Closed)
                    command.Connection.Open();
                try
                {
                    using (DbDataReader dr = command.ExecuteReader())
                        return dr.MapToList<T>();
                }
                finally
                {
                    if (command.Transaction == null)
                        command.Connection.Close();
                }
            }
        }

        public static async Task<IList<T>> ExecuteAsync<T>(this DbCommand command)
        {
            IList<T> list;
            using (command)
            {
                int num = 1;
                if (num != 0 && command.Connection.State == ConnectionState.Closed)
                    command.Connection.Open();
                try
                {
                    using (DbDataReader dr = await command.ExecuteReaderAsync())
                        list = dr.MapToList<T>();
                }
                finally
                {
                    if (command.Transaction == null)
                        command.Connection.Close();
                }
            }
            return list;
        }

        public static DbCommand Execute(this DbCommand command)
        {
            if (command.Connection.State == ConnectionState.Closed)
                command.Connection.Open();
            try
            {
                command.ExecuteNonQuery();
                return command;
            }
            finally
            {
                if (command.Transaction == null)
                    command.Connection.Close();
            }
        }

        public static async Task<DbCommand> ExecuteAsync(this DbCommand command)
        {
            int num1 = 1;
            if (num1 != 0 && command.Connection.State == ConnectionState.Closed)
                command.Connection.Open();
            DbCommand dbCommand;
            try
            {
                int num2 = await command.ExecuteNonQueryAsync();
                dbCommand = command;
            }
            finally
            {
                if (command.Transaction == null)
                    command.Connection.Close();
            }
            return dbCommand;
        }

        public static PageResult<T> GetPage<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            PageResult<T> page1 = new PageResult<T>();
            page1.CurrentPage = page;
            page1.PageSize = pageSize;
            page1.RowCount = Queryable.Count<T>(query);
            page1.PageCount = (int)Math.Ceiling((double)page1.RowCount / (double)pageSize);
            int count = (page - 1) * pageSize;
            page1.Results = Queryable.Take<T>(Queryable.Skip<T>(query, count), pageSize).ToList<T>();
            return page1;
        }

    }
}
