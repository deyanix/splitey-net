namespace Splitey.Models.Authorization;

public interface IUserContext
{
    public int Id { get; }
    public string Username { get; }
}