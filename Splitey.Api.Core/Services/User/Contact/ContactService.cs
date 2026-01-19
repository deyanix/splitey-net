using Splitey.Api.Models.Contact;
using Splitey.Authorization;
using Splitey.Data.Repositories.User.Contact;
using Splitey.Data.Repositories.User.ContactAccess;
using Splitey.Data.Transaction;
using Splitey.DependencyInjection.Attributes;
using Splitey.Models.User;
using Splitey.Models.User.Contact;

namespace Splitey.Core.Services.User.Contact;

[ScopedDependency]
public class ContactService(
    AuthorizationService authorizationService,
    ContactAccessorService contactAccessorService,
    ContactRepository contactRepository,
    ContactAccessRepository contactAccessRepository) 
{
    public async Task<IEnumerable<ContactDto>> GetList()
    {
        return await contactRepository.GetList(authorizationService.User.Id);
    }
    
    public async Task<ContactGetResponse> Get(int contactId)
    {
        await contactAccessorService.EnsureAccess(contactId, AccessMode.ReadOnly);
        var contact = await contactRepository.Get(contactId);
        var contactAccess = await contactAccessRepository.GetList(contactId);

        return new ContactGetResponse()
        {
            Id = contact.Id,
            Email = contact.Email,
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            AccessItems = contactAccess,
        };
    }
    
    public async Task<int> Create(ContactUpdate request)
    {
        using (var transaction = TransactionBuilder.Default)
        {
            var contactId = await contactRepository.Create(request);
            await contactAccessRepository.Upsert(contactId, authorizationService.User.Id, AccessMode.FullAccess);
            transaction.Complete();
            
            return contactId;
        }
    }
    
    public async Task Update(int contactId, ContactUpdate request)
    {
        await contactAccessorService.EnsureAccess(contactId, AccessMode.ReadWrite);
        await contactRepository.Update(contactId, request);
    }
    
    public async Task Delete(int contactId)
    {
        await contactAccessorService.EnsureAccess(contactId, AccessMode.FullAccess);
        await contactRepository.Delete(contactId);
    }

    public async Task UpsertAccess(int contactId, ContactUpdateAccess request)
    {
        await contactAccessorService.EnsureAccess(contactId, AccessMode.FullAccess);
        await contactAccessRepository.Upsert(contactId, request.UserId, request.AccessModeId);
    }

    public async Task DeleteAccess(int contactId, ContactDeleteAccess request)
    {
        await contactAccessorService.EnsureAccess(contactId, AccessMode.FullAccess);
        await contactAccessRepository.Delete(contactId, request.UserId);
    }
}