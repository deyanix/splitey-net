SELECT
    SM.[Id],
    SM.[SettlementId],
    SM.[UserId],
    SM.[ContactId],
    SM.[AccessModeId]
FROM [settlement].[SettlementMember] SM
WHERE
    SM.[SettlementId] = @SettlementId AND (
        SM.[UserId] = @UserId OR
        SM.[ContactId] = @ContactId
    )