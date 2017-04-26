USE [YOURDBNAME]
GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterExportCSVData]    Script Date: 3/21/2017 15:14:06 ******/
SET ANSI_NULLS OFF
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
/// Return the data needed to export a CSV  file
/// </summary>
*/
ALTER PROCEDURE [dbo].[vts_spVoterExportCSVData]
				@SurveyID int,
				@StartDate datetime ,
				@EndDate datetime
AS

SELECT  SUBSTRING(Q.QuestionText,1,20) as QuestionText,Q.QuestionId,
 AnswerID,SelectionModeId,AnswerTypeId, 
SUBSTRING(Q.QuestionText,1,20)+'...'+' | '+ AnswerText   as ColumnHeader ,
AnswerText,
Q.DisplayOrder QuestionDisplayOrder,
Q.QuestionId,
Q.Alias QuestionAlias,
Q.QuestionIdText QuestionIdText,
A.DisplayOrder AnswerDisplayOrder,
A.AnswerId ,
A.AnswerAlias,Q.ParentQuestionid,
	case when q.parentQuestionId is null then null
	else (select count(*)+1 from vts_tbquestion q1 
	         where q1.parentquestionid=q.parentquestionid
	         and   q1.questionid<q.questionid
	         ) 
	end as roworder,
	case when q.parentQuestionId is null then null
	else (select QuestionText from vts_tbquestion q1 
	         where q1.questionid=q.parentquestionid
	         ) 
	end as ParentQuestiontext,
	case when q.parentQuestionId is null then null
	else (select QuestionIdText from vts_tbquestion q1 
	         where q1.questionid=q.parentquestionid
	         ) 
	end as ParentQuestionIdtext,
	case when q.parentQuestionId is null then null
	else (select ALIAS from vts_tbquestion q1 
	         where q1.questionid=q.parentquestionid
	         ) 
	end as ParentQuestionAliastext,
A.AnswerIDText AnswerIdText
 FROM vts_tbQuestion Q
INNER JOIN vts_tbAnswer A
	ON A.QuestionID = Q.QuestionID
WHERE 
	SurveyID = @SurveyID  
ORDER BY Q.DisplayOrder, Q.QuestionID, A.DisplayOrder

SELECT
	V.VoterID,
	V.VoteDate,
	V.StartDate,
	V.IPSource,
	V.ContextUserName as username,
	(SELECT sum(ScorePoint) FROM vts_tbVoter 
		INNER JOIN vts_tbVoterAnswers
			ON vts_tbVoterAnswers.VoterID = vts_tbVoter.VoterID
		INNER JOIN vts_tbAnswer
			ON vts_tbAnswer.AnswerID = vts_tbVoterAnswers.AnswerID
		WHERE vts_tbVoter.VoterID = V.VoterID) AS Score,
	E.Email as email
	FROM vts_tbVoter V

		LEFT JOIN vts_tbVoterEmail 
		ON V.VoterID = vts_tbVoterEmail.VoterID
	LEFT JOIN vts_tbEmail E
		ON E.EmailID = vts_tbVoterEmail.EmailId

	WHERE 
		V.SurveyID = @SurveyID AND
		V.Validated <> 0 AND
		DATEDIFF (d,@startDate,V.VoteDate) >= 0 AND DATEDIFF (d,@endDate,V.VoteDate) <= 0
	ORDER BY V.VoterID DESC

SELECT
	V.VoterID,
	VA.AnswerID,
	SectionNumber,
	VA.AnswerText,
	AnswerTypeId,
	SelectionModeId,
	Q.QuestionId,
	A.AnswerText AnswerAnswerText,
	A.DisplayOrder AnswerDisplayOrder,
A.AnswerAlias,
A.AnswerIDText AnswerIdAlias
FROM vts_tbVoterAnswers VA
INNER JOIN vts_tbVoter V
	ON V.VoterID = VA.VoterID
INNER JOIN vts_tbAnswer A
    ON VA.AnswerId=A.AnswerId
INNER JOIN vts_tbQuestion Q
     ON A.QuestionId=Q.QuestionId
WHERE 
	V.SurveyID = @SurveyID AND
	V.Validated <> 0 AND
	DATEDIFF (d,@startDate,V.VoteDate) >= 0 AND DATEDIFF (d,@endDate,V.VoteDate) <= 0
ORDER BY V.VoterID DESC



