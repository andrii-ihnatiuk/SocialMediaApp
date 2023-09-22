using Moq;
using NUnit.Framework;
using SocialMediaApp.API.Models;
using SocialMediaApp.API.Repositories;
using SocialMediaApp.API.Services;

namespace SocialMediaApp.Tests.Services
{
    [TestFixture]
    public class PostServiceTests
    {
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private PostService _postService;
        private Mock<IPostRepository> _postRepositoryMock;
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [SetUp]
        public void SetUp()
        {
            _postRepositoryMock = new Mock<IPostRepository>();
            _postService = new PostService(_postRepositoryMock.Object);
        }

        [Test]
        public async Task GetPostsByUserIdAsync_ValidUserId_ReturnsPosts()
        {
            // Arrange
            int userId = 1;
            var expectedPosts = new List<Post>
            {
                new Post { Id = 1, Title = "Post 1", Body = "Body 1", AuthorId = userId },
                new Post { Id = 2, Title = "Post 2", Body = "Body 2", AuthorId = userId }
            };
            _postRepositoryMock.Setup(repo => repo.GetPostsByUserIdAsync(userId))
                .ReturnsAsync(expectedPosts);

            // Act
            var resultPosts = await _postService.GetPostsByUserIdAsync(userId);

            // Assert
            CollectionAssert.AreEqual(expectedPosts, resultPosts);
        }

        [Test]
        public async Task CreatePostAsync_ValidPost_CallsRepositoryMethod()
        {
            // Arrange
            var post = new Post { Title = "New Post", Body = "Body", AuthorId = 1 };

            // Act
            await _postService.CreatePostAsync(post);

            // Assert
            _postRepositoryMock.Verify(
                repo => repo.CreatePostAsync(post),
                Times.Once
            );
        }
    }
}
