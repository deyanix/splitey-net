using Splitey.Models.User.ContactAccess;

namespace Splitey.Api.Models.Contact;

public class ContactGetResponse
{
    public required int Id { get; init; }
    public required string? Email { get; init; }
    public required string FirstName { get; init; }
    public required string? LastName { get; init; }
    public required IEnumerable<ContactAccessDto> AccessItems { get; init; }
}