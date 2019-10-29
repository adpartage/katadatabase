using DbKata.Utils;
using System;
using System.Data;

namespace DbKata.Entities
{
    [DbName("TCUSTOMER")]
    public class Customer : IEntity<string>
    {
        public string Id { get; set; }

        [DbName("FNAME")]
        public string Firstname { get; set; }

        [DbName("LNAME")]
        public string Lastname { get; set; }
    }
}
