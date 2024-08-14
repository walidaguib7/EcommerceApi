namespace Ecommerce.Models
{
    public class ProductsVariants
    {
        public int Id { get; set; }
        public string size { get; set; }
        public string color { get; set; }
        public int quantity { get; set; }
        public int ProductId { get; set; }
        public Products Product { get; set; }
    }
}
