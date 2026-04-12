using Splitey.Models.Settlement.Settlement;

namespace Splitey.Data.Repositories.Settlement.Settlement;

public interface ISettlementRepository
{
    Task<IEnumerable<SettlementItem>> GetList(int userId);
    Task<SettlementItem> Get(int settlementId);
    Task<int> Create(SettlementUpdate request);
    Task Update(int id, SettlementUpdate request);
    Task Delete(int id);
    Task<IEnumerable<SettlementDebtItem>> GetDebts(int settlementId);
}
