using SocialMediaApp.API.Models;
using SocialMediaApp.API.Repositories;

namespace SocialMediaApp.API.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        }

        public async Task<IEnumerable<Post>> GetPostsByUserIdAsync(int userId)
        {
            return await _postRepository.GetPostsByUserIdAsync(userId);
        }

        public async Task CreatePostAsync(Post post)
        {
            await _postRepository.CreatePostAsync(post);
        }
    }
}
