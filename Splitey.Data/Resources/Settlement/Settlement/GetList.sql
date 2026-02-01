SELECT 
    S.[Id],
    S.[Name],
    S.[Description],
    C.[Id] AS CurrencyId,
    C.[Code] AS CurrencyCode,
    SM.[AccessModeId]
FROM [settlement].[Settlement] S
JOIN [settlement].[SettlementMember] SM 
    ON S.Id = SM.SettlementId
JOIN [env].[Currency] C 
    ON S.CurrencyId = C.Id
WHERE
    SM.[UserId] = @UserId