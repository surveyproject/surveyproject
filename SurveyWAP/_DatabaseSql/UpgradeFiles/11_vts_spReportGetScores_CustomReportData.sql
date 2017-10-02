USE [sp25dev]
GO

/****** Object:  StoredProcedure [dbo].[vts_spReportGetScores]    Script Date: 9/29/2017 09:58:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		W3DevPro
-- Create date: 2017-09-27
-- Description:	Stored Procedure to get calculated scores for custom reports
-- =============================================
CREATE PROCEDURE [dbo].[vts_spReportGetScores] 

	-- Add the parameters for the stored procedure here
	@SurveyID int,
	@VoterID int

AS
BEGIN TRANSACTION CalculateScores
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- calculate scores 

-- STEP 1.
	select 
	count(va.voterid) nrvoter,
case	when a.scorepoint = 1 then 'Detractor'
		when a.scorepoint = 2 then 'Passive'
		when a.scorepoint = 3 then 'Promoter'
end scorepoint

into #npsscore

from vts_tbvoteranswers va 
left join vts_tbanswer a on va.answerid = a.answerid

where va.voterid in 

(select voterid from vts_Tbvoter where surveyid = @SurveyID and votedate >= DATEADD (month , -1 , getdate() ))

-- and va.answerid <> 142
group by scorepoint

-- STEP 2. Calculate NPS

select  

	(select VoterId = @VoterID) as VoterID,

cast(
cast
(
(select nrvoter from #npsscore where scorepoint = 'Promoter') - 
(select nrvoter from #npsscore where scorepoint = 'Detractor') 
 as decimal(10,2))
/ (select sum(nrvoter) from #npsscore) * 100
as decimal(10,2)) as nps
--into #nps

-- STEP 3. Clean
drop table #npsscore

-- Finalize calculations

COMMIT TRANSACTION CalculateScores


GO


