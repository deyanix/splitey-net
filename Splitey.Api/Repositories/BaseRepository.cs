using Dapper;
using Microsoft.Data.SqlClient;

namespace Splitey.Api.Repositories;

public abstract class BaseRepository
{
    private readonly SqlConnection _sqlConnection;

    protected BaseRepository(SqlConnection sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }
    
    public async Task<IEnumerable<T>> Query<T>(string query, object? param)
    {
        // using var transaction = TransactionBuilder.Default;
        return await _sqlConnection.QueryAsync<T>(query, param: param);
    } 
    
    public async Task<T> QueryFirst<T>(string query, object? param)
    {
        // using var transaction = TransactionBuilder.Default;
        return await _sqlConnection.QueryFirstAsync<T>(query, param: param);
    } 
    
    public async Task<int> Execute(string query, object? param)
    {
        // using var transaction = TransactionBuilder.Default;
        return await _sqlConnection.ExecuteAsync(query, param: param);
    } 
}