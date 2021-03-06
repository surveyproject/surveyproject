USE [dbname]
GO
/****** Object:  Table [dbo].[vts_tbMenu]    Script Date: 9/12/2018 11:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbMenu](
	[MenuID] [int] NOT NULL,
	[Code] [int] NOT NULL,
	[Main] [nvarchar](50) NOT NULL,
	[SubOne] [nvarchar](50) NULL,
	[SubTwo] [nvarchar](50) NULL,
	[SubThree] [nvarchar](75) NULL,
 CONSTRAINT [PK_vts_tbMenus] PRIMARY KEY CLUSTERED 
(
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vts_tbMenuSecurityRight]    Script Date: 9/12/2018 11:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbMenuSecurityRight](
	[MenuID] [int] NOT NULL,
	[SecurityRightID] [int] NOT NULL,
 CONSTRAINT [PK_vts_tbMenuSecurityRight] PRIMARY KEY CLUSTERED 
(
	[MenuID] ASC,
	[SecurityRightID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (1, 1000, N'Surveys', NULL, NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (2, 1100, N'Surveys', N'SurveyList', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (3, 1200, N'Surveys', N'New Survey', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (4, 1201, N'Surveys', N'New Survey', NULL, N'Button - import survey xml')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (5, 1300, N'Surveys', N'Statistics', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (6, 1301, N'Surveys', N'Statistics', NULL, N'Link – reset votes')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (7, 1302, N'Surveys', N'Statistics', NULL, N'Button - delete unvalidated')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (8, 1400, N'Surveys', N'Settings', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (9, 1410, N'Surveys', N'Settings', N'System Settings', NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (10, 1411, N'Surveys', N'Settings', N'System Settings', N'Error Log')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (11, 1420, N'Surveys', N'Settings', N'Survey Settings', NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (12, 1421, N'Surveys', N'Settings', N'Survey Settings', N'Button - delete survey')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (13, 1422, N'Surveys', N'Settings', N'Survey Settings', N'Button - export survey')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (14, 1423, N'Surveys', N'Settings', N'Survey Settings', N'Button - apply changes')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (15, 1424, N'Surveys', N'Settings', N'Survey Settings', N'Button - clone survey')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (16, 1430, N'Surveys', N'Settings', N'Multi Language', NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (17, 1440, N'Surveys', N'Settings', N'Completion', NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (18, 1500, N'Surveys', N'Security', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (19, 1510, N'Surveys', N'Security', N'Form Security', NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (20, 1520, N'Surveys', N'Security', N'Token Security', NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (21, 1530, N'Surveys', N'Security', N'IP Filter Security', NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (22, 2000, N'Designer', NULL, NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (23, 2100, N'Designer', N'Form Builder', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (24, 2101, N'Designer', N'Form Builder', N'[Insert Question]', N'Copy question from Library')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (25, 2102, N'Designer', N'Form Builder', N'[Insert Question]', N'Copy question from Survey')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (26, 2103, N'Designer', N'Form Builder', N'[Insert Question]', N'Button – Import XML')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (27, 2200, N'Designer', N'Question Libraries', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (28, 2210, N'Designer', N'Question Libraries', N'Library List', NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (29, 2211, N'Designer', N'Question Libraries', N'Library List', N'Button – Import XML')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (30, 2220, N'Designer', N'Question Libraries', N'Library New', NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (31, 2300, N'Designer', N'Question Groups', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (32, 2400, N'Designer', N'Answer Type Editor', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (33, 2401, N'Designer', N'Answer Type Editor', NULL, N'Button – Create SQL answertype')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (34, 2500, N'Designer', N'Regular Expressions', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (35, 2600, N'Designer', N'Layout', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (36, 3000, N'Results', NULL, NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (37, 3100, N'Results', N'Reports', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (38, 3101, N'Results', N'Reports', NULL, N'Link – Edit/ create filter link')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (39, 3102, N'Results', N'Reports', NULL, N'Button – Cross Tabulation Reports')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (40, 3103, N'Results', N'Reports', NULL, N'Button – SSRS Reports')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (41, 3110, N'Results', N'Reports', N'Filters', NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (42, 3200, N'Results', N'Individual Responses', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (43, 3201, N'Results', N'Individual Responses', NULL, N'Filtered Responses List')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (44, 3202, N'Results', N'Individual Responses', NULL, N'Responses List – Button  - Delete')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (45, 3203, N'Results', N'Individual Responses', NULL, N'Voter Report – Button – Edit')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (46, 3300, N'Results', N'File Manager', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (47, 3301, N'Results', N'File Manager', NULL, N'Button – export files')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (48, 3400, N'Results', N'Data Export', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (49, 3500, N'Results', N'Data import', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (50, 4000, N'Campaigns', NULL, NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (51, 4100, N'Campaigns', N'Preview', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (52, 4200, N'Campaigns', N'Mailing', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (53, 4300, N'Campaigns', N'Take Survey', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (54, 5000, N'Accounts', NULL, NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (55, 5100, N'Accounts', N'UserList', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (56, 5101, N'Accounts', N'UserList', NULL, N'Users Edit Options')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (57, 5200, N'Accounts', N'User Roles', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (58, 5300, N'Accounts', N'Import Users', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (59, 6000, N'Help', NULL, NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (60, 6100, N'Help', N'Help options', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (61, 6200, N'Help', N'Help Files', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (62, 6300, N'Help', N'About SP', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (63, 2212, N'Designer', N'Question Libraries', N'Library List', N'Button – Insert Question')
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (2, 40)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (3, 1)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (4, 29)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (5, 9)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (6, 10)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (7, 11)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (11, 3)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (12, 2)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (13, 4)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (14, 5)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (15, 6)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (16, 34)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (17, 7)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (19, 8)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (20, 38)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (21, 47)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (23, 12)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (24, 25)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (25, 26)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (26, 46)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (28, 24)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (29, 46)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (30, 27)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (31, 36)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (32, 13)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (33, 28)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (34, 31)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (35, 39)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (37, 14)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (38, 15)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (39, 19)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (40, 42)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (42, 16)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (43, 41)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (44, 18)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (45, 17)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (46, 32)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (47, 33)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (48, 20)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (49, 37)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (51, 22)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (52, 21)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (53, 30)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (55, 23)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (56, 43)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (57, 44)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (58, 45)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (60, 35)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (63, 12)
GO
ALTER TABLE [dbo].[vts_tbMenuSecurityRight]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbMenuSecurityRight_vts_tbMenu] FOREIGN KEY([MenuID])
REFERENCES [dbo].[vts_tbMenu] ([MenuID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbMenuSecurityRight] CHECK CONSTRAINT [FK_vts_tbMenuSecurityRight_vts_tbMenu]
GO
ALTER TABLE [dbo].[vts_tbMenuSecurityRight]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbMenuSecurityRight_vts_tbSecurityRight] FOREIGN KEY([SecurityRightID])
REFERENCES [dbo].[vts_tbSecurityRight] ([SecurityRightID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbMenuSecurityRight] CHECK CONSTRAINT [FK_vts_tbMenuSecurityRight_vts_tbSecurityRight]
GO
