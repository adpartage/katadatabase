namespace DbKata.Entities
{
    public class Purchase : IEntity<string>
    {
        public string Id { get; set; }
        public Product Product { get; set; }
        public Customer Buyer { get; set; }
        public double Quantity { get; set; }
        public PurchageStatus Status { get; set; }
    }
}
