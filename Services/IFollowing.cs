using Ecommerce.Dtos.Followers;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IFollowing
    {
        public Task<ICollection<Follower>> GetFollowers(string userId);
        public Task<ICollection<Following>> GetFollowings(string followerId);   
        public Task<Follower> getFollower(string userId, string followerId);
        public Task<Following> GetFollowing(string followerId, string followingId);
        public Task<Follower> FollowUser(FollowDto dto);
        public Task<Follower> Unfollow(FollowDto dto);
    }
}
