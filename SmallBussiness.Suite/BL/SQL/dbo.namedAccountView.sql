/*
CREATE VIEW [dbo].[namedAccountView]
	AS SELECT ([users].[lastName] + ', ' + [users].firstName) AS [Name], [users].[email] AS [Email], [transferSumsView].[Totals]
FROM [users] LEFT JOIN [transferSumsView] ON [users].[userId] = [transferSumsView].[userId]

*/