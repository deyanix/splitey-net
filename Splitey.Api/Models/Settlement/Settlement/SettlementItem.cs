namespace Splitey.Api.Models.Settlement.Settlement;

public class SettlementItem
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string? Description { get; init; }
    public required int CurrencyId { get; init; }
    public required string CurrencyCode { get; init; }
}