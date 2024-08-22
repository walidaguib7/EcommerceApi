using Ecommerce.Data;
using Ecommerce.Dtos.Followers;
using Ecommerce.Mappers;
using Ecommerce.Models;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Repositories
{
    public class FollowingRepo(
        ApplicationDBContext _context ,
        [FromKeyedServices("following")] IValidator<FollowDto> _validator,
        ICache _cacheService
        ) : IFollowing
    {
        private readonly ApplicationDBContext context = _context;
        private readonly IValidator<FollowDto> validator = _validator;
        private readonly ICache cacheService = _cacheService;
        public async Task<Follower> FollowUser(FollowDto dto)
        {
            var result = validator.Validate(dto);
            if (result.IsValid)
            {
                var follow = dto.ToModel();
                await context.followers.AddAsync(follow);
                await context.followings.AddAsync(new Following { followerId = dto.followerId, followingId = dto.userId });
                await context.SaveChangesAsync();
                return follow;
            }
            else
            {
                 throw new ValidationException(result.Errors);
            }
            
        }

        public Task<Follower> getFollower(string userId, string followerId)
        {
            
            return context.followers.Where(f => f.UserId == userId && f.FollowerId == followerId)
                .FirstAsync();
        }

        public async Task<ICollection<Follower>> GetFollowers(string userId)
        {
            var key = $"followers_{userId}";
            var CachedFollowers = await cacheService.GetFromCacheAsync<ICollection<Follower>>(key);
            if(!CachedFollowers.IsNullOrEmpty())
            {
                return CachedFollowers;
            }
            var followers = await context.followers
                .Include(f => f.follower)
                .Where(f => f.UserId == userId).ToListAsync();
            await cacheService.SetAsync<ICollection<Follower>>(key, followers);
            return followers;

        }

        public async Task<ICollection<Following>> GetFollowings(string followerId)
        {
            var key = $"followings_{followerId}";
            var CachedFollowings = await cacheService.GetFromCacheAsync<ICollection<Following>>(key);
            if (!CachedFollowings.IsNullOrEmpty())
            {
                return CachedFollowings;
            }
            var followings = await context.followings
                .Include(f => f.following)
                
                .Where(f => f.followerId == followerId).ToListAsync();

            await cacheService.SetAsync(key, followings);
            return followings;
         }

        public async Task<Follower> Unfollow(FollowDto dto) {

            var result = validator.Validate(dto);
            if (result.IsValid)
            {
                var user = await context.followers
                                .Where(f => f.UserId == dto.userId && f.FollowerId == dto.followerId).FirstAsync();
                context.followers.Remove(user);
                await context.SaveChangesAsync();
                return user;
            }
            else
            {
                throw new ValidationException(result.Errors);
            }

            
        }
    }
}
