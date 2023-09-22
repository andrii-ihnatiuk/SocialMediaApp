using Moq;
using NUnit.Framework;
using SocialMediaApp.API.Data;
using SocialMediaApp.API.Exceptions;
using SocialMediaApp.API.Models;
using SocialMediaApp.API.Repositories;

namespace SocialMediaApp.Tests.Repositories
{
    [TestFixture]
    public class UserRepositoryTests
    {
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private UserRepository _userRepository;
        private Mock<IDapperWrapper> _dapperWrapperMock;
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [SetUp]
        public void Setup()
        {
            _dapperWrapperMock = new Mock<IDapperWrapper>();
            _userRepository = new UserRepository(_dapperWrapperMock.Object);
        }

        [Test]
        public async Task GetUserAsync_ValidUserId_ReturnsUser()
        {
            // Arrange
            int userId = 1;
            var expectedUser = new User { Id = userId, Username = "TestUser" };

            _dapperWrapperMock.Setup(dapper => dapper.QueryFirstOrDefaultAsync<User>(
                    It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(expectedUser);

            // Act
            var user = await _userRepository.GetUserAsync(userId);

            // Assert
            Assert.AreEqual(expectedUser, user);
        }

        [Test]
        public void GetUserAsync_DbError_ThrowsRepositoryException()
        {
            // Arrange: Simulate an exception during execution
            int userId = 0;

            _dapperWrapperMock.Setup(dapper => dapper.QueryFirstOrDefaultAsync<User>(
                    It.IsAny<string>(), It.IsAny<object>()))
                .ThrowsAsync(new Exception());

            // Act & Assert
            Assert.ThrowsAsync<RepositoryException>(
                async () => await _userRepository.GetUserAsync(userId));
        }
    }
}