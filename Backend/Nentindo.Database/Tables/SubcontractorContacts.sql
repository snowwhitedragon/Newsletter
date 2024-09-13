CREATE TABLE [dbo].[SubcontractorContacts]
(
	[SubcontractorId] INT NOT NULL FOREIGN KEY REFERENCES [Subcontractors](Id) ON DELETE CASCADE,
	[ContactId] INT NOT NULL FOREIGN KEY REFERENCES [Contacts](Id) ON DELETE CASCADE,
    PRIMARY KEY ([SubcontractorId], [ContactId]), 
)

GO

CREATE INDEX [IX_SubcontractorContacts_Subcontractor] ON [dbo].[SubcontractorContacts]([SubcontractorId])

GO

CREATE INDEX [IX_SubcontractorContacts_Contact] ON [dbo].[SubcontractorContacts]([ContactId])