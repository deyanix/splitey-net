using Splitey.Authorization;
using Splitey.Data.Repositories.Settlement.Settlement;
using Splitey.Data.Repositories.Settlement.SettlementMember;
using Splitey.DependencyInjection.Attributes;
using Splitey.Models.Settlement.Settlement;
using Splitey.Models.Settlement.SettlementMember;
using Splitey.Models.User;

namespace Splitey.Core.Services.Settlement.Settlement;

[ScopedDependency]
public class SettlementDebtService(
    SettlementRepository settlementRepository,
    SettlementAccessorService settlementAccessorService,
    SettlementMemberRepository settlementMemberRepository)
{
    public async Task<IList<SettlementDebtItem>> GetDebts(int settlementId)
    {
        await settlementAccessorService.EnsureAccess(settlementId, AccessMode.ReadOnly);
        
        return (await settlementRepository.GetDebts(settlementId))
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

                return new SettlementDebtItem()
                {
                    From = totalUser1 > totalUser2 ? user1 : user2,
                    To = totalUser1 > totalUser2 ? user2 : user1,
                    Balance = Math.Abs(totalUser1 - totalUser2),
                };
            })
            .Where(item => item != null)
            .OfType<SettlementDebtItem>()
            .ToList();
    }

    public async Task<IList<SettlementSummaryItem>> GetSummary(int settlementId)
    {
        IList<SettlementDebtItem> items = await GetDebts(settlementId);
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

    public async Task<IList<SettlementDebtItem>> GetSimplifiedDebts(int settlementId)
    {
        IList<SettlementSummaryItem> items = await GetSummary(settlementId);
        IList<SettlementDebtItem> arrangementItems = new List<SettlementDebtItem>();
        
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
            
            arrangementItems.Add(new SettlementDebtItem()
            {
                From = minRecord.MemberId,
                To = maxRecord.MemberId,
                Balance = min,
            });
        }

        return arrangementItems;
    }
}