using Dapper.FluentMap.Mapping;
using DbKata.Utils;
using System.Reflection;

namespace DbKata.Sql
{
    public class EntityMapper<T> : EntityMap<T> where T : class
    {
        public EntityMapper()
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
