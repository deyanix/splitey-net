namespace Splitey.Api.Models.Payment.Settlement;

public class SettlementSummaryItem
{
    public required int MemberId { get; set; }
    public required decimal Balance { get; set; }
}