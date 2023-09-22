using Moq;
using NUnit.Framework;
using SocialMediaApp.API.Models;
using SocialMediaApp.API.Repositories;
using SocialMediaApp.API.Services;

namespace SocialMediaApp.Tests.Services
{
    [TestFixture]
    public class LikeServiceTests
    {
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private LikeService _likeService;
        private Mock<ILikeRepository> _likeRepositoryMock;
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [SetUp]
        public void SetUp()
        {
            _likeRepositoryMock = new Mock<ILikeRepository>();
            _likeService = new LikeService(_likeRepositoryMock.Object);
        }

        [Test]
        public async Task GetLikesByPostIdAsync_ValidPostId_ReturnsLikes()
        {
            // Arrange
            int postId = 1;
            var expectedLikes = new List<Like>
            {
                new Like { Id = 1, UserId = 1, PostId = postId },
                new Like { Id = 2, UserId = 2, PostId = postId }
            };
            _likeRepositoryMock.Setup(repo => repo.GetLikesByPostIdAsync(postId))
                .ReturnsAsync(expectedLikes);

            // Act
            var resultLikes = await _likeService.GetLikesByPostIdAsync(postId);

            // Assert
            CollectionAssert.AreEqual(expectedLikes, resultLikes);
        }

        [Test]
        public async Task LikePostAsync_ValidData_CallsRepositoryMethod()
        {
            // Arrange
            int userId = 1;
            int postId = 1;

            // Act
            await _likeService.LikePostAsync(userId, postId);

            // Assert
            _likeRepositoryMock.Verify(
                repo => repo.LikePostAsync(userId, postId),
                Times.Once
            );
        }

        [Test]
        public async Task UnlikePostAsync_ValidData_CallsRepositoryMethod()
        {
            // Arrange
            int userId = 1;
            int postId = 1;

            // Act
            await _likeService.UnlikePostAsync(userId, postId);

            // Assert
            _likeRepositoryMock.Verify(
                repo => repo.UnlikePostAsync(userId, postId),
                Times.Once
            );
        }
    }
}
