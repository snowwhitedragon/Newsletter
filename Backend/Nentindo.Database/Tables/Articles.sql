CREATE TABLE [dbo].[Articles]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Title] NVARCHAR(100) NOT NULL,
	[Summary] NVARCHAR(255) NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL,
	[Link] NVARCHAR(255) NOT NULL,
	[Picture] VARBINARY(MAX) NOT NULL, 
	[NewsletterId] INT NOT NULL FOREIGN KEY REFERENCES [Newsletters](Id) ON DELETE CASCADE,
	[CreatedAt] DATETIME2 NOT NULL,
	[CreatedById] INT NOT NULL FOREIGN KEY REFERENCES [Users](Id),
	[Published] BIT NOT NULL DEFAULT(0),
	[PublishedAt] DATETIME2 NULL,
	[PublishedById] INT NULL FOREIGN KEY REFERENCES [Users](Id) ON DELETE SET NULL,
)

GO

CREATE INDEX [IX_Articles_Newsletter] ON [dbo].[Articles]([NewsletterId])
GO

CREATE INDEX [IX_Articles_Creator] ON [dbo].[Articles]([CreatedById])
GO

CREATE INDEX [IX_Articles_Publisher] ON [dbo].[Articles]([PublishedById])