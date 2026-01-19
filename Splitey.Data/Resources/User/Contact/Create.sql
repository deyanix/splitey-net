INSERT INTO [user].[Contact]
    ([Email], [FirstName], [LastName]) 
VALUES
    (@Email, @FirstName, @LastName);
    
SELECT SCOPE_IDENTITY();