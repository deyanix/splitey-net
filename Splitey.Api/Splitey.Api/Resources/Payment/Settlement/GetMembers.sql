SELECT
    SM."Id",
    SM."UserId",
    SM."PersonId"
FROM "payment"."SettlementMember" SM
WHERE 
    SM."SettlementId" = @SettlementId