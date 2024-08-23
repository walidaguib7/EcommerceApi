using Ecommerce.Dtos.Profiles;
using Ecommerce.Models;

namespace Ecommerce.Mappers
{
    public static class ProfileMapper
    {
        public static Profile ToProfileModel(this CreateProfileDto dto)
        {
            return new Profile
            {
                first_name = dto.first_name,
                last_name = dto.last_name,
                gender = dto.gender,
                ZipCode = dto.ZipCode,
                age = dto.age,
                city = dto.city,
                country = dto.country,
                userId = dto.userId,
                fileId = dto.fileId
            };
        }

        public static ProfileDto ToProfileDto(this Profile profile)
        {
            return new ProfileDto
            {
                Id = profile.Id,
                first_name = profile.first_name,
                last_name = profile.last_name,
                username = profile.user.UserName,
               email = profile.user.Email,
                age = profile.age,
                gender = profile.gender.Value,
                city = profile.city,
                country = profile.country,
                ZipCode = profile.ZipCode,
                fileId = profile.fileId,
                userId = profile.userId,
                ProfilePicture = profile.file.file,

            };
        }
    }
}
