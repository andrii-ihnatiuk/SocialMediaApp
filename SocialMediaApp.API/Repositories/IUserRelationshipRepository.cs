using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Repositories
{
    public interface IUserRelationshipRepository
    {
        Task<IEnumerable<User>> GetFollowersAsync(int userId);
        Task<IEnumerable<User>> GetFollowingAsync(int userId);
        Task FollowUserAsync(int followerId, int followingId);
        Task UnfollowUserAsync(int followerId, int followingId);
    }
}
