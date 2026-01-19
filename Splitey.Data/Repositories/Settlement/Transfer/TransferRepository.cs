using Splitey.Data.Resources.Settlement.Transfer;
using Splitey.Data.Sql;
using Splitey.DependencyInjection.Attributes;
using Splitey.Models.Settlement.Transfer;

namespace Splitey.Data.Repositories.Settlement.Transfer;

[SingletonDependency]
public class TransferRepository(ISqlConnectionFactory sqlConnectionFactory) : BaseRepository(sqlConnectionFactory)
{
    public Task<IEnumerable<TransferDto>> GetList(int settlementId)
    {
        return Query<TransferDto>(SqlQuery.GetList, param: new
        {
            SettlementId = settlementId,
        });
    }
    
    public Task<TransferDto> Get(int transferId)
    {
        return QueryFirst<TransferDto>(SqlQuery.Get, param: new
        {
            TransferId = transferId,
        });
    }
    
    public Task<int> Create(int settlementId, TransferUpdate request)
    {
        return QueryFirst<int>(SqlQuery.Create, param: new
        {
            SettlementId = settlementId,
            Name = request.Name,
            Date = request.Date,
            PayerMemberId = request.PayerMemberId,
            TypeId = request.TypeId,
            DivisionModeId = request.DivisionModeId,
            TotalValue = request.TotalValue,
            CurrencyId = request.CurrencyId,
        });
    }
    
    public Task Update(int transferId, TransferUpdate request)
    {
        return Execute(SqlQuery.Update, param: new
        {
            TransferId = transferId,
            Name = request.Name,
            Date = request.Date,
            PayerMemberId = request.PayerMemberId,
            TypeId = request.TypeId,
            DivisionModeId = request.DivisionModeId,
            TotalValue = request.TotalValue,
            CurrencyId = request.CurrencyId,
        });
    }
    
    public Task Delete(int transferId)
    {
        return Execute(SqlQuery.Delete, param: new
        {
            TransferId = transferId,
        });
    }
}