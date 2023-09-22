using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPostsByUserIdAsync(int userId);
        Task CreatePostAsync(Post post);
    }
}
