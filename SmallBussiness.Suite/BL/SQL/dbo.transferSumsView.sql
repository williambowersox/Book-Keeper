/*
CREATE VIEW [dbo].[transferSumsView]
	AS SELECT [transfers].[userId], Sum([transfers].[amount]) AS Totals
	FROM [transfers] LEFT JOIN [users] ON [users].[userId] = [transfers].[userId]
	GROUP BY [transfers].[userId]
	*/