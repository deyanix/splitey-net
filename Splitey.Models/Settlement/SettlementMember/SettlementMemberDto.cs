using Splitey.Models.User;

namespace Splitey.Models.Settlement.SettlementMember;

public class SettlementMemberDto
{
    public required int Id { get; init; }
    public required int? UserId { get; init; }
    public required int? ContactId { get; init; }
    public required AccessMode AccessModeId { get; init; }
    public required string FirstName { get; init; }
    public required string? LastName { get; init; }
}