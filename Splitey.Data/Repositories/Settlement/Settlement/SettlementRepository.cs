using Microsoft.Data.SqlClient;
using Splitey.Data.Resources.Settlement.Settlement;
using Splitey.DependencyInjection.Attributes;
using Splitey.Models.Settlement.Settlement;

namespace Splitey.Data.Repositories.Settlement.Settlement;

[SingletonDependency]
public class SettlementRepository(SqlConnection sqlConnection) : BaseRepository(sqlConnection)
{
    public Task<IEnumerable<SettlementItem>> GetList(int userId)
    {
        return Query<SettlementItem>(SqlQuery.GetList, param: new { UserId = userId });
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
    
    public Task<IEnumerable<SettlementArrangementItem>> GetArrangement(int settlementId)
    {
        return Query<SettlementArrangementItem>(SqlQuery.GetArrangement, param: new
        {
            SettlementId = settlementId
        });
    }
}