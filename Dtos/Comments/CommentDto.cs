using System;
using System.Collections;

namespace Ecommerce.Dtos.Comments
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public string username { get; set; }
        public int Likes { get; set; }
        public ICollection LikeCollection { get; set; }
    }

}