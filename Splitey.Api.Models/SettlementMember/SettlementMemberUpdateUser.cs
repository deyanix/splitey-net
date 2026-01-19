using Splitey.Models.User;

namespace Splitey.Api.Models.SettlementMember;

public class SettlementMemberUpdateUser
{
    public required int UserId { get; init; }
    public required AccessMode AccessModeId { get; init; }
}