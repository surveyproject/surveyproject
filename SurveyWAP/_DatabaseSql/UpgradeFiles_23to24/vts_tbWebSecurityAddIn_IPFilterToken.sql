
SET IDENTITY_INSERT [dbo].[vts_tbWebSecurityAddIn] ON
INSERT [dbo].[vts_tbWebSecurityAddIn] ([WebSecurityAddInId], [Description], [BuiltIn], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (8, N'Token Protection', 1, N'Votations.NSurvey.Security.TokenSecurityAddIn', N'Votations.NSurvey.WebControls', 0)
INSERT [dbo].[vts_tbWebSecurityAddIn] ([WebSecurityAddInId], [Description], [BuiltIn], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (9, N'IP Filter', 1, N'Votations.NSurvey.Security.IPRangeSecurityAddIn', N'Votations.NSurvey.WebControls', 0)
SET IDENTITY_INSERT [dbo].[vts_tbWebSecurityAddIn] OFF