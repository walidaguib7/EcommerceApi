namespace Ecommerce.Models
{
    public class MediaModel
    {
        public int Id { get; set; }
        public string file { get; set; }
        public DateOnly CreatedAt { get; set; }
        public IEnumerable<ProductFiles> productFiles { get; set; } = [];
    }
}
