CREATE TABLE [dbo].[SupplierContacts]
(
	[SupplierId] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [Suppliers](Id) ON DELETE CASCADE,
	[ContactId] UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [Contacts](Id) ON DELETE CASCADE,
    PRIMARY KEY ([SupplierId], [ContactId]), 
)

GO

CREATE INDEX [IX_SupplierContacts_Supplier] ON [dbo].[SupplierContacts]([SupplierId])

GO

CREATE INDEX [IX_SupplierContacts_Contact] ON [dbo].[SupplierContacts]([ContactId])