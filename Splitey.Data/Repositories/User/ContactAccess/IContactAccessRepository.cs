using Splitey.Models.User;
using Splitey.Models.User.ContactAccess;

namespace Splitey.Data.Repositories.User.ContactAccess;

public interface IContactAccessRepository
{
    Task<IEnumerable<ContactAccessDto>> GetList(int contactId);
    Task<ContactAccessDto?> Get(int contactId, int userId);
    Task Upsert(int contactId, int userId, AccessMode accessMode);
    Task Delete(int contactId, int userId);
}
