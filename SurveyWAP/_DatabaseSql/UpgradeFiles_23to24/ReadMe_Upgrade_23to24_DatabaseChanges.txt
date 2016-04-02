_______________________________________________________________________


Databasechanges based on file comparison SP v. 2.3 and SP v. 2.4

_______________________________________________________________________


Notes: 

- In case of upgrading an SP v. 2.2 database to v.2.3 the following changes have to be made:

Detailed code can be copied from the main SP v. 2.3 Beta SQl files.

- DB upgrades are not formally supported. Always make sure to backup your databases first before making any changes or running scripts.


***********************************************************************************


- added stored procedure: vts_spGetReportScoresDws

- new answertype FieldAddressType

SET IDENTITY_INSERT [dbo].[vts_tbAnswerType] ON 


INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDataSource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) 
VALUES (55, 1, N'FieldAddressType', 20, 0, 100, 1090, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerFieldAddressItem', N'Votations.NSurvey.WebControls', NULL)


GO
SET IDENTITY_INSERT [dbo].[vts_tbAnswerType] OFF


- changes/edited stored procedures:
* vts_spVoterInvitationAnsweredGetAll.sql
* vts_spVoterInvitationQueueGetAll.sql
Both to add paging on mailingstatus.aspx page.

- issue Github 26: missing entries from vts_tbSecurityAddIn
* add token and filter add in's to vts_tbsecurityaddin

