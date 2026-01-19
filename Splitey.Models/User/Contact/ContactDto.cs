namespace Splitey.Models.User.Contact;

public class ContactDto
{
    public required int Id { get; init; }
    public required string? Email { get; init; }
    public required string FirstName { get; init; }
    public required string? LastName { get; init; }
    public required AccessMode AccessModeId { get; init; }
}