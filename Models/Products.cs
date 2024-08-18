namespace Ecommerce.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DateOnly? UpdateAt { get; set; }
        public IEnumerable<Order_Product> order_Products { get; set; } = [];

        public IEnumerable<ProductFiles> productFiles { get; set; } = [];
    }
}
