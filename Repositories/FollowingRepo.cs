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
        ApplicationDBContext _context,
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

        public async Task<Follower> getFollower(string userId, string followerId)
        {
            var key = $"follower_{userId}_{followerId}";
            var cachedFollower = await cacheService.GetFromCacheAsync<Follower>(key);
            if (cachedFollower != null)
            {
                return cachedFollower;
            }

            var follower = await context.followers
                .Include(f => f.follower)
                .Include(f => f.follower.Profile)
                .Where(f => f.UserId == userId && f.FollowerId == followerId)
                .FirstAsync();
            await cacheService.SetAsync(key, follower);
            return follower;
        }

        public async Task<ICollection<Follower>> GetFollowers(string userId)
        {
            var key = $"followers_{userId}";
            var CachedFollowers = await cacheService.GetFromCacheAsync<ICollection<Follower>>(key);
            if (!CachedFollowers.IsNullOrEmpty())
            {
                return CachedFollowers;
            }
            var followers = await context.followers
                .Include(f => f.follower)
                .Include(f => f.follower.Profile)
                .Where(f => f.UserId == userId).ToListAsync();
            await cacheService.SetAsync<ICollection<Follower>>(key, followers);
            return followers;

        }

        public async Task<Following> GetFollowing(string followerId, string followingId)
        {
            var key = $"following_{followerId}_{followingId}";
            var cachedFollowing = await cacheService.GetFromCacheAsync<Following>(key);
            if (cachedFollowing != null) return cachedFollowing;
            var followingUser = await context.followings
                .Include(f => f.following)
                .Include(f => f.follower.Profile)
                .Where(f => f.followerId == followerId && f.followingId == followingId)
                .FirstAsync();
            await cacheService.SetAsync(key, followingUser);
            return followingUser;
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

        public async Task<Follower> Unfollow(FollowDto dto)
        {

            var result = validator.Validate(dto);
            if (result.IsValid)
            {
                var user = await context.followers
                                .Where(f => f.UserId == dto.userId && f.FollowerId == dto.followerId).FirstAsync();
                var followingUser = await context.followings
                    .Where(f => f.followerId == dto.followerId && f.followingId == dto.userId)
                    .FirstAsync();
                context.followers.Remove(user);
                context.followings.Remove(followingUser);
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
