using Splitey.Api.Models.Transfer;
using Splitey.Core.Services.Settlement.Settlement;
using Splitey.Data.Repositories.Settlement.SettlementMember;
using Splitey.Data.Repositories.Settlement.Transfer;
using Splitey.Data.Repositories.Settlement.TransferMember;
using Splitey.Data.Transaction;
using Splitey.DependencyInjection.Attributes;
using Splitey.Models.Settlement;
using Splitey.Models.Settlement.Transfer;
using Splitey.Models.Settlement.TransferMember;
using Splitey.Models.User;

namespace Splitey.Core.Services.Settlement.Transfer;

[ScopedDependency]
public class TransferService(
    SettlementAccessorService settlementAccessorService,
    SettlementMemberRepository settlementMemberRepository,
    TransferRepository transferRepository,
    TransferMemberRepository transferMemberRepository)
{
    public async Task<IEnumerable<TransferDto>> GetList(int settlementId)
    {
        await settlementAccessorService.EnsureAccess(settlementId, AccessMode.ReadOnly);
        return await transferRepository.GetList(settlementId);
    }

    public async Task<TransferGetResponse> Get(int settlementId, int transferId)
    {
        var transferDto = await EnsureTransferAccess(settlementId, transferId, AccessMode.ReadOnly);
        var transferMembers = await transferMemberRepository.GetList(transferId);
        var settlementMembers = await settlementMemberRepository.GetList(settlementId);

        return new TransferGetResponse()
        {
            Id = transferDto.Id,
            Name = transferDto.Name,
            Date = transferDto.Date,
            PayerMemberId = transferDto.PayerMemberId,
            TypeId = transferDto.TypeId,
            DivisionModeId = transferDto.DivisionModeId,
            TotalValue = transferDto.TotalValue,
            CurrencyId = transferDto.CurrencyId,
            Members = transferMembers
                .Select(x =>
                {
                    var settlementMember = settlementMembers.FirstOrDefault(m => m.Id == x.MemberId)
                        ?? throw new Exception("Not found transfer member");

                    return new TransferGetMemberResponse()
                    {
                        MemberId = settlementMember.Id,
                        MemberFirstName = settlementMember.FirstName,
                        MemberLastName = settlementMember.LastName,
                        Value = x.Value,
                        Weight = x.Weight,
                    };
                })
                .ToList(),
        };
    }
    
    public async Task<int> Create(int settlementId, TransferUpdateRequest request)
    {
        await settlementAccessorService.EnsureAccess(settlementId, AccessMode.ReadWrite);
        await EnsureTransferMembers(settlementId, request);
        
        var (transfer, transferMembers) = ParseTransferUpdate(request);
        using (var transaction = TransactionBuilder.Default)
        {
            var transferId = await transferRepository.Create(settlementId, transfer);
            await transferMemberRepository.Merge(transferId, transferMembers);
            transaction.Complete();

            return transferId;
        }
    }
    
    public async Task Update(int settlementId, int transferId, TransferUpdateRequest request)
    {
        var transferDto = await EnsureTransferAccess(settlementId, transferId, AccessMode.ReadWrite);
        await EnsureTransferMembers(transferDto.SettlementId, request);
        
        var (transfer, transferMembers) = ParseTransferUpdate(request);
        using (var transaction = TransactionBuilder.Default)
        {
            await transferRepository.Update(transferId, transfer);
            await transferMemberRepository.Merge(transferId, transferMembers);
            transaction.Complete();
        }
    }

    public async Task Delete(int settlementId, int transferId)
    {
        await EnsureTransferAccess(settlementId, transferId, AccessMode.ReadWrite);
        await transferRepository.Delete(transferId);
    }

    private async Task EnsureTransferMembers(int settlementId, TransferUpdateRequest request)
    {
        var members = await settlementMemberRepository.GetList(settlementId);
        if (request.Members.Any(rm => !members.Any(m => m.Id == rm.MemberId)))
            throw new Exception("Selected a member outside the settlement");
    }

    private async Task<TransferDto> EnsureTransferAccess(int settlementId, int transferId, AccessMode accessMode)
    {
        var transferDto = await transferRepository.Get(transferId);
        if (transferDto == null)
            throw new Exception("Transfer not exists");
        
        if (transferDto.SettlementId != settlementId)
            throw new Exception("Incorrect settlement identifier");
        
        await settlementAccessorService.EnsureAccess(transferDto.SettlementId, accessMode);
        return transferDto;
    }

    private (TransferUpdate, List<TransferMemberDto>) ParseTransferUpdate(TransferUpdateRequest request)
    {
        var transferMembers = request.Members
            .Select(x => new TransferMemberDto()
            {
                MemberId = x.MemberId,
                Value = x.Value,
                Weight = x.Weight,
            })
            .ToList();
        var totalValue = transferMembers.Sum(x => x.Value);

        var transferUpdate = new TransferUpdate()
        {
            Name = request.Name,
            DivisionModeId = request.DivisionModeId,
            TypeId = request.TypeId,
            TotalValue = totalValue,
            CurrencyId = request.CurrencyId,
            PayerMemberId = request.PayerMemberId,
            Date = request.Date,
        };
        
        return (transferUpdate, transferMembers);
    }

    private List<TransferMemberDto> BuildTransferUpdateMembers(TransferUpdateRequest request)
    {
        var totalWeight = request.Members.Sum(x => x.Weight);
        
        switch (request.DivisionModeId)
        {
            case DivisionMode.Amount:
                return request.Members
                    .Select(x => new TransferMemberDto()
                    {
                        MemberId = x.MemberId,
                        Value = x.Value,
                        Weight = null,
                    })
                    .ToList();
            case DivisionMode.Percentage:
                return request.Members
                    .Select(x => new TransferMemberDto()
                    {
                        MemberId = x.MemberId,
                        Value = Math.Round(
                            x.Weight * request.TotalValue 
                                ?? throw new Exception("Not specified weight for percentage division"), 
                            2),
                        Weight = x.Weight,
                    })
                    .ToList();
            case DivisionMode.Proportionally:
                return request.Members
                    .Select(x => new TransferMemberDto()
                    {
                        MemberId = x.MemberId,
                        Value = Math.Round(
                            (x.Weight / totalWeight) * request.TotalValue
                                ?? throw new Exception("Not specified weight for percentage division"),
                            2),
                        Weight = x.Weight,
                    })
                    .ToList();
            default:
                throw new Exception("Not supported division mode");
        }
    }
}