use [dbname]

-- check current
select * from vts_tbSecurityRight

-- add new securityright
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (43, N'AccessUserAccount')
GO

INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (44, N'AccessRolesManager')
GO

INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (45, N'AccessImportUsers')
GO

INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (46, N'AllowQuestionXmlImport')
GO

INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (47, N'IpFilterSecurityRight')
GO

INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (48, N'AllowQuestionInsert')
GO