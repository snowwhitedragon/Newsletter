CREATE TABLE [dbo].[Customers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [ReadableId] NVARCHAR(15) NULL,
    [ContactId] INT NOT NULL FOREIGN KEY REFERENCES [Contacts](Id),
)

GO

CREATE INDEX [IX_Customers_Contact] ON [dbo].[Customers]([ContactId])

GO

CREATE TRIGGER [dbo].[Trigger_GenerateReadable_ForCustomers]
    ON [dbo].[Customers]
    AFTER INSERT
    AS
    BEGIN
		UPDATE t1
			SET t1.ReadableId = 'CUST-' +  RIGHT('000000' + CAST(t2.Id AS VARCHAR(8)),6)
        FROM [dbo].[Customers] AS t1
        INNER JOIN inserted AS t2
        ON t1.Id = t2.Id
        WHERE t1.Id = t2.Id
    END