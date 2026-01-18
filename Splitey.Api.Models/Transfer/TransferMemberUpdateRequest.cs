namespace Splitey.Api.Models.Transfer;

public class TransferMemberUpdateRequest
{
    public required int MemberId { get; set; }
    public required decimal Value { get; set; }
    public required decimal? Weight { get; set; }
}