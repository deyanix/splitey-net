namespace Splitey.Api.Models.Transfer;

public class TransferUpdateMemberRequest
{
    public required int MemberId { get; set; }
    public required string MemberFirstName { get; init; }
    public required string? MemberLastName { get; init; }
    public required decimal Value { get; set; }
    public required decimal? Weight { get; set; }
}