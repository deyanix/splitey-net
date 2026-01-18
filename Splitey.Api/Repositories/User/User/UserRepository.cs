using Dapper;
using Microsoft.Data.SqlClient;
using Splitey.Api.Common.DependencyInjection.Attributes;
using Splitey.Api.Models.User.User;
using Splitey.Api.Resources.User.User;

namespace Splitey.Api.Repositories.User.User;

[SingletonDependency]
public class UserRepository(SqlConnection connection)
{
    public async Task<UserModel?> Get(int id)
    {
        return (await connection.QueryAsync<UserModel>(SqlQuery.Get, param: new { UserId = id }))
            .FirstOrDefault();
    }
    
    public async Task<UserModel?> GetByLogin(string login)
    {
        return (await connection.QueryAsync<UserModel>(SqlQuery.GetByLogin, param: new { Login = login }))
            .FirstOrDefault();
    }
}