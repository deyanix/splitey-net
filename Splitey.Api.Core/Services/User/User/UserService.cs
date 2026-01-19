using Splitey.Api.Models.User;
using Splitey.Authorization;
using Splitey.Data.Repositories.User.User;
using Splitey.DependencyInjection.Attributes;
using Splitey.Models.User.User;

namespace Splitey.Core.Services.User.User;

[ScopedDependency]
public class UserService(
    UserRepository userRepository, 
    AuthorizationService authorizationService)
{
    public Task<UserModel?> Get(int id)
    {
        return userRepository.Get(id);
    }

    public Task<UserModel?> GetByLogin(string login)
    {
        return userRepository.GetByLogin(login);
    }

    public async Task Login(LoginRequest data)
    {
        UserModel? user = await GetByLogin(data.Login);
        if (user == null || !BCrypt.Net.BCrypt.Verify(data.Password, user.Password))
        {
            return;
        }

        await authorizationService.SignIn(user, data.RememberMe);
    }

    public async Task Logout()
    {
        await authorizationService.SignOut();
    }
}