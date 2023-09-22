using Dapper;
using System.Data;

namespace SocialMediaApp.API.Data
{
    public class DapperWrapper : IDapperWrapper
    {
        private readonly IDbConnection _connection;

        public DapperWrapper(IAppDbContext dbContext)
        {
            _connection = dbContext.GetOpenConnection();
        }

        public async Task<int> ExecuteAsync(string sql, object? param = null)
        {
            return await _connection.ExecuteAsync(sql, param);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null)
        {
            return await _connection.QueryAsync<T>(sql, param);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? param = null)
        {
            return await _connection.QueryFirstOrDefaultAsync<T>(sql, param);
        }
    }
}