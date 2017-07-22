/*CREATE TABLE [dbo].[Transfers] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [userId]   INT           NOT NULL,
    [amount]   MONEY         NOT NULL,
    [toUserId] INT           NOT NULL,
    [note]     NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Transfers_Users] FOREIGN KEY ([userId]) REFERENCES [dbo].[users] ([userID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Transfers_userID]
    ON [dbo].[Transfers]([userId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Transfers_userToID]
    ON [dbo].[Transfers]([toUserId] ASC);

	*/