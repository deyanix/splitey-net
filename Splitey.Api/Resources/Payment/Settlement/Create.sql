INSERT INTO [settlement].[Settlement]
    ([Name], [Description], [CurrencyId]) 
VALUES 
    (@Name, @Description, @CurrencyId);

SELECT SCOPE_IDENTITY();