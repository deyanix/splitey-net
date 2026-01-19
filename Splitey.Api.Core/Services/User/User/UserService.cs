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
    public Task<UserDto?> Get(int id)
    {
        return userRepository.Get(id);
    }

    public Task<UserDto?> GetByLogin(string login)
    {
        return userRepository.GetByLogin(login);
    }

    public async Task<UserGetResponse> Login(LoginRequest data)
    {
        UserDto? user = await GetByLogin(data.Login);
        if (user == null || !BCrypt.Net.BCrypt.Verify(data.Password, user.Password))
            throw new Exception("Invalid credentials");
        
        await authorizationService.SignIn(user, data.RememberMe);
        return UserGetResponse.FromDto(user);
    }

    public async Task Logout()
    {
        await authorizationService.SignOut();
    }

    public async Task<UserGetResponse?> GetCurrent()
    {
        var userContext = authorizationService.GetCurrentUser();
        if (userContext == null)
            return null;

        var user = await Get(userContext.Id);
        if (user == null)
            return null;

        return UserGetResponse.FromDto(user);
    }
}