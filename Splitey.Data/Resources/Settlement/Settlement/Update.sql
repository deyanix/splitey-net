UPDATE [settlement].[Settlement]
SET 
    [Name] = @Name,
    [Description] = @Description,
    [CurrencyId] = @CurrencyId
WHERE
    [Id] = @SettlementId