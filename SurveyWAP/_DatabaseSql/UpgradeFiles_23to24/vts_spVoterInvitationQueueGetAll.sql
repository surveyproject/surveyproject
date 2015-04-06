USE [sp23test]
GO

/****** Object:  StoredProcedure [dbo].[vts_spVoterInvitationQueueGetAll]    Script Date: 03/29/2015 23:30:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[vts_spVoterInvitationQueueGetAll]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[vts_spVoterInvitationQueueGetAll]
GO


CREATE PROCEDURE [dbo].[vts_spVoterInvitationQueueGetAll]
				@TotalRecords int OUTPUT,
				@SurveyID int,
				@CurrentPage int ,
				@PageSize int
AS
-- Turn off count return.
Set NoCount On
-- Output value.

 SELECT  @TotalRecords =  count(*) FROM vts_tbInvitationQueue WHERE SurveyID =@SurveyID
-- Declare variables.
DECLARE @FirstRec int
DECLARE @LastRec int
-- Initialize variables.
SET @FirstRec = @PageSize * @CurrentPage 
SET @LastRec= @FirstRec+@PageSize + 1 

-- Create a temp table to hold the current page of data
-- Add an ID column to count the records
CREATE TABLE #TempTable (RowId int IDENTITY PRIMARY KEY, SurveyID int NOT NULL, EmailID int NOT NULL, Email varchar(150), AnonymousEntry bit, UID varchar(50), RequestDate DateTime)
--Fill the temp table with the reminders
INSERT INTO #TempTable (SurveyId, EmailID, Email, AnonymousEntry, UID, RequestDate)
	SELECT SurveyId, vts_tbInvitationQueue.EmailID, Email, AnonymousEntry, UID, RequestDate
	FROM vts_tbInvitationQueue
	INNER JOIN vts_tbEmail
		ON vts_tbEmail.EmailID = vts_tbInvitationQueue.EmailID
	WHERE SurveyID =@SurveyID
	ORDER BY RequestDate DESC
SELECT SurveyId, EmailID, Email, AnonymousEntry, UID, RequestDate
FROM #TempTable
WHERE 
	RowId > @FirstRec AND
	RowId < @LastRec
DROP TABLE #TempTable



GO


