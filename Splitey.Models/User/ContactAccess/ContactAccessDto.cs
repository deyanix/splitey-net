namespace Splitey.Models.User.ContactAccess;

public class ContactAccessDto
{
    public required int UserId { get; set; }
    public required string FirstName { get; init; }
    public required string? LastName { get; init; }
    public required AccessMode AccessModeId { get; init; }
}