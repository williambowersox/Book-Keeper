CREATE PROCEDURE [dbo].[debitCreditIntegrityCheck]
AS
	/*declare @prolbem bit = 0, @count int
	SET @count = (SELECT COUNT(*)
					FROM [debit] INNER JOIN [creditDebitTransactions] ON [debit].[Id] = [creditDebitTransactions].[debitId]
								INNER JOIN [credit] ON [credit].[Id] = [creditDebitTransactions].[creditId]
					WHERE [debit].[complete] = 0 OR [credit].[complete] = 0)
	IF @count > 0
	BEGIN
		RETURN 1
	END
	IF @count = 0
	BEGIN
		RETURN 1
	END*/
	RETURN 0