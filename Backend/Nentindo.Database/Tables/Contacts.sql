CREATE TABLE [dbo].[Contacts]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [ReadableId] NVARCHAR(15) NOT NULL,
    [Salutation] NVARCHAR(10) NOT NULL,
    [FirstName] NVARCHAR(100) NOT NULL,
    [LastName] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(100) NOT NULL,
    [StateId] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [States](Id),
    [LanguageId] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [States](Id),
)

GO

CREATE INDEX [IX_Contact_State] ON [dbo].[Contacts]([StateId])

GO

CREATE INDEX [IX_Contacts_Language] ON [dbo].[Contacts]([LanguageId])

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