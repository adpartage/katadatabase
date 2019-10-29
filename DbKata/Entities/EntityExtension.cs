using DbKata.Utils;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DbKata.Entities
{
    public static class EntityExtension
    {
        public static string GetDbName(this PropertyInfo propertyInfo)
        {
            var attribute = propertyInfo.GetCustomAttribute<DbNameAttribute>();
            return !string.IsNullOrEmpty(attribute?.Name) ? attribute.Name : propertyInfo.Name;
        }

        public static string GetDbName<T, TU>(this T entity, Expression<Func<T, TU>> propertyAccessor)
            where T:IEntity
        {
            if(propertyAccessor.Body is MemberExpression memberExpression)
            {
                return (memberExpression.Member.GetCustomAttributes(typeof(DbNameAttribute), true).FirstOrDefault() as DbNameAttribute)?.Name ?? memberExpression.Member.Name;
            }

            return propertyAccessor.Name;
        }
    }
}