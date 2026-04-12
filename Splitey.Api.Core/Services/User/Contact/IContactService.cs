using Splitey.Api.Models.Contact;
using Splitey.Models.User.Contact;

namespace Splitey.Core.Services.User.Contact;

public interface IContactService
{
    Task<IEnumerable<ContactDto>> GetList();
    Task<ContactGetResponse> Get(int contactId);
    Task<int> Create(ContactUpdate request);
    Task Update(int contactId, ContactUpdate request);
    Task Delete(int contactId);
    Task UpsertAccess(int contactId, ContactUpdateAccess request);
    Task DeleteAccess(int contactId, ContactDeleteAccess request);
}