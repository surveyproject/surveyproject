USE [sp24dev]
GO

/****** Object:  StoredProcedure [dbo].[vts_spReportGetScoresDws]    Script Date: 11-10-2014 22:09:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Fryslan Webservices.com
-- Create date: 2014-10-10
-- Description:	SP to get calculated scores for custom report Dierenwetenschap.com
-- =============================================
CREATE PROCEDURE [dbo].[vts_spReportGetScoresDws] 

	-- Add the parameters for the stored procedure here
	@SurveyID int,
	@VoterID int

AS
BEGIN TRANSACTION CalculateDwsScores
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- calculate scores & save to temptable (?)

	Create table #temptableDwsScores
	(SurveyID int,
	VoterID int,	
	Dws_score1 int,
	Dws_score2 int,
	Dws_score3 int,
	Dws_score4 int,
	Dws_average int)


	insert into #temptableDwsScores
	(SurveyID,
	VoterID,	
	Dws_score1,
	Dws_score2,
	Dws_score3,
	Dws_score4,
	Dws_average)
	(select @SurveyID, @VoterID, 110, 201, 345, 444, 1009)


	Select 
	VoterID,
	Dws_score1,
	Dws_score2,
	Dws_score3,
	Dws_score4,
	Dws_average
	from #temptableDwsScores where SurveyID = @SurveyID and VoterID = @VoterID

COMMIT TRANSACTION CalculateDwsScores

DROP TABLE #temptableDwsScores

GO


