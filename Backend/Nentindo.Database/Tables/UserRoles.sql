CREATE TABLE [dbo].[UserRoles]
(
	[UserId] INT NOT NULL FOREIGN KEY REFERENCES [Users](Id) ON DELETE CASCADE,
	[RoleId] INT NOT NULL FOREIGN KEY REFERENCES [Roles](Id) ON DELETE CASCADE,
    PRIMARY KEY ([UserId], [RoleId]), 
)

GO

CREATE INDEX [IX_UserRoles_User] ON [dbo].[UserRoles]([UserId])

GO

CREATE INDEX [IX_UserRoles_Role] ON [dbo].[UserRoles]([RoleId])
