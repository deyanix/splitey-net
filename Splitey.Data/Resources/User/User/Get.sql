SELECT 
    U.[Id],
    U.[Username],
    U.[Email],
    U.[FirstName],
    U.[LastName],
    U.[Password]
FROM [user].[User] U
WHERE
    U.[Id] = @UserId