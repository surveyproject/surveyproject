USE [sp24dbtest]
GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionGetForExport]    Script Date: 25-6-2016 22:18:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[vts_spQuestionGetForExport]  @QuestionID int AS

SELECT DISTINCT vts_tbAnswerType.* FROM vts_tbAnswerType
INNER JOIN vts_tbAnswer 
	ON vts_tbAnswer.AnswerTypeID = vts_tbAnswerType.AnswerTypeID
INNER JOIN vts_tbQuestion 
	ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID  
WHERE vts_tbQuestion.QuestionID = @QuestionID OR vts_tbQuestion.ParentQuestionID = @QuestionID

SELECT DISTINCT vts_tbRegularExpression.* FROM vts_tbRegularExpression
INNER JOIN vts_tbAnswer 
	ON vts_tbAnswer.RegularExpressionID = vts_tbRegularExpression.RegularExpressionID
INNER JOIN vts_tbQuestion 
	ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID  
WHERE vts_tbQuestion.QuestionID = @QuestionID OR vts_tbQuestion.ParentQuestionID = @QuestionID


-- Get main questions and answers
SELECT 
	QuestionID,
	ParentQuestionID,
	QuestionText, 
	vts_tbQuestion.SelectionModeId,
	LayoutModeId,
	MinSelectionRequired,
	MaxSelectionAllowed,
	RandomizeAnswers,
	RatingEnabled,
	ColumnsNumber,
	QuestionPipeAlias,
	Alias,
	HelpText,
	QuestionIdText,
	QuestionGroupId,
	ShowHelpText
FROM vts_tbQuestion 
WHERE QuestionID = @QuestionID AND ParentQuestionID is null

SELECT 
	vts_tbAnswer.AnswerID,
	vts_tbAnswer.QuestionID,
	AnswerText,
	ImageURL,
	DefaultText,
	AnswerPipeAlias,
	vts_tbAnswer.DisplayOrder,
	ScorePoint,
	RatePart,
	Selected,
	AnswerTypeID,
	RegularExpressionId,
	Mandatory,
	Answeralias,
	answerIdText,
	SliderRange,
	SliderValue,
	SliderMin,
	SliderMax,
	SliderAnimate,
	SliderStep,
	CssClass,
	vts_tbAnswer.AnswerID OldId
	
FROM vts_tbAnswer
INNER JOIN vts_tbQuestion 
	ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID  
WHERE vts_tbQuestion.QuestionID = @QuestionID AND vts_tbQuestion.ParentQuestionID is null

SELECT 
	PublisherAnswerID,
	SubscriberAnswerID,
	vts_tbAnswer.QuestionId
FROM vts_tbAnswerConnection
INNER JOIN vts_tbAnswer
	ON vts_tbAnswer.AnswerId = PublisherAnswerID
INNER JOIN vts_tbQuestion 
	ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID  
WHERE vts_tbQuestion.QuestionID = @QuestionID AND vts_tbQuestion.ParentQuestionID is null

-- Retrieves all child questions and their answers
SELECT 
	ParentQuestionID,
	QuestionText
FROM vts_tbQuestion 
WHERE ParentQuestionID = @QuestionID


SELECT 
	vts_tbAnswerProperty.AnswerID,
	Properties
FROM vts_tbAnswerProperty
INNER JOIN vts_tbAnswer
	ON vts_tbAnswerProperty.AnswerID = vts_tbAnswer.AnswerID  
INNER JOIN vts_tbQuestion 
	ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID  
WHERE vts_tbQuestion.QuestionID = @QuestionID AND vts_tbQuestion.ParentQuestionID is null

SELECT 
	DeleteSectionLinkText,
	EditSectionLinkText,
	UpdateSectionLinkText,
	AddSectionLinkText,
	QuestionId,
	MaxSections,
	RepeatableSectionModeId
FROM vts_tbQuestionSectionOption
WHERE QuestionID = @QuestionID

SELECT QuestionID, AnswerID FROM vts_tbQuestionSectionGridAnswer WHERE QuestionID = @QuestionID

SELECT [LanguageItemId]
      ,[LanguageCode]
      ,[LanguageMessageTypeId]
      ,[ItemText]
  FROM [dbo].[vts_tbMultiLanguageText]
  where
  (
   languageMessageTypeId=10 OR
  ( [LanguageItemId] =@QuestionID and
  [LanguageMessageTypeId] in(3,11,12))
  OR( [LanguageItemId] in (SELECT answerid from 
  vts_tbAnswer as ans  where ans.QuestionId=@QuestionID ) and
  [LanguageMessageTypeId] in(1,2,13))  )
  and len(ItemText) !=0
  and LanguageItemId in(
  SELECT g.ID
   FROM vts_tbQuestionGroups AS g
  WHERE g.ID  IN(
  SELECT q.QuestionGroupID FROM vts_tbQuestion AS  q WHERE q.QuestionId=@QuestionId)
  UNION
  SELECT g.ID FROM vts_tbQuestionGroups AS g
  WHERE ID IN(
  SELECT g.ParentGroupID FROM vts_tbQuestionGroups AS g
  WHERE g.ID  IN(
  SELECT q.QuestionGroupID FROM vts_tbQuestion AS  q WHERE q.QuestionId=@QuestionId)
  )
  )
  
SELECT g.ID,g.ParentGroupID,g.GroupName,g.DisplayOrder,g.ID OldId
   FROM vts_tbQuestionGroups AS g
  WHERE g.ID  IN(
  SELECT q.QuestionGroupID FROM vts_tbQuestion AS  q WHERE q.QuestionId=@QuestionId)
  UNION
  SELECT g.ID,g.ParentGroupID,g.GroupName,g.DisplayOrder ,g.ID OldId FROM vts_tbQuestionGroups AS g
  WHERE ID IN(
  SELECT g.ParentGroupID FROM vts_tbQuestionGroups AS g
  WHERE g.ID  IN(
  SELECT q.QuestionGroupID FROM vts_tbQuestion AS  q WHERE q.QuestionId=@QuestionId)
  )
  



