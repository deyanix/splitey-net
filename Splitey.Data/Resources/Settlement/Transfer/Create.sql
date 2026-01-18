INSERT INTO [settlement].[Transfer]
(
    [SettlementId], 
    [Name], 
    [Date], 
    [PayerMemberId], 
    [TypeId], 
    [DivisionModeId], 
    [TotalValue], 
    [CurrencyId]
) VALUES (
    @SettlementId,
    @Name,
    @Date,
    @PayerMemberId,
    @TypeId,
    @DivisionModeId,
    @TotalValue,
    @CurrencyId
);
    
SELECT SCOPE_IDENTITY();