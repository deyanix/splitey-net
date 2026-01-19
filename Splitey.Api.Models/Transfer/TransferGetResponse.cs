using Splitey.Models.Settlement;

namespace Splitey.Api.Models.Transfer;

public class TransferGetResponse
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required DateTime Date { get; init; }
    public required int PayerMemberId { get; init; }
    public required TransferType TypeId { get; init; } 
    public required DivisionMode DivisionModeId { get; init; }
    public required decimal TotalValue { get; init; }
    public required int CurrencyId { get; init; }
    public required IEnumerable<TransferGetMemberResponse> Members { get; init; }
}