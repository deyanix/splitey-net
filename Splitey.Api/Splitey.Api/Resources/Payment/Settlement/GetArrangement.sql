SELECT
    S."From" AS "From",
    S."To" AS "To",
    SUM(S."Value") AS "Balance"
FROM (
    SELECT
        TPC."SettlementMemberId" AS "From",
        T."PaidBy" AS "To",
        TPC."Amount" * TP."Price" AS "Value"
    FROM "payment"."Transfer" T
        JOIN "payment"."TransferProduct" TP 
            ON T."Id" = TP."PaymentId"
        JOIN "payment"."TransferProductContribution" TPC 
            ON TP."Id" = TPC."TransferProductId"
        JOIN "payment"."SettlementMember" SM
            ON SM."Id" = T."PaidBy"
    WHERE 
        SM."SettlementId" = @SettlementId AND
        T."Type" = 'RESTAURANT' AND 
        TPC."SettlementMemberId" <> T."PaidBy"
    UNION
    SELECT
        TD."SettlementMemberId" AS "From",
        T."PaidBy" AS "To",
        TD."Amount" AS "Value"
    FROM "payment"."Transfer" T
        JOIN "payment"."TransferDivision" TD 
            ON T."Id" = TD."TransferId"
        JOIN "payment"."SettlementMember" SM
            ON SM."Id" = T."PaidBy"
    WHERE
        SM."SettlementId" = @SettlementId AND
        T."Type" = 'STANDARD' AND 
        TD."SettlementMemberId" != T."PaidBy"
) S
GROUP BY S."From", S."To"