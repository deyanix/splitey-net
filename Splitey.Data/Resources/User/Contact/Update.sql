UPDATE [user].[Contact]
SET
    [Email] = @Email,
    [FirstName] = @FirstName,
    [LastName] = @LastName
WHERE
    [Id] = @ContactId