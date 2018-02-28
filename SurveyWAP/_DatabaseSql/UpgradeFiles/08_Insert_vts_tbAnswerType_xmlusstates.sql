-- Run the following insert query:

use [dbname]

GO
SET IDENTITY_INSERT [dbo].[vts_tbAnswerType] ON 

GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDataSource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (5, 1, N'XMLUSStatesList', NULL, NULL, NULL, 88, N'usstates.xml', 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerXmlListItem', N'SurveyProject.WebControls', NULL)
GO

GO
SET IDENTITY_INSERT [dbo].[vts_tbAnswerType] OFF
GO