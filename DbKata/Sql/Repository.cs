using Dapper;
using DbKata.Entities;
using DbKata.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DbKata.Sql
{
    public class Repository<TKey, T> : IRepository<TKey, T> where T : IEntity<TKey>
    {
        private readonly ISqlRequestBuilder<T, TKey> _sqlQueryBuilder;
        protected IDbConnection Connection;
        private SqlConnection connection;

        public Repository(IDbConnection connection, ISqlRequestBuilder<T, TKey> requestBuilder)
        {
            Connection = connection;
            _sqlQueryBuilder = requestBuilder;
        }

        public Repository(IDbConnection connection) : this(connection, new SqlRequestBuilder<T, TKey>())
        {
        }

        public IEnumerable<T> GetAll()
        {
            var request = _sqlQueryBuilder.BuildGetAllRequest();
            return Connection.Query<T>(request.Sql, request.Parameters);
        }

        public T GetOne(TKey id)
        {
            var request = _sqlQueryBuilder.BuildGetOneRequest(id);
            return Connection.ExecuteScalar<T>(request.Sql, request.Parameters);
        }

        public TKey Add(T entity)
        {
            var request = _sqlQueryBuilder.BuildAddRequest(entity);
            return Connection.QuerySingle(request.Sql, request.Parameters);

        }

        public bool Update(T entity)
        {
            var request = _sqlQueryBuilder.BuildUpdateRequest(entity);
            return Connection.Execute(request.Sql, request.Parameters) != 0;
        }

        public bool Delete(T entity)
        {
            var request = _sqlQueryBuilder.BuildDeleteRequest(entity);
            return Connection.Execute(request.Sql, request.Parameters) != 0;
        }
    }
}
