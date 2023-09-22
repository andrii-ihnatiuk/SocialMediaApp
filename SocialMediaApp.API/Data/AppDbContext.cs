using Npgsql;
using System.Data;

namespace SocialMediaApp.API.Data
{

    public class AppDbContext : IAppDbContext
    {
        private readonly string _connectionString;

        public AppDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? throw new Exception("Couldn't retrieve db connection configuration!");
        }

        public IDbConnection GetOpenConnection()
        {
            var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
