using System;
using System.Data;

namespace DbKata.Utils
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct)]
    public class DbNameAttribute : Attribute
    {
        public string Name { get; }
        public CommandType CommandType { get; }

        public DbNameAttribute(string name)
        {
            Name = name;
        }
    }
}