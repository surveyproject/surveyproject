USE [DBNAME]

SET IDENTITY_INSERT [dbo].[vts_tbAnswerType] ON 


INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDataSource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) 
VALUES (55, 1, N'FieldAddressType', 20, 0, 100, 1090, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerFieldAddressItem', N'Votations.NSurvey.WebControls', NULL)


GO
SET IDENTITY_INSERT [dbo].[vts_tbAnswerType] OFF