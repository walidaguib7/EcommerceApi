using Ecommerce.Dtos.Profiles;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IProfiles
    {
        public Task<Profile?> GetProfile(string userId);
        public Task<Profile> CreateProfile(CreateProfileDto dto);
        public Task<Profile?> UpdateProfile(int id, UpdateProfileDto dto);
    }
}
