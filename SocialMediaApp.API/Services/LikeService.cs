using SocialMediaApp.API.Models;
using SocialMediaApp.API.Repositories;

namespace SocialMediaApp.API.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;

        public LikeService(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository ?? throw new ArgumentNullException(nameof(likeRepository));
        }

        public async Task<IEnumerable<Like>> GetLikesByPostIdAsync(int postId)
        {
            return await _likeRepository.GetLikesByPostIdAsync(postId);
        }

        public async Task LikePostAsync(int userId, int postId)
        {
            await _likeRepository.LikePostAsync(userId, postId);
        }

        public async Task UnlikePostAsync(int userId, int postId)
        {
            await _likeRepository.UnlikePostAsync(userId, postId);
        }
    }
}
