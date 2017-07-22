CREATE TABLE [dbo].[credit] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [userId]       INT           NOT NULL,
    [businessId]   INT           NOT NULL,
    [amount]       MONEY         NOT NULL,
    [toBusinessId] INT           NULL,
    [toUserId]     INT           NULL,
    [note]         NVARCHAR (50) NULL,
    [debitId]       INT            NULL,
    [trigger] BIT NULL DEFAULT ((1)), 
    CONSTRAINT [AK_credit_Id] UNIQUE NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_credit_ToBusinessCredit] FOREIGN KEY ([toBusinessId]) REFERENCES [dbo].[business] ([businessId]),
    CONSTRAINT [FK_credit_ToUsersFromCredit] FOREIGN KEY ([userId]) REFERENCES [dbo].[users] ([userID])
);


GO
CREATE TRIGGER [dbo].[Trigger_creditToDebit]
    ON [dbo].[credit]
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
		declare @debitId int
		declare @trigger bit
		SET @trigger = 1
		
		SELECT TOP 1 @id = [Id], @userId=[userId], @businessId = [businessId],@amount = [amount],@toBusinessId = [toBusinessId], @toUserId = [toUserId], @note = [note], @debitId = [debitId], @trigger = [debitId]
		FROM [credit]
		ORDER BY id DESC
		IF @trigger = 1
		BEGIN
			INSERT INTO [debit] VALUES(@userID,@businessId,(@amount * -1),@toBusinessId, @toUserId, @debitId, @note, @debitId, 0)
		END
    END