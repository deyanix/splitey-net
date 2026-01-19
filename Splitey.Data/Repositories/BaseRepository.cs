using Dapper;
using Splitey.Data.Sql;

namespace Splitey.Data.Repositories;

public abstract class BaseRepository
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    protected BaseRepository(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    
    public async Task<IEnumerable<T>> Query<T>(string query, object? param = null)
    {
        using var connection = _sqlConnectionFactory.Create();
        return await connection.QueryAsync<T>(query, param: param);
    } 
    
    public async Task<T> QueryFirst<T>(string query, object? param = null)
    {
        using var connection = _sqlConnectionFactory.Create();
        return await connection.QueryFirstAsync<T>(query, param: param);
    } 
    
    public async Task<T?> QueryFirstOrDefault<T>(string query, object? param = null)
    {
        using var connection = _sqlConnectionFactory.Create();
        return await connection.QueryFirstOrDefaultAsync<T>(query, param: param);
    } 
    
    public async Task<int> Execute(string query, object? param = null)
    {
        using var connection = _sqlConnectionFactory.Create();
        return await connection.ExecuteAsync(query, param: param);
    } 
}