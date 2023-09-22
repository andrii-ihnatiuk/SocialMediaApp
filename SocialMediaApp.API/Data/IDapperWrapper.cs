namespace SocialMediaApp.API.Data
{
    public interface IDapperWrapper
    {
        Task<int> ExecuteAsync(string sql, object? param = null);
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null);
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? param = null);
    }
}
