﻿CREATE TABLE [dbo].[Roles]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[Code] NVARCHAR(5) NOT NULL,
	[Title] NVARCHAR(100) NOT NULL
)
