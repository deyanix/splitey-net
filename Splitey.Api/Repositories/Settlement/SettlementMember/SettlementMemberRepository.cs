using Dapper;
using Microsoft.Data.SqlClient;
using Splitey.Api.Common.DependencyInjection.Attributes;
using Splitey.Api.Models.Settlement.SettlementMember;
using Splitey.Api.Repositories.User;
using Splitey.Api.Resources.Payment.SettlementMember;

namespace Splitey.Api.Repositories.Settlement.SettlementMember;

[SingletonDependency]
public class SettlementMemberRepository(SqlConnection sqlConnection) : BaseRepository(sqlConnection)
{
    public Task UpsertUser(int settlementId, int userId, AccessMode accessMode)
    {
        return Merge(settlementId, userId: userId, accessMode: accessMode);
    }
    
    public Task UpsertContact(int settlementId, int contactId, AccessMode accessMode)
    {
        return Merge(settlementId, contactId: contactId, accessMode: accessMode);
    }
    
    public Task DeleteUser(int settlementId, int userId)
    {
        return Merge(settlementId, userId: userId);
    }

    public Task DeleteContact(int settlementId, int contactId)
    {
        return Merge(settlementId, contactId: contactId);
    }

    private Task<int> Merge(int settlementId, int? userId = null, int? contactId = null, AccessMode? accessMode = null)
    {
        return Execute(SqlQuery.Merge, param: new
        {
            SettlementId = settlementId,
            UserId = userId,
            ContactId = contactId,
            AccessModeId = (int?)accessMode,
        });
    }
    
    public Task<IEnumerable<SettlementMemberItem>> GetList(int settlementId)
    {
        return Query<SettlementMemberItem>(SqlQuery.GetList, param: new
        {
            SettlementId = settlementId
        });
    }
}