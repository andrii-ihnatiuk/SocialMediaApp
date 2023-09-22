using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Services
{
    public interface IUserService
    {
        Task<User> GetUserAsync(int userId);
        Task<IEnumerable<User>> GetFollowersAsync(int userId);
        Task<IEnumerable<User>> GetFollowingAsync(int userId);
        Task FollowUserAsync(int followerId, int followingId);
        Task UnfollowUserAsync(int followerId, int followingId);
    }
}
