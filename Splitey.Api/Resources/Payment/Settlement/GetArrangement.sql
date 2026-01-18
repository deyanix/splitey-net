-- DECLARE @SettlementId INT = 1;

SELECT
    TM.[MemberId] AS [From],
    T.[PayerMemberId] AS [To],
    SUM(IIF(TT.[Name] = 'Expense', TM.[Value], -TM.[Value])) AS [Balance]
FROM [settlement].[Transfer] T
    JOIN [settlement].[TransferMember] TM
ON T.[Id] = TM.[TransferId]
    JOIN [settlement].[TransferType] TT
    ON TT.[Id] = T.[TypeId]
WHERE
    T.[SettlementId] = @SettlementId AND
    TM.[MemberId] <> T.[PayerMemberId]
GROUP BY
    T.[PayerMemberId],
    TM.[MemberId];