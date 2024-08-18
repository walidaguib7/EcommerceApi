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
        public IEnumerable<Order_Product> order_Products { get; set; } = [];
        public IEnumerable<Payments> payments { get; set; } = [];
    }
}
