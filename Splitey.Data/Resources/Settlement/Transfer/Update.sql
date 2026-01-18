UPDATE [settlement].[Transfer]
SET
    [Name] = @Name,
    [Date] = @Date,
    [PayerMemberId] = @PayerMemberId,
    [TypeId] = @TypeId,
    [DivisionModeId] = @DivisionModeId,
    [TotalValue] = @TotalValue,
    [CurrencyId] = @CurrencyId
WHERE
    [Id] = @TransferId