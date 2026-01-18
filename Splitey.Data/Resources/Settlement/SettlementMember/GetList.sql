SELECT 
    SM.[Id], 
    SM.[SettlementId], 
    SM.[UserId], 
    SM.[ContactId], 
    SM.[AccessModeId],
    IIF(SM.[UserId] IS NOT NULL, U.[FirstName], C.[FirstName]) AS FirstName,
    IIF(SM.[UserId] IS NOT NULL, U.[LastName], C.[LastName]) AS LastName
FROM [settlement].[SettlementMember] SM
LEFT JOIN [user].[Contact] C 
    ON SM.[ContactId] = C.[Id]
LEFT JOIN [user].[User] U 
    on SM.[UserId] = U.[Id]
WHERE
    SM.[SettlementId] = @SettlementId