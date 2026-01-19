SELECT TOP 1
    U.[Id],
    U.[Username],
    U.[Email],
    U.[FirstName],
    U.[LastName],
    U.[Password]
FROM [user].[User] U
WHERE
    U.[Username] = @Login OR
    U.[Email] = @Login