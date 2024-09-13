CREATE TABLE [dbo].[Contacts]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [ReadableId] NVARCHAR(15) NOT NULL,
    [Salutation] NVARCHAR(10) NOT NULL,
    [FirstName] NVARCHAR(100) NOT NULL,
    [LastName] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(100) NOT NULL,
    [Country] NVARCHAR(100) NOT NULL DEFAULT('Deutschland'),
    [LanguageCode] NVARCHAR(5) NOT NULL DEFAULT('de')
)

GO

CREATE TRIGGER [dbo].[Trigger_GenerateReadable_ForContacts]
    ON [dbo].[Contacts]
    AFTER INSERT
    AS
    BEGIN
		UPDATE t1
			SET t1.ReadableId = 'CON' +  RIGHT('000000' + CAST(t2.Id AS VARCHAR(8)),6)
        FROM [dbo].[Contacts] AS t1
        INNER JOIN inserted AS t2
        ON t1.Id = t2.Id
        WHERE t1.Id = t2.Id
    END