using Splitey.Models.Settlement.TransferMember;

namespace Splitey.Data.Repositories.Settlement.TransferMember;

public interface ITransferMemberRepository
{
    Task<IEnumerable<TransferMemberDto>> GetList(int transferId);
    Task Merge(int transferId, IEnumerable<TransferMemberDto> transferMembers);
}
