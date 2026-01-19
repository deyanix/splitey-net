namespace Splitey.Models.Settlement.TransferMember;

public class TransferMemberDto
{
    public required int MemberId { get; set; }
    public required decimal Value { get; set; }
    public required decimal? Weight { get; set; }
}