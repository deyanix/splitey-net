namespace Splitey.Models.Settlement.TransferMember;

public class TransferMemberUpdate
{
    public required int MemberId { get; set; }
    public required decimal Value { get; set; }
    public required decimal? Weight { get; set; }
}