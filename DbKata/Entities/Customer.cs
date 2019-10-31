using DbKata.Utils;

namespace DbKata.Entities
{
    [DbName("TCUSTOMER")]
    public class Customer : IEntity<int>
    {
        [DbName("CId")]
        public int Id { get; set; }

        [DbName("FNAME")]
        public string Firstname { get; set; }

        [DbName("LNAME")]
        public string Lastname { get; set; }
    }
}
