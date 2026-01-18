SELECT 
    U.[Id],
    U.[Username],
    U.[Email],
    U.[Password]
FROM [user].[User] U
WHERE
    U.[Id] = @UserId