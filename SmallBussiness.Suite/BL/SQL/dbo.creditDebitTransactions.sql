CREATE TABLE [dbo].[creditDebitTransactions] (
	[Id]	   INT PRIMARY KEY IDENTITY(1,1),
    [debitId]  INT NULL,
    [creditId] INT NULL,
    [complete] BIT NOT NULL DEFAULT 0, 
    UNIQUE NONCLUSTERED ([debitId] ASC),
    UNIQUE NONCLUSTERED ([creditId] ASC),
    /*CONSTRAINT [FK_creditDebitTransactions_ToDebit] FOREIGN KEY ([creditId]) REFERENCES [dbo].[credit] ([Id]),
    CONSTRAINT [FK_creditDebitTransactions_ToDebit] FOREIGN KEY ([debitId]) REFERENCES [dbo].[debit] ([Id])*/
);


GO
CREATE NONCLUSTERED INDEX [IX_creditDebitTransactions_debitId]
    ON [dbo].[creditDebitTransactions]([debitId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_creditDebitTransactions_creditId]
    ON [dbo].[creditDebitTransactions]([creditId] ASC);


GO

CREATE TRIGGER [dbo].[Trigger_creditDebitTransactions_Processing]
    ON [dbo].[creditDebitTransactions]
    AFTER INSERT
    AS
    BEGIN
        declare @debitId int,
				@creditId int,
				@complete bit

		SELECT TOP 1 @debitId = [debitId], @creditId = [creditId], @complete = [complete]
		FROM [creditDebitTransactions]
		ORDER BY [Id] DESC

		IF (@debitId IS NOT NULL) AND (@creditId IS NOT NULL)
		BEGIN
			SET @complete = 1
			UPDATE [creditDebitTransactions]
			SET [complete] = @complete
			WHERE [debitId] = @debitId AND [creditId] = @creditId
		END

		IF @complete = 0
		BEGIN

			declare @id int , 
				@userId int,
				@businessId int,
				@amount money,
				@toBusinessId int,
				@toUserId int,
				@note nvarchar(50)
		/*THE ONE YOU HAVE IS THE ONE THAT STARTED THE TRIGGER*/
			IF @debitId IS NULL
			BEGIN
				SELECT 'DEBIT IS NULL'
				
				SELECT @userId=[userId],@businessId=[businessId],@amount=[amount],@toBusinessId=[toBusinessId],@toUserId=[toUserId],@note=[note] 
				FROM [credit]
				WHERE Id = @debitId
				
				SET @amount = @amount * -1
				INSERT INTO [debit]([userId],[businessId],[amount],[toBusinessId],[toUserId],[note],[complete]) VALUES(@userID,@businessId,@amount,@toBusinessId,@toUserId,@note,1)
				
			END
			IF @creditId IS NULL
			BEGIN
				SELECT 'CREDIT IS NULL'
				
				SELECT @userId=[userId],@businessId=[businessId],@amount=[amount],@toBusinessId=[toBusinessId],@toUserId=[toUserId],@note=[note] 
				FROM [debit]
				WHERE Id = @debitId
				
				SET @amount = @amount * -1
				INSERT INTO [credit]([userId],[businessId],[amount],[toBusinessId],[toUserId],[note],[complete]) VALUES(@userID,@businessId,@amount,@toBusinessId,@toUserId,@note,1)
			END
		END
		
    END
GO

/*
CREATE TRIGGER [dbo].[Trigger_creditDebitTransactions_CleanUp]
    ON [dbo].[creditDebitTransactions]
    FOR UPDATE
    AS
    BEGIN
		declare @remaining int
		SET @remaining = (SELECT COUNT(*) FROM [creditDebitTransactions] WHERE [complete] = 1)

		declare @integrityCheck bit = 0
		SET @integrityCheck = [dbo].[debitCreditIntegrityCheck]
		
		IF @integrityCheck = -1
		BEGIN
			SELECT 'THERE IS A PROBLEM WITH: PROCEDURE [dbo].[debitCreditIntegrityCheck]'
		END
		
		IF @integrityCheck = 0
		BEGIN
			WHILE @remaining > 0
			BEGIN
				SET @remaining = @remaining - 1
				SELECT 'UPDATING', @remaining AS [Remaining]

			END
		END
   
    END
	*/