using Ecommerce.Helpers;

namespace Ecommerce.Dtos.Profiles
{
    public class UpdateProfileDto
    {
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public GenderType? gender { get; set; }
        public int? age { get; set; }
        public string? country { get; set; }
        public string? city { get; set; }
        public int? ZipCode { get; set; }
        public int? fileId { get; set; }
    }
}
