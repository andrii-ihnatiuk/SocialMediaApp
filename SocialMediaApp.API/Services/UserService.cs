using SocialMediaApp.API.Models;
using SocialMediaApp.API.Repositories;

namespace SocialMediaApp.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRelationshipRepository _userRelationshipRepository;

        public UserService(IUserRepository userRepository, IUserRelationshipRepository userRelationshipRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userRelationshipRepository = userRelationshipRepository ?? throw new ArgumentNullException(nameof(userRelationshipRepository));
        }

        public async Task<User> GetUserAsync(int userId)
        {
            return await _userRepository.GetUserAsync(userId);
        }

        public async Task<IEnumerable<User>> GetFollowersAsync(int userId)
        {
            return await _userRelationshipRepository.GetFollowersAsync(userId);
        }

        public async Task<IEnumerable<User>> GetFollowingAsync(int userId)
        {
            return await _userRelationshipRepository.GetFollowingAsync(userId);
        }

        public async Task FollowUserAsync(int followerId, int followingId)
        {
            await _userRelationshipRepository.FollowUserAsync(followerId, followingId);
        }

        public async Task UnfollowUserAsync(int followerId, int followingId)
        {
            await _userRelationshipRepository.UnfollowUserAsync(followerId, followingId);
        }
    }
}
