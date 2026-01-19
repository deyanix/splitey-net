using Microsoft.Data.SqlClient;
using Splitey.Data.Resources.User.ContactAccess;
using Splitey.DependencyInjection.Attributes;
using Splitey.Models.User;
using Splitey.Models.User.ContactAccess;

namespace Splitey.Data.Repositories.User.ContactAccess;

[SingletonDependency]
public class ContactAccessRepository(SqlConnection sqlConnection) : BaseRepository(sqlConnection)
{
    public Task<IEnumerable<ContactAccessDto>> GetList(int contactId)
    {
        return Query<ContactAccessDto>(SqlQuery.GetList, param: new
        {
            ContactId = contactId,
        });
    }
    
    public Task<ContactAccessDto?> Get(int contactId, int userId)
    {
        return QueryFirstOrDefaultAsync<ContactAccessDto>(SqlQuery.Get, param: new
        {
            ContactId = contactId,
            UserId = userId,
        });
    }

    public Task Upsert(int contactId, int userId, AccessMode accessMode)
    {
        return Merge(contactId, userId, accessMode);
    }

    public Task Delete(int contactId, int userId)
    {
        return Merge(contactId, userId);
    }
    
    private Task Merge(int contactId, int userId, AccessMode? accessMode = null)
    {
        return Execute(SqlQuery.Merge, param: new
        {
            ContactId = contactId,
            UserId = userId,
            AccessModeId = (int?)accessMode,
        });
    }
}