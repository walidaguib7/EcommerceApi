using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Dtos.Blocking;
using Ecommerce.Dtos.Followers;
using Ecommerce.Mappers;
using Ecommerce.Models;
using Ecommerce.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Repositories
{
    public class blockingRepo
    (
        ApplicationDBContext _context,
        [FromKeyedServices("blocking")] IValidator<BlockUserDto> _validator,
        ICache _cache,
        IFollowing _followingService
    )
     : IBlocking
    {
        private readonly ApplicationDBContext context = _context;
        private readonly IValidator<BlockUserDto> validator = _validator;
        private readonly ICache cache = _cache;
        private readonly IFollowing followingService = _followingService;
        public async Task<BlockedUsers?> BlockUser(BlockUserDto dto)
        {
            var result = validator.Validate(dto);
            if (result.IsValid)
            {
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var follower = await followingService.getFollower(dto.UserId, dto.BlockedUserId);
                        if (follower == null)
                        {
                            return null;
                        }
                        else
                        {
                            await followingService.Unfollow(new FollowDto
                            {
                                userId = dto.UserId,
                                followerId = dto.BlockedUserId
                            });
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    try
                    {
                        var following = await followingService.getFollower(dto.BlockedUserId, dto.UserId);
                        if (following == null)
                        {
                            return null;
                        }
                        else
                        {
                            await followingService.Unfollow(new FollowDto
                            {
                                userId = dto.BlockedUserId,
                                followerId = dto.UserId
                            });
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    var user = dto.ToModel();
                    await context.blockedUsers.AddAsync(user);
                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return user;
                }


            }
            else
            {
                throw new ValidationException(result.Errors);
            }
        }

        public async Task<List<BlockedUsers>> GetBlockedUsers(string userId)
        {
            string key = $"blocking_{userId}";
            var cachedBlockedUsers = await cache.GetFromCacheAsync<List<BlockedUsers>>(key);
            if (!cachedBlockedUsers.IsNullOrEmpty())
            {
                return cachedBlockedUsers;
            }

            var users = await context.blockedUsers
            .Include(b => b.blockedUser)
            .Include(b => b.user)

            .Where(b => b.userId == userId).ToListAsync();
            await cache.SetAsync(key, users);
            return users;
        }

        public async Task<BlockedUsers?> UnblockUser(BlockUserDto dto)
        {
            var user = await context.blockedUsers
            .Include(b => b.blockedUser)
            .Where(b => b.userId == dto.UserId && b.blockedUserId == dto.BlockedUserId).FirstAsync();

            if (user == null) return null;

            context.blockedUsers.Remove(user);
            await context.SaveChangesAsync();
            return user;
        }
    }
}