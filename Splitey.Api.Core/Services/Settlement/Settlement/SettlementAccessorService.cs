using Splitey.Authorization;
using Splitey.Data.Repositories.Settlement.SettlementMember;
using Splitey.DependencyInjection.Attributes;
using Splitey.Models.User;

namespace Splitey.Core.Services.Settlement.Settlement;

[ScopedDependency]
public class SettlementAccessorService(
    AuthorizationService authorizationService,
    SettlementMemberRepository settlementMemberRepository)
{
public async Task<AccessMode?> GetAccessMode(int settlementId)
{
    var member = await settlementMemberRepository.GetUser(settlementId, authorizationService.User.Id);
    return member?.AccessModeId;
}

public async Task<bool> HasAccess(int settlementId, AccessMode expectedMode)
{
    var accessMode = await GetAccessMode(settlementId);
    var accessModeIdx = accessMode.HasValue ? (int)accessMode : 0;
    
    return accessModeIdx >= (int)expectedMode;
}

public async Task EnsureAccess(int settlementId, AccessMode expectedMode)
{
    if (await HasAccess(settlementId, expectedMode))
        return;
    
    throw new Exception("Not authorized");
}
}