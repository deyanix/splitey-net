using Splitey.Models.Settlement.Transfer;

namespace Splitey.Data.Repositories.Settlement.Transfer;

public interface ITransferRepository
{
    Task<IEnumerable<TransferDto>> GetList(int settlementId);
    Task<TransferDto> Get(int transferId);
    Task<int> Create(int settlementId, TransferUpdate request);
    Task Update(int transferId, TransferUpdate request);
    Task Delete(int transferId);
    Task DeleteBySettlement(int settlementId);
}
