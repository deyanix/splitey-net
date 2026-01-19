SELECT 
    TM.[MemberId], 
    TM.[Value], 
    TM.[Weight]
FROM [settlement].[TransferMember] TM
WHERE
    TM.TransferId = @TransferId