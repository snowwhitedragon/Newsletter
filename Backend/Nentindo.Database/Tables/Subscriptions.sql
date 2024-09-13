CREATE TABLE [dbo].[Subscriptions]
(
	[NewsletterId] INT NOT NULL FOREIGN KEY REFERENCES [Newsletters](Id) ON DELETE CASCADE,
	[ContactId] INT NOT NULL FOREIGN KEY REFERENCES [Contacts](Id) ON DELETE CASCADE,
	PRIMARY KEY ([ContactId], [NewsletterId])
)

GO

CREATE INDEX [IX_Subscriptions_Contact] ON [dbo].[Subscriptions]([ContactId])

GO

CREATE INDEX [IX_Subscriptions_Newsletter] ON [dbo].[Subscriptions]([NewsletterId])