use [DBNAME}

GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (42, N'AccessSsrsReports')

-- check the list of security rights
-- select * from vts_tbSecurityRight