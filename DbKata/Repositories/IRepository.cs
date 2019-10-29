using DbKata.Entities;
using System.Collections.Generic;

namespace DbKata.Repositories
{
    public interface IRepository<TKey, T> where T : IEntity<TKey>
    {
        IEnumerable<T> GetAll();
        T GetOne(TKey id);
        TKey Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}