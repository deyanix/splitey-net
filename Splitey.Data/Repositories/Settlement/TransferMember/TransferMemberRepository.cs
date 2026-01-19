using System.Text.Json;
using Microsoft.Data.SqlClient;
using Splitey.Data.Resources.Settlement.TransferMember;
using Splitey.DependencyInjection.Attributes;
using Splitey.Models.Settlement.TransferMember;

namespace Splitey.Data.Repositories.Settlement.TransferMember;

[SingletonDependency]
public class TransferMemberRepository(SqlConnection sqlConnection) : BaseRepository(sqlConnection)
{
    public Task<IEnumerable<TransferMemberDto>> GetList(int transferId)
    {
        return Query<TransferMemberDto>(SqlQuery.GetList, param: new
        {
            TransferId = transferId,
        });
    }
    
    public Task Merge(int transferId, IEnumerable<TransferMemberDto> transferMembers)
    {
        return Execute(SqlQuery.Merge, param: new
        {
            TransferId = transferId,
            TransferMembers = JsonSerializer.Serialize(transferMembers),
        });
    }
}