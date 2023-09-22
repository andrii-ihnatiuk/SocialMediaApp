using SocialMediaApp.API.Data;
using SocialMediaApp.API.Exceptions;
using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly IDapperWrapper _dapper;

        public LikeRepository(IDapperWrapper dapperWrapper)
        {
            _dapper = dapperWrapper;
        }

        public async Task<IEnumerable<Like>> GetLikesByPostIdAsync(int postId)
        {
            try
            {
                return await _dapper.QueryAsync<Like>(
                    "SELECT * FROM Likes WHERE PostId = @PostId", new { PostId = postId });
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error occurred while fetching likes by post ID.", ex);
            }
        }

        public async Task LikePostAsync(int userId, int postId)
        {
            try
            {
                await _dapper.ExecuteAsync(
                    "INSERT INTO Likes (UserId, PostId) " +
                    "VALUES (@UserId, @PostId)",
                    new { UserId = userId, PostId = postId });
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error occurred while liking a post.", ex);
            }
        }

        public async Task UnlikePostAsync(int userId, int postId)
        {
            try
            {
                await _dapper.ExecuteAsync(
                    "DELETE FROM Likes WHERE UserId = @UserId AND PostId = @PostId",
                    new { UserId = userId, PostId = postId });
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error occurred while unliking a post.", ex);
            }
        }
    }
}