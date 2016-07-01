USE [DBname]
GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyUpdate]    Script Date: 27-6-2016 13:27:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Survey Project: (c) 2016, Fryslan Webservices TM (http://survey.codeplex.com)

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
///  Updates a survey
/// </summary>
/// <remarks>
/// Only one survey can be activated at one time
/// </remarks>
*/
ALTER PROCEDURE [dbo].[vts_spSurveyUpdate] 

				@LanguageCode nvarchar(50),
				@SurveyID int,
				@OpenDate datetime,
				@CloseDate datetime,
				@Title nvarchar(255),
				@ThankYouMessage nvarchar(4000),
				@RedirectionURL varchar(1024),
				@Archive bit,
				@Activated bit,
				@ResumeModeID int,
				@NavigationEnabled bit,
				@ProgressDisplayModeId int,
				@NotificationModeId int,
				@Scored bit,
				@QuestionNumberingDisabled bit,
			    @DefaultSurvey bit=0
AS

if exists(
          select 1 from vts_tbSurvey where FolderId=
          (select folderid from vts_tbsurvey where surveyId=@SurveyID)
          and Title =@Title  and surveyId!=@SurveyID)
         begin
    
           raiserror('DUPLICATEFOLDER',16,4);
           return;
         end;
         
if @DefaultSurvey <> 0
-- Only one survey can be activated at one time
UPDATE vts_tbSurvey SET DefaultSurvey = 0 WHERE DefaultSurvey<>0


         
         
UPDATE vts_tbSurvey 
SET 
	ProgressDisplayModeId = @ProgressDisplayModeId,
	NotificationModeId = @NotificationModeId,
	ResumeModeID = @ResumeModeID,
	OpenDate = @OpenDate,
	CloseDate = @CloseDate,
	Title = @Title, 
	NavigationEnabled = @NavigationEnabled,
	Archive = @Archive, 
	Activated = @Activated,
	Scored = @Scored,
	QuestionNumberingDisabled = @QuestionNumberingDisabled,
    DefaultSurvey=@DefaultSurvey
WHERE SurveyID = @SurveyID

-- Updates text
IF @LanguageCode is null OR @LanguageCode='' 
BEGIN
	UPDATE vts_tbSurvey
	SET 	ThankYouMessage = @ThankYouMessage,
		RedirectionURL = @RedirectionURL
	WHERE
		SurveyID = @SurveyID
END
ELSE

IF @LanguageCode='dna'
BEGIN
	-- do nothing
	SET @LanguageCode = @LanguageCode
END
ELSE

BEGIN
	-- Updates localized text
	exec vts_spMultiLanguageTextUpdate @SurveyId, @LanguageCode, 4, @ThankYouMessage
	exec vts_spMultiLanguageTextUpdate @SurveyId, @LanguageCode, 5, @RedirectionURL
END



