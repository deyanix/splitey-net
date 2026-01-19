SELECT 
    U.[Id] AS UserId,
    U.[FirstName] AS FirstName,
    U.[LastName] AS LastName,
    CA.[AccessModeId]
FROM [user].[ContactAccess] CA 
JOIN [user].[User] U 
    on U.[Id] = CA.[UserId]
WHERE
    CA.[ContactId] = @ContactId