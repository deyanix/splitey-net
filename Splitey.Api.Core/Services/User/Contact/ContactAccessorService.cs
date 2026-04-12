using Splitey.Authorization;
using Splitey.Data.Repositories.User.ContactAccess;
using Splitey.DependencyInjection.Attributes;
using Splitey.Models.User;

namespace Splitey.Core.Services.User.Contact;

[Scoped]
public class ContactAccessorService(
    AuthorizationService authorizationService,
    IContactAccessRepository contactAccessRepository)
{
    public async Task<AccessMode?> GetAccessMode(int contactId)
    {
        var member = await contactAccessRepository.Get(contactId, authorizationService.User.Id);
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
