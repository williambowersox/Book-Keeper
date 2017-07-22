CREATE TABLE [dbo].[debit] (
    [Id]           INT           NOT NULL IDENTITY(1,1),
    [userId]       INT           NOT NULL,
    [businessId]   INT           NOT NULL,
    [amount]       MONEY         NOT NULL,
    [toBusinessId] INT           NULL,
    [toUserId]     INT           NULL,
    [note]         NVARCHAR (50) NULL,
    [creditId]        INT            NULL,
    [trigger] BIT NULL DEFAULT ((1)), 
    CONSTRAINT [FK_debit_ToUsersFromDebit] FOREIGN KEY ([userId]) REFERENCES [dbo].[users] ([userID]),
    CONSTRAINT [FK_debit_ToBusinessFromDebit] FOREIGN KEY ([toBusinessId]) REFERENCES [dbo].[business] ([businessId]), 
    CONSTRAINT [AK_debit_Id] UNIQUE ([Id]), 
);


GO
CREATE TRIGGER [dbo].[Trigger_debitToCredit]
    ON [dbo].[debit]
    FOR INSERT
    AS
    BEGIN
		declare @id int
		declare @userId int
		declare @businessId int
		declare @amount money
		declare @toBusinessId int
		declare @toUserId int
		declare @note nvarchar(50)
		declare @creditId int
		declare @trigger bit
		SET @trigger = 1
		
		SELECT TOP 1 @id = [Id], @userId=[userId], @businessId = [businessId],@amount = [amount],@toBusinessId = [toBusinessId], @toUserId = [toUserId], @note = [note], @creditId = creditId, @trigger = [creditId]
		FROM [debit]
		ORDER BY id DESC
		IF @trigger = 1
		BEGIN
			INSERT INTO [credit] VALUES(@userID,@businessId,(@amount * -1),@toBusinessId, @toUserId, @note,@creditId, 0)
		END

    END