using Microsoft.Data.SqlClient;
using Splitey.Data.Resources.Settlement.SettlementMember;
using Splitey.DependencyInjection.Attributes;
using Splitey.Models.Settlement.SettlementMember;
using Splitey.Models.User;

namespace Splitey.Data.Repositories.Settlement.SettlementMember;

[SingletonDependency]
public class SettlementMemberRepository(SqlConnection sqlConnection) : BaseRepository(sqlConnection)
{
    public Task<IEnumerable<SettlementMemberDto>> GetList(int settlementId)
    {
        return Query<SettlementMemberDto>(SqlQuery.GetList, param: new
        {
            SettlementId = settlementId,
        });
    }
    
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
    
    public Task<SettlementMemberItem?> GetUser(int settlementId, int userId)
    {
        return Get(settlementId, userId: userId);
    }

    public Task<SettlementMemberItem?> GetContact(int settlementId, int contactId)
    {
        return Get(settlementId, contactId: contactId);
    }

    private Task<SettlementMemberItem?> Get(int settlementId, int? userId = null, int? contactId = null)
    {
        return QueryFirstOrDefaultAsync<SettlementMemberItem>(SqlQuery.Get, param: new
        {
            SettlementId = settlementId,
            UserId = userId,
            ContactId = contactId,
        });
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
}