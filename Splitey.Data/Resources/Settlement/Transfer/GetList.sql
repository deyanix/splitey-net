SELECT
    T.[Id], 
    T.[SettlementId],
    T.[Name],
    T.[Date],
    T.[PayerMemberId],
    T.[TypeId],
    T.[DivisionModeId], 
    T.[TotalValue], 
    C.[Id] AS CurrencyId,
    C.[Code] AS CurrencyCode
FROM [settlement].[Transfer] T
JOIN [env].[Currency] C 
    ON T.[CurrencyId] = C.[Id]
WHERE
    T.[SettlementId] = @SettlementId