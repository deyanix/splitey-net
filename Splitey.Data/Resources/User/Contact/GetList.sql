SELECT 
    C.[Id],
    C.[Email],
    C.[FirstName],
    C.[LastName],
    CA.[AccessModeId]
FROM [user].[Contact] C
JOIN [user].[ContactAccess] CA 
    ON C.[Id] = CA.[ContactId]
WHERE
    CA.[UserId] = @UserId