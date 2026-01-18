using Splitey.Authorization;
using Splitey.Data.Repositories.Settlement.Settlement;
using Splitey.Data.Repositories.Settlement.SettlementMember;
using Splitey.Data.Transaction;
using Splitey.DependencyInjection.Attributes;
using Splitey.Models.Settlement.Settlement;
using Splitey.Models.Settlement.SettlementMember;
using Splitey.Models.User;

namespace Splitey.Core.Services.Settlement.Settlement;

[ScopedDependency]
public class SettlementService(
    AuthorizationService authorizationService,
    SettlementAccessorService settlementAccessorService,
    SettlementRepository settlementRepository,
    SettlementMemberRepository settlementMemberRepository)
{
    public Task<IEnumerable<SettlementItem>> GetList()
    {
        return settlementRepository.GetList(authorizationService.UserId);
    }
    
    public async Task<int> Create(SettlementUpdate request)
    {
        using (var transaction = TransactionBuilder.Default)
        {
            int settlementId = await settlementRepository.Create(request);
            await settlementMemberRepository.UpsertUser(settlementId, authorizationService.UserId, AccessMode.FullAccess);
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
        await settlementRepository.Delete(settlementId);
    }
    
    public Task<IList<SettlementArrangementItem>> GetArrangement(int settlementId, bool optimized)
    {
        return optimized ? GetOptimizedArrangement(settlementId) : GetArrangement(settlementId);
    }
    
    public async Task<IList<SettlementArrangementItem>> GetArrangement(int settlementId)
    {
        return (await settlementRepository.GetArrangement(settlementId))
            .GroupBy(item => new
            {
                User1 = Math.Min(item.From, item.To), 
                User2 = Math.Max(item.From, item.To),
            })
            .Select(group =>
            {
                int user1 = group.Key.User1;
                int user2 = group.Key.User2;
                decimal totalUser1 = group
                    .Where(item => item.To == user1)
                    .Sum(item => item.Balance);
                decimal totalUser2 = group
                    .Where(item => item.To == user2)
                    .Sum(item => item.Balance);
                
                if (totalUser1 == totalUser2)
                {
                    return null;
                }

                return new SettlementArrangementItem()
                {
                    From = totalUser1 > totalUser2 ? user1 : user2,
                    To = totalUser1 > totalUser2 ? user2 : user1,
                    Balance = Math.Abs(totalUser1 - totalUser2),
                };
            })
            .Where(item => item != null)
            .OfType<SettlementArrangementItem>()
            .ToList();
    }

    public async Task<IList<SettlementSummaryItem>> GetSummary(int settlementId)
    {
        IList<SettlementArrangementItem> items = await GetArrangement(settlementId);
        IEnumerable<SettlementMemberModel> members = await settlementMemberRepository.GetList(settlementId);

        return members
            .Select(member =>
            {
                decimal expenses = items
                    .Where(item => item.From == member.Id)
                    .Sum(item => item.Balance);
                decimal incomes = items
                    .Where(item => item.To == member.Id)
                    .Sum(item => item.Balance);
                
                return new SettlementSummaryItem()
                {
                    MemberId = member.Id,
                    Balance = incomes - expenses,
                };
            })
            .ToList();
    }

    public async Task<IList<SettlementArrangementItem>> GetOptimizedArrangement(int settlementId)
    {
        IList<SettlementSummaryItem> items = await GetSummary(settlementId);
        IList<SettlementArrangementItem> arrangementItems = new List<SettlementArrangementItem>();
        while (true)
        {
            SettlementSummaryItem? minRecord = items.MinBy(item => item.Balance);
            SettlementSummaryItem? maxRecord = items.MaxBy(item => item.Balance);
            if (minRecord == null || maxRecord == null)
            {
                break;
            }
            
            decimal minBalance = minRecord.Balance;
            decimal maxBalance = maxRecord.Balance;
            if (minBalance == 0 || maxBalance == 0)
            {
                break;
            }
            
            decimal min = Math.Min(-minBalance, maxBalance);
            maxRecord.Balance = maxBalance - min;
            minRecord.Balance = minBalance + min;
            
            arrangementItems.Add(new SettlementArrangementItem()
            {
                From = minRecord.MemberId,
                To = maxRecord.MemberId,
                Balance = min,
            });
        }

        return arrangementItems;
    }
}