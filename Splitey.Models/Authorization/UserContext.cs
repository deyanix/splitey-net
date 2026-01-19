namespace Splitey.Models.Authorization;

public class UserContext : IUserContext
{
    public required int Id { get; init; }
    public required string Username { get; init; }
}