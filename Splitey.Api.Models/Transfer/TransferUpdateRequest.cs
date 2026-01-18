using Splitey.Models.Settlement;

namespace Splitey.Api.Models.Transfer;

public class TransferUpdateRequest
{
    public required string Name { get; set; }
    public required DateTime Date { get; set; }
    public required int PayerMemberId { get; set; }
    public required TransferType TypeId { get; set; } 
    public required DivisionMode DivisionModeId { get; set; }
    public required decimal TotalValue { get; set; }
    public required int CurrencyId { get; set; }
    public required IEnumerable<TransferMemberUpdateRequest> Members { get; set; }
}