using SocialMediaApp.API.Data;
using SocialMediaApp.API.Exceptions;
using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IDapperWrapper _dapper;

        public PostRepository(IDapperWrapper dapperWrapper)
        {
            _dapper = dapperWrapper;
        }

        public async Task<IEnumerable<Post>> GetPostsByUserIdAsync(int userId)
        {
            try
            {
                return await _dapper.QueryAsync<Post>(
                    "SELECT * FROM Posts WHERE AuthorId = @UserId", new { UserId = userId });
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error occurred while fetching posts by user ID.", ex);
            }
        }

        public Task CreatePostAsync(Post post)
        {
            if (string.IsNullOrWhiteSpace(post.Title) || string.IsNullOrWhiteSpace(post.Body))
            {
                throw new ArgumentException("Title and Body are required fields.");
            }

            return this.CreatePosInternaltAsync(post);
        }

        private async Task CreatePosInternaltAsync(Post post)
        {
            try
            {
                await _dapper.ExecuteAsync(
                    "INSERT INTO Posts (Title, Body, AuthorId) " +
                    "VALUES (@Title, @Body, @AuthorId)",
                    new { post.Title, post.Body, post.AuthorId });
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error occurred while creating new post.", ex);
            }
        }
    }
}
