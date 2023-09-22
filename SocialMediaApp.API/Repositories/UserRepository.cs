using SocialMediaApp.API.Data;
using SocialMediaApp.API.Exceptions;
using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDapperWrapper _dapper;

        public UserRepository(IDapperWrapper dapperWrapper)
        {
            _dapper = dapperWrapper;
        }

        public async Task<User> GetUserAsync(int userId)
        {
            try
            {
                return await _dapper.QueryFirstOrDefaultAsync<User>(
                    "SELECT * FROM Users WHERE Id = @UserId", new { UserId = userId });
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error occurred while fetching user by ID.", ex);
            }
        }
    }
}
