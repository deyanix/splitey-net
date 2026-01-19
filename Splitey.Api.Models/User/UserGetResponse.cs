using Splitey.Models.User.User;

namespace Splitey.Api.Models.User;

public class UserGetResponse
{
    public required int Id { get; init; }
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }

    public static UserGetResponse FromDto(UserDto user)
    {
        return new UserGetResponse()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
        };
    }
}