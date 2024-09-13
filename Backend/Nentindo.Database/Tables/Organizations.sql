CREATE TABLE [dbo].[Organizations]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Title] NVARCHAR(100) NOT NULL,
	[ResponsibilityType] INT NOT NULL,
	[Description] NVARCHAR(255) NULL
)
