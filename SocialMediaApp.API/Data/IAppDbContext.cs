using System.Data;

namespace SocialMediaApp.API.Data
{
    public interface IAppDbContext
    {
        IDbConnection GetOpenConnection();
    }
}
