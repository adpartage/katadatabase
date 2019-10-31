using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DbKata.Utils
{
    public static class EntityExtension
    {
        public static string GetDbName(this PropertyInfo propertyInfo)
        {
            var attribute = propertyInfo.GetCustomAttribute<DbNameAttribute>();
            return !string.IsNullOrEmpty(attribute?.Name) ? attribute.Name : propertyInfo.Name;
        }

        public static string GetDbName<T, TU>(this T entity, Expression<Func<T, TU>> propertyAccessor)           
        {
            if (propertyAccessor.Body is MemberExpression memberExpression)
            {
                return typeof(T).GetProperty(memberExpression.Member.Name).GetDbName();
            }

            return propertyAccessor.Name;
        }
    }
}