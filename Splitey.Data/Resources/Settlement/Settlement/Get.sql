SELECT 
    S.[Id],
    S.[Name],
    S.[Description],
    C.[Id] AS CurrencyId,
    C.[Code] AS CurrencyCode
FROM [settlement].[Settlement] S
JOIN [env].[Currency] C 
    ON S.CurrencyId = C.Id
WHERE
    S.[Id] = @SettlementId