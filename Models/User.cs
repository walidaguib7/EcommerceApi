
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
        public IEnumerable<Orders> orders { get; set; } = [];
        public ICollection<CommentLikes> commentLikes { get; set; } = [];
        public IEnumerable<Payments> payments { get; set; } = [];
        public IEnumerable<Follower> followers { get; set; } = [];
        public IEnumerable<BlockedUsers> blockedUsers { get; set; } = [];
    }
}
