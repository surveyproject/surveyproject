USE [spdbdws]
GO

/****** Object:  StoredProcedure [dbo].[vts_spReportGetScoresDws]    Script Date: 30-3-2015 11:13:48 ******/
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

	-- calculate scores 


	select 


	-- VoterID

	(select VoterId = @VoterId) as VoterId,


	-- 1. Eigen Impulsiviteit
(
SELECT 

SUM(
CASE WHEN vts_tbAnswer.QuestionId In (653,656,657,659) THEN
(
CASE WHEN
(Case When CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) is NULL Then CAST(vts_tbAnswer.AnswerText AS VARCHAR(255))  
ELSE CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) End) = 'Nooit/zelden' THEN 1 ELSE
CASE WHEN
(Case When CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) is NULL Then CAST(vts_tbAnswer.AnswerText AS VARCHAR(255))  
ELSE CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) End) = 'Af en toe' THEN 2 ELSE
CASE WHEN 
(Case When CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) is NULL Then CAST(vts_tbAnswer.AnswerText AS VARCHAR(255))  
ELSE CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) End) = 'Vaak' THEN 3 ELSE
CASE WHEN 
(Case When CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) is NULL Then CAST(vts_tbAnswer.AnswerText AS VARCHAR(255))  
ELSE CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) End) = '(Bijna) altijd' THEN 4 ELSE 0 END 
END 
END
END 
 )
 

ELSE

(
CASE WHEN
(Case When CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) is NULL Then CAST(vts_tbAnswer.AnswerText AS VARCHAR(255))  
ELSE CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) End) = 'Nooit/zelden' THEN 4 ELSE
CASE WHEN
(Case When CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) is NULL Then CAST(vts_tbAnswer.AnswerText AS VARCHAR(255))  
ELSE CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) End) = 'Af en toe'  THEN 3 ELSE
CASE WHEN 
(Case When CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) is NULL Then CAST(vts_tbAnswer.AnswerText AS VARCHAR(255))  
ELSE CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) End) = 'Vaak' THEN 2 ELSE 
CASE WHEN 
(Case When CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) is NULL Then CAST(vts_tbAnswer.AnswerText AS VARCHAR(255))  
ELSE CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) End) = '(Bijna) altijd' THEN 1 ELSE 0 END 
END 
END
END 
 )
END
) 

FROM
vts_tbVoterAnswers INNER JOIN 
(vts_tbAnswer LEFT JOIN vts_tbQuestion ON vts_tbAnswer.QuestionId = vts_tbQuestion.QuestionId) 
ON vts_tbVoterAnswers.AnswerID = vts_tbAnswer.AnswerId

WHERE (vts_tbQuestion.SurveyID)= @SurveyID And vts_tbAnswer.QuestionId In (652,653,654,655,656,657,658,659) And vts_tbVoterAnswers.VoterID=@VoterID


) as 'Eigen Impulsiviteit',

	/* Kat Imp score */
	
	(
SELECT 

SUM(
CASE WHEN vts_tbAnswer.QuestionId In (79,80,81,82,83,84) THEN
(
CASE WHEN
(Case When CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) is NULL Then CAST(vts_tbAnswer.AnswerText AS VARCHAR(255))  
ELSE CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) End) = '1' THEN 5 ELSE
CASE WHEN
(Case When CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) is NULL Then CAST(vts_tbAnswer.AnswerText AS VARCHAR(255))  
ELSE CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) End) = '2' THEN 4 ELSE
CASE WHEN 
(Case When CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) is NULL Then CAST(vts_tbAnswer.AnswerText AS VARCHAR(255))  
ELSE CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) End) = '3' THEN 3 ELSE
CASE WHEN 
(Case When CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) is NULL Then CAST(vts_tbAnswer.AnswerText AS VARCHAR(255))  
ELSE CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) End) = '4' THEN 2 ELSE 
CASE WHEN 
(Case When CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) is NULL Then CAST(vts_tbAnswer.AnswerText AS VARCHAR(255))  
ELSE CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) End) = '5' THEN 1 ELSE 0 END 
END 
END 
END
END 
 )

ELSE

(
CASE WHEN
(Case When CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) is NULL Then CAST(vts_tbAnswer.AnswerText AS VARCHAR(255))  
ELSE CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) End) = '1' THEN 1 ELSE
CASE WHEN
(Case When CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) is NULL Then CAST(vts_tbAnswer.AnswerText AS VARCHAR(255))  
ELSE CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) End) = '2' THEN 2 ELSE
CASE WHEN 
(Case When CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) is NULL Then CAST(vts_tbAnswer.AnswerText AS VARCHAR(255))  
ELSE CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) End) = '3' THEN 3 ELSE
CASE WHEN 
(Case When CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) is NULL Then CAST(vts_tbAnswer.AnswerText AS VARCHAR(255))  
ELSE CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) End) = '4' THEN 4 ELSE 
CASE WHEN 
(Case When CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) is NULL Then CAST(vts_tbAnswer.AnswerText AS VARCHAR(255))  
ELSE CAST(vts_tbVoterAnswers.AnswerText AS VARCHAR(255)) End) = '5' THEN 5 ELSE 0 END 
END 
END 
END
END 
 )
END
) 


FROM
vts_tbVoterAnswers INNER JOIN 
(vts_tbAnswer LEFT JOIN vts_tbQuestion ON vts_tbAnswer.QuestionId = vts_tbQuestion.QuestionId) 
ON vts_tbVoterAnswers.AnswerID = vts_tbAnswer.AnswerId 


WHERE (vts_tbQuestion.SurveyID)= @SurveyID And vts_tbAnswer.QuestionId In (70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87) And vts_tbVoterAnswers.VoterID= @VoterID

) as 'Impulsiviteit van de Kat'


COMMIT TRANSACTION CalculateDwsScores


GO


