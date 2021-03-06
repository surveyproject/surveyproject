USE [dbname]
GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyAddNew]    Script Date: 1/19/2018 10:19:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Survey Project: (c) 2017, W3DevPro TM (https://github.com/surveyproject)

	NSurvey - The web survey and form engine
	Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)

	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
	as published by the Free Software Foundation; either version 2
	of the License, or (at your option) any later version.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with this program; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

/// <summary>
/// Creates a new survey
/// </summary>
*/
ALTER PROCEDURE [dbo].[vts_spSurveyAddNew] 
					@CreationDate datetime,
					@OpenDate datetime,
					@CloseDate datetime,
					@Title NVARCHAR(255),
					@AccessPassword NVARCHAR(255) = null,
					@SurveyDisplayTimes int = 0,
					@ResultsDisplayTimes int = 0,
					@Archive bit = 0,
					@Activated bit = 0,
					@CookieExpires int = 1440,
					@IPExpires int = 1440,
					@OnlyInvited bit = 0,
					@ProgressDisplayModeID int = 2,
					@ResumeModeID int = 1,
					@Scored bit = 0,
					@NavigationEnabled bit =0,
					@QuestionNumberingDisabled bit = 0,
					@FolderID int = null,
					@MultiLanguageModeID int=null,		
					@ThankYouMessage NVARCHAR(4000)=null,	
					@UnAuthentifiedUserActionID int = 0,		
					@SurveyID int out
AS
	declare @PID int
	-- we get home folder
	select @PID =
	(case when @FolderID is null or @FolderID<=0 then FolderID 
	else @FolderID end )
	from vts_tbFolders where ParentFolderID IS NULL
	
	if exists(
          select 1 from vts_tbSurvey where FolderID=@PID and Title =@Title )
         begin
           raiserror('DUPLICATEFOLDER',16,4);
           return;
         end;

INSERT INTO vts_tbSurvey(
	CreationDate,
	OpenDate,
	CloseDate,
	Title,
	AccessPassword,
	SurveyDisplayTimes,
	ResultsDisplayTimes,
	Archive,
	IPExpires,
	CookieExpires,
	Activated,
	OnlyInvited,
	ProgressDisplayModeID,
	ResumeModeID,
	Scored,
	NavigationEnabled,
	QuestionNumberingDisabled,
	FolderID,
	MultiLanguageModeID,
	ThankYouMessage,
	UnAuthentifiedUserActionID) 
VALUES (
	@CreationDate,
	@OpenDate,
	@CloseDate,
	@Title,
	@AccessPassword,
	@SurveyDisplayTimes,
	@ResultsDisplayTimes,
	@Archive,
	@IPExpires,
	@CookieExpires,
	@Activated,
	@OnlyInvited,
	@ProgressDisplayModeID,
	@ResumeModeID,
	@Scored,
	@NavigationEnabled,
	@QuestionNumberingDisabled,
	@PID,
	@MultiLanguageModeID,
	@ThankYouMessage,
	@UnAuthentifiedUserActionID)
	
SET @SurveyID = SCOPE_IDENTITY()



