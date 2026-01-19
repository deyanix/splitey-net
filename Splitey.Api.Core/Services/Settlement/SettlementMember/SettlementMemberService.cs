using Splitey.Api.Models.SettlementMember;
using Splitey.Core.Services.Settlement.Settlement;
using Splitey.Data.Repositories.Settlement.SettlementMember;
using Splitey.DependencyInjection.Attributes;
using Splitey.Models.Settlement.SettlementMember;
using Splitey.Models.User;

namespace Splitey.Core.Services.Settlement.SettlementMember;

[ScopedDependency]
public class SettlementMemberService(
    SettlementAccessorService settlementAccessorService,
    SettlementMemberRepository settlementMemberRepository) 
{
    public async Task<IEnumerable<SettlementMemberDto>> GetList(int settlementId)
    {
        await settlementAccessorService.EnsureAccess(settlementId, AccessMode.ReadOnly);
        return await settlementMemberRepository.GetList(settlementId);
    }

    public async Task UpsertUser(int settlementId, SettlementMemberUpdateUser request)
    {
        await settlementAccessorService.EnsureAccess(settlementId, AccessMode.FullAccess);
        await settlementMemberRepository.UpsertUser(settlementId, request.UserId, request.AccessModeId);
    }

    public async Task UpsertContact(int settlementId, SettlementMemberUpdateContact request)
    {
        await settlementAccessorService.EnsureAccess(settlementId, AccessMode.FullAccess);
        await settlementMemberRepository.UpsertContact(settlementId, request.ContactId, AccessMode.ReadOnly);
    }

    public async Task DeleteUser(int settlementId, SettlementMemberDeleteUser request)
    {
        await settlementAccessorService.EnsureAccess(settlementId, AccessMode.FullAccess);
        await settlementMemberRepository.DeleteUser(settlementId, request.UserId);
    }

    public async Task DeleteContact(int settlementId, SettlementMemberUpdateContact request)
    {
        await settlementAccessorService.EnsureAccess(settlementId, AccessMode.FullAccess);
        await settlementMemberRepository.DeleteContact(settlementId, request.ContactId);
    }
}