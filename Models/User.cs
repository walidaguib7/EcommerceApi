
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Models
{
    public class User : IdentityUser
    {
        public Profile? profile { get; set; }
        public ICollection<Orders> orders { get; set; } = [];
        public ICollection<CommentLikes> commentLikes { get; set; } = [];
        public ICollection<Payments> payments { get; set; } = [];
        public ICollection<Follower> followers { get; set; } = [];
        public ICollection<Following> followings { get; set; } = [];
        public ICollection<BlockedUsers> blockedUsers { get; set; } = [];
    }
}
