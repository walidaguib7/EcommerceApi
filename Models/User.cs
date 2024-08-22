
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Models
{
    public class User : IdentityUser
    {
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? gender { get; set; }
        public int? age { get; set; }
        public string? country { get; set; }
        public string? city { get; set; }
        public int? ZipCode { get; set; }
        public ICollection<Orders> orders { get; set; } = [];
        public ICollection<CommentLikes> commentLikes { get; set; } = [];
        public ICollection<Payments> payments { get; set; } = [];
        public ICollection<Follower> followers { get; set; } = [];
        public ICollection<Following> followings { get; set; } = [];
        public ICollection<BlockedUsers> blockedUsers { get; set; } = [];
    }
}
