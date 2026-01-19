SELECT 
    C.[Id],
    C.[Email],
    C.[FirstName],
    C.[LastName]
FROM [user].[Contact] C
WHERE
    C.[Id] = @ContactId