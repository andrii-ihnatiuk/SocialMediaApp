using Moq;
using NUnit.Framework;
using SocialMediaApp.API.Data;
using SocialMediaApp.API.Exceptions;
using SocialMediaApp.API.Models;
using SocialMediaApp.API.Repositories;

namespace SocialMediaApp.Tests.Repositories
{
    [TestFixture]
    public class UserRelationshipRepositoryTests
    {
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private UserRelationshipRepository _userRelationshipRepository;
        private Mock<IDapperWrapper> _dapperWrapperMock;
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [SetUp]
        public void Setup()
        {
            _dapperWrapperMock = new Mock<IDapperWrapper>();
            _userRelationshipRepository = new UserRelationshipRepository(_dapperWrapperMock.Object);
        }

        [Test]
        public async Task GetFollowersAsync_ValidUserId_ReturnsFollowers()
        {
            // Arrange
            int userId = 1;
            var expectedFollowers = new List<User> { new User { Id = 2, Username = "Follower1" } };

            // Mock DapperWrapper behavior
            _dapperWrapperMock.Setup(dapper => dapper.QueryAsync<User>(
                    It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(expectedFollowers);

            // Act
            var followers = await _userRelationshipRepository.GetFollowersAsync(userId);

            // Assert
            CollectionAssert.AreEqual(expectedFollowers, followers);
        }

        [Test]
        public async Task GetFollowingAsync_ValidUserId_ReturnsFollowingUsers()
        {
            // Arrange
            int userId = 1;
            var expectedFollowing = new List<User> { new User { Id = 3, Username = "Following1" } };

            // Mock DapperWrapper behavior
            _dapperWrapperMock.Setup(dapper => dapper.QueryAsync<User>(
                    It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(expectedFollowing);

            // Act
            var following = await _userRelationshipRepository.GetFollowingAsync(userId);

            // Assert
            CollectionAssert.AreEqual(expectedFollowing, following);
        }

        [Test]
        public void FollowUserAsync_ValidData_DoesNotThrow()
        {
            // Arrange
            int followerId = 1;
            int followingId = 2;

            // Mock DapperWrapper behavior
            _dapperWrapperMock.Setup(dapper => dapper.ExecuteAsync(
                    It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(1); // Simulate a successful insertion

            // Act
            async Task FollowUser() => await _userRelationshipRepository.FollowUserAsync(followerId, followingId);

            // Assert: No exceptions indicate success
            Assert.DoesNotThrowAsync(FollowUser);
        }

        [Test]
        public void UnfollowUserAsync_ValidData_DoesNotThrow()
        {
            // Arrange
            int followerId = 1;
            int followingId = 2;

            // Mock DapperWrapper behavior
            _dapperWrapperMock.Setup(dapper => dapper.ExecuteAsync(
                    It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(1); // Simulate a successful deletion

            // Act
            async Task UnfollowUser() => await _userRelationshipRepository.UnfollowUserAsync(followerId, followingId);

            // Assert: No exceptions indicate success
            Assert.DoesNotThrowAsync(UnfollowUser);
        }

        [Test]
        public void FollowUserAsync_DbError_ThrowsRepositoryException()
        {
            // Arrange: Simulate an exception during execution
            int followerId = 1;
            int followingId = 0;

            _dapperWrapperMock.Setup(dapper => dapper.ExecuteAsync(
                    It.IsAny<string>(), It.IsAny<object>()))
                .ThrowsAsync(new Exception());

            // Act & Assert
            Assert.ThrowsAsync<RepositoryException>(
                async () => await _userRelationshipRepository.FollowUserAsync(followerId, followingId));
        }

        [Test]
        public void UnfollowUserAsync_DbError_ThrowsRepositoryException()
        {
            // Arrange: Simulate an exception during execution
            int followerId = 0;
            int followingId = 2;

            _dapperWrapperMock.Setup(dapper => dapper.ExecuteAsync(
                    It.IsAny<string>(), It.IsAny<object>()))
                .ThrowsAsync(new Exception());

            // Act & Assert
            Assert.ThrowsAsync<RepositoryException>(
                async () => await _userRelationshipRepository.UnfollowUserAsync(followerId, followingId));
        }
    }
}