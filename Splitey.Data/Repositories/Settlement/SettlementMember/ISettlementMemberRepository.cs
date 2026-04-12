using Splitey.Models.Settlement.SettlementMember;
using Splitey.Models.User;

namespace Splitey.Data.Repositories.Settlement.SettlementMember;

public interface ISettlementMemberRepository
{
    Task<IEnumerable<SettlementMemberDto>> GetList(int settlementId);
    Task UpsertUser(int settlementId, int userId, AccessMode accessMode);
    Task UpsertContact(int settlementId, int contactId, AccessMode accessMode);
    Task DeleteUser(int settlementId, int userId);
    Task DeleteContact(int settlementId, int contactId);
    Task<SettlementMemberItem?> GetUser(int settlementId, int userId);
    Task<SettlementMemberItem?> GetContact(int settlementId, int contactId);
}
