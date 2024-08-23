using Ecommerce.Data;
using Ecommerce.Dtos.Profiles;
using Ecommerce.Mappers;
using Ecommerce.Models;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories
{
    public class ProfilesRepo(
        ApplicationDBContext _context,
        [FromKeyedServices("createProfile")] IValidator<CreateProfileDto> _CreateProfileValidator,
        [FromKeyedServices("updateProfile")] IValidator<UpdateProfileDto> _UpdateProfileValidator,
        ICache _cacheService
        ) : IProfiles
    {

        private readonly ApplicationDBContext context = _context;
        private readonly IValidator<CreateProfileDto> CreateProfileValidator = _CreateProfileValidator;
        private readonly IValidator<UpdateProfileDto> UpdateProfileValidator = _UpdateProfileValidator;
        private readonly ICache cacheService = _cacheService;

        public async Task<Profile> CreateProfile(CreateProfileDto dto)
        {
            var result = CreateProfileValidator.Validate(dto);
            if (result.IsValid)
            {
                var profile = dto.ToProfileModel();
                await context.profiles.AddAsync(profile);
                await context.SaveChangesAsync();
                return profile;
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public async Task<Profile?> GetProfile(string userId)
        {
            var key = $"profile_{userId}";
            var cachedProfile = await cacheService.GetFromCacheAsync<Profile>(key);
            if (cachedProfile != null) return cachedProfile;

            var profile = await context.profiles
                .Include(p => p.user)
                .Include(p => p.file)
                .Where(p => p.userId == userId).FirstAsync();
            if (profile == null) return null;
            await cacheService.SetAsync<Profile>(key, profile);
                return profile;
        }

        public async Task<Profile?> UpdateProfile(int id, UpdateProfileDto dto)
        {
            var profile = await context.profiles.FindAsync(id);
            if (profile == null) return null;
            var result = UpdateProfileValidator.Validate(dto);
            if (result.IsValid)
            {
                profile.first_name = dto.first_name;
                profile.last_name = dto.last_name;
                profile.age = dto.age;
                profile.gender = dto.gender;
                profile.country = dto.country;
                profile.city = dto.city;
                profile.ZipCode = dto.ZipCode;
                profile.fileId = dto.fileId;

                await context.SaveChangesAsync();
                return profile;
            }
            else
            {
                throw new ValidationException(result.Errors);
            }
            
        }
    }
}
