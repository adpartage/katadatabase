namespace DbKata.Entities
{
    public class Product : IEntity<string>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
