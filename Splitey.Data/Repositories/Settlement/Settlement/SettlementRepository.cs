using Splitey.Data.Resources.Settlement.Settlement;
using Splitey.Data.Sql;
using Splitey.DependencyInjection.Attributes;
using Splitey.Models.Settlement.Settlement;

namespace Splitey.Data.Repositories.Settlement.Settlement;

[SingletonDependency]
public class SettlementRepository(ISqlConnectionFactory sqlConnectionFactory) : BaseRepository(sqlConnectionFactory)
{
    public Task<IEnumerable<SettlementItem>> GetList(int userId)
    {
        return Query<SettlementItem>(SqlQuery.GetList, param: new { UserId = userId });
    }
    
    public Task<SettlementItem> Get(int settlementId)
    {
        return QueryFirst<SettlementItem>(SqlQuery.Get, param: new { SettlementId = settlementId });
    }
    
    public Task<int> Create(SettlementUpdate request)
    {
        return QueryFirst<int>(SqlQuery.Create, param: new
        {
            Name = request.Name,
            Description = request.Description,
            CurrencyId = request.CurrencyId,
        });
    }
    
    public Task Update(int id, SettlementUpdate request)
    {
        return Query<SettlementItem>(SqlQuery.Update, param: new
        {
            SettlementId = id,
            Name = request.Name,
            Description = request.Description,
            CurrencyId = request.CurrencyId,
        });
    }
    
    public Task Delete(int id)
    {
        return Query<SettlementItem>(SqlQuery.Delete, param: new
        {
            SettlementId = id,
        });
    }
    
    public Task<IEnumerable<SettlementDebtItem>> GetDebts(int settlementId)
    {
        return Query<SettlementDebtItem>(SqlQuery.GetDebts, param: new
        {
            SettlementId = settlementId
        });
    }
}