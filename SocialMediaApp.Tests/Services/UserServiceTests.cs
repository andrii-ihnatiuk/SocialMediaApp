using Moq;
using NUnit.Framework;
using SocialMediaApp.API.Models;
using SocialMediaApp.API.Repositories;
using SocialMediaApp.API.Services;

namespace SocialMediaApp.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private UserService _userService;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IUserRelationshipRepository> _userRelationshipRepositoryMock;
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userRelationshipRepositoryMock = new Mock<IUserRelationshipRepository>();

            _userService = new UserService(
                _userRepositoryMock.Object,
                _userRelationshipRepositoryMock.Object
            );
        }

        [Test]
        public async Task GetUserAsync_ValidUserId_ReturnsUser()
        {
            // Arrange
            int userId = 1;
            var expectedUser = new User { Id = userId, Username = "John Doe" };
            _userRepositoryMock.Setup(repo => repo.GetUserAsync(userId))
                .ReturnsAsync(expectedUser);

            // Act
            var resultUser = await _userService.GetUserAsync(userId);

            // Assert
            Assert.AreEqual(expectedUser, resultUser);
        }

        [Test]
        public async Task GetFollowersAsync_ValidUserId_ReturnsFollowers()
        {
            // Arrange
            int userId = 1;
            var expectedFollowers = new List<User>
            {
                new User { Id = 2, Username = "Follower 1" },
                new User { Id = 3, Username = "Follower 2" }
            };
            _userRelationshipRepositoryMock.Setup(repo => repo.GetFollowersAsync(userId))
                .ReturnsAsync(expectedFollowers);

            // Act
            var resultFollowers = await _userService.GetFollowersAsync(userId);

            // Assert
            CollectionAssert.AreEqual(expectedFollowers, resultFollowers);
        }

        [Test]
        public async Task GetFollowingAsync_ValidUserId_ReturnsFollowing()
        {
            // Arrange
            int userId = 1;
            var expectedFollowing = new List<User>
            {
                new User { Id = 2, Username = "Following 1" },
                new User { Id = 3, Username = "Following 2" }
            };
            _userRelationshipRepositoryMock.Setup(repo => repo.GetFollowingAsync(userId))
                .ReturnsAsync(expectedFollowing);

            // Act
            var resultFollowing = await _userService.GetFollowingAsync(userId);

            // Assert
            CollectionAssert.AreEqual(expectedFollowing, resultFollowing);
        }

        [Test]
        public async Task FollowUserAsync_ValidData_CallsRepositoryMethod()
        {
            // Arrange
            int followerId = 1;
            int followingId = 2;

            // Act
            await _userService.FollowUserAsync(followerId, followingId);

            // Assert
            _userRelationshipRepositoryMock.Verify(
                repo => repo.FollowUserAsync(followerId, followingId),
                Times.Once
            );
        }

        [Test]
        public async Task UnfollowUserAsync_ValidData_CallsRepositoryMethod()
        {
            // Arrange
            int followerId = 1;
            int followingId = 2;

            // Act
            await _userService.UnfollowUserAsync(followerId, followingId);

            // Assert
            _userRelationshipRepositoryMock.Verify(
                repo => repo.UnfollowUserAsync(followerId, followingId),
                Times.Once
            );
        }
    }
}
