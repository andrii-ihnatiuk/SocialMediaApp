using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Repositories
{
    public interface ILikeRepository
    {
        Task<IEnumerable<Like>> GetLikesByPostIdAsync(int postId);
        Task LikePostAsync(int userId, int postId);
        Task UnlikePostAsync(int userId, int postId);
    }
}
