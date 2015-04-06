USE [sp23test]
GO

/****** Object:  StoredProcedure [dbo].[vts_spVoterInvitationAnsweredGetAll]    Script Date: 04/01/2015 20:59:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[vts_spVoterInvitationAnsweredGetAll]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[vts_spVoterInvitationAnsweredGetAll]
GO

USE [sp23test]
GO

/****** Object:  StoredProcedure [dbo].[vts_spVoterInvitationAnsweredGetAll]    Script Date: 04/01/2015 20:59:48 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

/*
	Survey changes: copyright (c) 2010, Fryslan Webservices TM (http://survey.codeplex.com)	

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
///  Get pending invited emails for the survey
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spVoterInvitationAnsweredGetAll]
				@SurveyID int,
				@CurrentPage int = 1,
				@PageSize int=10,
				@TotalRecords int OUTPUT
AS
-- Turn off count return.
Set NoCount On
-- Declare variables.
DECLARE @FirstRec int
DECLARE @LastRec int
-- Initialize variables.
SET @FirstRec = @PageSize*@CurrentPage
SET @LastRec= @FirstRec+@PageSize + 1
-- Create a temp table to hold the current page of data
-- Add an ID column to count the records
CREATE TABLE #TempTable (RowId int IDENTITY PRIMARY KEY, SurveyID int NOT NULL, VoterID int NOT NULL, Email varchar(150), VoteDate DateTime)
--Fill the temp table with the reminders
INSERT INTO #TempTable (SurveyId, VoterID, Email, VoteDate)
	SELECT SurveyID, vts_tbVoter.VoterID, Email, VoteDate
	FROM vts_tbVoter
	INNER JOIN vts_tbVoterEmail 
		ON vts_tbVoterEmail.VoterID = vts_tbVoter.VoterID
	INNER JOIN vts_tbEmail
		ON vts_tbEmail.EmailID = vts_tbVoterEmail.EmailID
	WHERE SurveyID = @SurveyID AND Validated<>0
	ORDER BY VoteDate DESC
SELECT @TotalRecords = count(*) 
FROM vts_tbVoter
INNER JOIN vts_tbVoterEmail 
	ON vts_tbVoterEmail.VoterID = vts_tbVoter.VoterID
INNER JOIN vts_tbEmail
	ON vts_tbEmail.EmailID = vts_tbVoterEmail.EmailID
WHERE SurveyID = @SurveyID AND Validated<>0
SELECT SurveyID, VoterID, VoteDate, Email
FROM #TempTable
WHERE 
	RowId > @FirstRec AND
	RowId < @LastRec
DROP TABLE #TempTable




GO


