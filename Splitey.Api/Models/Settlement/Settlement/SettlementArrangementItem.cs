namespace Splitey.Api.Models.Settlement.Settlement;

public class SettlementArrangementItem
{
    public required int From { get; init; }
    public required int To { get; init; }
    public required decimal Balance { get; init; }
}