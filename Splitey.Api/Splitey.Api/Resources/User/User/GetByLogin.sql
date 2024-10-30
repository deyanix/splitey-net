SELECT
    U."Id",
    U."Username",
    U."Email",
    U."Password"
FROM "user"."User" U
WHERE
    U."Username" = @Login OR
    U."Email" = @Login
LIMIT 1