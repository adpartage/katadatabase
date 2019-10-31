using Dapper;
using Dapper.FluentMap;
using DbKata.Entities;
using DbKata.Sql;
using System.Collections.Generic;
using System.Data;

namespace DbKata.Repositories
{
    public class Repository<TKey, T> : IRepository<TKey, T> where T : class, IEntity<TKey>
    {
        private readonly ISqlRequestBuilder<T, TKey> _sqlQueryBuilder;
        private IDbConnection _connection;

        static Repository()
        {
            FluentMapper.Initialize(c => c.AddMap(new EntityMapper<T>()));
        }

        public Repository(IDbConnection connection, ISqlRequestBuilder<T, TKey> requestBuilder)
        {
            _connection = connection;
            _sqlQueryBuilder = requestBuilder;
        }

        public IEnumerable<T> GetAll()
        {
            var request = _sqlQueryBuilder.BuildGetAllRequest();
            return _connection.Query<T>(request.Sql, request.Parameters);
        }

        public T GetOne(TKey id)
        {
            var request = _sqlQueryBuilder.BuildGetOneRequest(id);
            return _connection.QuerySingleOrDefault<T>(request.Sql, request.Parameters);
        }

        public TKey Add(T entity)
        {
            var (sql, parameters) = _sqlQueryBuilder.BuildAddRequest(entity);
            return _connection.QuerySingleOrDefault<TKey>(sql, parameters);
        }

        public bool Update(T entity)
        {
            var request = _sqlQueryBuilder.BuildUpdateRequest(entity);
            return _connection.Execute(request.Sql, request.Parameters) != 0;
        }

        public bool Delete(T entity)
        {
            var request = _sqlQueryBuilder.BuildDeleteRequest(entity.Id);
            return _connection.Execute(request.Sql, request.Parameters) != 0;
        }
    }
}
