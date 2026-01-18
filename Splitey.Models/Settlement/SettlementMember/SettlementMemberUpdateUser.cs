using Splitey.Models.User;

namespace Splitey.Models.Settlement.SettlementMember;

public class SettlementMemberUpdateUser
{
    public required int UserId { get; init; }
    public required AccessMode AccessMode { get; init; }
}