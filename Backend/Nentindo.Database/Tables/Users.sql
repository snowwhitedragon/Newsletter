CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Username] NVARCHAR(50) NOT NULL UNIQUE,
	[PasswordHash] NVARCHAR(MAX) NOT NULL,
	[DisplayName] NVARCHAR(200) NOT NULL,
	[OrganizationId] INT NULL FOREIGN KEY ([OrganizationId]) REFERENCES [Organizations]([Id]),
	[RegistratedAt] DATETIME2 NOT NULL DEFAULT(GETDATE()),
)
