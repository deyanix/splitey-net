using Splitey.Api.Common.Authorization;
using Splitey.Api.Common.DependencyInjection.Attributes;
using Splitey.Api.Models.User.User;
using Splitey.Api.Repositories.User.User;

namespace Splitey.Api.Services.User.User;

[SingletonDependency]
public class UserService(UserRepository userRepository, JwtService jwtService)
{
    public Task<UserModel?> Get(int id)
    {
        return userRepository.Get(id);
    }

    public Task<UserModel?> GetByLogin(string login)
    {
        return userRepository.GetByLogin(login);
    }

    public async Task<string?> Login(LoginRequest data)
    {
        UserModel? user = await GetByLogin(data.Login);
        if (user == null || !BCrypt.Net.BCrypt.Verify(data.Password, user.Password))
        {
            return null;
        }

        return jwtService.GenerateToken(user);
    }
}