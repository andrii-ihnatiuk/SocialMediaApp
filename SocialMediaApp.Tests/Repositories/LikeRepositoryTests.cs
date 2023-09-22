using Moq;
using NUnit.Framework;
using SocialMediaApp.API.Data;
using SocialMediaApp.API.Exceptions;
using SocialMediaApp.API.Models;
using SocialMediaApp.API.Repositories;

namespace SocialMediaApp.Tests.Repositories
{
    [TestFixture]
    public class LikeRepositoryTests
    {
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private LikeRepository _likeRepository;
        private Mock<IDapperWrapper> _dapperWrapperMock;
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [SetUp]
        public void Setup()
        {
            _dapperWrapperMock = new Mock<IDapperWrapper>();
            _likeRepository = new LikeRepository(_dapperWrapperMock.Object);
        }

        [Test]
        public async Task GetLikesByPostIdAsync_ValidPostId_ReturnsLikes()
        {
            // Arrange
            var postId = 1;
            var expectedLikes = new List<Like>
            {
                new Like { Id = 1, UserId = 1, PostId = 1 },
                new Like { Id = 2, UserId = 2, PostId = 1 }
            };

            // Mock database connection behavior
            _dapperWrapperMock.Setup(db => db.QueryAsync<Like>(
                    It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(expectedLikes);

            // Act
            var result = await _likeRepository.GetLikesByPostIdAsync(postId);

            // Assert
            Assert.AreEqual(expectedLikes.Count, result?.Count());
        }

        [Test]
        public void LikePostAsync_ValidData_DoesNotThrow()
        {
            // Arrange
            var userId = 1;
            var postId = 1;

            // Mock database connection behavior
            _dapperWrapperMock.Setup(db => db.ExecuteAsync(
                    It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(1); // Simulate a successful insertion          

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await _likeRepository.LikePostAsync(userId, postId));
        }

        [Test]
        public void UnlikePostAsync_ValidData_DoesNotThrow()
        {
            // Arrange
            var userId = 1;
            var postId = 1;

            // Mock database connection behavior
            _dapperWrapperMock.Setup(db => db.ExecuteAsync(
                    It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(1);

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await _likeRepository.UnlikePostAsync(userId, postId));
        }

        [Test]
        public void GetLikesByPostIdAsync_DbError_ThrowsRepositoryException()
        {
            // Arrange
            _dapperWrapperMock.Setup(db => db.QueryAsync<Like>(
                It.IsAny<string>(), It.IsAny<object>()))
                .ThrowsAsync(new Exception());

            // Act & Assert
            Assert.ThrowsAsync<RepositoryException>(
                async () => await _likeRepository.GetLikesByPostIdAsync(0));
        }
    }
}
