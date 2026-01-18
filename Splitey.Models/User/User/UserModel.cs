namespace Splitey.Models.User.User;

public class UserModel
{
    public required int Id { get; init; }
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}