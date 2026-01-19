using Microsoft.Data.SqlClient;
using Splitey.Data.Resources.User.Contact;
using Splitey.DependencyInjection.Attributes;
using Splitey.Models.User.Contact;

namespace Splitey.Data.Repositories.User.Contact;

[SingletonDependency]
public class ContactRepository(SqlConnection sqlConnection) : BaseRepository(sqlConnection)
{
    public Task<IEnumerable<ContactDto>> GetList(int userId)
    {
        return Query<ContactDto>(SqlQuery.GetList, param: new
        {
            UserId = userId,
        });
    }
    
    public Task<ContactItem> Get(int contactId)
    {
        return QueryFirst<ContactItem>(SqlQuery.Get, param: new
        {
            ContactId = contactId,
        });
    }
    
    public Task<int> Create(ContactUpdate request)
    {
        return QueryFirst<int>(SqlQuery.Create, param: new
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
        });
    }
    
    public Task Update(int contactId, ContactUpdate request)
    {
        return Execute(SqlQuery.Update, param: new
        {
            ContactId = contactId,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
        });
    }
    
    public Task Delete(int transferId)
    {
        return Execute(SqlQuery.Delete, param: new
        {
            TransferId = transferId,
        });
    }
}