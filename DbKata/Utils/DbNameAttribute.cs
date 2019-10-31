using System;

namespace DbKata.Utils
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class DbNameAttribute : Attribute
    {
        public string Name { get; }

        public DbNameAttribute(string name)
        {
            Name = name;
        }
    }
}