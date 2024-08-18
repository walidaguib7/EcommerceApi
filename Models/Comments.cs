using Microsoft.Extensions.Hosting;


namespace Ecommerce.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public DateOnly CreatedAt { get; set; }
        public int productId { get; set; }
        public User user { get; set; }
        public Products product { get; set; }

        public ICollection<CommentLikes> commentLikes { get; set; } = [];
    }
}
