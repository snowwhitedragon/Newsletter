﻿CREATE TABLE [dbo].[Articles]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[Title] NVARCHAR(100) NOT NULL,
	[Summary] NVARCHAR(255) NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL,
	[Picture] VARBINARY(MAX) NOT NULL, 
	[NewsletterId] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [Newsletters](Id) ON DELETE CASCADE,
	[CreatedAt] DATETIME2 NOT NULL,
	[CreatedById] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [Users](Id),
	[UpdatedAt] DATETIME2 NOT NULL,
	[UpdatedById] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [Users](Id),
	[Published] BIT NOT NULL DEFAULT(0),
	[PublishedAt] DATETIME2 NULL,
	[PublishedById] UNIQUEIDENTIFIER NULL FOREIGN KEY REFERENCES [Users](Id) ON DELETE SET NULL,
)

GO

CREATE INDEX [IX_Articles_Newsletter] ON [dbo].[Articles]([NewsletterId])
GO

CREATE INDEX [IX_Articles_Creator] ON [dbo].[Articles]([CreatedById])
GO

CREATE INDEX [IX_Articles_Updated] ON [dbo].[Articles]([UpdatedById])
GO

CREATE INDEX [IX_Articles_Publisher] ON [dbo].[Articles]([PublishedById])