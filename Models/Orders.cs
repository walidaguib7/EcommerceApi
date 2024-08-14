namespace Ecommerce.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public DateOnly Order_Date { get; set; }
        public int Quantity { get; set; }
        public string Order_Status { get; set; }
        public string userId { get; set; }
        public User user { get; set; }
        public IEnumerable<Products> Product { get; set; } = [];
        public IEnumerable<Payments> payments { get; set; } = [];
    }
}
