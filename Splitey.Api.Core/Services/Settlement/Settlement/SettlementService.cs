using Splitey.Authorization;
using Splitey.Data.Repositories.Settlement.Settlement;
using Splitey.Data.Repositories.Settlement.SettlementMember;
using Splitey.Data.Repositories.Settlement.Transfer;
using Splitey.Data.Transaction;
using Splitey.DependencyInjection.Attributes;
using Splitey.Models.Settlement.Settlement;
using Splitey.Models.User;

namespace Splitey.Core.Services.Settlement.Settlement;

[ScopedDependency]
public class SettlementService(
    AuthorizationService authorizationService,
    SettlementAccessorService settlementAccessorService,
    SettlementRepository settlementRepository,
    SettlementMemberRepository settlementMemberRepository,
    TransferRepository transferRepository)
{
    public Task<IEnumerable<SettlementItem>> GetList()
    {
        return settlementRepository.GetList(authorizationService.User.Id);
    }
    
    public async Task<SettlementItem> Get(int id)
    {
        await settlementAccessorService.EnsureAccess(id, AccessMode.ReadOnly);
        var settlement = await settlementRepository.Get(id);
        settlement.AccessModeId = (int?) await settlementAccessorService.GetAccessMode(id);
        return settlement;
    }
    
    public async Task<int> Create(SettlementUpdate request)
    {
        using (var transaction = TransactionBuilder.Default)
        {
            int settlementId = await settlementRepository.Create(request);
            await settlementMemberRepository.UpsertUser(settlementId, authorizationService.User.Id, AccessMode.FullAccess);
            transaction.Complete();
            
            return settlementId;
        }
    }
    
    public async Task Update(int settlementId, SettlementUpdate request)
    {
        await settlementAccessorService.EnsureAccess(settlementId, AccessMode.FullAccess);
        await settlementRepository.Update(settlementId, request);
    }
    
    public async Task Delete(int settlementId)
    {
        await settlementAccessorService.EnsureAccess(settlementId, AccessMode.FullAccess);
        using (var transaction = TransactionBuilder.Default)
        {
            await transferRepository.DeleteBySettlement(settlementId);
            await settlementRepository.Delete(settlementId);
            transaction.Complete();
        }
    }
    
    
}