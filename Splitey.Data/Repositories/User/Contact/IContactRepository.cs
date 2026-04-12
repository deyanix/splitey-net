using Splitey.Models.User.Contact;

namespace Splitey.Data.Repositories.User.Contact;

public interface IContactRepository
{
    Task<IEnumerable<ContactDto>> GetList(int userId);
    Task<ContactItem> Get(int contactId);
    Task<int> Create(ContactUpdate request);
    Task Update(int contactId, ContactUpdate request);
    Task Delete(int transferId);
}
