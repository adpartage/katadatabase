using System.Collections.Generic;

namespace Contracts
{
    public interface IRepository<T> where T:IEntity
    {
        IEnumerable<T> GetAll();
        T GetAll(int id);
        int Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}