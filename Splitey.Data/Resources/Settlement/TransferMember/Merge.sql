MERGE [settlement].[TransferMember] AS T
USING OPENJSON(@TransferMembers) WITH (
    [MemberId] INT '$.MemberId',
    [Value] DECIMAL(18, 2) '$.Value',
    [Weight] DECIMAL(18, 2) '$.Weight'
) AS S
    ON T.[TransferId] = @TransferId AND T.[MemberId] = S.[MemberId]

WHEN MATCHED THEN
UPDATE 
SET 
    T.[Value] = S.[Value],
    T.[Weight] = S.[Weight]

WHEN NOT MATCHED  THEN
INSERT 
(
    [TransferId],
    [MemberId],
    [Value],
    [Weight]
) VALUES (
    @TransferId,
    S.[MemberId],
    S.[Value],
    S.[Weight]
)

WHEN NOT MATCHED BY SOURCE THEN
DELETE;