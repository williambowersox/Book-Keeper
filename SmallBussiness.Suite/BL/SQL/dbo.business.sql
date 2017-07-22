/*

CREATE TABLE [dbo].[business] (
    [businessId]   INT           IDENTITY (1, 1) NOT NULL,
    [businessName] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([businessId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Table_businessName]
    ON [dbo].[business]([businessName] ASC);

*/