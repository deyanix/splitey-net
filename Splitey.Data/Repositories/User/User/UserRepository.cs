using Dapper;
using Microsoft.Data.SqlClient;
using Splitey.Data.Resources.User.User;
using Splitey.DependencyInjection.Attributes;
using Splitey.Models.User.User;

namespace Splitey.Data.Repositories.User.User;

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