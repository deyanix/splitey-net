namespace Splitey.Api.Models.User;

public class LoginRequest
{
    public required string Login { get; init; }
    public required string Password { get; init; }
    public bool RememberMe { get; init; }
}