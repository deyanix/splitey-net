using Dapper;
using Dapper.FastCrud;
using Npgsql;
using Splitey.Api.Common.DependencyInjection.Attributes;
using Splitey.Api.DataModels.Payment;
using Splitey.Api.Models.Payment.Settlement;
using Splitey.Api.Resources.Payment.Settlement;

namespace Splitey.Api.Repositories.Payment.Settlement;

[SingletonDependency]
public class SettlementRepository(NpgsqlConnection connection)
{
    public Task<IEnumerable<SettlementArrangementItem>> GetArrangement(int settlementId)
    {
        return connection
            .QueryAsync<SettlementArrangementItem>(SqlQuery.GetArrangement, param: new { SettlementId = settlementId });
    }
    
    public Task<IEnumerable<SettlementMemberModel>> GetMembers(int settlementId)
    {
        var parameters = new { SettlementId = settlementId };
        
        return connection.FindAsync<SettlementMemberModel>(statement => statement
            .WithAlias("sm")
            .Where($@"{nameof(SettlementMemberModel.SettlementId):of sm} = {nameof(parameters.SettlementId):P}")
            .WithParameters(parameters));
    }
}