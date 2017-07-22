/*
CREATE TABLE [dbo].[bussinessOwners] (
    [userId]     INT NOT NULL,
    [businessId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([userId] ASC),
    CONSTRAINT [FK_bussinessOwners_ToUsers] FOREIGN KEY ([userId]) REFERENCES [dbo].[users] ([userID]),
    CONSTRAINT [FK_bussinessOwners_ToBusiness] FOREIGN KEY ([businessId]) REFERENCES [dbo].[business] ([businessId])
);

*/