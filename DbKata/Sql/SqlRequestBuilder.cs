using Dapper;
using DbKata.Entities;
using DbKata.Utils;
using System.Data;
using System.Linq;

namespace DbKata.Sql
{
    public class SqlRequestBuilder<T, TKey> : ISqlRequestBuilder<T, TKey> where T : IEntity<TKey>
    {
        private readonly string _tableName;

        public SqlRequestBuilder()
        {
            _tableName = typeof(T).GetCustomAttributes(typeof(DbNameAttribute), true).FirstOrDefault() is DbNameAttribute dbCommand
                ? dbCommand.Name
                : typeof(T).Name;
        }

        public (string Sql, DynamicParameters Parameters) BuildGetAllRequest()
        {
            return ($"Select * From {_tableName}", null);
        }

        public (string Sql, DynamicParameters Parameters) BuildGetOneRequest(TKey id)
        {
            var idColumn = default(T).GetDbName(e => e.Id);
            var selectByIdSql = $"Select * From {_tableName} Where {idColumn}=@{idColumn}";
            var parameters = new DynamicParameters();
            parameters.Add($"@{idColumn}", id);
            return (selectByIdSql, parameters);
        }

        public (string Sql, DynamicParameters Parameters) BuildAddRequest(T entity)
        {
            var properties = typeof(T).GetProperties().Select(p => (Name: p.GetDbName(), Info: p));

            var valuesNames = string.Join(", ", properties.Select(p => p.Name));
            var paramsNames = string.Join(", ", properties.Select(p => $"@{p.Name}"));
            var insertSql = $"Insert into {_tableName}({valuesNames}) OUTPUT INSERTED.{entity.GetDbName(e => e.Id)} values({paramsNames})";

            var parameters = new DynamicParameters();
            foreach (var prop in properties)
            {
                parameters.Add($"@{prop.Name}", prop.Info.GetValue(entity));
            }
            return (insertSql, parameters);
        }

        public (string Sql, DynamicParameters Parameters) BuildUpdateRequest(T entity)
        {
            throw new System.NotImplementedException();
        }

        public (string Sql, DynamicParameters Parameters) BuildDeleteRequest(T entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
