CREATE TABLE [dbo].[users] (
    [userID]    INT           IDENTITY (1, 1) NOT NULL,
    [firstName] NVARCHAR (25) NOT NULL,
    [lastName]  NVARCHAR (25) NOT NULL,
    [email]     NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([userID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_users_email]
    ON [dbo].[users]([email] ASC);

