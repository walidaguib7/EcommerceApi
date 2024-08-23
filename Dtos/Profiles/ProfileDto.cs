using Ecommerce.Helpers;
using Ecommerce.Models;

namespace Ecommerce.Dtos.Profiles
{
    public class ProfileDto
    {
        public int Id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public GenderType gender { get; set; }
        public int? age { get; set; }
        public string? country { get; set; }
        public string? city { get; set; }
        public int? ZipCode { get; set; }
        public int? fileId { get; set; }
        public string ProfilePicture { get; set; }
        public string userId { get; set; }
        public  string username { get; set; }
        public string email { get; set; }
    }
}
