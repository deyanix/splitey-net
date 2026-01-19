using Splitey.Models.Authorization;

namespace Splitey.Models.User.User;

public class UserDto : IUserContext
{
    public required int Id { get; init; }
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Password { get; init; }
}