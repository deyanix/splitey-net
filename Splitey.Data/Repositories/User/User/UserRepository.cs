using Splitey.Data.Resources.User.User;
using Splitey.Data.Sql;
using Splitey.DependencyInjection.Attributes;
using Splitey.Models.User.User;

namespace Splitey.Data.Repositories.User.User;

[SingletonDependency]
public class UserRepository(ISqlConnectionFactory sqlConnectionFactory) : BaseRepository(sqlConnectionFactory)
{
    public Task<UserDto?> Get(int id)
    {
        return QueryFirstOrDefault<UserDto>(SqlQuery.Get, param: new { UserId = id });
    }
    
    public Task<UserDto?> GetByLogin(string login)
    {
        return QueryFirstOrDefault<UserDto>(SqlQuery.GetByLogin, param: new { Login = login });
    }
}