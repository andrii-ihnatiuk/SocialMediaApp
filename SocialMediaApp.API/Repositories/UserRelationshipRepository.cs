using SocialMediaApp.API.Data;
using SocialMediaApp.API.Exceptions;
using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Repositories
{
    public class UserRelationshipRepository : IUserRelationshipRepository
    {
        private readonly IDapperWrapper _dapper;

        public UserRelationshipRepository(IDapperWrapper dapperWrapper)
        {
            _dapper = dapperWrapper;
        }

        public async Task<IEnumerable<User>> GetFollowersAsync(int userId)
        {
            try
            {
                return await _dapper.QueryAsync<User>(
                    "SELECT u.* FROM Users u " +
                    "INNER JOIN UserRelationships ur ON u.Id = ur.FollowerId " +
                    "WHERE ur.FollowingId = @UserId", new { UserId = userId });
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error occurred while fetching followers.", ex);
            }
        }

        public async Task<IEnumerable<User>> GetFollowingAsync(int userId)
        {
            try
            {
                return await _dapper.QueryAsync<User>(
                    "SELECT u.* FROM Users u " +
                    "INNER JOIN UserRelationships ur ON u.Id = ur.FollowingId " +
                    "WHERE ur.FollowerId = @UserId", new { UserId = userId });
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error occurred while fetching following users.", ex);
            }
        }

        public async Task FollowUserAsync(int followerId, int followingId)
        {
            try
            {
                await _dapper.ExecuteAsync(
                    "INSERT INTO UserRelationships (FollowerId, FollowingId) " +
                    "VALUES (@FollowerId, @FollowingId)",
                    new { FollowerId = followerId, FollowingId = followingId });
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error occurred while following a user.", ex);
            }
        }

        public async Task UnfollowUserAsync(int followerId, int followingId)
        {
            try
            {
                await _dapper.ExecuteAsync(
                    "DELETE FROM UserRelationships " +
                    "WHERE FollowerId = @FollowerId AND FollowingId = @FollowingId",
                    new { FollowerId = followerId, FollowingId = followingId });
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error occurred while unfollowing a user.", ex);
            }
        }
    }
}