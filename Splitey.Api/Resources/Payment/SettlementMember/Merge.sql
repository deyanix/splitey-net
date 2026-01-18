-- DECLARE
--     @SettlementId INT = 1,
--     @UserId INT = NULL,
--     @ContactId INT = 2,
--     @AccessModeId INT = 2;

MERGE [settlement].[SettlementMember] AS T
USING (VALUES (@SettlementId, @UserId, @ContactId)) 
    AS S (SettlementId, UserId, ContactId)
ON 
    T.[SettlementId] = S.[SettlementId] AND (
        T.[UserId] = S.[UserId] OR 
        T.[ContactId] = S.[ContactId]
    )
    
WHEN MATCHED AND @AccessModeId IS NULL THEN
    DELETE -- TODO cascade delete!

WHEN MATCHED AND @AccessModeId IS NOT NULL THEN
    UPDATE SET [AccessModeId] = @AccessModeId

WHEN NOT MATCHED AND @AccessModeId IS NOT NULL THEN
    INSERT ([SettlementId], [UserId], [ContactId], [AccessModeId])
    VALUES (S.[SettlementId], S.[UserId], S.[ContactId], @AccessModeId);