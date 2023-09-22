using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Repositories
{

    public interface IUserRepository
    {
        Task<User> GetUserAsync(int userId);
    }
}
