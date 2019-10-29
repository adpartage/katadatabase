using Dapper.FluentMap.Mapping;
using DbKata.Entities;
using DbKata.Utils;
using System.Reflection;

namespace DbKata.Sql
{
    public abstract class EntityMapBase<T> : EntityMap<T> where T : class
    {
        public EntityMapBase()
        {
            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                var dbName = propertyInfo.GetCustomAttribute<DbNameAttribute>();
                if (!string.IsNullOrEmpty(dbName?.Name))
                {
                    PropertyMaps.Add(new PropertyMap(propertyInfo, dbName.Name));
                }
            }
        }
    }
}
