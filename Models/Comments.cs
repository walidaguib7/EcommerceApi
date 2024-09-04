﻿using Microsoft.Extensions.Hosting;


namespace Ecommerce.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public User user { get; set; }
        public List<CommentLikes> commentLikes { get; set; } = [];
    }
}
