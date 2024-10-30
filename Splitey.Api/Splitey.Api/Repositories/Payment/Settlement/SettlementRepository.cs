using Dapper;
using Npgsql;
using Splitey.Api.Common.DependencyInjection.Attributes;
using Splitey.Api.Models.Payment.Settlement;
using Splitey.Api.Resources.Payment.Settlement;

namespace Splitey.Api.Repositories.Payment.Settlement;

[SingletonDependency]
public class SettlementRepository(NpgsqlConnection connection)
{
    public Task<IEnumerable<SettlementArrangementItem>> GetArrangement(int settlementId)
    {
        return connection
            .QueryAsync<SettlementArrangementItem>(Sql.GetArrangement, param: new { SettlementId = settlementId });
    }
    
    public Task<IEnumerable<SettlementMember>> GetMembers(int settlementId)
    {
        return connection
            .QueryAsync<SettlementMember>(Sql.GetMembers, param: new { SettlementId = settlementId });
    }
}