using Dapper;
using DbKata.Entities;

namespace DbKata.Sql
{
    public interface ISqlRequestBuilder<T, TKey> where T : IEntity<TKey>
    {
        (string Sql, DynamicParameters Parameters) BuildAddRequest(T entity);
        (string Sql, DynamicParameters Parameters) BuildGetAllRequest();
        (string Sql, DynamicParameters Parameters) BuildGetOneRequest(TKey id);
        (string Sql, DynamicParameters Parameters) BuildUpdateRequest(T entity);
        (string Sql, DynamicParameters Parameters) BuildDeleteRequest(T entity);
    }
}