using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPostsByUserIdAsync(int userId);
        Task CreatePostAsync(Post post);
    }
}
