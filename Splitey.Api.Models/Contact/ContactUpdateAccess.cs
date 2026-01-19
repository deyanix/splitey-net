using Splitey.Models.User;

namespace Splitey.Api.Models.Contact;

public class ContactUpdateAccess
{
    public required int UserId { get; init; }
    public required AccessMode AccessModeId { get; init; }
}