namespace Splitey.Api.Models.Payment.Settlement;

public class SettlementMember
{
    public required int Id { get; init; }
    public required int UserId { get; init; }
    public required int PersonId { get; init; }
}