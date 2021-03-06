USE [DBname]
GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterGetPagedIndiv]    Script Date: 8/6/2017 21:38:52 ******/
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
/// Return a paged results of available voters
/// </summary>
/// <param name="@SurveyID">
/// ID of the survey to pivot answers
/// </param>
/// <param name="@CurrentPage">
/// Current page number
/// </param>
/// <param name="@PageSize">
/// Page size
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spVoterGetPagedIndiv]
				@SurveyID int,
				@UserID int,
				@CurrentPage int = 1,
				@PageSize int=10,
				@StartDate datetime ,
				@EndDate datetime,
				@TotalRecords int OUTPUT
AS
-- Turn off count return.
Set NoCount On
-- Declare variables.
DECLARE @FirstRec int
DECLARE @LastRec int
-- Initialize variables.
SET @FirstRec = (@CurrentPage - 1) * @PageSize
SET @LastRec = (@CurrentPage * @PageSize + 1)
-- Create a temp table to hold the current page of data
-- Add an ID column to count the records
CREATE TABLE #TempTable (RowId int IDENTITY PRIMARY KEY, VoterID int NOT NULL, [Date] dateTime, StartDate datetime, IP varchar(50), Score int)
--Fill the temp table with the reminders
INSERT INTO #TempTable (VoterID, [Date], StartDate, IP, Score)
	SELECT
		V.VoterID,
		V.VoteDate,
		V.StartDate,
		V.IPSource,
		(SELECT sum(ScorePoint) FROM vts_tbVoter 
			INNER JOIN vts_tbVoterAnswers
				ON vts_tbVoterAnswers.VoterID = vts_tbVoter.VoterID
			INNER JOIN vts_tbAnswer
				ON vts_tbAnswer.AnswerID = vts_tbVoterAnswers.AnswerID
			WHERE vts_tbVoter.VoterID = V.VoterID) AS Score
	FROM vts_tbVoter V
	WHERE 
		V.SurveyID = @SurveyID AND
		V.Validated <> 0 AND
		DATEDIFF (d,@startDate,V.VoteDate) >= 0 AND DATEDIFF (d,@endDate,V.VoteDate) <= 0 AND
		contextusername = (	select username from vts_tbuser where userid = @UserID)
	ORDER BY V.VoterID DESC
SELECT
	@TotalRecords = count(*)
FROM vts_tbVoter
WHERE 
	SurveyID = @SurveyID AND
	Validated<>0 AND
	StartDate between @StartDate AND @EndDate
IF @PageSize = -1
BEGIN
	SELECT
		VoterID,
		[Date],
		StartDate,
		IP,
		Score
	FROM #TempTable
END
ELSE
BEGIN
	SELECT
		VoterID,
		[Date],
		StartDate,
		IP,
		Score
	FROM #TempTable
	WHERE 
		RowId > @FirstRec AND
		RowId < @LastRec
END
DROP TABLE #TempTable
