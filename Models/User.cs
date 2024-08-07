using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Models
{
    public class User : IdentityUser
    {
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? gender { get; set; }
        public  int? age { get; set; }
        public string? country { get; set; }
        public string? city { get; set; }
    }
}
