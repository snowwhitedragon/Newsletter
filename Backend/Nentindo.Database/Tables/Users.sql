CREATE TABLE [dbo].[Users]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[Username] NVARCHAR(50) NOT NULL UNIQUE,
	[PasswordHash] NVARCHAR(MAX) NOT NULL,
	[DisplayName] NVARCHAR(200) NOT NULL,
	[OrganizationId] UNIQUEIDENTIFIER NULL FOREIGN KEY REFERENCES [Organizations]([Id]),
	[ContactId] UNIQUEIDENTIFIER NULL FOREIGN KEY REFERENCES [Contacts]([Id]),
	[RegistratedAt] DATETIME2 NOT NULL DEFAULT(GETDATE()),
)

GO

CREATE INDEX [IX_Users_Organization] ON [dbo].[Users]([OrganizationId])

GO

CREATE INDEX [IX_Users_Contact] ON [dbo].[Users]([ContactId])