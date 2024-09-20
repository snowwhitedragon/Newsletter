CREATE TABLE [dbo].[OrganizationNewsletters]
(
	[OrganizationId] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [Organizations](Id) ON DELETE CASCADE,
	[NewsletterId] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [Newsletters](Id) ON DELETE CASCADE,
    PRIMARY KEY ([OrganizationId], [NewsletterId]), 
)

GO

CREATE INDEX [IX_OrganizationNewsletters_Organization] ON [dbo].[OrganizationNewsletters]([OrganizationId])

GO

CREATE INDEX [IX_OrganizationNewsletters_Newsletter] ON [dbo].[OrganizationNewsletters]([NewsletterId])