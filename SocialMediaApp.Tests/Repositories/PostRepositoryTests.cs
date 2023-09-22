using Moq;
using NUnit.Framework;
using SocialMediaApp.API.Data;
using SocialMediaApp.API.Models;
using SocialMediaApp.API.Repositories;

namespace SocialMediaApp.Tests.Repositories
{
    [TestFixture]
    public class PostRepositoryTests
    {
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private PostRepository _postRepository;
        private Mock<IDapperWrapper> _dapperWrapperMock;
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [SetUp]
        public void Setup()
        {
            _dapperWrapperMock = new Mock<IDapperWrapper>();
            _postRepository = new PostRepository(_dapperWrapperMock.Object);
        }

        [Test]
        public void CreatePostAsync_ValidPost_CreatesPost()
        {
            // Arrange
            var post = new Post
            {
                Title = "New Post",
                Body = "Post Body",
                AuthorId = 1
            };

            // Mock DapperWrapper behavior
            _dapperWrapperMock.Setup(dapper => dapper.ExecuteAsync(
                    It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(1); // Simulate a successful insertion

            // Act
            async Task CreatePost() => await _postRepository.CreatePostAsync(post);

            // Assert: No exceptions indicate success
            Assert.DoesNotThrowAsync(CreatePost);
        }

        [TestCase("", "Post Body", 1)]
        [TestCase("New Post", "", 1)]
        [TestCase(null, "Post Body", 1)]
        [TestCase("New Post", null, 1)]
        public void CreatePostAsync_InvalidPost_ThrowsArgumentException(
            string title, string body, int authorId)
        {
            // Arrange
            var post = new Post
            {
                Title = title,
                Body = body,
                AuthorId = authorId
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _postRepository.CreatePostAsync(post));
        }
    }
}
