-- DECLARE
--     @SettlementId INT = 1,
--     @UserId INT = NULL,
--     @ContactId INT = 2,
--     @AccessModeId INT = 2;

MERGE [settlement].[SettlementMember] AS T
USING (VALUES (@SettlementId, @UserId, @ContactId, @AccessModeId)) 
    AS S (SettlementId, UserId, ContactId, AccessModeId)
ON 
    T.[SettlementId] = S.[SettlementId] AND (
        T.[UserId] = S.[UserId] OR 
        T.[ContactId] = S.[ContactId]
    )
    
WHEN MATCHED AND S.[AccessModeId] IS NULL THEN
DELETE

WHEN MATCHED AND S.[AccessModeId] IS NOT NULL THEN
UPDATE 
SET 
    [AccessModeId] = S.[AccessModeId]

WHEN NOT MATCHED AND S.[AccessModeId] IS NOT NULL THEN
INSERT 
    ([SettlementId], [UserId], [ContactId], [AccessModeId])
VALUES 
    (S.[SettlementId], S.[UserId], S.[ContactId], S.[AccessModeId]);