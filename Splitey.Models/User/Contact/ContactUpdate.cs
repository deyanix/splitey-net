namespace Splitey.Models.User.Contact;

public class ContactUpdate
{
    public required string? Email { get; init; }
    public required string FirstName { get; init; }
    public required string? LastName { get; init; }
}