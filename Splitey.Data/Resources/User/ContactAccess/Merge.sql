-- DECLARE
--     @ContactId INT = 2,
--     @UserId INT = 1,
--     @AccessModeId INT = 2;

MERGE [user].[ContactAccess] AS T
USING (VALUES (@ContactId, @UserId, @AccessModeId)) 
    AS S (ContactId, UserId, AccessModeId)
ON T.[ContactId] = S.[ContactId] AND T.[UserId] = S.[UserId]
    
WHEN MATCHED AND S.[AccessModeId] IS NULL THEN
DELETE

WHEN MATCHED AND S.[AccessModeId] IS NOT NULL THEN
UPDATE 
SET 
    [AccessModeId] = S.[AccessModeId]

WHEN NOT MATCHED AND S.[AccessModeId] IS NOT NULL THEN
INSERT 
    ([ContactId], [UserId], [AccessModeId])
VALUES 
    (S.[ContactId], S.[UserId], S.[AccessModeId]);