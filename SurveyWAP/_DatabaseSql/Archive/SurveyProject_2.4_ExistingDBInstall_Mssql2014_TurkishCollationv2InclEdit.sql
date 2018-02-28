USE [tez3]
GO
/****** Object:  UserDefinedTableType [dbo].[IntTableType]    Script Date: 19-8-2014 22:01:40 ******/
CREATE TYPE [dbo].[IntTableType] AS TABLE(
	[value] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[VarcharTableType]    Script Date: 19-8-2014 22:01:40 ******/
CREATE TYPE [dbo].[VarcharTableType] AS TABLE(
	[value] [varchar](40) NULL
)
GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerAddNew]    Script Date: 26-4-2016 9:37:51 ******/
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
/// Adds a new answer to a question
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerAddNew]
			@QuestionID int,
			@AnswerText NVARCHAR(4000), 
			@DefaultText NVARCHAR(4000), 
			@AnswerPipeAlias NVARCHAR(255), 
			@ImageURL NVARCHAR(1000), 
			@AnswerTypeID int,
			@Selected bit,
			@RatePart bit,
			@ScorePoint int,
			@DisplayOrder int = null,
			@RegularExpressionID int = null,
			@Mandatory bit,
			@AnswerIDText NVARCHAR(255),
			@AnswerAlias NVARCHAR(255),			
			@SliderRange NVARCHAR(3),
			@SliderValue int,
			@SliderMin int,
			@SliderMax int,
			@SliderAnimate bit,
			@SliderStep int,
			@CssClass NVARCHAR(50),
			@AnswerID int OUTPUT
AS

BEGIN TRANSACTION ADDNEWANSWER

if @Selected <> 0
BEGIN
-- Clear current Selected status if we only one selection is possible for the question
UPDATE vts_tbAnswer SET Selected = 0 
WHERE AnswerID IN (
	SELECT AnswerID FROM vts_tbAnswer 
	INNER JOIN vts_tbQuestion
		ON vts_tbAnswer.QuestionID = vts_tbQuestion.QuestionID
	INNER JOIN vts_tbQuestionSelectionMode
		ON vts_tbQuestionSelectionMode.QuestionSelectionModeID = vts_tbQuestion.SelectionModeID
	WHERE 	
		vts_tbAnswer.QuestionID = (SELECT QuestionID FROM vts_tbAnswer WHERE AnswerID = @AnswerID) AND 
		vts_tbQuestionSelectionMode.TypeMode & 16 = 0)
END 

IF @DisplayOrder is null OR (@DisplayOrder is not null AND 
	Exists(SELECT DisplayOrder FROM vts_tbAnswer WHERE DisplayOrder = @DisplayOrder AND QuestionID = @QuestionID))
BEGIN
	select @DisplayOrder = ISNULL ( max(DisplayOrder) + 1 , 1)  FROM vts_tbAnswer WHERE QuestionID = @QuestionID
END


INSERT INTO vts_tbAnswer 
	( QuestionID, 
	AnswerText,
	DefaultText, 
	ImageURL, 
	AnswerTypeID,
	Selected,
	RatePart,
	ScorePoint,
	DisplayOrder,
	AnswerPipeAlias,
	RegularExpressionID,
	Mandatory,
	AnswerIDText,
	AnswerAlias,
	SliderRange,
	SliderValue,
	SliderMin,
	SliderMax,
	SliderAnimate,
	SliderStep,
	CssClass
	)
VALUES
	 (@QuestionID, 
	@AnswerText, 
	@DefaultText, 
	@ImageURL, 
	@AnswerTypeID,
	@Selected,
	@RatePart,
	@ScorePoint,
	@DisplayOrder,
	@AnswerPipeAlias,
	@RegularExpressionID,
	@Mandatory,
	@AnswerIDText,
	@AnswerAlias,
	@SliderRange,
	@SliderValue,
	@SliderMin,
	@SliderMax,
	@SliderAnimate,
	@SliderStep,
	@CssClass
	
	)

set @AnswerID = SCOPE_IDENTITY()

COMMIT TRANSACTION ADDNEWANSWER


GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerConnectionCloneByQuestionID]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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

*/
CREATE PROCEDURE [dbo].[vts_spAnswerConnectionCloneByQuestionID] 
	@QuestionID int,
	@ClonedQuestionID int  
AS

-- Clone the answer publishers / subscribers 
INSERT INTO vts_tbAnswerConnection
	(PublisherAnswerID,
	SubscriberAnswerID)
SELECT      
	PublisherAnswerID = 
	(select AnswerID from vts_tbAnswer WHERE QuestionID = @ClonedQuestionID AND 
		DisplayOrder = (select DisplayOrder FROM vts_tbAnswer WHERE AnswerID = ASB.PublisherAnswerID)),
	SubscriberAnswerID = 
	(select AnswerID from vts_tbAnswer WHERE QuestionID = @ClonedQuestionID AND 
		DisplayOrder = (select DisplayOrder FROM vts_tbAnswer WHERE AnswerID = ASB.SubscriberAnswerID))
FROM vts_tbAnswerConnection ASB
INNER JOIN vts_tbAnswer A
	ON ASB.PublisherAnswerID = A.AnswerID
WHERE QuestionID = @QuestionID



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerConnectionSubscribeToPublisher]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Adds a new subscriber to the publisher
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerConnectionSubscribeToPublisher] @PublisherAnswerID int, @SubscriberAnswerID int AS

SELECT PublisherAnswerID FROM vts_tbAnswerConnection WHERE PublisherAnswerID = @PublisherAnswerID AND SubscriberAnswerID = @SubscriberAnswerID

IF @@RowCount = 0
BEGIN
	INSERT INTO vts_tbAnswerConnection (PublisherAnswerID, SubscriberAnswerID) VALUES (@PublisherAnswerID, @SubscriberAnswerID)
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerConnectionUnSubscribeFromPublisher]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Unsubscribes from the publisher
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerConnectionUnSubscribeFromPublisher] @PublisherAnswerID int, @SubscriberAnswerID int AS

DELETE FROM vts_tbAnswerConnection WHERE PublisherAnswerID = @PublisherAnswerID AND SubscriberAnswerID = @SubscriberAnswerID



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerDelete]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Delete an answer
/// </summary>
/// <param Name="@AnswerID">
/// Answer to delete
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerDelete] @AnswerID int as
DECLARE 
	@QuestionID int,
	@DisplayOrder int	
BEGIN TRANSACTION DeleteAnswer

-- Delete multi language texts
DELETE FROM vts_tbMultiLanguageText WHERE LanguageItemID = @AnswerID AND (LanguageMessageTypeID = 1 OR LanguageMessageTypeID = 2)

-- Delete associated Branching rules
DELETE FROM vts_tbBranchingRule WHERE AnswerID = @AnswerID

-- Delete associated skip logic rules
DELETE FROM vts_tbSkipLogicRule WHERE AnswerID = @AnswerID

-- Delete associated Message conditions
DELETE FROM vts_tbMessageCondition WHERE AnswerID = @AnswerID

-- Delete associated Filter  rules
DELETE FROM vts_tbFilterRule WHERE AnswerID = @AnswerID

-- Delete subscriber or publishers
DELETE FROM vts_tbAnswerConnection WHERE PublisherAnswerID = @AnswerID OR SubscriberAnswerID = @AnswerID

-- Retrieve the current Display order
SELECT @QuestionID = QuestionID, @DisplayOrder  = DisplayOrder
FROM vts_tbAnswer 
WHERE AnswerID = @AnswerID

-- Deletes the answer
DELETE FROM vts_tbAnswer WHERE AnswerID = @AnswerID

-- Updates the answers Display order
UPDATE vts_tbAnswer
SET DisplayOrder = DisplayOrder - 1 
WHERE 
	QuestionID = @QuestionID AND
	DisplayOrder >= @DisplayOrder
COMMIT TRANSACTION DeleteAnswer



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerGetAnswerTypeMode]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Retrieves the answer type mode
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerGetAnswerTypeMode]
		@AnswerID int
AS

SELECT 
	vts_tbAnswerType.TypeMode 
FROM 
	vts_tbAnswer
INNER JOIN vts_tbAnswerType 
	ON vts_tbAnswer.AnswerTypeID = vts_tbAnswerType.AnswerTypeID
WHERE 
	AnswerID = @AnswerID



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerGetDetails]    Script Date: 26-4-2016 9:46:57 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

/*
	Survey Project changes: copyright (c) 2016, Fryslan Webservices TM (http://survey.codeplex.com)	

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
/// get all question's answers
/// </summary>
/// <param Name="@QuestionID">
/// ID of the question from which we want the answers
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerGetDetails] @AnswerID int, @LanguageCode NVARCHAR(50) AS
SELECT 
	AnswerID,
	vts_tbAnswer.AnswerTypeID,
	QuestionID,
	AnswerText = 
		CASE @LanguageCode
		WHEN null THEN
			AnswerText 
		WHEN '' THEN
			AnswerText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbAnswer.AnswerID AND
			LanguageMessageTypeID = 1 AND
			LanguageCode = @LanguageCode), AnswerText)		
		END,
		AnswerAlias = 
		CASE @LanguageCode
		WHEN null THEN
			AnswerAlias
		WHEN '' THEN
			AnswerAlias
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbAnswer.AnswerID AND
			LanguageMessageTypeID = 13 AND
			LanguageCode = @LanguageCode), AnswerAlias)		
		END,
	ImageURL, 
	(SELECT count(*) FROM vts_tbVoterAnswers INNER JOIN vts_tbVoter ON vts_tbVoter.VoterID = vts_tbVoterAnswers.VoterID WHERE  vts_tbVoter.Validated<>0 AND AnswerID = vts_tbAnswer.AnswerID) as VoteCount,
	RatePart,
	Selected,
	ScorePoint,
	DefaultText = 
		CASE @LanguageCode 
		WHEN null THEN
			DefaultText 
		WHEN '' THEN
			DefaultText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbAnswer.AnswerID AND
			LanguageMessageTypeID = 2 AND
			LanguageCode = @LanguageCode), null)		
		END,
	FieldWidth,
	FieldHeight,
	FieldLength,
	TypeMode,	
	XMLDatasource,
	DataSource,
	JavascriptCode,
	JavascriptFunctionName,
	JavascriptErrorMessage,
	TypeNameSpace,
	TypeAssembly,
	AnswerPipeAlias,
	Mandatory,
	RegExpression,
	RegExMessage,
	vts_tbAnswer.RegularExpressionID,
	AnswerIDText,
	SliderRange,
	SliderValue,
	SliderMin,
	SliderMax,
	SliderAnimate,
	SliderStep,
	CssClass
		
FROM vts_tbAnswer
INNER JOIN vts_tbAnswerType 
	ON vts_tbAnswer.AnswerTypeID = vts_tbAnswerType.AnswerTypeID
LEFT JOIN vts_tbRegularExpression
	ON vts_tbAnswer.RegularExpressionID = vts_tbRegularExpression.RegularExpressionID
WHERE AnswerID=@AnswerID


GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerGetPublishersList]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Get a list of all answer that can be subscribed to
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerGetPublishersList] @AnswerID int AS

DECLARE @PageNumber int,
	@QuestionID int,
	@SurveyID int

SELECT  @SurveyID = Q.SurveyID, @PageNumber = Q.PageNumber, @QuestionID = Q.QuestionID
FROM vts_tbAnswer A
INNER JOIN vts_tbQuestion Q
	ON A.QuestionID = Q.QuestionID
WHERE A.AnswerID=@AnswerID
 

SELECT 
	A.AnswerID,
	A.AnswerText
FROM vts_tbAnswer A
INNER JOIN vts_tbQuestion Q
	ON A.QuestionID = Q.QuestionID
INNER JOIN vts_tbAnswerType 
	ON vts_tbAnswerType.AnswerTypeID = A.AnswerTypeID
WHERE A.AnswerID<>@AnswerID AND 
	A.AnswerID NOT IN ( Select PublisherAnswerID FROM vts_tbAnswerConnection WHERE SubscriberAnswerID = @AnswerID) AND
	Q.PageNumber = @PageNumber AND
	@QuestionID = Q.QuestionID AND 
	(Q.SurveyID = @SurveyID OR @SurveyID is null) AND
	vts_tbAnswerType.TypeMode & 16 > 1



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerGetScoreTotal]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// get all question's answers
/// </summary>
/// <param Name="@QuestionID">
/// ID of the question from which we want the answers
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerGetScoreTotal] @AnswerIDCSV varchar(7998) AS

SET @AnswerIDCSV = ',' +@AnswerIDCSV+ ','

SELECT sum(ScorePoint)
FROM vts_tbAnswer
WHERE @AnswerIDCSV LIKE '%,' +convert(varchar(12), AnswerID)+ ',%'



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerGetSubscriptionList]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Get a list of all answers to which the answer has subscribed to receive answer events
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerGetSubscriptionList] @AnswerID int AS

SELECT 
	AnswerID,
	AnswerText
FROM vts_tbAnswer 
WHERE AnswerID in 
(SELECT 
	PublisherAnswerID as AnswerID
FROM vts_tbAnswerConnection
WHERE SubscriberAnswerID=@AnswerID)



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerMatrixAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Adds a new column answer to a matrix
/// </summary>
/// <param Name="@QuestionID">
/// The id of the parent matrix question to which you will add the column
/// </param>
/// <param Name="@AnswerText">
/// The answer text
/// </param>
/// <param Name="@ImageURL">
/// Image that will be associated with the answer
/// </param>
/// <param Name="@AnswerTypeID">
/// The type of answer we want to create
/// </param>
/// <param Name="@AnswerID">
/// The created answer's id
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerMatrixAddNew] 
			@ParentQuestionID int, 
			@AnswerText NVARCHAR(4000), 
			@ImageURL NVARCHAR(1000), 
			@AnswerTypeID int,
			@AnswerID int OUTPUT
AS
BEGIN TRAN InsertMatrixColumn

DECLARE @DisplayOrder int

select @DisplayOrder = ISNULL ( max(DisplayOrder) + 1 , 1)  FROM vts_tbAnswer WHERE QuestionID = @ParentQuestionID

INSERT INTO vts_tbAnswer 
	( QuestionID, 
	AnswerText,
	ImageURL,
	DisplayOrder,
	AnswerTypeID)
VALUES
	 (@ParentQuestionID, 
	@AnswerText, 
	@ImageURL,
	@DisplayOrder,
	@AnswerTypeID)
set @AnswerID = SCOPE_IDENTITY()
exec vts_spAnswerMatrixChildAddNew 
			@ParentQuestionID, 
			@AnswerText, 
			@ImageURL,
			@DisplayOrder,
			@AnswerTypeID
COMMIT TRAN InsertMatrixColumn



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerMatrixChildAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Insert  the answers for all the child questions of the ParentQuestionID
/// </summary>
/// <param Name="@QuestionID">
/// The id of the parent matrix question to get the child question ids
/// </param>
/// <param Name="@AnswerText">
/// The answer text
/// </param>
/// <param Name="@ImageURL">
/// Image that will be associated with the answer
/// </param>
/// <param Name="@AnswerTypeID">
/// The type of answer we want to create
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerMatrixChildAddNew] 
			@ParentQuestionID int, 
			@AnswerText NVARCHAR(4000), 
			@ImageURL NVARCHAR(1000),
			@DisplayOrder int, 
			@AnswerTypeID int 
AS
INSERT INTO vts_tbAnswer  
	(QuestionID, 
	AnswerText,
	ImageURL,
	AnswerTypeID,
	DisplayOrder)
SELECT      
	QuestionID, 
	@AnswerText, 
	@ImageURL,
	@AnswerTypeID,
	@DisplayOrder
FROM vts_tbQuestion WHERE ParentQuestionID = @ParentQuestionID



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerMatrixDelete]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Delete a columns of the given matrix
/// </summary>
/// <param Name="@AnswerID">
/// Answer to delete from the matrix
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerMatrixDelete] 
				@AnswerID int
AS

-- Delete multi language texts
DELETE FROM vts_tbMultiLanguageText WHERE LanguageItemID  in 
	(SELECT AnswerID FROM vts_tbAnswer
	INNER JOIN vts_tbQuestion 
		ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID,
	(SELECT AnswerText, QuestionID FROM vts_tbAnswer WHERE AnswerID = @AnswerID) AS tbParentAnswer
	WHERE 
		vts_tbAnswer.AnswerText = tbParentAnswer.AnswerText AND 
		(vts_tbQuestion.ParentQuestionID = tbParentAnswer.QuestionID 
		OR vts_tbQuestion.QuestionID = tbParentAnswer.QuestionID))  AND LanguageMessageTypeID = 1

DELETE FROM vts_tbAnswer 
WHERE AnswerID in 
	(SELECT AnswerID FROM vts_tbAnswer
	INNER JOIN vts_tbQuestion 
		ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID,
	(SELECT AnswerText, QuestionID FROM vts_tbAnswer WHERE AnswerID = @AnswerID) AS tbParentAnswer
	WHERE 
		vts_tbAnswer.AnswerText = tbParentAnswer.AnswerText AND 
		(vts_tbQuestion.ParentQuestionID = tbParentAnswer.QuestionID 
		OR vts_tbQuestion.QuestionID = tbParentAnswer.QuestionID))



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerMatrixUpdate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Updates the settings of a matrix column
/// </summary>
/// <param Name="@AnswerID">
/// The answer to update
/// </param>
/// <param Name="@AnswerText">
/// The answer text
/// </param>
/// <param Name="@ImageURL">
/// Image that will be associated with the answer
/// </param>
/// <param Name="@AnswerTypeID">
/// The type of answer we want to create
/// </param>
/// <param Name="@RatePart">
/// Is this a rating parameter ?
/// </param>*/
CREATE PROCEDURE [dbo].[vts_spAnswerMatrixUpdate] 
			@AnswerID int, 
			@AnswerText NVARCHAR(4000), 
			@ImageURL NVARCHAR(1000), 
			@AnswerTypeID int,
			@RatePart bit,
			@Mandatory bit,
			@LanguageCode NVARCHAR(50)
AS
BEGIN TRAN UpdateChildAnswers

UPDATE vts_tbAnswer 
SET 	ImageURL = @ImageURL,
	AnswerTypeID = @AnswerTypeID,
	RatePart = @RatePart,
	Mandatory = @Mandatory
WHERE AnswerID in 
	(SELECT AnswerID FROM vts_tbAnswer
	INNER JOIN vts_tbQuestion 
		ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID,
	(SELECT AnswerText, QuestionID FROM vts_tbAnswer WHERE AnswerID = @AnswerID) AS tbParentAnswer
	WHERE 
		vts_tbAnswer.AnswerText = tbParentAnswer.AnswerText AND 
		(vts_tbQuestion.ParentQuestionID = tbParentAnswer.QuestionID 
		OR vts_tbQuestion.QuestionID = tbParentAnswer.QuestionID))

-- Updates text
IF @LanguageCode is null OR @LanguageCode=''
BEGIN
	UPDATE vts_tbAnswer 
	SET 	AnswerText = @AnswerText
	WHERE AnswerID in 
		(SELECT AnswerID FROM vts_tbAnswer
		INNER JOIN vts_tbQuestion 
			ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID,
		(SELECT AnswerText, QuestionID FROM vts_tbAnswer WHERE AnswerID = @AnswerID) AS tbParentAnswer
		WHERE 
			vts_tbAnswer.AnswerText = tbParentAnswer.AnswerText AND 
			(vts_tbQuestion.ParentQuestionID = tbParentAnswer.QuestionID 
			OR vts_tbQuestion.QuestionID = tbParentAnswer.QuestionID))
END
ELSE
BEGIN
	DECLARE @ChildAnswerID int
	DECLARE GetChildAnswers CURSOR LOCAL READ_ONLY  FOR 
		SELECT AnswerID FROM vts_tbAnswer
		INNER JOIN vts_tbQuestion 
			ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID,
		(SELECT AnswerText, QuestionID FROM vts_tbAnswer WHERE AnswerID = @AnswerID) AS tbParentAnswer
		WHERE 
			vts_tbAnswer.AnswerText = tbParentAnswer.AnswerText AND 
			(vts_tbQuestion.ParentQuestionID = tbParentAnswer.QuestionID 
			OR vts_tbQuestion.QuestionID = tbParentAnswer.QuestionID)

	OPEN GetChildAnswers
	FETCH NEXT FROM GetChildAnswers INTO @ChildAnswerID
	WHILE @@FETCH_STATUS = 0
	BEGIN      
		-- Updates localized text
		exec vts_spMultiLanguageTextUpdate @ChildAnswerID, @LanguageCode, 1, @AnswerText
		FETCH NEXT FROM GetChildAnswers INTO @ChildAnswerID
	END 
	CLOSE GetChildAnswers
	DEALLOCATE GetChildAnswers
	

END
COMMIT TRAN UpdateChildAnswers



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerMoveDown]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
///  Moves an answer's positions down 
/// </summary>
/// <param Name="@AnswerID">
/// ID of the answer to move one position down
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerMoveDown] @AnswerID int 
AS
DECLARE
	@OldDisplayOrder int,
	@NewDisplayOrder int,
	@QuestionID int
SELECT 
	@OldDisplayOrder = DisplayOrder,
	@QuestionID = QuestionID
FROM 
	vts_tbAnswer
WHERE
	AnswerID = @AnswerID
SELECT TOP 1  
	@NewDisplayOrder = DisplayOrder
FROM 
	vts_tbAnswer
WHERE
	QuestionID = @QuestionID AND
	DisplayOrder > @OldDisplayOrder
	ORDER BY DisplayOrder ASC
-- Is this already the last answer
IF @@RowCount <>0
BEGIN
	-- Move up previous answer
	UPDATE vts_tbAnswer
		set DisplayOrder = @OldDisplayOrder 
	WHERE 
		DisplayOrder = @NewDisplayOrder AND
		QuestionID = @QuestionID 
	-- Move down current answer
	UPDATE vts_tbAnswer set DisplayOrder = @NewDisplayOrder WHERE AnswerID = @AnswerID
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerMoveUp]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
///  Moves an answer's positions up 
/// </summary>
/// <param Name="@AnswerID">
/// ID of the answer to move one position up
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerMoveUp] @AnswerID int 
AS
DECLARE
	@OldDisplayOrder int,
	@NewDisplayOrder int,
	@QuestionID int
SELECT 
	@OldDisplayOrder = DisplayOrder,
	@QuestionID = QuestionID
FROM 
	vts_tbAnswer
WHERE
	AnswerID = @AnswerID
SELECT TOP 1  
	@NewDisplayOrder = DisplayOrder
FROM 
	vts_tbAnswer
WHERE
	QuestionID = @QuestionID AND
	DisplayOrder < @OldDisplayOrder
	ORDER BY DisplayOrder DESC
-- Is this the first answer
IF @@RowCount <>0
BEGIN
	-- Move down previous answer
	UPDATE vts_tbAnswer
		set DisplayOrder = @OldDisplayOrder 
	WHERE 
		DisplayOrder = @NewDisplayOrder AND
		QuestionID = @QuestionID 
	-- Move up current answer
	UPDATE vts_tbAnswer set DisplayOrder = @NewDisplayOrder WHERE AnswerID = @AnswerID
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerPropertyDelete]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Deletes  the answer properties data
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerPropertyDelete]
		@AnswerID int
AS

DELETE FROM 	vts_tbAnswerProperty WHERE AnswerID = @AnswerID



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerPropertyRestore]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Retrieves the properties  data
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerPropertyRestore]
		@AnswerID int
AS

SELECT 
	Properties 
FROM 
	vts_tbAnswerProperty
WHERE 
	AnswerID = @AnswerID



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerPropertyStore]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Store the serialized answer properties
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerPropertyStore]
		@AnswerID int, 
		@Properties image 
AS

DELETE FROM vts_tbAnswerProperty where AnswerID = @AnswerID
INSERT INTO vts_tbAnswerProperty(AnswerID, Properties) VALUES (@AnswerID, @Properties)

select SCOPE_IDENTITY()



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswersCloneByQuestionID]    Script Date: 26-4-2016 9:44:50 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

/*
	Survey Project changes: copyright (c) 2016, Fryslan Webservices TM (http://survey.codeplex.com)	

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
*/
CREATE PROCEDURE [dbo].[vts_spAnswersCloneByQuestionID] 
	@QuestionID int,
	@ClonedQuestionID int  
AS
BEGIN TRAN CloneAnswers
-- Clone the answer
INSERT INTO vts_tbAnswer  
	(QuestionID, 
	AnswerTypeID, 
	AnswerText,
	ImageURL,
	RatePart,
	DisplayOrder,
	Selected,
	DefaultText,
	ScorePoint,
	AnswerPipeAlias,
	RegularExpressionID,
	Mandatory,
	AnswerIDText,
	AnswerAlias,
	SliderRange,
	SliderValue,
	SliderMin,
	SliderMax,
	SliderAnimate,
	SliderStep,
	CssClass)
SELECT      
	@ClonedQuestionID, 
	AnswerTypeID, 
	AnswerText, 
	ImageURL,
	RatePart,
	DisplayOrder,
	Selected,
	DefaultText,
	ScorePoint,
	AnswerPipeAlias,
	RegularExpressionID,
	Mandatory,
	AnswerIDText,
	AnswerAlias,
	SliderRange,
	SliderValue,
	SliderMin,
	SliderMax,
	SliderAnimate,
	SliderStep,
	CssClass
FROM vts_tbAnswer WHERE QuestionID = @QuestionID

--- Clone any available answer multi language text or answer default value in different languages
INSERT INTO vts_tbMultiLanguageText
	(LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText)
SELECT      
	LanguageItemID = 
	(select AnswerID from vts_tbAnswer WHERE QuestionID = @ClonedQuestionID AND 
		DisplayOrder = (select DisplayOrder FROM vts_tbAnswer WHERE AnswerID = A.AnswerID)),
	LanguageCode, LanguageMessageTypeID, ItemText 
FROM vts_tbMultiLanguageText
INNER JOIN vts_tbAnswer A
	ON vts_tbMultiLanguageText.LanguageItemID = A.AnswerID
WHERE QuestionID = @QuestionID AND (LanguageMessageTypeID = 1 OR LanguageMessageTypeID = 2 OR LanguageMessageTypeID = 13)


INSERT INTO vts_tbAnswerProperty
	(AnswerID,
	Properties)
SELECT      
	AnswerID = 
	(select AnswerID from vts_tbAnswer WHERE QuestionID = @ClonedQuestionID AND 
		DisplayOrder = (select DisplayOrder FROM vts_tbAnswer WHERE AnswerID = A.AnswerID)),
	Properties 
FROM vts_tbAnswerProperty
INNER JOIN vts_tbAnswer A
	ON vts_tbAnswerProperty.AnswerID = A.AnswerID
WHERE QuestionID = @QuestionID

exec vts_spAnswerConnectionCloneByQuestionID @QuestionID, @ClonedQuestionID

COMMIT TRAN CloneAnswers


GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerTypeAddNew]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Adds a new answer type
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerTypeAddNew]
			@UserID int, 
			@Description varchar(200), 
			@XMLDatasource varchar(200) = null,
			@DataSource NVARCHAR(4000) = null, 
			@TypeMode int = 0,
			@FieldWidth int = 0,
			@FieldHeight int = 0,
			@FieldLength int = 0,
			@PublicFieldResults bit = 0,
			@JavascriptFunctionName varchar(1000) = null, 
			@JavascriptErrorMessage varchar(1000) = null, 
			@JavascriptCode varchar(8000) = null, 			
			@TypeAssembly varchar(200), 
			@TypeNameSpace varchar(200), 
			@AnswerTypeID int OUTPUT
AS

SELECT 
	@AnswerTypeID = AnswerTypeID
FROM vts_tbAnswerType
WHERE
	Description = @Description AND
	(XMLDatasource = @XMLDatasource OR XMLDatasource is null) AND
	(DataSource = @DataSource OR DataSource is null) AND
	TypeMode = @TypeMode AND
	(FieldWidth = @FieldWidth OR FieldWidth is null) AND
	(FieldHeight = @FieldHeight OR FieldHeight is null) AND
	(FieldLength = @FieldLength OR FieldLength is null)AND
	(JavascriptFunctionName = @JavascriptFunctionName OR JavascriptFunctionName is null) AND
	(JavascriptErrorMessage = @JavascriptErrorMessage OR JavascriptErrorMessage is null) AND
	(JavascriptCode = @JavascriptCode OR JavascriptCode is null) AND
	TypeAssembly = @TypeAssembly AND
	TypeNameSpace = @TypeNameSpace


if @@RowCount = 0
BEGIN
	INSERT INTO vts_tbAnswerType
		(Description, 
		XMLDatasource,
		DataSource,
		TypeMode,
		FieldWidth,
		FieldHeight,
		FieldLength,
		PublicFieldResults,
		JavascriptFunctionName,
		JavascriptErrorMessage,
		JavascriptCode,
		TypeAssembly,
		TypeNameSpace)
	VALUES
		 (@Description, 
		@XMLDatasource,
		@DataSource,
		@TypeMode,
		@FieldWidth,
		@FieldHeight,
		@FieldLength,
		@PublicFieldResults,
		@JavascriptFunctionName,
		@JavascriptErrorMessage,
		@JavascriptCode,
		@TypeAssembly,
		@TypeNameSpace)
	set @AnswerTypeID = SCOPE_IDENTITY()
	IF @UserID > 0
	BEGIN
		exec vts_spUserAnswerTypeAssignUser @AnswerTypeID, @UserID
	END
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerTypeDelete]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Deletes the given answer type id
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerTypeDelete] @AnswerTypeID int AS
DELETE FROM vts_tbAnswerType WHERE AnswerTypeID = @AnswerTypeID



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerTypeGetAll]    Script Date: 19-8-2014 22:01:40 ******/
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

*/
CREATE PROCEDURE [dbo].[vts_spAnswerTypeGetAll] AS
SELECT 
	AnswerTypeID,
	Description,
	XMLDatasource,
	DataSource,
	TypeMode,
	FieldWidth,
	FieldHeight,
	FieldLength,
	PublicFieldResults,
	JavascriptFunctionName,
	JavascriptErrorMessage,
	JavascriptCode,
	BuiltIn
 FROM vts_tbAnswerType ORDER BY Description



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerTypeGetDetails]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Retrieves all the infos of the given answer type id
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerTypeGetDetails] @AnswerTypeID int AS
SELECT 
	AnswerTypeID,
	Description, 
	XMLDatasource,
	DataSource,
	TypeMode,
	FieldWidth,
	FieldHeight,
	FieldLength,
	PublicFieldResults,
	JavascriptFunctionName,
	JavascriptErrorMessage,
	JavascriptCode,
	BuiltIn,
	TypeNameSpace,
	TypeAssembly
 FROM vts_tbAnswerType 
 WHERE AnswerTypeID = @AnswerTypeID



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerTypeGetEditableList]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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

*/
CREATE PROCEDURE [dbo].[vts_spAnswerTypeGetEditableList] AS
SELECT 
	AnswerTypeID,
	Description
 FROM vts_tbAnswerType
WHERE TypeMode & 4 = 0 AND TypeMode & 256 = 0
 ORDER BY Description



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerTypeGetEditableListForUser]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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

*/
CREATE PROCEDURE [dbo].[vts_spAnswerTypeGetEditableListForUser] @UserID int AS
SELECT 
	vts_tbAnswerType.AnswerTypeID,
	vts_tbAnswerType.Description
 FROM vts_tbAnswerType 
LEFT JOIN vts_tbUserAnswerType
	ON vts_tbUserAnswerType.AnswerTypeID = vts_tbAnswerType.AnswerTypeID
WHERE TypeMode & 4 = 0 AND TypeMode & 256 = 0 AND (vts_tbUserAnswerType.UserID = @UserID)
ORDER BY Description



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerTypeGetList]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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

*/
CREATE PROCEDURE [dbo].[vts_spAnswerTypeGetList] AS
SELECT 
	AnswerTypeID,
	Description
 FROM vts_tbAnswerType ORDER BY Description



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerTypeGetListForUser]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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

*/
CREATE PROCEDURE [dbo].[vts_spAnswerTypeGetListForUser] @UserID int, @SurveyID int AS
SELECT 
	distinct vts_tbAnswerType.AnswerTypeID,
	vts_tbAnswerType.Description
 FROM vts_tbAnswerType 
LEFT JOIN vts_tbUserAnswerType
	ON vts_tbUserAnswerType.AnswerTypeID = vts_tbAnswerType.AnswerTypeID
WHERE vts_tbUserAnswerType.UserID = @UserID OR 
	vts_tbAnswerType.BuiltIn<>0 OR 
	vts_tbAnswerType.AnswerTypeID in 
		(SELECT AnswerTypeID 
		FROM vts_tbAnswer 
		INNER JOIN vts_tbQuestion 
			ON vts_tbAnswer.QuestionID = vts_tbQuestion.QuestionID 
		WHERE SurveyID = @SurveyID)
ORDER BY Description



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerTypeIsInUse]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Check if the answer type is in use
/// by an answer
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerTypeIsInUse] @AnswerTypeID int AS
SELECT TOP 1 AnswerID FROM vts_tbAnswer WHERE AnswerTypeID = @AnswerTypeID



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerTypeSetBuiltIn]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Set  answer type to be built in
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerTypeSetBuiltIn] @AnswerTypeID int AS

BEGIN TRAN SetBuiltIn

exec vts_spUserAnswerTypeUnAssignAllUser @AnswerTypeID

UPDATE vts_tbAnswerType SET BuiltIn = 1 WHERE AnswerTypeID =@AnswerTypeID

COMMIT TRAN SetBuiltIn



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerTypeUpdate]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Updates the answer type data
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerTypeUpdate]
			@AnswerTypeID int, 
			@Description varchar(200), 
			@XMLDatasource varchar(200),
			@DataSource NVARCHAR(4000),
			@TypeMode int = 0,
			@FieldWidth int = 0,
			@FieldHeight int = 0,
			@FieldLength int = 0,
			@PublicFieldResults bit = 0,
			@JavascriptFunctionName varchar(1000), 
			@JavascriptErrorMessage varchar(1000), 
			@JavascriptCode varchar(8000),
			@TypeAssembly varchar(200), 
			@TypeNameSpace varchar(200)			
AS
UPDATE vts_tbAnswerType SET
	Description = @Description, 
	XMLDatasource = @XMLDatasource,
	DataSource = @DataSource,
	TypeMode = @TypeMode,
	FieldWidth = @FieldWidth,
	FieldHeight = @FieldHeight,
	FieldLength = @FieldLength,
	PublicFieldResults = @PublicFieldResults,
	JavascriptFunctionName = @JavascriptFunctionName,
	JavascriptErrorMessage = @JavascriptErrorMessage,
	JavascriptCode = @JavascriptCode,
	TypeAssembly = @TypeAssembly, 
	TypeNameSpace = @TypeNameSpace
WHERE AnswerTypeID = @AnswerTypeID



GO
/****** Object:  StoredProcedure [dbo].[vts_spAnswerUpdate]    Script Date: 26-4-2016 9:49:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
	Survey Project changes: copyright (c) 2016, Fryslan Webservices TM (http://survey.codeplex.com)	

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
/// Updates the settings of an answer
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spAnswerUpdate] 
			@AnswerID int,
			@AnswerText NVARCHAR(4000), 
			@DefaultText NVARCHAR(4000), 
			@ImageURL NVARCHAR(1000), 
			@AnswerPipeAlias NVARCHAR(255),
			@AnswerTypeID int,
			@Selected bit,
			@RatePart bit,
			@ScorePoint int,
			@RegularExpressionID int = null,
			@Mandatory bit,
			@LanguageCode NVARCHAR(50) = null,
			@AnswerIDText NVARCHAR(255),
			@AnswerAlias NVARCHAR(255),		
			@SliderRange NVARCHAR(3),
			@SliderValue int,
			@SliderMin int,
			@SliderMax int,
			@SliderAnimate bit,
			@SliderStep int,
			@CssClass NVARCHAR(50)
AS
BEGIN TRAN UpdateAnswer

if @Selected <> 0
BEGIN
-- Clear current Selected status if we only one selection is possible for the question
UPDATE vts_tbAnswer SET Selected = 0 
WHERE AnswerID IN (
	SELECT AnswerID FROM vts_tbAnswer 
	INNER JOIN vts_tbQuestion
		ON vts_tbAnswer.QuestionID = vts_tbQuestion.QuestionID
	INNER JOIN vts_tbQuestionSelectionMode
		ON vts_tbQuestionSelectionMode.QuestionSelectionModeID = vts_tbQuestion.SelectionModeID
	WHERE 	
		vts_tbAnswer.QuestionID = (SELECT QuestionID FROM vts_tbAnswer WHERE AnswerID = @AnswerID) AND 
		vts_tbQuestionSelectionMode.TypeMode & 16 = 0)
END 
UPDATE vts_tbAnswer
SET	ImageURL = @ImageURL,
	AnswerTypeID = @AnswerTypeID,
	RatePart = @RatePart,
	Selected = @Selected,
	ScorePoint = @ScorePoint,
	AnswerPipeAlias = @AnswerPipeAlias,
	RegularExpressionID = @RegularExpressionID,
	Mandatory = @Mandatory,
	AnswerIDText = @AnswerIDText,
	SliderRange = @SliderRange,
	SliderValue = @SliderValue,
	SliderMin = @SliderMin,
	SliderMax = @SliderMax,
	SliderAnimate = @SliderAnimate,
	SliderStep = @SliderStep,
	CssClass = @CssClass
	
WHERE
	AnswerID = @AnswerID

-- Updates text
IF @LanguageCode is null OR @LanguageCode=''
BEGIN
	UPDATE vts_tbAnswer
	SET 	AnswerText = @AnswerText,
		DefaultText = @DefaultText,
	    AnswerAlias = @AnswerAlias
	WHERE AnswerID = @AnswerID
END
ELSE
BEGIN
	-- Updates localized text
	exec vts_spMultiLanguageTextUpdate @AnswerID, @LanguageCode, 1, @AnswerText
	exec vts_spMultiLanguageTextUpdate @AnswerID, @LanguageCode, 2, @DefaultText
	exec vts_spMultiLanguageTextUpdate @AnswerID, @LanguageCode, 13, @AnswerAlias
END

COMMIT TRAN UpdateAnswer

GO
/****** Object:  StoredProcedure [dbo].[vts_spEmailAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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

*/
CREATE PROCEDURE [dbo].[vts_spEmailAddNew] 
				@Email varchar(150),
				@EmailID int output
AS
SELECT @EmailID = EmailID FROM vts_tbEmail WHERE Email = @Email
if @EmailID is null
BEGIN
	INSERT INTO vts_tbEmail (Email) VALUES (@Email)
	set @EmailID = SCOPE_IDENTITY()
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spEmailNotificationSettingsAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spEmailNotificationSettingsAddNew]
					@SurveyID int,
					@EmailFrom varchar(255),
					@EmailTo varchar(255),
					@EmailSubject NVARCHAR(255)
 AS

UPDATE vts_tbEmailNotificationSettings SET EmailFrom=@EmailFrom, EmailTo=@EmailTo, EmailSubject = @EmailSubject WHERE SurveyID = @SurveyID

if @@RowCount = 0
BEGIN
	INSERT INTO vts_tbEmailNotificationSettings (SurveyID, EmailFrom, EmailTo, EmailSubject) 
	VALUES (@SurveyID, @EmailFRom, @EmailTo, @EmailSubject)
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spEmailNotificationSettingsDelete]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spEmailNotificationSettingsDelete]
					@SurveyID int
 AS


DELETE FROM vts_tbEmailNotificationSettings WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spFileAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Adds a new file in its file group
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spFileAddNew] 
		@GroupGuid varchar(40), 
		@FileName NVARCHAR(1024), 
		@FileSize int, 
		@FileType NVARCHAR(1024), 
		@FileData image,
		@UploadedFileTimeOut int = 24,
		@SessionUploadedFileTimeOut int = 336

AS

-- clean up the table of the expired files
exec vts_spFileDeleteExpired @UploadedFileTimeOut, @SessionUploadedFileTimeOut

INSERT INTO vts_tbFile(GroupGuid, FileName, FileSize, FileType, FileData) VALUES (@GroupGuid, @FileName, @FileSize, @FileType, @FileData)

select SCOPE_IDENTITY()



GO
/****** Object:  StoredProcedure [dbo].[vts_spFileDelete]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Delete all file data
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spFileDelete]
		@FileID int,
		@GroupGuid varchar(40)
AS

DELETE
	vts_tbFile
WHERE 
	FileID=@FileID AND GroupGuid = @GroupGuid



GO
/****** Object:  StoredProcedure [dbo].[vts_spFileDeleteExpired]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Delete all file data that have expired 
/// after the hours specified in the timeout
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spFileDeleteExpired]
		@UploadedFileTimeOut int,
		@SessionUploadedFileTimeOut int
AS

DELETE  FROM vts_tbFile WHERE FileID IN (
	SELECT f.FileID FROM vts_tbFile f WHERE 
	GetDate()>DateAdd(hh, @UploadedFileTimeOut,f.SaveDate) AND 
	NOT EXISTS (SELECT VoterID FROM vts_tbVoterAnswers WHERE AnswerText like f.GroupGuid))

DELETE  FROM vts_tbFile WHERE FileID IN (
	SELECT f.FileID FROM vts_tbFile f WHERE 
	GetDate()>DateAdd(hh, @SessionUploadedFileTimeOut,f.SaveDate) AND 
	EXISTS 
	(SELECT vts_tbVoterAnswers.VoterID 
	 FROM vts_tbVoterAnswers INNER JOIN vts_tbVoter ON vts_tbVoter.VoterID = vts_tbVoterAnswers.VoterID
	WHERE AnswerText like f.GroupGuid AND vts_tbVoter.Validated=0))



GO
/****** Object:  StoredProcedure [dbo].[vts_spFileGetData]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Retrieves the image file data
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spFileGetData]
		@FileID int,
		@GroupGuid varchar(40)
AS

SELECT 
	FileData 
FROM 
	vts_tbFile 
WHERE 
	FileID=@FileID AND GroupGuid = @GroupGuid



GO
/****** Object:  StoredProcedure [dbo].[vts_spFileGetDetails]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// get all details about the requested file
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spFileGetDetails]
		@FileID int,
		@GroupGuid varchar(40)
AS

SELECT FileID, GroupGuid, FileName, FileSize, SaveDate, -1 as VoterID FROM vts_tbFile WHERE FileID=@FileID AND GroupGuid = @GroupGuid



GO
/****** Object:  StoredProcedure [dbo].[vts_spFileGetGroupCount]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Retrieves the number of files in a group
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spFileGetGroupCount]
		@GroupGuid varchar(40)
AS

SELECT count(*) as FileCount
FROM  vts_tbFile 
WHERE GroupGuid = @GroupGuid



GO
/****** Object:  StoredProcedure [dbo].[vts_spFileGetListForGuid]    Script Date: 26-4-2016 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// get all files of the given group using the Guid
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spFileGetListForGuid]
		@GroupGuid varchar(40)
AS

SELECT FileID, GroupGuid, FileName, FileSize, FileType, SaveDate, -1 as VoterID FROM vts_tbFile WHERE GroupGuid = @GroupGuid



GO
/****** Object:  StoredProcedure [dbo].[vts_spFileValidatedGetAll]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
///  Get Validated files
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spFileValidatedGetAll]
				@SurveyID int,
				@CurrentPage int = 1,
				@PageSize int=10,
				@TotalRecords int OUTPUT
AS
-- Turn off count return.
Set NOCOUNT On
-- Declare variables.
DECLARE @FirstRec int
DECLARE @LastRec int
-- Initialize variables.
SET @FirstRec = (@CurrentPage - 1) * @PageSize
SET @LastRec = (@CurrentPage * @PageSize + 1)

-- Create a temp table to hold the current page of data
-- Add an ID column to count the records
CREATE TABLE #TempTable (RowID int IDENTITY PRIMARY KEY, FileID int NOT NULL, VoterID int, GroupGuid varchar(40), FileName NVARCHAR(1024), FileType varchar(1024), FileSize int, SaveDate DateTime)

--Fill the temp table with the reminders
INSERT INTO #TempTable (FileID, VoterID, GroupGuid, FileName, FileType, FileSize, SaveDate)
	SELECT FileID, vts_tbVoter.VoterID, GroupGuid, FileName, FileType, FileSize, SaveDate
	FROM vts_tbFile
	INNER JOIN vts_tbVoterAnswers ON 
		AnswerText like GroupGuid
	INNER JOIN vts_tbVoter ON
		vts_tbVoter.VoterID = vts_tbVoterAnswers.VoterID
	WHERE vts_tbVoter.SurveyID = @SurveyID AND vts_tbVoter.Validated<>0
	ORDER BY SaveDate DESC

SELECT @TotalRecords = count(*) FROM vts_tbFile
INNER JOIN vts_tbVoterAnswers ON 
	AnswerText like GroupGuid
INNER JOIN vts_tbVoter ON
	vts_tbVoter.VoterID = vts_tbVoterAnswers.VoterID
WHERE vts_tbVoter.SurveyID = @SurveyID AND vts_tbVoter.Validated<>0

SELECT FileID, VoterID, GroupGuid, FileName, FileType, FileSize, SaveDate
FROM #TempTable
WHERE 
	RowID > @FirstRec AND
	RowID < @LastRec
DROP TABLE #TempTable



GO
/****** Object:  StoredProcedure [dbo].[vts_spFilterAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// insert a new  filter
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spFilterAddNew]
			@SurveyID int,
			@Description varchar(200), 
			@LogicalOperatorTypeID smallint = 0,
			@ParentFilterID int,
			@FilterID int output 
AS
INSERT INTO vts_tbFilter(SurveyID, Description, LogicalOperatorTypeID, ParentFilterID) VALUES (@SurveyID, @Description, @LogicalOperatorTypeID, @ParentFilterID)
set  @FilterID = SCOPE_IDENTITY()

GO
/****** Object:  StoredProcedure [dbo].[vts_spFilterDelete]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Deletes the filter 
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spFilterDelete] 
			@FilterID int
AS
DELETE FROM vts_tbFilter WHERE FilterID = @FilterID



GO
/****** Object:  StoredProcedure [dbo].[vts_spFilterGetDetails]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Retrieves all the infos of the given filter
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spFilterGetDetails] @FilterID int AS
SELECT 
	FilterID,
	Description, 
	LogicalOperatorTypeID,
	ParentFilterID
 FROM vts_tbFilter 
 WHERE FilterID = @FilterID

GO
/****** Object:  StoredProcedure [dbo].[vts_spFilterGetForSurvey]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spFilterGetForSurvey] @SurveyID int AS
SELECT 
	FilterID,
	Description, 
	LogicalOperatorTypeID,
	ParentFilterID
FROM vts_tbFilter 
WHERE SurveyID = @SurveyID
ORDER BY Description

GO
/****** Object:  StoredProcedure [dbo].[vts_spFilterGetForSurveyByParent]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
*/
Create PROCEDURE [dbo].[vts_spFilterGetForSurveyByParent] @SurveyID int, @ParentID int AS
SELECT 
	FilterID,
	Description, 
	LogicalOperatorTypeID,
	ParentFilterID
FROM vts_tbFilter 
WHERE SurveyID = @SurveyID and ISNULL(ParentFilterID,0) = @ParentID
ORDER BY Description
GO
/****** Object:  StoredProcedure [dbo].[vts_spFilterRuleAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Adds a new rule to a filter
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spFilterRuleAddNew] 
			@FilterID int,
			@QuestionID int,
			@AnswerID int,
			@TextFilter NVARCHAR(4000), 
			@FilterRuleID int OUTPUT
AS
INSERT INTO vts_tbFilterRule
	(FilterID,
	QuestionID,
	AnswerID,
	TextFilter)
VALUES
	(@FilterID,
	@QuestionID,
	@AnswerID,
	@TextFilter)
set @FilterRuleID = SCOPE_IDENTITY()



GO
/****** Object:  StoredProcedure [dbo].[vts_spFilterRuleDelete]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Deletes the filter rule
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spFilterRuleDelete] 
			@FilterRuleID int
AS
DELETE FROM vts_tbFilterRule WHERE FilterRuleID = @FilterRuleID



GO
/****** Object:  StoredProcedure [dbo].[vts_spFilterRuleGetForFilter]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spFilterRuleGetForFilter] @FilterID int AS
SELECT 
	FilterRuleID,
	FilterID,
	vts_tbFilterRule.QuestionID,
	vts_tbFilterRule.AnswerID,
	TextFilter,
	AnswerText,
	QuestionText
 FROM vts_tbFilterRule 
INNER JOIN vts_tbQuestion Q
	ON Q.QuestionID = vts_tbFilterRule.QuestionID
LEFT JOIN vts_tbAnswer A
	ON A.AnswerID = vts_tbFilterRule.AnswerID
WHERE FilterID = @FilterID



GO
/****** Object:  StoredProcedure [dbo].[vts_spFilterUpdate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Updates the filter data
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spFilterUpdate]
			@FilterID int, 
			@Description varchar(200), 
			@LogicalOperatorTypeID smallint = 0,
			@ParentFilterID int
AS
UPDATE vts_tbFilter SET
	Description = @Description, 
	LogicalOperatorTypeID = @LogicalOperatorTypeID,
	ParentFilterID = @ParentFilterID
WHERE FilterID = @FilterID

GO
/****** Object:  StoredProcedure [dbo].[vts_spFolderAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[vts_spFolderAddNew]	
	@ParentFolderID int,
	@FolderName varchar(200)
AS
BEGIN
if exists(
select 1 from vts_tbFolders where ParentFolderID=@ParentFolderID and FolderName=@FolderName)
begin
   raiserror('DUPLICATEFOLDER',16,4);
return;
end;
INSERT INTO vts_tbFolders(FolderName, ParentFolderID) VALUES (@FolderName, @ParentFolderID)
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spFolderDelete]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[vts_spFolderDelete]	
	@FolderID int
AS
BEGIN
declare @Folders as table(FID int,PFID int);
;with fcte1 (FID, PFID) AS
(
	SELECT FolderID, ParentFolderID FROM vts_tbFolders 
		where @FolderID = FolderID AND ParentFolderID IS NOT NULL
	UNION ALL
	SELECT f.FolderID, f.ParentFolderID FROM vts_tbFolders f 
	inner join fcte1 fc ON f.ParentFolderID = fc.FID	
)
 
insert into @Folders select * from fcte1;

declare c1 cursor for select sv.SurveyID
from [vts_tbSurvey] as sv
INNER JOIN @Folders AS fc ON fc.FID = sv.FolderID;
declare @SurveyID as int;
open c1;
fetch c1 into @SurveyID;
while @@FETCH_STATUS =0 
begin 
 exec vts_spSurveyDeleteByID @SurveyID
 fetch c1 into @SurveyID;
end;

close c1;
deallocate c1;
;with fcte (FID, PFID) AS
(
	SELECT FolderID, ParentFolderID FROM vts_tbFolders 
		where @FolderID = FolderID AND ParentFolderID IS NOT NULL
	UNION ALL
	SELECT f.FolderID, f.ParentFolderID FROM vts_tbFolders f 
	INNER JOIN fcte fc ON f.ParentFolderID = fc.FID
)--- delete passed folder
Delete fd from [vts_tbFolders] AS fd
INNER JOIN fcte AS fc ON fc.FID = fd.FolderID
where fd.ParentFolderID IS NOT NULL

END



GO
/****** Object:  StoredProcedure [dbo].[vts_spFolderGetByFolderID]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[vts_spFolderGetByFolderID] 	
	@FolderID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT CONVERT(varchar, fs.FolderID) as ItemID, fs.FolderName as NodeName, CONVERT(varchar, fs.ParentFolderID) as ParentFolderID
	FROM vts_tbFolders as fs
	WHERE FolderID = @FolderID
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spFolderMove]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[vts_spFolderMove]
	@ParentFolderID int,
	@FolderID int
AS
BEGIN
	SET NOCOUNT ON;
declare @FolderName varchar(300);
select @FolderName =FolderName from  vts_tbFolders where FolderID=@FolderID;
if exists(
select 1 from vts_tbFolders where ParentFolderID=@ParentFolderID and FolderName=@FolderName)
begin
   raiserror('DUPLICATEFOLDER',16,4);
return;
end;
    Update vts_tbFolders
	set ParentFolderID = @ParentFolderID	
	where FolderID = @FolderID and ParentFolderID IS NOT NULL
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spFolderUpdate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[vts_spFolderUpdate]	
	@FolderID int,
	@FolderName varchar(200),
	@ParentFolderID int
AS
BEGIN
if exists(
select 1 from vts_tbFolders where ParentFolderID=@ParentFolderID and FolderName=@FolderName and FolderID!=@FolderID)
begin
   raiserror('DUPLICATEFOLDER',16,4);
return;
end;
Update vts_tbFolders
set FolderName = @FolderName,
ParentFolderID = @ParentFolderID
where FolderID = @FolderID

END



GO
/****** Object:  StoredProcedure [dbo].[vts_spGetQuestionResults]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spGetQuestionResults] @QuestionID int AS



GO
/****** Object:  StoredProcedure [dbo].[vts_spInvitationLogAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Adds a new log
/// </summary>
*/

CREATE PROCEDURE 	[dbo].[vts_spInvitationLogAddNew]
			@SurveyID int,
			@Email NVARCHAR(155),
			@ExceptionMessage NVARCHAR(1024),
			@ExceptionType NVARCHAR(255),
			@ErrorDate datetime,
			@EmailID int OUTPUT,
			@InvitationLogID int OUTPUT
AS

SELECT @EmailID = EmailID FROM vts_tbEmail WHERE Email = @Email

if @@RowCount>0 
BEGIN
INSERT INTO vts_tbInvitationLog
	(SurveyID,
	EmailID,
	ExceptionMessage,
	ExceptionType,
	ErrorDate)
VALUES
	 (@SurveyID,
	@EmailID,
	@ExceptionMessage,
	@ExceptionType,
	@ErrorDate)

set @InvitationLogID = SCOPE_IDENTITY()
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spInvitationLogDelete]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
///  Deletes invitation log
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spInvitationLogDelete]
				@InvitationLogID int
AS

DELETE FROM vts_tbInvitationLog WHERE InvitationLogID = @InvitationLogID



GO
/****** Object:  StoredProcedure [dbo].[vts_spInvitationLogGetAll]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
///  get all logs
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spInvitationLogGetAll]
				@SurveyID int,
				@CurrentPage int = 1,
				@PageSize int=10,
				@TotalRecords int OUTPUT
AS
-- Turn off count return.
Set NOCOUNT On
-- Declare variables.
DECLARE @FirstRec int
DECLARE @LastRec int
-- Initialize variables.
SET @FirstRec = (@CurrentPage - 1) * @PageSize
SET @LastRec = (@CurrentPage * @PageSize + 1)

-- Create a temp table to hold the current page of data
-- Add an ID column to count the records
CREATE TABLE #TempTable (RowID int IDENTITY PRIMARY KEY, InvitationLogID int NOT NULL, SurveyID int, EmailID int, ExceptionMessage varchar(1024), ExceptionType NVARCHAR(255), Email varchar(155), ErrorDate DateTime)

--Fill the temp table with the reminders
INSERT INTO #TempTable (InvitationLogID, SurveyID, EmailID, ExceptionMessage, ExceptionType, Email, ErrorDate)
	SELECT InvitationLogID, SurveyID, vts_tbInvitationLog.EmailID, ExceptionMessage, ExceptionType, Email, ErrorDate
	FROM vts_tbInvitationLog
	INNER JOIN vts_tbEmail ON 
		vts_tbInvitationLog.EmailID = vts_tbEmail.EmailID
	WHERE vts_tbInvitationLog.SurveyID = @SurveyID
	ORDER BY ErrorDate DESC

SELECT @TotalRecords = count(*) FROM vts_tbInvitationLog
WHERE SurveyID = @SurveyID

SELECT InvitationLogID, SurveyID, EmailID, ExceptionMessage, ExceptionType, Email, ErrorDate
FROM #TempTable
WHERE 
	RowID > @FirstRec AND
	RowID < @LastRec
DROP TABLE #TempTable



GO
/****** Object:  StoredProcedure [dbo].[vts_spInvitationUidIsValid]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Check if the given UID is valid and returns its survey id
/// </summary>
/// <param Name="@UID">
/// UID to check
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spInvitationUidIsValid] @UID varchar(40)
AS
SELECT SurveyID
FROM vts_tbInvitationQueue 
WHERE UID = @UID



GO
/****** Object:  StoredProcedure [dbo].[vts_spIPRangeGetForSurvey]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Get the questions until next page break
/// </summary>
/// <param Name="@LibraryID">
/// ID of the Library  to retrieve questions from
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spIPRangeGetForSurvey]
			@SurveyID int
AS
	SELECT * from vts_tbSurveyIPRange
	WHERE SurveyID=@SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spLayoutModeGetAll]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spLayoutModeGetAll] AS
SELECT * FROM vts_tbLayoutMode



GO
/****** Object:  StoredProcedure [dbo].[vts_spLibraryAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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

*/
CREATE PROCEDURE [dbo].[vts_spLibraryAddNew] 
			@LibraryName NVARCHAR(255),
			@LibraryID int OUTPUT,
			@Description ntext,
			@DefaultLanguageCode NVARCHAR(50)

AS

INSERT INTO vts_tbLibrary (LibraryName, Description,DefaultLanguageCode)
 VALUES (@LibraryName, @Description,@DefaultLanguageCode)

set @LibraryID = SCOPE_IDENTITY()



GO
/****** Object:  StoredProcedure [dbo].[vts_spLibraryDelete]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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

*/
CREATE PROCEDURE [dbo].[vts_spLibraryDelete]
			@LibraryID int

AS

DELETE vts_tbLibrary WHERE LibraryID = @LibraryID



GO
/****** Object:  StoredProcedure [dbo].[vts_spLibraryGetAll]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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

*/
CREATE PROCEDURE [dbo].[vts_spLibraryGetAll] AS

SELECT
	l.LibraryID,
	l.LibraryName,
	l.Description,
	l.DefaultLanguageCode,
	(select count(q.QuestionID) from vts_tbQuestion q where q.LibraryID=l.LibraryID) as QuestionCnt
 FROM vts_tbLibrary l ORDER BY LibraryName



GO
/****** Object:  StoredProcedure [dbo].[vts_spLibraryGetDetails]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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

*/
CREATE PROCEDURE [dbo].[vts_spLibraryGetDetails] @LibraryID int AS

SELECT
	lib.LibraryID,
	lib.LibraryName,
	lib.Description,
	lib.DefaultLanguageCode,
	(select count(q.QuestionID) from vts_tbQuestion q where q.LibraryID=lib.LibraryID) as QuestionCnt
 FROM vts_tbLibrary lib WHERE lib.LibraryID = @LibraryID



GO
/****** Object:  StoredProcedure [dbo].[vts_spLibraryUpdate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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

*/
CREATE PROCEDURE [dbo].[vts_spLibraryUpdate]
			@LibraryName NVARCHAR(255),
			@LibraryID int,
			@Description ntext,
		    @DefaultLanguageCode NVARCHAR(50)

AS

UPDATE vts_tbLibrary SET LibraryName=@LibraryName, Description=@Description,
DefaultLanguageCode=@DefaultLanguageCode WHERE LibraryID = @LibraryID



GO
/****** Object:  StoredProcedure [dbo].[vts_spMultiLanguageGetAll]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Returns all languages
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spMultiLanguageGetAll] 
AS

SELECT * FROM vts_tbMultiLanguage ORDER BY LanguageDescription



GO
/****** Object:  StoredProcedure [dbo].[vts_spMultiLanguageTextAdd]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
///  Moves an answer's positions up 
/// </summary>
/// <param Name="@AnswerID">
/// ID of the answer to move one position up
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spMultiLanguageTextAdd] 
@LanguageItemID int,
@LanguageCode varchar(10),
@LanguageMessageTypeID int,
@ItemText varchar(max)
AS
INSERT INTO [vts_tbMultiLanguageText](LanguageItemID ,LanguageCode,LanguageMessageTypeID,ItemText)
VALUES (@LanguageItemID ,@LanguageCode,@LanguageMessageTypeID,@ItemText)


GO
/****** Object:  StoredProcedure [dbo].[vts_spMultiLanguageTextDeleteForSurvey]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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

*/
CREATE PROCEDURE [dbo].[vts_spMultiLanguageTextDeleteForSurvey]
			@SurveyID int
AS

-- Delete multi language texts for answer items
DELETE FROM vts_tbMultiLanguageText 
WHERE LanguageItemID in 
		(select AnswerID FROM vts_tbAnswer INNER JOIN vts_tbQuestion ON vts_tbAnswer.QuestionID = vts_tbQuestion.QuestionID WHERE vts_tbQuestion.SurveyID = @SurveyID)  AND 
	(LanguageMessageTypeID = 1 OR LanguageMessageTypeID = 2)

-- Delete multi languages texts for survey items
DELETE FROM vts_tbMultiLanguageText
 WHERE LanguageItemID = @SurveyID AND (LanguageMessageTypeID = 4 OR LanguageMessageTypeID = 5)

-- Delete multi languages texts for question items
DELETE FROM vts_tbMultiLanguageText
 WHERE LanguageItemID in  (select QuestionID FROM vts_tbQuestion WHERE SurveyID = @SurveyID) AND 
	(LanguageMessageTypeID = 3 OR LanguageMessageTypeID = 6 OR LanguageMessageTypeID = 7 OR LanguageMessageTypeID = 8 OR LanguageMessageTypeID = 9 )



GO
/****** Object:  StoredProcedure [dbo].[vts_spMultiLanguageTextUpdate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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

*/
CREATE PROCEDURE [dbo].[vts_spMultiLanguageTextUpdate] 
			@LanguageItemID int,
			@LanguageCode NVARCHAR(50),
			@LanguageMessageTypeID int,
			@ItemText NVARCHAR(4000)

AS

-- Updates localized text
UPDATE vts_tbMultiLanguageText
SET 	ItemText = @ItemText
WHERE 
	LanguageItemID = @LanguageItemID AND
	LanguageCode = @LanguageCode AND
	LanguageMessageTypeID = @LanguageMessageTypeID
	
-- If localized text doesnt exist, create it
IF @@RowCount = 0 AND @ItemText is not null
BEGIN
	INSERT INTO vts_tbMultiLanguageText (LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText)
	VALUES (@LanguageItemID, @LanguageCode, @LanguageMessageTypeID, @ItemText)
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spMutliLanguageAddForSurvey]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spMutliLanguageAddForSurvey] 
		 @SurveyID int,
         @LanguageCode NVARCHAR(50), 
		 @DefaultLanguage bit,
		 @Entity VARCHAR(20)='Survey'		 
AS
IF (@Entity='Survey') 
BEGIN
UPDATE vts_tbSurveyLanguage SET
	SurveyID = @SurveyID,
	LanguageCode = @LanguageCode,
	DefaultLanguage = @DefaultLanguage
WHERE 
	SurveyID = @SurveyID AND 
	LanguageCode = @LanguageCode ;
	IF @@RowCount = 0
	INSERT INTO vts_tbSurveyLanguage(SurveyID, LanguageCode, DefaultLanguage)
	VALUES (@SurveyID, @LanguageCode, @DefaultLanguage)
END
ELSE
BEGIN
if not Exists(select 1 from vts_tbMultiLanguage where LanguageCode=@LanguageCode)
BEGIN
   insert into vts_tbMultiLanguage(LanguageCode,LanguageDescription)
   values (@LanguageCode,@LanguageCode);
END;
UPDATE vts_tbLibraryLanguage SET
	LibraryID = @SurveyID,
	LanguageCode = @LanguageCode,
	DefaultLanguage = @DefaultLanguage
WHERE 
	LibraryID = @SurveyID AND 
	LanguageCode = @LanguageCode ;
	IF @@RowCount = 0
	INSERT INTO vts_tbLibraryLanguage(LibraryID, LanguageCode, DefaultLanguage)
	VALUES (@SurveyID, @LanguageCode, @DefaultLanguage)
END




GO
/****** Object:  StoredProcedure [dbo].[vts_spMutliLanguageCheckForSurvey]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spMutliLanguageCheckForSurvey] 
@SurveyID int, @LanguageCode NVARCHAR(50) AS

SELECT LanguageCode FROM vts_tbSurveyLanguage WHERE LanguageCode = @LanguageCode AND SurveyID = @SurveyID

IF @@RowCount = 0
	SELECT
		vts_tbMultiLanguage.LanguageCode
	FROM vts_tbMultiLanguage
	INNER JOIN vts_tbSurveyLanguage
		ON vts_tbMultiLanguage.LanguageCode = vts_tbSurveyLanguage.LanguageCode
	WHERE SurveyID = @SurveyID AND DefaultLanguage<>0



GO
/****** Object:  StoredProcedure [dbo].[vts_spMutliLanguageDeleteForSurvey]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spMutliLanguageDeleteForSurvey] 
@SurveyID int, @LanguageCode NVARCHAR(50),@Entity varchar(20)='Survey' AS

IF @Entity = 'Survey'
	DELETE FROM vts_tbSurveyLanguage 
	WHERE 
		SurveyID = @SurveyID AND 
		LanguageCode = @LanguageCode
ELSE IF @Entity = 'Library'
	DELETE FROM vts_tbLibraryLanguage 
	WHERE 
		LibraryID = @SurveyID AND
		LanguageCode = @LanguageCode



GO
/****** Object:  StoredProcedure [dbo].[vts_spMutliLanguageGetEnabledForSurvey]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spMutliLanguageGetEnabledForSurvey]
		 @SurveyID as int = -1,
		 @Entity VARCHAR(20) = 'Survey'
AS
SELECT 
	vts_tbMultiLanguage.LanguageCode,
	LanguageDescription, 
	DefaultLanguage
FROM vts_tbMultiLanguage
INNER JOIN vts_tbSurveyLanguage
	ON vts_tbMultiLanguage.LanguageCode = vts_tbSurveyLanguage.LanguageCode
WHERE SurveyID = @SurveyID
AND   @Entity='Survey'
UNION ALL
SELECT 
	vts_tbMultiLanguage.LanguageCode,
	LanguageDescription, 
	DefaultLanguage
FROM vts_tbMultiLanguage
INNER JOIN vts_tbLibraryLanguage
	ON vts_tbMultiLanguage.LanguageCode = vts_tbLibraryLanguage.LanguageCode
WHERE LibraryID = @SurveyID
AND   @Entity='Library'
ORDER BY DefaultLanguage DESC, LanguageDescription



GO
/****** Object:  StoredProcedure [dbo].[vts_spNotificationModeGetAll]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// get all surveys Email notification modes
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spNotificationModeGetAll] AS
SELECT * FROM vts_tbNotificationMode



GO
/****** Object:  StoredProcedure [dbo].[vts_spNSurveyGetVersion]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spNSurveyGetVersion]  AS
SELECT '1.9.1' as version



GO
/****** Object:  StoredProcedure [dbo].[vts_spPageOptionGetDetails]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
///  retrieves the options that were setup for the page
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spPageOptionGetDetails] @SurveyID int, @PageNumber int AS

SELECT * FROM vts_tbPageOption WHERE SurveyID = @SurveyID AND PageNumber = @PageNumber



GO
/****** Object:  StoredProcedure [dbo].[vts_spPageOptionUpdate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// updates the options that were setup for the page
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spPageOptionUpdate] 
				@SurveyID int, 
				@PageNumber int,
				@RandomizeQuestions bit,
				@EnableSubmitButton bit
AS

UPDATE vts_tbPageOption 
SET RandomizeQuestions = @RandomizeQuestions, EnableSubmitButton = @EnableSubmitButton
WHERE SurveyID = @SurveyID AND PageNumber = @PageNumber

if (@@RowCount = 0)
BEGIN
	-- no options were set add the new one in the db
	INSERT INTO vts_tbPageOption (SurveyID, PageNumber, RandomizeQuestions, EnableSubmitButton)
	VALUES (@SurveyID, @PageNumber, @RandomizeQuestions, @EnableSubmitButton)
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spProgressModeGetAll]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// get all surveys progress modes
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spProgressModeGetAll] AS
SELECT * FROM vts_tbProgressDisplayMode



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionAddNew]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Adds a new question to a survey
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionAddNew]
			@SurveyID int,
			@LibraryID int, 
			@QuestionText NVARCHAR(4000),
			@SelectionModeID int,
			@LayoutModeID int,
			@DisplayOrder int, 
			@PageNumber int,
			@ColumnsNumber int,
			@MinSelectionRequired int,
			@MaxSelectionAllowed int,
			@RandomizeAnswers bit,
			@RatingEnabled bit,
			@QuestionPipeAlias NVARCHAR(255),
			@QuestionIDText NVARCHAR(255),
			@Alias NVARCHAR(255)='',
			@HelpText NVARCHAR(4000)='',
			@ShowHelpText bit =0,
			@QuestionGroupID int=null,
			@QuestionID int OUTPUT
AS

BEGIN TRAN InsertQuestion

DECLARE @UpdateDisplayOrder bit 

-- Check if there is already a question with the same Display order
if @SurveyID is not null AND Exists(SELECT DisplayOrder FROM vts_tbQuestion WHERE ParentQuestionID is null AND DisplayOrder = @DisplayOrder AND SurveyID = @SurveyID)
BEGIN
	set @UpdateDisplayOrder = 1
END 
ELSE
BEGIN
	set @UpdateDisplayOrder = 0	
END

INSERT INTO vts_tbQuestion
	(SurveyID,
	LibraryID,
	SelectionModeID,
	LayoutModeID,
	DisplayOrder,
	PageNumber,
	QuestionText,
	ColumnsNumber,
	MinSelectionRequired,
	MaxSelectionAllowed,
	RandomizeAnswers,
	RatingEnabled,
	QuestionPipeAlias,
	QuestionIDText,
	Alias,
	HelpText,
	ShowHelpText,
	QuestionGroupID
	)
VALUES
	(@SurveyID,
	@LibraryID, 
	@SelectionModeID,
	@LayoutModeID,
	@DisplayOrder,
	@PageNumber,
	@QuestionText,
	@ColumnsNumber,
	@MinSelectionRequired,
	@MaxSelectionAllowed,
	@RandomizeAnswers,
	@RatingEnabled,
	@QuestionPipeAlias,
	@QuestionIDText,
	@Alias,
	@HelpText,
	@ShowHelpText,
	@QuestionGroupID)

set @QuestionID = SCOPE_IDENTITY()

IF @@RowCount<>0 AND @SurveyID is not null AND @UpdateDisplayOrder = 1
BEGIN
	-- Update the Display order
	UPDATE vts_tbQuestion 
	SET DisplayOrder = DisplayOrder + 1 
	WHERE 
		SurveyID = @SurveyID AND
		((QuestionID<>@QuestionID AND ParentQuestionID is null) OR
 		(ParentQuestionID is not null AND ParentQuestionID <> @QuestionID)) AND
 		DisplayOrder >= @DisplayOrder
END

COMMIT TRAN InsertQuestion



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionAnswerableList]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Get a list of all question that can have any type of answers
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey  to retrieve questions
/// </param>
/// <returns>
///	QuestionID,
///	SurveyID
///	QuestionText, 
/// </returns>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionAnswerableList] @SurveyID int  AS
SELECT DISTINCT Q.QuestionID, Q.QuestionText, Q.DisplayOrder ,
Q.QuestionIDText,Q.Alias
FROM vts_tbQuestion Q
INNER JOIN vts_tbQuestionSelectionMode
	ON  QuestionSelectionModeID= Q.SelectionModeID
WHERE 
	SurveyID = @SurveyID AND 
	TypeMode & 4 > 1 AND 
	NOT EXISTS(SELECT QuestionID FROM vts_tbQuestion WHERE ParentQuestionID = Q.QuestionID)
ORDER BY Q.DisplayOrder, Q.QuestionID



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionAnswerableListForPage]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Get a list of all question that can have any type of answers
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey  to retrieve questions
/// </param>
/// <param Name="@PageNumber">
/// Page to rertieve question from
/// </param>
/// <returns>
///	QuestionID,
///	SurveyID
///	QuestionText, 
/// </returns>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionAnswerableListForPage] @SurveyID int, @PageNumber int  AS
SELECT DISTINCT Q.QuestionID, Q.QuestionText, Q.DisplayOrder 
FROM vts_tbQuestion Q
INNER JOIN vts_tbQuestionSelectionMode
	ON  QuestionSelectionModeID= Q.SelectionModeID
WHERE 
	SurveyID = @SurveyID AND 
	Q.PageNumber = @PageNumber AND
	TypeMode & 4 > 1 AND 
	NOT EXISTS(SELECT QuestionID FROM vts_tbQuestion WHERE ParentQuestionID = Q.QuestionID)
ORDER BY Q.DisplayOrder, Q.QuestionID



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionAnswerableListForPageRange]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Get a list of all question that can have any type of answers
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey  to retrieve questions
/// </param>
/// <param Name="@PageNumber">
/// Page to rertieve question from
/// </param>
/// <returns>
///	QuestionID,
///	SurveyID
///	QuestionText, 
/// </returns>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionAnswerableListForPageRange] @SurveyID int, @StartPageNumber int, @EndPageNumber int AS
SELECT DISTINCT Q.QuestionID, Q.QuestionText, Q.DisplayOrder 
FROM vts_tbQuestion Q
INNER JOIN vts_tbQuestionSelectionMode
	ON  QuestionSelectionModeID= Q.SelectionModeID
WHERE 
	SurveyID = @SurveyID AND 
	(Q.PageNumber >= @StartPageNumber AND Q.PageNumber <= @EndPageNumber) AND
	TypeMode & 4 > 1 AND 
	NOT EXISTS(SELECT QuestionID FROM vts_tbQuestion WHERE ParentQuestionID = Q.QuestionID)
ORDER BY Q.DisplayOrder, Q.QuestionID



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionAnswerableListWithoutChilds]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Get a list of al parentl question that can have any type of answers
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey  to retrieve questions
/// </param>
/// <returns>
///	QuestionID,
///	SurveyID
///	QuestionText, 
/// </returns>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionAnswerableListWithoutChilds] @SurveyID int  AS
SELECT DISTINCT Q.QuestionID, Q.QuestionText, Q.DisplayOrder 
FROM vts_tbQuestion Q
INNER JOIN vts_tbQuestionSelectionMode
	ON  QuestionSelectionModeID= Q.SelectionModeID
WHERE 
	SurveyID = @SurveyID AND 
	TypeMode & 4 > 1 AND 
	ParentQuestionID is null
ORDER BY Q.DisplayOrder, Q.QuestionID



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionAnswerableWithoutChilds]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// get all parent questions that can have any type of answers
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey  to retrieve questions
/// </param>
/// <returns>
///	QuestionID,
///	SurveyID
///	QuestionText, 
/// </returns>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionAnswerableWithoutChilds] @SurveyID int  AS

SELECT DISTINCT 
	vts_tbQuestion.QuestionID,
	SurveyID,
	QuestionText, 
	vts_tbQuestion.SelectionModeID,
	LayoutModeID,
	DisplayOrder,
	MinSelectionRequired,
	MaxSelectionAllowed,
	RandomizeAnswers,
	TypeNameSpace,
	TypeAssembly,
	TypeMode,
	PageNumber,
	ColumnsNumber,
	QuestionPipeAlias,
	(ISNULL(RepeatableSectionModeID, 0)) as RepeatableSectionModeID,
	AddSectionLinkText,
	DeleteSectionLinkText,
	(ISNULL(MaxSections, 0)) as MaxSections	
FROM vts_tbQuestion 
LEFT JOIN vts_tbQuestionSectionOption
	ON vts_tbQuestionSectionOption.QuestionID = vts_tbQuestion.QuestionID
INNER JOIN vts_tbQuestionSelectionMode
	ON  QuestionSelectionModeID= SelectionModeID
WHERE 
	SurveyID = @SurveyID AND 
	TypeMode & 4 > 1 AND 
	ParentQuestionID is null
ORDER BY DisplayOrder, vts_tbQuestion.QuestionID



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionCheckUserAssigned]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Check if the question is assigned to this user
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionCheckUserAssigned] @QuestionID int, @UserID int AS
SELECT QuestionID FROM vts_tbQuestion
LEFT JOIN vts_tbSurvey 
	ON vts_tbSurvey.SurveyID = vts_tbQuestion.SurveyID
LEFT JOIN vts_tbUserSurvey 
	ON vts_tbUserSurvey .SurveyID = vts_tbSurvey.SurveyID
WHERE QuestionID = @QuestionID AND (UserID = @UserID OR LibraryID is not null)



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionChildAddNew]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Adds a new question to a survey
/// </summary>
/// <param Name="@ParentQuestionID">
/// Question  to which the child question will be added
/// </param>
/// <param Name="@QuestionText">
/// Question's text
/// </param>
/// <param Name="@QuestionID">
/// Created child question's ID
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionChildAddNew]
			@ParentQuestionID int,
			@QuestionText NVARCHAR(4000), 
			@QuestionID int OUTPUT
AS
-- Get parent default values
DECLARE 
	@SurveyID int,
	@DisplayOrder int,
	@SelectionModeID int,
	@PageNumber int,
	@LibraryID int,
	@RatingEnabled bit

SELECT @SurveyID = SurveyID, @LibraryID = LibraryID, @PageNumber = PageNumber, @DisplayOrder = DisplayOrder, @SelectionModeID = SelectionModeID, @RatingEnabled = RatingEnabled
FROM vts_tbQuestion WHERE QuestionID = @ParentQuestionID

INSERT INTO vts_tbQuestion
	(ParentQuestionID,
	SurveyID,
	LibraryID,
	SelectionModeID,
	DisplayOrder,
	PageNumber,
	RatingEnabled,
	QuestionText)
VALUES
	( @ParentQuestionID,
	@SurveyID,
	@LibraryID,
	@SelectionModeID,
	@DisplayOrder,
	@PageNumber,
	@RatingEnabled,
	@QuestionText)
IF @@RowCount<>0
BEGIN
	set @QuestionID = SCOPE_IDENTITY()
	-- Assign the same columns to the row
	-- as the parent question
	exec vts_spAnswersCloneByQuestionID @ParentQuestionID,@QuestionID
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionChildCloneByID]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Clones a child question and its answers
/// </summary>
/// <param Name="@QuestionID">
/// ID of the child question to clone
/// </param>
/// <param Name="@ParentQuestionID">
/// Parent of the child question
/// </param>
/// <return>
/// returns the cloned question
/// </return>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionChildCloneByID] 
				@QuestionID int, 
				@ParentQuestionID int,
				@SurveyID int
AS
INSERT INTO vts_tbQuestion  
	(ParentQuestionID, 
	SurveyID,
	LibraryID,
	SelectionModeID, 
	LayoutModeID, 
	DisplayOrder,
	PageNumber, 
	MinSelectionRequired, 
	MaxSelectionAllowed, 
	RatingEnabled,
	ColumnsNumber,
	RandomizeAnswers,
	QuestionText,
	QuestionIDText,
	Alias,
	HelpText,
	ShowHelpText,
	QuestionGroupID
	)
SELECT      
	@ParentQuestionID, 
	@SurveyID,
	LibraryID,
	SelectionModeID, 
	LayoutModeID, 
	DisplayOrder,
	PageNumber,
	MinSelectionRequired, 
	MaxSelectionAllowed, 
	RatingEnabled,
	ColumnsNumber,
	RandomizeAnswers,
	QuestionText ,
	QuestionIDText,
	Alias,
	HelpText,
	ShowHelpText,
	QuestionGroupID
FROM vts_tbQuestion WHERE QuestionID = @QuestionID
-- Check if the cloned question was created
IF @@RowCount <> 0
BEGIN
	DECLARE @ClonedQuestionID int
	-- Clone the question's answers
	set @ClonedQuestionID = SCOPE_IDENTITY()
	INSERT INTO vts_tbMultiLanguageText(LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText)
		SELECT @ClonedQuestionID as LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText
		FROM vts_tbMultiLanguageText
		WHERE LanguageItemID = @QuestionID AND LanguageMessageTypeID = 3
	exec vts_spAnswersCloneByQuestionID @QuestionID, @ClonedQuestionID
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionChildsClone]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Clones all childs of a question and its answers
/// </summary>
/// <param Name="@QuestionID">
/// ID of the parent question to clone the childs
/// </param>
/// <param Name="@CloneQuestionID">
/// ID of the cloned child parent
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionChildsClone] 
				@QuestionID int, 
				@ClonedQuestionID int,
				@ClonedSurveyID int
AS
	DECLARE ChildQuestionsCursor  CURSOR FOR
	SELECT QuestionID FROM vts_tbQuestion WHERE ParentQuestionID = @QuestionID
	DECLARE @ChildQuestionID int
	OPEN ChildQuestionsCursor
	FETCH ChildQuestionsCursor INTO @ChildQuestionID
	WHILE @@FETCH_STATUS = 0
	BEGIN
		EXEC vts_spQuestionChildCloneByID 	@ChildQuestionID, @ClonedQuestionID, @ClonedSurveyID
		FETCH ChildQuestionsCursor INTO @ChildQuestionID
	END
	CLOSE ChildQuestionsCursor
	DEALLOCATE ChildQuestionsCursor



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionChildUpdate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Updates a child question
/// </summary>
/// <param Name="ChildQuestionID">
/// ID of the child question to update
/// </param>
/// <param Name="@QuestionText">
/// Question's text
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionChildUpdate] 
			@ChildQuestionID int, 
			@QuestionText NVARCHAR(4000),
			@LanguageCode NVARCHAR(50)
AS

-- Updates text
IF @LanguageCode is null OR @LanguageCode='' 
BEGIN
	UPDATE vts_tbQuestion  
	SET 	QuestionText = @QuestionText
	WHERE
		QuestionID = @ChildQuestionID
END
ELSE
BEGIN
	-- Updates localized text
	exec vts_spMultiLanguageTextUpdate @ChildQuestionID, @LanguageCode, 3, @QuestionText
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionCloneByID]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Clones a question and its answers
/// </summary>
/// <param Name="@QuestionID">
/// ID of the question to clone
/// </param>
/// <return>
/// returns the cloned question
/// </return>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionCloneByID] @QuestionID int AS
BEGIN TRANSACTION CloneQuestion
DECLARE 
	@ClonedQuestionID int,
	@OldDisplayOrder int,
	@SurveyID int
-- Clone the question
INSERT INTO vts_tbQuestion  
	(ParentQuestionID, 
	SurveyID, 
	SelectionModeID, 
	LayoutModeID, 
	DisplayOrder,
	PageNumber, 
	MinSelectionRequired, 
	MaxSelectionAllowed, 
	RatingEnabled,
	ColumnsNumber,
	RandomizeAnswers,
	QuestionText,
	QuestionPipeAlias,
	LibraryID,
	QuestionIDText,
	Alias,
	HelpText,
	ShowHelpText,
	QuestionGroupID
	)
SELECT      
	ParentQuestionID, 
	SurveyID, 
	SelectionModeID, 
	LayoutModeID, 
	DisplayOrder,
	PageNumber, 
	MinSelectionRequired, 
	MaxSelectionAllowed, 
	RatingEnabled,
	ColumnsNumber,
	RandomizeAnswers,
	QuestionText + ' - Cloned',
	QuestionPipeAlias,
	LibraryID,
	QuestionIDText,
	Alias,
	HelpText,
	ShowHelpText,
	QuestionGroupID
FROM vts_tbQuestion WHERE QuestionID = @QuestionID
-- Check if the cloned question was created
IF @@RowCount <> 0
BEGIN
	-- Clone the question's answers
	set @ClonedQuestionID = SCOPE_IDENTITY()
	INSERT INTO vts_tbMultiLanguageText(LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText)
		SELECT @ClonedQuestionID as LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText
		FROM vts_tbMultiLanguageText
		WHERE LanguageItemID = @QuestionID AND LanguageMessageTypeID IN (3,11,12)

	SELECT @OldDisplayOrder = DisplayOrder, @SurveyID = SurveyID 
	FROM vts_tbQuestion WHERE QuestionID = @QuestionID
	exec vts_spAnswersCloneByQuestionID @QuestionID, @ClonedQuestionID

	-- Clone question's child question, if any available
	exec vts_spQuestionChildsClone @QuestionID, @ClonedQuestionID, @SurveyID

	-- Clone question section options
	exec vts_spQuestionSectionOptionClone @QuestionID, @ClonedQuestionID

	-- Clone question skip rules
	INSERT INTO vts_tbSkipLogicRule (
		ConditionalOperator,
		ExpressionOperator,
		AnswerID,
		SkipQuestionID,
		vts_tbSkipLogicRule.QuestionID,
		TextFilter,
		Score,
		ScoreMax)
	SELECT 
		ConditionalOperator,
		ExpressionOperator,
		AnswerID,
		@ClonedQuestionID,
		vts_tbSkipLogicRule.QuestionID,
		TextFilter,
		Score,
		ScoreMax
	FROM vts_tbSkipLogicRule WHERE SkipQuestionID = @QuestionID

	-- Update the Display order
	UPDATE vts_tbQuestion 
	SET DisplayOrder = DisplayOrder + 1 
	WHERE 
		SurveyID = @SurveyID AND
		((QuestionID<>@QuestionID AND ParentQuestionID is null) OR
 		(ParentQuestionID is not null AND ParentQuestionID <> @ClonedQuestionID)) AND
 		DisplayOrder >= @OldDisplayOrder 
END
COMMIT TRANSACTION CloneQuestion
exec vts_spQuestionGetDetails @ClonedQuestionID, null



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionCopy]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Copy an existing question to another survey
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionCopy]
				@QuestionID int, 
				@NewSurveyID int,
				@DisplayOrder int,
				@PageNumber int,
				@QuestionCopyID int output
AS

BEGIN TRANSACTION CopyQuestion

INSERT INTO vts_tbQuestion  
	(ParentQuestionID, 
	SurveyID,
	LibraryID,
	SelectionModeID, 
	LayoutModeID, 
	DisplayOrder,
	PageNumber, 
	MinSelectionRequired, 
	MaxSelectionAllowed, 
	RatingEnabled,
	ColumnsNumber,
	RandomizeAnswers,
	QuestionText,
	QuestionPipeAlias,
	QuestionIDText,
	HelpText,
	Alias,
	QuestionGroupID,
	ShowHelpText)
SELECT      
	ParentQuestionID, 
	@NewSurveyID,
	null, 
	SelectionModeID, 
	LayoutModeID, 
	@DisplayOrder,
	@PageNumber, 
	MinSelectionRequired, 
	MaxSelectionAllowed, 
	RatingEnabled,
	ColumnsNumber,
	RandomizeAnswers,
	QuestionText,
	QuestionPipeAlias,
	QuestionIDText,
	HelpText,
	Alias,
	QuestionGroupID,
	ShowHelpText
FROM vts_tbQuestion WHERE QuestionID = @QuestionID

-- Check if the cloned question was created
IF @@RowCount <> 0
BEGIN
	-- Clone the question's answers
	set @QuestionCopyID = SCOPE_IDENTITY()
	INSERT INTO vts_tbMultiLanguageText(LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText)
		SELECT @QuestionCopyID as LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText
		FROM vts_tbMultiLanguageText
		WHERE LanguageItemID = @QuestionID AND LanguageMessageTypeID in(3,10,11,12)	

	exec vts_spQuestionChildsClone @QuestionID, @QuestionCopyID, @NewSurveyID
	UPDATE vts_tbQuestion SET DisplayOrder = @DisplayOrder, PageNumber = @PageNumber 
	WHERE SurveyID = @NewSurveyID AND ParentQuestionID = @QuestionCopyID

	exec vts_spAnswersCloneByQuestionID @QuestionID, @QuestionCopyID

	exec vts_spQuestionSectionOptionClone @QuestionID, @QuestionCopyID

	-- Update the Display order
	UPDATE vts_tbQuestion 
	SET DisplayOrder = DisplayOrder + 1 
	WHERE 
		SurveyID = @NewSurveyID AND
		((QuestionID<>@QuestionCopyID AND ParentQuestionID is null) OR
 		(ParentQuestionID is not null AND ParentQuestionID <> @QuestionCopyID)) AND
 		DisplayOrder >= @DisplayOrder
END

COMMIT TRANSACTION CopyQuestion



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionCopyToLibrary]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Copy an existing question to a Library
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionCopyToLibrary]
				@QuestionID int, 
				@LibraryID int,
				@QuestionCopyID int output
AS

BEGIN TRANSACTION CopyQuestionToLibrary

INSERT INTO vts_tbQuestion  
	(ParentQuestionID, 
	SurveyID,
	LibraryID,
	LayoutModeID, 
	SelectionModeID, 
	ColumnsNumber,
	QuestionText,
	DisplayOrder,
	MinSelectionRequired, 
	MaxSelectionAllowed, 
	RatingEnabled,
	RandomizeAnswers,
	PageNumber, 
	QuestionPipeAlias,
	QuestionIDText,
	HelpText,
	Alias,
	QuestionGroupID,
	ShowHelpText)
SELECT      
	ParentQuestionID, 
	null,
	@LibraryID, 
	LayoutModeID, 
	SelectionModeID, 
	ColumnsNumber,
	QuestionText,
	1,
	MinSelectionRequired, 
	MaxSelectionAllowed, 
	RatingEnabled,
	RandomizeAnswers,
	1, 
	QuestionPipeAlias,
	QuestionIDText,
	HelpText,
	Alias,
	QuestionGroupID,
	ShowHelpText
FROM vts_tbQuestion WHERE QuestionID = @QuestionID

-- Check if the cloned question was created
IF @@RowCount <> 0
BEGIN
	-- Clone the question's answers
	set @QuestionCopyID = SCOPE_IDENTITY()
	INSERT INTO vts_tbMultiLanguageText(LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText)
		SELECT @QuestionCopyID as LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText
		FROM vts_tbMultiLanguageText
		WHERE LanguageItemID = @QuestionID AND LanguageMessageTypeID = 3	

	exec vts_spQuestionChildsClone @QuestionID, @QuestionCopyID, null

	UPDATE vts_tbQuestion SET LibraryID = @LibraryID WHERE ParentQuestionID = @QuestionCopyID

	exec vts_spAnswersCloneByQuestionID @QuestionID, @QuestionCopyID

	exec vts_spQuestionSectionOptionClone @QuestionID, @QuestionCopyID

END

COMMIT TRANSACTION CopyQuestionToLibrary



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionCopyToSurvey]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Copy all question of a survey to another survey, mainly Used in the clone survey process
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionCopyToSurvey] 
				@SurveyID int, 
				@NewSurveyID int
AS
DECLARE @QuestionID int
DECLARE @QuestionCopyID int
DECLARE QuestionListCursor  CURSOR FOR
	SELECT QuestionID FROM vts_tbQuestion
	WHERE SurveyID = @SurveyID AND ParentQuestionID is null
OPEN QuestionListCursor
FETCH QuestionListCursor INTO @QuestionID
WHILE @@FETCH_STATUS = 0
BEGIN
	INSERT INTO vts_tbQuestion  
		(ParentQuestionID, 
		SurveyID, 
		SelectionModeID, 
		LayoutModeID, 
		DisplayOrder,
		PageNumber, 
		MinSelectionRequired, 
		MaxSelectionAllowed, 
		RatingEnabled,
		ColumnsNumber,
		RandomizeAnswers,
		QuestionText,
		QuestionPipeAlias,
		QuestionIDText,
	HelpText,
	Alias,QuestionGroupID,
	ShowHelpText)
	SELECT      
		ParentQuestionID, 
		@NewSurveyID, 
		SelectionModeID, 
		LayoutModeID, 
		DisplayOrder,
		PageNumber, 
		MinSelectionRequired, 
		MaxSelectionAllowed, 
		RatingEnabled,
		ColumnsNumber,
		RandomizeAnswers,
		QuestionText,
		QuestionPipeAlias,
		QuestionIDText,
	HelpText,
	Alias,QuestionGroupID,
	ShowHelpText
	FROM vts_tbQuestion WHERE QuestionID = @QuestionID
-- Check if the cloned question was created
IF @@RowCount <> 0
BEGIN
	-- Clone the question's answers
	set @QuestionCopyID = SCOPE_IDENTITY()
	INSERT INTO vts_tbMultiLanguageText(LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText)
		SELECT @QuestionCopyID as LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText
		FROM vts_tbMultiLanguageText
		WHERE LanguageItemID = @QuestionID AND LanguageMessageTypeID = 3	

	exec vts_spQuestionChildsClone @QuestionID, @QuestionCopyID, @NewSurveyID
	exec vts_spAnswersCloneByQuestionID @QuestionID, @QuestionCopyID
	exec vts_spQuestionSectionOptionClone @QuestionID, @QuestionCopyID
END
FETCH QuestionListCursor INTO @QuestionID
END
CLOSE QuestionListCursor 
DEALLOCATE QuestionListCursor



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionDeleteByID]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Deletes a question and reorder the other questions
/// </summary>
/// <param Name="@QuestionID">
/// ID of the question to delete
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionDeleteByID]  @QuestionID int AS
DECLARE
		@DisplayOrder int,
		@SurveyID int,
		@ParentQuestionID int

BEGIN TRANSACTION DeleteQuestion

-- Delete multi language texts
DELETE FROM vts_tbMultiLanguageText WHERE LanguageItemID in  (select AnswerID FROM vts_tbAnswer WHERE QuestionID = @QuestionID OR QuestionID in (select QuestionID FROM vts_tbQuestion WHERE ParentQuestionID = @QuestionID)) AND
	 (LanguageMessageTypeID = 1 OR LanguageMessageTypeID = 2)

-- Delete multi language texts
DELETE FROM vts_tbMultiLanguageText WHERE LanguageItemID in  (select QuestionID FROM vts_tbQuestion WHERE ParentQuestionID = @QuestionID) AND LanguageMessageTypeID = 3

-- Delete multi language texts
DELETE FROM vts_tbMultiLanguageText WHERE LanguageItemID = @QuestionID AND 
	(LanguageMessageTypeID = 3 OR LanguageMessageTypeID = 6 OR LanguageMessageTypeID = 7 OR LanguageMessageTypeID = 8 OR LanguageMessageTypeID = 9)

-- Retrieve the current Display order
SELECT @SurveyID = SurveyID, @DisplayOrder  = DisplayOrder, @ParentQuestionID = ParentQuestionID
FROM vts_tbQuestion 
WHERE QuestionID = @QuestionID

-- Deletes Filter rules associated with this question
DELETE FROM vts_tbFilterRule WHERE QuestionID = @QuestionID

-- Deletes Branching rules associated with this question
DELETE FROM vts_tbBranchingRule WHERE QuestionID = @QuestionID

-- Deletes skip logic rules associated with this question
DELETE FROM vts_tbSkipLogicRule WHERE QuestionID = @QuestionID

-- Deletes Message condition rules associated with this question
DELETE FROM vts_tbMessageCondition WHERE QuestionID = @QuestionID

-- Deletes the answer subscribers
DELETE FROM vts_tbAnswerConnection WHERE PublisherAnswerID in (select AnswerID FROM vts_tbAnswer WHERE QuestionID = @QuestionID OR QuestionID in (select QuestionID FROM vts_tbQuestion WHERE ParentQuestionID = @QuestionID))

-- Deletes the questions answers
DELETE FROM vts_tbAnswer WHERE AnswerID in (select AnswerID FROM vts_tbAnswer WHERE QuestionID = @QuestionID OR QuestionID in (select QuestionID FROM vts_tbQuestion WHERE ParentQuestionID = @QuestionID))

-- Deletes the child questions
DELETE FROM vts_tbQuestion WHERE ParentQuestionID = @QuestionID

-- Deletes the question
DELETE FROM vts_tbQuestion WHERE QuestionID = @QuestionID


if @ParentQuestionID is null
BEGIN
	-- Updates the questions Display order if a parent question is deleted
	UPDATE vts_tbQuestion 
	SET DisplayOrder = DisplayOrder - 1 
	WHERE 
		SurveyID = @SurveyID AND
		DisplayOrder >= @DisplayOrder
END
COMMIT TRANSACTION DeleteQuestion



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionGetAnswerConnection]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Get a list of all answers that have subscribe to other answers in the question
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionGetAnswerConnection] @QuestionID int AS

SELECT PublisherAnswerID,
	SubscriberAnswerID
FROM vts_tbAnswerConnection
INNER JOIN vts_tbAnswer 
	ON vts_tbAnswerConnection.SubscriberAnswerID = vts_tbAnswer.AnswerID
WHERE vts_tbAnswer.QuestionID = @QuestionID



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionGetAnswers]    Script Date: 26-4-2016 9:51:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
	Survey Project changes: copyright (c) 2016, Fryslan Webservices TM (http://survey.codeplex.com)	

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
/// get all question's answers
/// </summary>
/// <param Name="@QuestionID">
/// ID of the question from which we want the answers
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionGetAnswers] @QuestionID int, @LanguageCode NVARCHAR(50) AS

SELECT 
	DisplayOrder,
	AnswerID,
	vts_tbAnswer.AnswerTypeID,
	QuestionID,
	AnswerText = 
		CASE @LanguageCode 
		WHEN null THEN
			AnswerText 
		WHEN '' THEN
			AnswerText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbAnswer.AnswerID AND
			LanguageMessageTypeID = 1 AND
			LanguageCode = @LanguageCode), AnswerText)		
		END,
	ImageURL, 
	(SELECT count(*) FROM vts_tbVoterAnswers INNER JOIN vts_tbVoter ON vts_tbVoter.VoterID = vts_tbVoterAnswers.VoterID WHERE  vts_tbVoter.Validated<>0 AND AnswerID = vts_tbAnswer.AnswerID) as VoteCount,
	RatePart,
	Selected,
	DefaultText = 
		CASE @LanguageCode 
		WHEN null THEN
			DefaultText 
		WHEN '' THEN
			DefaultText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbAnswer.AnswerID AND
			LanguageMessageTypeID = 2 AND
			LanguageCode = @LanguageCode), DefaultText)		
		END,
	ScorePoint,
	FieldWidth,
	FieldHeight,
	FieldLength,
	TypeMode,	
	XMLDatasource,
	DataSource,
	JavascriptCode,
	JavascriptFunctionName,
	JavascriptErrorMessage,
	TypeNameSpace,
	TypeAssembly,
	AnswerPipeAlias,
	Mandatory,
	RegExpression,
	RegExMessage,
	vts_tbAnswer.RegularExpressionID,
	AnswerAlias,
	CssClass
FROM vts_tbAnswer
INNER JOIN vts_tbAnswerType 
	ON vts_tbAnswer.AnswerTypeID = vts_tbAnswerType.AnswerTypeID
LEFT JOIN vts_tbRegularExpression
	ON vts_tbAnswer.RegularExpressionID = vts_tbRegularExpression.RegularExpressionID
WHERE QuestionID=@QuestionID ORDER BY vts_tbAnswer.DisplayOrder, AnswerID ASC

GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionGetAnswersList]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Get a list of all question's answers
/// </summary>
/// <param Name="@QuestionID">
/// ID of the question from which we want the answers
/// </param>
/// <returns>
/// AnswerID, Answer
/// </returns>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionGetAnswersList] @QuestionID int AS
SELECT 
	AnswerID,
	vts_tbAnswer.AnswerTypeID,
	AnswerText,
	TypeMode,
	DisplayOrder,
	AnswerIDText,
	AnswerAlias	
FROM vts_tbAnswer
INNER JOIN vts_tbAnswerType 
	ON vts_tbAnswer.AnswerTypeID = vts_tbAnswerType.AnswerTypeID
WHERE QuestionID=@QuestionID ORDER BY vts_tbAnswer.DisplayOrder



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionGetCrossTabResults]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// returns a results set with the compare question's answers number of voter 
/// that have also answered the base question answer
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionGetCrossTabResults] @CompareQuestionID int, @BaseQuestionAnswerID int AS

SELECT 
	(select count(VoterID) FROM vts_tbVoterAnswers WHERE AnswerID = asw.AnswerID AND VoterID in 
		(select VoterID FROM vts_tbVoterAnswers WHERE AnswerID = @BaseQuestionAnswerID)) 
	AS Total
FROM  vts_tbAnswer asw
INNER JOIN vts_tbAnswerType 
	ON asw.AnswerTypeID = vts_tbAnswerType.AnswerTypeID
WHERE QuestionID = @CompareQuestionID AND vts_tbAnswerType.TypeMode & 1 > 0



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionGetCrossTabTotalResults]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// returns a results set with the compare question's answers total number of voter 
/// that have or have not answered the base question answers
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionGetCrossTabTotalResults] @CompareQuestionID int, @BaseQuestionID int AS

SELECT 
	-- answered count
	(select count(VoterID) FROM vts_tbVoterAnswers WHERE AnswerID = asw.AnswerID AND 
		VoterID IN 
		(select VoterID FROM vts_tbVoterAnswers
 		WHERE AnswerID = asw.AnswerID and VoterID IN 
		(select VoterID FROM vts_tbVoterAnswers
 		INNER JOIN vts_tbAnswer ON vts_tbAnswer.AnswerID = vts_tbVoterAnswers.AnswerID
 		WHERE QuestionID = @BaseQuestionID)))

	+ 	
	-- unanswered count
	(select count(VoterID) FROM vts_tbVoterAnswers WHERE AnswerID = asw.AnswerID AND (VoterID NOT IN 
		(select VoterID FROM vts_tbVoterAnswers
 		INNER JOIN vts_tbAnswer ON vts_tbAnswer.AnswerID = vts_tbVoterAnswers.AnswerID
 		WHERE QuestionID = @BaseQuestionID)))
	AS Total
FROM vts_tbAnswer asw
INNER JOIN vts_tbAnswerType 
	ON asw.AnswerTypeID = vts_tbAnswerType.AnswerTypeID
WHERE QuestionID = @CompareQuestionID AND vts_tbAnswerType.TypeMode & 1 > 0



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionGetCrossTabUnansweredResults]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// returns a results set with the compare question's answers number of voter 
/// that have not answered the base question answers
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionGetCrossTabUnansweredResults] @CompareQuestionID int, @BaseQuestionID int AS

SELECT 
	(select count(VoterID) FROM vts_tbVoterAnswers WHERE AnswerID = asw.AnswerID AND  VoterID NOT IN 
		(select VoterID FROM vts_tbVoterAnswers
 		INNER JOIN vts_tbAnswer ON vts_tbAnswer.AnswerID = vts_tbVoterAnswers.AnswerID
 		WHERE QuestionID = @BaseQuestionID)) as Total
FROM vts_tbAnswer asw
INNER JOIN vts_tbAnswerType 
	ON asw.AnswerTypeID = vts_tbAnswerType.AnswerTypeID
WHERE QuestionID = @CompareQuestionID AND vts_tbAnswerType.TypeMode & 1 > 0



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionGetDetails]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Get details of a question
/// </summary>
/// <param Name="@QuestionID">
/// ID of the questions from which we want the details
/// </param>
/// <returns>
///	QuestionID,
///	SurveyID, 
///	QuestionText, 
///	SelectionModeID,
///	LayoutModeID,
///	DisplayOrder
///	MinSelectionRequired,
//	MaxSelectionAllowed
//	RandomizeAnswers,
//	RatingEnabled,
//	TypeMode
/// </returns>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionGetDetails]  @QuestionID int, @LanguageCode NVARCHAR(50) AS
SELECT 
	vts_tbQuestion.QuestionID,
	SurveyID,
	LibraryID,
	QuestionText = 
		CASE @LanguageCode 
		WHEN null THEN
			QuestionText 
		WHEN '' THEN
			QuestionText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestion.QuestionID AND
			LanguageMessageTypeID = 3 AND
			LanguageCode = @LanguageCode), QuestionText)		
		END,
	vts_tbQuestion.SelectionModeID,
	LayoutModeID,
	DisplayOrder,
	MinSelectionRequired,
	MaxSelectionAllowed,
	RandomizeAnswers,
	RatingEnabled,
	TypeMode,
	PageNumber,
	ColumnsNumber,
	QuestionPipeAlias,
	(ISNULL(RepeatableSectionModeID, 0)) as RepeatableSectionModeID,
	AddSectionLinkText = 
		CASE @LanguageCode 
		WHEN null THEN
			AddSectionLinkText 
		WHEN '' THEN
			AddSectionLinkText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestionSectionOption.QuestionID AND
			LanguageMessageTypeID = 6 AND
			LanguageCode = @LanguageCode), AddSectionLinkText)	
		END,
	DeleteSectionLinkText = 
		CASE @LanguageCode 
		WHEN null THEN
			DeleteSectionLinkText 
		WHEN '' THEN
			DeleteSectionLinkText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestionSectionOption.QuestionID AND
			LanguageMessageTypeID = 7 AND
			LanguageCode = @LanguageCode), DeleteSectionLinkText)	
		END,

	EditSectionLinkText = 
		CASE @LanguageCode 
		WHEN null THEN
			EditSectionLinkText 
		WHEN '' THEN
			EditSectionLinkText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestionSectionOption.QuestionID AND
			LanguageMessageTypeID = 9 AND
			LanguageCode = @LanguageCode), EditSectionLinkText)	
		END,
	UpdateSectionLinkText = 
		CASE @LanguageCode 
		WHEN null THEN
			UpdateSectionLinkText 
		WHEN '' THEN
			UpdateSectionLinkText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestionSectionOption.QuestionID AND
			LanguageMessageTypeID = 8 AND
			LanguageCode = @LanguageCode), UpdateSectionLinkText)
		END,
	(ISNULL(MaxSections, 0)) as MaxSections, 
	QuestionIDText,
	QuestionGroupID,
	HelpText = 
		CASE @LanguageCode 
		WHEN null THEN
			HelpText 
		WHEN '' THEN
			HelpText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestion.QuestionID AND
			LanguageMessageTypeID = 11 AND
			LanguageCode = @LanguageCode), HelpText)		
		END,
	ShowHelpText,
	Alias= 
		CASE @LanguageCode 
		WHEN null THEN
			Alias 
		WHEN '' THEN
			Alias
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestion.QuestionID AND
			LanguageMessageTypeID = 12 AND
			LanguageCode = @LanguageCode), Alias)		
		END
FROM vts_tbQuestion 
LEFT JOIN vts_tbQuestionSectionOption
	ON vts_tbQuestionSectionOption.QuestionID = vts_tbQuestion.QuestionID
INNER JOIN vts_tbQuestionSelectionMode 
	ON vts_tbQuestionSelectionMode.QuestionSelectionModeID = vts_tbQuestion.SelectionModeID
WHERE vts_tbQuestion.QuestionID=@QuestionID



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionGetForExport]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[vts_spQuestionGetForExport]  @QuestionID int AS

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
	vts_tbQuestion.SelectionModeID,
	LayoutModeID,
	MinSelectionRequired,
	MaxSelectionAllowed,
	RandomizeAnswers,
	RatingEnabled,
	ColumnsNumber,
	QuestionPipeAlias,
	Alias,
	HelpText,
	QuestionIDText,
	QuestionGroupID,
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
	RegularExpressionID,
	Mandatory,
	AnswerAlias,
	AnswerIDText,
	SliderRange,
	SliderValue,
	SliderMin,
	SliderMax,
	SliderAnimate,
	SliderStep,
	CssClass,
	vts_tbAnswer.AnswerID OldID
	
FROM vts_tbAnswer
INNER JOIN vts_tbQuestion 
	ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID  
WHERE vts_tbQuestion.QuestionID = @QuestionID AND vts_tbQuestion.ParentQuestionID is null

SELECT 
	PublisherAnswerID,
	SubscriberAnswerID,
	vts_tbAnswer.QuestionID
FROM vts_tbAnswerConnection
INNER JOIN vts_tbAnswer
	ON vts_tbAnswer.AnswerID = PublisherAnswerID
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
	QuestionID,
	MaxSections,
	RepeatableSectionModeID
FROM vts_tbQuestionSectionOption
WHERE QuestionID = @QuestionID

SELECT QuestionID, AnswerID FROM vts_tbQuestionSectionGridAnswer WHERE QuestionID = @QuestionID

SELECT [LanguageItemID]
      ,[LanguageCode]
      ,[LanguageMessageTypeID]
      ,[ItemText]
  FROM [dbo].[vts_tbMultiLanguageText]
  where
  (
   LanguageMessageTypeID=10 OR
  ( [LanguageItemID] =@QuestionID and
  [LanguageMessageTypeID] in(3,11,12))
  OR( [LanguageItemID] in (SELECT AnswerID from 
  vts_tbAnswer as ans  where ans.QuestionID=@QuestionID ) and
  [LanguageMessageTypeID] in(1,2,13))  )
  and len(ItemText) !=0
  and LanguageItemID in(
  SELECT g.ID
   FROM vts_tbQuestionGroups AS g
  WHERE g.ID  IN(
  SELECT q.QuestionGroupID FROM vts_tbQuestion AS  q WHERE q.QuestionID=@QuestionID)
  UNION
  SELECT g.ID FROM vts_tbQuestionGroups AS g
  WHERE ID IN(
  SELECT g.ParentGroupID FROM vts_tbQuestionGroups AS g
  WHERE g.ID  IN(
  SELECT q.QuestionGroupID FROM vts_tbQuestion AS  q WHERE q.QuestionID=@QuestionID)
  )
  )
  
SELECT g.ID,g.ParentGroupID,g.GroupName,g.DisplayOrder,g.ID OldID
   FROM vts_tbQuestionGroups AS g
  WHERE g.ID  IN(
  SELECT q.QuestionGroupID FROM vts_tbQuestion AS  q WHERE q.QuestionID=@QuestionID)
  UNION
  SELECT g.ID,g.ParentGroupID,g.GroupName,g.DisplayOrder ,g.ID OldID FROM vts_tbQuestionGroups AS g
  WHERE ID IN(
  SELECT g.ParentGroupID FROM vts_tbQuestionGroups AS g
  WHERE g.ID  IN(
  SELECT q.QuestionGroupID FROM vts_tbQuestion AS  q WHERE q.QuestionID=@QuestionID)
  )
  



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionGetHierarchyForSurvey]    Script Date: 19-8-2014 22:01:40 ******/
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
/// get all questions and child questions
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey from
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionGetHierarchyForSurvey] @SurveyID int
AS
SELECT 
	QuestionID,
	ParentQuestionID,
	SurveyID,
	QuestionText, 
	vts_tbQuestion.SelectionModeID,
	LayoutModeID,
	DisplayOrder,
	MinSelectionRequired,
	MaxSelectionAllowed,
	TypeNameSpace,
	TypeAssembly,
	TypeMode,
	PageNumber,
	ColumnsNumber,
	RandomizeAnswers,
	RatingEnabled,
	QuestionPipeAlias
FROM vts_tbQuestion
INNER JOIN vts_tbQuestionSelectionMode 
	ON vts_tbQuestionSelectionMode.QuestionSelectionModeID = vts_tbQuestion.SelectionModeID
WHERE 
	SurveyID=@SurveyID AND
	TypeMode & 4 = 4 
ORDER BY DisplayOrder ASC



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionGetResults]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spQuestionGetResults] 
				@QuestionID int,
				@FilterID int = -1,
				@SortOrder varchar(4) = 'ANS',
				@LanguageCode NVARCHAR(50),
				@StartDate datetime,
				@EndDate datetime
 AS
SELECT 
	Q.QuestionID,
	Q.ParentQuestionID,
	Q.SurveyID,
	Q.SelectionModeID,
	Q.QuestionText, 
	 (select QuestionText FROM vts_tbQuestion WHERE QuestionID = Q.ParentQuestionID) as ParentQuestionText,
	Q.DisplayOrder,
	Q.RatingEnabled
FROM vts_tbQuestion Q
WHERE 
	Q.QuestionID=@QuestionID OR Q.ParentQuestionID = @QuestionID
ORDER BY Q.QuestionID


IF @FilterID = -1
BEGIN
	SELECT 
		AnswerID,
		AnswerText,
		 (select count(*) 
			FROM vts_tbVoterAnswers 
			INNER JOIN vts_tbVoter ON 
				vts_tbVoter.VoterID = vts_tbVoterAnswers.VoterID
			WHERE Validated<>0 AND AnswerID = vts_tbAnswer.AnswerID AND
			DATEDIFF (d,@StartDate,vts_tbVoter.VoteDate) >= 0 AND DATEDIFF (d,@EndDate,vts_tbVoter.VoteDate) <= 0
			AND (vts_tbVoter.LanguageCode = @LanguageCode OR
			 ((@LanguageCode is null OR @LanguageCode = '') AND (LanguageCode is null OR LanguageCode ='')) OR
			(@LanguageCode = '-1' AND (LanguageCode is not null OR LanguageCode is null)))

) as VoterCount,
		vts_tbAnswer.QuestionID,
		vts_tbAnswer.AnswerTypeID,
		SelectionModeID,
		TypeMode,
		RatePart
	FROM vts_tbAnswer
	INNER JOIN vts_tbQuestion
		ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID
	INNER JOIN vts_tbAnswerType
		ON vts_tbAnswerType.AnswerTypeID = vts_tbAnswer.AnswerTypeID
	WHERE 
		vts_tbQuestion.QuestionID=@QuestionID OR vts_tbQuestion.ParentQuestionID = @QuestionID
 	ORDER BY 
		case when @SortOrder = 'ANS' then vts_tbAnswer.DisplayOrder end ,
		case when @SortOrder = 'ASC' then 
		 (select count(*) 
			FROM vts_tbVoterAnswers 
			INNER JOIN vts_tbVoter ON 
				vts_tbVoter.VoterID = vts_tbVoterAnswers.VoterID
			WHERE Validated<>0 AND AnswerID = vts_tbAnswer.AnswerID) end ASC ,
		case when @SortOrder = 'DESC' then  
		(select count(*) 
			FROM vts_tbVoterAnswers 
			INNER JOIN vts_tbVoter ON 
				vts_tbVoter.VoterID = vts_tbVoterAnswers.VoterID
			WHERE Validated<>0 AND AnswerID = vts_tbAnswer.AnswerID) end DESC
END 
ELSE
	EXEC vts_spVoterFilter @FilterID, @QuestionID, @SortOrder, @StartDate, @EndDate, @LanguageCode



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionGetSelectableAnswers]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// get all question's answers that can be Selected
/// </summary>
/// <param Name="@QuestionID">
/// ID of the question from which we want the selectable answers
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionGetSelectableAnswers] @QuestionID int AS
SELECT 
	AnswerID,
	vts_tbAnswer.AnswerTypeID,
	QuestionID,
	AnswerText,
	ImageURL, 
	(SELECT count(*) FROM vts_tbVoterAnswers INNER JOIN vts_tbVoter ON vts_tbVoter.VoterID = vts_tbVoterAnswers.VoterID WHERE  vts_tbVoter.Validated<>0 AND AnswerID = vts_tbAnswer.AnswerID) as VoteCount,
	RatePart,
	Selected,
	DefaultText,
	FieldWidth,
	FieldHeight,
	FieldLength,
	TypeMode,	
	XMLDatasource,
	DataSource,
	JavascriptCode,
	JavascriptFunctionName,
	JavascriptErrorMessage,
	TypeNameSpace,
	TypeAssembly,
	AnswerPipeAlias,
	Mandatory,
	RegExpression,
	RegExMessage,
	vts_tbAnswer.RegularExpressionID
FROM vts_tbAnswer
INNER JOIN vts_tbAnswerType 
	ON vts_tbAnswer.AnswerTypeID = vts_tbAnswerType.AnswerTypeID
LEFT JOIN vts_tbRegularExpression
	ON vts_tbAnswer.RegularExpressionID = vts_tbRegularExpression.RegularExpressionID
WHERE QuestionID=@QuestionID
AND vts_tbAnswerType.TypeMode & 1 > 0
ORDER BY vts_tbAnswer.DisplayOrder



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionGroupAddNew]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Adds a new question to a survey
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionGroupAddNew]
			@GroupName NVARCHAR(255),
			@ParentGroupID int, 
			@LanguageCode NVARCHAR(50) = null,
			@QuestionGroupID int OUTPUT
AS

BEGIN TRAN InsertQuestionGroup
declare @Disp int
select @Disp = ISNULL(MAX(DisplayOrder), 0) from vts_tbQuestionGroups 
where ParentGroupID = @ParentGroupID

INSERT INTO vts_tbQuestionGroups
	(GroupName,
	ParentGroupID,
	DisplayOrder
	)
VALUES
	(@GroupName,	 
	case when @ParentGroupID=-1 then null else @ParentGroupID end,
	@Disp + 1)

set @QuestionGroupID = SCOPE_IDENTITY()

-- Updates text
IF @LanguageCode is not null and @LanguageCode<>''
BEGIN
	-- Updates localized text
	exec vts_spMultiLanguageTextUpdate @QuestionGroupID, @LanguageCode, 10, @GroupName	
END

COMMIT TRAN InsertQuestionGroup



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionGroupDelete]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Adds a new question to a survey
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionGroupDelete]
			@QuestionGroupID int 
AS

BEGIN TRAN InsertQuestionGroup

delete from vts_tbMultiLanguageText where
LanguageMessageTypeID=10
and LanguageItemID in (
  select id from vts_tbQuestionGroups
where ID = @QuestionGroupID or ParentGroupID = @QuestionGroupID
);
update vts_tbQuestion 
set QuestionGroupID = null
where QuestionGroupID = @QuestionGroupID or
QuestionGroupID in(Select ID from vts_tbQuestionGroups
                             where ParentGroupID=@QuestionGroupID);

delete from vts_tbQuestionGroups
where ID = @QuestionGroupID or ParentGroupID = @QuestionGroupID


COMMIT TRAN InsertQuestionGroup



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionGroupGetAll]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Adds a new question to a survey
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionGroupGetAll]
@LanguageCode NVARCHAR(50) = null
AS

select ID, 
		CASE @LanguageCode 
		WHEN null THEN
			GroupName 
		WHEN '' THEN
			GroupName 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestionGroups.ID AND
			LanguageMessageTypeID = 10 AND
			LanguageCode = ISNULL(@LanguageCode,'@@@@')), GroupName)		
		END as GroupName,
	ParentGroupID,
	DisplayOrder from vts_tbQuestionGroups
order by DisplayOrder



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionGroupGetByQuestionID]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Adds a new question to a survey
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionGroupGetByQuestionID]
	@QuestionID int
AS
	declare @GID int
	select @GID = QuestionGroupID from vts_tbQuestion where QuestionID = @QuestionID

	select ID, GroupName,
		ParentGroupID,
		DisplayOrder from vts_tbQuestionGroups
		where ParentGroupID = @GID	
	UNION 
	select ID, GroupName,
		ParentGroupID,
		DisplayOrder from vts_tbQuestionGroups
		where ID = @GID



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionGroupUpdate]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Adds a new question to a survey
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionGroupUpdate]
			@GroupName NVARCHAR(255),			
			@QuestionGroupID int,
			@ParentGroupID int,
			@LanguageCode NVARCHAR(50) = null
AS

BEGIN TRAN InsertQuestionGroup

UPDATE vts_tbQuestionGroups
	set
	ParentGroupID = case when @ParentGroupID=-1 then null else @ParentGroupID End
where ID = @QuestionGroupID

-- Updates text
IF @LanguageCode is null OR @LanguageCode=''
BEGIN
	UPDATE vts_tbQuestionGroups
	SET 	GroupName = @GroupName
	WHERE ID = @QuestionGroupID
END
ELSE
BEGIN
	-- Updates localized text
	exec vts_spMultiLanguageTextUpdate @QuestionGroupID, @LanguageCode, 10, @GroupName	
END

COMMIT TRAN InsertQuestionGroup



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionGroupUpdateDisplayID]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Adds a new question to a survey
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionGroupUpdateDisplayID]
	@QuestionGroupID int,
	@Up int = 0
AS
	BEGIN TRAN UpdateQuestionGroup

	declare @ParentID int
	declare @Disp int	
	declare @PrevID int	
	declare @PrevDisp int
	declare @Way int
	declare @RID int		

	select @ParentID = ParentGroupID from vts_tbQuestionGroups 
		where ID = @QuestionGroupID	
	
	create table #TempQuestionGroups
    (GroupID int, DisplayOrder int, RowID int IDENTITY (1, 1))
    
	insert #TempQuestionGroups select ID, DisplayOrder from vts_tbQuestionGroups 
		where ISNULL(ParentGroupID, -1) = ISNULL(@ParentID, -1) order by DisplayOrder 
	
	set @Way = 
		case (@Up) 
		when 1 then -1
		when 0 then 1
		end
		
	select @RID = RowID + @Way from #TempQuestionGroups where GroupID = @QuestionGroupID
	
	if (not exists(select 1 from #TempQuestionGroups where @RID = RowID))
		select @RID = 
			case (@Up)
			when 0 then MAX(RowID)
			when 1 then MIN(RowID)			
			end
			 from #TempQuestionGroups 				 	
	
	select @PrevID = GroupID from #TempQuestionGroups where RowID = @RID
	
	update vts_tbQuestionGroups 
		set DisplayOrder = tmp.RowID
		from vts_tbQuestionGroups as QG
		inner join #TempQuestionGroups tmp on QG.ID = tmp.GroupID
	
	select @PrevDisp = DisplayOrder from vts_tbQuestionGroups where ID = @PrevID
	select @Disp = DisplayOrder from vts_tbQuestionGroups where ID = @QuestionGroupID
				
	update vts_tbQuestionGroups 
		set DisplayOrder = @Disp 
		where ID = @PrevID
	
	update vts_tbQuestionGroups 
		set DisplayOrder = @PrevDisp 
		where ID = @QuestionGroupID	
	
	drop table #TempQuestionGroups	
	
	
	COMMIT TRAN UpdateQuestionGroup



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionLibrarySingleAnswerableListWithoutChilds]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Get a list from the Library of all parent questions that can have any type of answers, except those that have child questions 
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionLibrarySingleAnswerableListWithoutChilds] @LibraryID int  AS
SELECT DISTINCT Q.QuestionID, Q.QuestionText, Q.DisplayOrder 
FROM vts_tbQuestion Q
INNER JOIN vts_tbQuestionSelectionMode
	ON  QuestionSelectionModeID= Q.SelectionModeID
WHERE 
	LibraryID = @LibraryID AND 
	TypeMode & 4 > 1 AND 
	ParentQuestionID is null
	AND QuestionID NOT IN (SELECT ParentQuestionID FROM vts_tbQuestion WHERE ParentQuestionID = Q.QuestionID)
ORDER BY Q.DisplayOrder, Q.QuestionID



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionListWithSelectableAnswers]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Get a list of all question that have selectable answers
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey  to retrieve questions
/// </param>
/// <returns>
///	QuestionID,
///	SurveyID
///	QuestionText, 
/// </returns>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionListWithSelectableAnswers] @SurveyID int  AS
SELECT DISTINCT Q.QuestionID, Q.QuestionText, Q.DisplayOrder FROM vts_tbQuestion Q
INNER JOIN vts_tbAnswer 
	ON vts_tbAnswer.QuestionID = Q.QuestionID
INNER JOIN vts_tbAnswerType
	ON vts_tbAnswer.AnswerTypeID = vts_tbAnswerType.AnswerTypeID
WHERE 
	SurveyID = @SurveyID AND 
	vts_tbAnswerType.TypeMode & 1 > 0 AND 
	NOT EXISTS(SELECT QuestionID FROM vts_tbQuestion WHERE ParentQuestionID = Q.QuestionID)
ORDER BY Q.DisplayOrder, Q.QuestionID



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionListWithSelectableAnswersForPage]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Get a list of all question that have selectable answers
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey  to retrieve questions
/// </param>
/// <param Name="@SurveyID">
/// Page to rertieve question from
/// </param>
/// <returns>
///	QuestionID,
///	SurveyID
///	QuestionText, 
/// </returns>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionListWithSelectableAnswersForPage] @SurveyID int, @PageNumber int  AS
SELECT DISTINCT Q.QuestionID, Q.QuestionText, Q.DisplayOrder FROM vts_tbQuestion Q
INNER JOIN vts_tbAnswer 
	ON vts_tbAnswer.QuestionID = Q.QuestionID
INNER JOIN vts_tbAnswerType
	ON vts_tbAnswer.AnswerTypeID = vts_tbAnswerType.AnswerTypeID
WHERE 
	SurveyID = @SurveyID AND 
	Q.PageNumber = @PageNumber AND
	vts_tbAnswerType.TypeMode & 1 > 0 AND 
	NOT EXISTS(SELECT QuestionID FROM vts_tbQuestion WHERE ParentQuestionID = Q.QuestionID)
ORDER BY Q.DisplayOrder, Q.QuestionID



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionMatrixDeleteAnswers]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Delete all the columns of the given matrix
/// </summary>
/// <param Name="@ParentQuestionID">
/// Parent matrix question id to find child questions
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionMatrixDeleteAnswers]
				@ParentQuestionID int
AS
DELETE FROM vts_tbAnswer 
WHERE AnswerID in 
	(SELECT AnswerID 
	FROM vts_tbAnswer 
	INNER JOIN vts_tbQuestion 
		ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID
	WHERE 
		 vts_tbQuestion.QuestionID = @ParentQuestionID OR 
		 vts_tbQuestion.ParentQuestionID = @ParentQuestionID)



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionMoveDown]    Script Date: 19-8-2014 22:01:40 ******/
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
///  Moves a question positions down 
/// </summary>
/// <param Name="@QuestionID">
/// ID of the questions to move one position down
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionMoveDown] @QuestionID int 
AS
DECLARE
	@OldDisplayOrder int,
	@NewDisplayOrder int,
	@OldPageNumber int,
	@NewPageNumber int,
	@SurveyID int
SELECT 
	@OldDisplayOrder = DisplayOrder,
	@OldPageNumber = PageNumber,
	@SurveyID = SurveyID
FROM 
	vts_tbQuestion
WHERE
	QuestionID = @QuestionID
SELECT TOP 1  
	@NewDisplayOrder = DisplayOrder,
	@NewPageNumber = PageNumber
FROM 
	vts_tbQuestion
WHERE
	SurveyID = @SurveyID AND
	ParentQuestionID is null AND
	DisplayOrder > @OldDisplayOrder
	ORDER BY DisplayOrder ASC	
if @@RowCount <>0
BEGIN
	-- Are we just changing the page or are we moving the question behind another one ?
	IF @OldPageNumber = @NewPageNumber 
	BEGIN
		-- Move down previous question
		UPDATE vts_tbQuestion 
			set DisplayOrder = @OldDisplayOrder 
		WHERE 
			DisplayOrder = @NewDisplayOrder AND
			SurveyID = @SurveyID 
		-- Move up current question
		UPDATE vts_tbQuestion set DisplayOrder = @NewDisplayOrder WHERE QuestionID = @QuestionID OR ParentQuestionID = @QuestionID
	END
	ELSE IF @OldPageNumber +1 < @NewPageNumber 
	BEGIN
		-- Move one page down
		UPDATE vts_tbQuestion set PageNumber = PageNumber+1 WHERE QuestionID = @QuestionID OR ParentQuestionID = @QuestionID
	END 
	ELSE
	BEGIN
		-- Move one page down
		UPDATE vts_tbQuestion set PageNumber = @NewPageNumber WHERE QuestionID = @QuestionID OR ParentQuestionID = @QuestionID
	END
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionMoveUp]    Script Date: 19-8-2014 22:01:40 ******/
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
///  Moves a question positions up 
/// </summary>
/// <param Name="@QuestionID">
/// ID of the questions to move one position up
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionMoveUp] @QuestionID int 
AS
DECLARE
	@OldDisplayOrder int,
	@OldPageNumber int,
	@NewDisplayOrder int,
	@NewPageNumber int,
	@SurveyID int
SELECT 
	@OldDisplayOrder = DisplayOrder,
	@OldPageNumber = PageNumber,
	@SurveyID = SurveyID
FROM 
	vts_tbQuestion
WHERE
	QuestionID = @QuestionID
SELECT TOP 1  
	@NewDisplayOrder = DisplayOrder,
	@NewPageNumber = PageNumber
FROM 
	vts_tbQuestion
WHERE
	SurveyID = @SurveyID AND
	ParentQuestionID is null AND
	DisplayOrder < @OldDisplayOrder
	ORDER BY DisplayOrder DESC
-- Is this the first question ?
IF @@RowCount <>0
BEGIN
	-- Are we just changing the page or are we moving the question in front of another one ?
	IF @OldPageNumber = @NewPageNumber 
	BEGIN
		-- Move down previous question
		UPDATE vts_tbQuestion 
			set DisplayOrder = @OldDisplayOrder 
		WHERE 
			DisplayOrder = @NewDisplayOrder AND
			SurveyID = @SurveyID
		-- Move up current question
		UPDATE vts_tbQuestion set DisplayOrder = @NewDisplayOrder WHERE QuestionID = @QuestionID OR ParentQuestionID = @QuestionID
	END
	ELSE IF @OldPageNumber - 1 > @NewPageNumber 
	BEGIN
		-- Move one page up
		UPDATE vts_tbQuestion set PageNumber = PageNumber-1 WHERE QuestionID = @QuestionID OR ParentQuestionID = @QuestionID	
	END 
	ELSE
	BEGIN
		-- Move one page up
		UPDATE vts_tbQuestion set PageNumber = @NewPageNumber WHERE QuestionID = @QuestionID OR ParentQuestionID = @QuestionID 		
	END 
END
ELSE
BEGIN
	-- Check if there are any page breaks before
	IF @OldPageNumber>1
	BEGIN
		UPDATE vts_tbQuestion set DisplayOrder = 1, PageNumber = PageNumber-1 WHERE QuestionID = @QuestionID OR ParentQuestionID = @QuestionID
	END
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionOrderUpdate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[vts_spQuestionOrderUpdate] 
@QuestionID int, 
@UpdateUp  bit = 0 -- 1 to move up, or zero to move down
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

declare @NewDisplayOrder int, @LibID int, @OldQuestionID int
declare @OrderIndex int, @CurrentDisplayOrder int, @MaxOrderID int
declare @QID int

select @LibID = LibraryID from vts_tbQuestion where QuestionID = @QuestionID

create table #TempQuestions
    (QuestionID int, DisplayOrder int)

insert #TempQuestions select QuestionID, DisplayOrder from vts_tbQuestion where LibraryID = @LibID order by DisplayOrder 

DECLARE CursorQuestions CURSOR for
 SELECT QuestionID, DisplayOrder FROM #TempQuestions order by DisplayOrder

 -- we make reorder of dysplayorder in case of duplicating DisplayOrderid
 set @OrderIndex = 0
 OPEN CursorQuestions
 FETCH NEXT FROM CursorQuestions
 INTO @QID, @CurrentDisplayOrder
 While @@FETCH_STATUS = 0
 Begin
	set @OrderIndex = @OrderIndex + 1
	UPDATE vts_tbQuestion SET DisplayOrder = @OrderIndex WHERE QuestionID = @QID
	FETCH NEXT FROM CursorQuestions
		INTO @QID, @CurrentDisplayOrder
 End
 CLOSE CursorQuestions;
 DEALLOCATE CursorQuestions;
 drop table #TempQuestions;

 select @MaxOrderID = MAX(DisplayOrder) from vts_tbQuestion where LibraryID = @LibID
 select @CurrentDisplayOrder = DisplayOrder, @LibID = LibraryID from vts_tbQuestion where QuestionID = @QuestionID
 
 if @UpdateUp > 0
	set @NewDisplayOrder = @CurrentDisplayOrder - 1
 else
	set @NewDisplayOrder = @CurrentDisplayOrder + 1 
       
 if @NewDisplayOrder < 1
	set @NewDisplayOrder = 1
 if @NewDisplayOrder >= @MaxOrderID
	set @NewDisplayOrder = @MaxOrderID
         
 select @OldQuestionID = QuestionID from vts_tbQuestion where DisplayOrder = @NewDisplayOrder and LibraryID = @LibID
 
 update vts_tbQuestion set DisplayOrder = @NewDisplayOrder 
	where QuestionID = @QuestionID 
 
 update vts_tbQuestion set DisplayOrder = @CurrentDisplayOrder 
	where QuestionID = @OldQuestionID 
  
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionResetDisplayOrder]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[vts_spQuestionResetDisplayOrder] @SurveyID int AS

DECLARE 	@DisplayOrder int,
		@QuestionID int

SET @DisplayOrder = 1

DECLARE QuestionsToUpdate CURSOR FOR
SELECT QuestionID FROM vts_tbQuestion 
WHERE SurveyID = @SurveyID AND ParentQuestionID is null ORDER BY PageNumber ASC, QuestionID 

OPEN QuestionsToUpdate

FETCH NEXT FROM QuestionsToUpdate into @QuestionID

WHILE @@FETCH_STATUS = 0
BEGIN
	UPDATE vts_tbQuestion SET DisplayOrder = @DisplayOrder WHERE QuestionID = @QuestionID
	UPDATE vts_tbQuestion SET DisplayOrder = @DisplayOrder WHERE ParentQuestionID = @QuestionID
	
	SET @DisplayOrder = @DisplayOrder + 1
	FETCH NEXT FROM QuestionsToUpdate into @QuestionID
END

CLOSE QuestionsToUpdate
DEALLOCATE QuestionsToUpdate



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionsAnswersGetForSurvey]    Script Date: 19-8-2014 22:01:40 ******/
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
/// get all questions with their answers
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey from
/// </param>
/// <returns>
/// A multiple resultset of questions and answers
/// </returns>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionsAnswersGetForSurvey] @SurveyID int
AS
SELECT 
	QuestionID,
	ParentQuestionID,
	SurveyID,
	QuestionText, 
	vts_tbQuestion.SelectionModeID,
	LayoutModeID,
	DisplayOrder,
	MinSelectionRequired,
	MaxSelectionAllowed,
	RatingEnabled
	TypeNameSpace,
	TypeAssembly
FROM vts_tbQuestion
INNER JOIN vts_tbQuestionSelectionMode 
	ON vts_tbQuestionSelectionMode.QuestionSelectionModeID = vts_tbQuestion.SelectionModeID
WHERE 
	SurveyID=@SurveyID 
ORDER BY DisplayOrder ASC
SELECT 
	AnswerID,
	vts_tbAnswer.AnswerTypeID,
	vts_tbQuestion.QuestionID,
	AnswerText,
	ImageURL, 
	(SELECT count(*) FROM vts_tbVoterAnswers INNER JOIN vts_tbVoter ON vts_tbVoter.VoterID = vts_tbVoterAnswers.VoterID WHERE  vts_tbVoter.Validated<>0 AND AnswerID = vts_tbAnswer.AnswerID) as VoteCount,
	FieldWidth,
	FieldHeight,
	FieldLength,
	TypeMode,
	XMLDatasource,
	DataSource,
	JavascriptCode,
	JavascriptFunctionName,
	JavascriptErrorMessage,
	RatePart,
	ScorePoint,
	DefaultText,
	AnswerPipeAlias,
	Mandatory,
	RegExpression,
	RegExMessage,
	vts_tbAnswer.RegularExpressionID
FROM vts_tbAnswer
INNER JOIN vts_tbAnswerType 
	ON vts_tbAnswer.AnswerTypeID = vts_tbAnswerType.AnswerTypeID
INNER JOIN vts_tbQuestion
	ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID
LEFT JOIN vts_tbRegularExpression
	ON vts_tbAnswer.RegularExpressionID = vts_tbRegularExpression.RegularExpressionID
WHERE SurveyID = @SurveyID
ORDER BY vts_tbAnswer.DisplayOrder



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionSectionGridAnswerAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// adds a new grid answer
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionSectionGridAnswerAddNew]
			@QuestionID int,
			@AnswerID int
			
AS

SELECT AnswerID FROM vts_tbQuestionSectionGridAnswer WHERE QuestionID = @QuestionID AND AnswerID = @AnswerID

IF @@RowCount = 0
BEGIN
	INSERT INTO vts_tbQuestionSectionGridAnswer(QuestionID, AnswerID) VALUES (@QuestionID, @AnswerID)
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionSectionGridAnswerDelete]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Delete a grid answer
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionSectionGridAnswerDelete]
			@QuestionID int,
			@AnswerID int
			
AS

DELETE FROM vts_tbQuestionSectionGridAnswer WHERE QuestionID = @QuestionID AND AnswerID = @AnswerID



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionSectionGridAnswerGet]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// returns all answers to be shown in the section grid
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionSectionGridAnswerGet]
			@QuestionID int
			
AS

SELECT AnswerID FROM vts_tbQuestionSectionGridAnswer WHERE QuestionID = @QuestionID



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionSectionOptionClone]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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

*/
CREATE PROCEDURE [dbo].[vts_spQuestionSectionOptionClone]
	@QuestionID int,
	@ClonedQuestionID int  
AS

BEGIN TRAN CloneSections

-- Clone the options
INSERT INTO vts_tbQuestionSectionOption
	(QuestionID,
	RepeatableSectionModeID,
	AddSectionLinkText,
	DeleteSectionLinkText,
	MaxSections,
	UpdateSectionLinkText,
	EditSectionLinkText)
SELECT      
	@ClonedQuestionID, 
	RepeatableSectionModeID,
	AddSectionLinkText,
	DeleteSectionLinkText,
	MaxSections,
	UpdateSectionLinkText,
	EditSectionLinkText
FROM vts_tbQuestionSectionOption WHERE QuestionID = @QuestionID

INSERT INTO vts_tbMultiLanguageText(LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText)
	SELECT @ClonedQuestionID as LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText
	FROM vts_tbMultiLanguageText
	WHERE LanguageItemID = @QuestionID AND LanguageMessageTypeID = 6

INSERT INTO vts_tbMultiLanguageText(LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText)
	SELECT @ClonedQuestionID as LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText
	FROM vts_tbMultiLanguageText
	WHERE LanguageItemID = @QuestionID AND LanguageMessageTypeID = 7

INSERT INTO vts_tbMultiLanguageText(LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText)
	SELECT @ClonedQuestionID as LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText
	FROM vts_tbMultiLanguageText
	WHERE LanguageItemID = @QuestionID AND LanguageMessageTypeID = 8

INSERT INTO vts_tbMultiLanguageText(LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText)
	SELECT @ClonedQuestionID as LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText
	FROM vts_tbMultiLanguageText
	WHERE LanguageItemID = @QuestionID AND LanguageMessageTypeID = 9

IF Exists(select AnswerID from vts_tbQuestionSectionGridAnswer where QuestionID = @QuestionID)
BEGIN
	-- Clone answers to be shown in grid section mode
	INSERT INTO vts_tbQuestionSectionGridAnswer
		(AnswerID,
		QuestionID)
	SELECT      
		AnswerID = 
		(select AnswerID from vts_tbAnswer WHERE QuestionID = @ClonedQuestionID AND 
			DisplayOrder = (select DisplayOrder FROM vts_tbAnswer WHERE AnswerID = A.AnswerID)),
		@ClonedQuestionID 
	FROM vts_tbQuestionSectionGridAnswer
	INNER JOIN vts_tbAnswer A
		ON vts_tbQuestionSectionGridAnswer.AnswerID = A.AnswerID
	WHERE vts_tbQuestionSectionGridAnswer.QuestionID = @QuestionID AND vts_tbQuestionSectionGridAnswer.AnswerID is not null
END

COMMIT TRAN CloneSections



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionSectionOptionDelete]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// deletes  the section options for questions
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionSectionOptionDelete] @QuestionID int  AS

-- Delete multi language texts
DELETE FROM vts_tbMultiLanguageText WHERE LanguageItemID = @QuestionID AND 
	(LanguageMessageTypeID = 6 OR LanguageMessageTypeID = 7 OR LanguageMessageTypeID = 8 OR LanguageMessageTypeID = 9)

DELETE FROM vts_tbQuestionSectionGridAnswer WHERE QuestionID = @QuestionID

DELETE FROM vts_tbQuestionSectionOption
WHERE QuestionID = @QuestionID



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionSectionOptionGetDetails]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Get the section options for questions
/// </summary>
/// <param Name="@QuestionID">
/// ID of the question to retrieve section options from
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionSectionOptionGetDetails] @QuestionID int, @LanguageCode NVARCHAR(50)  AS
SELECT 
	QuestionID,
	RepeatableSectionModeID,
	AddSectionLinkText = 
		CASE @LanguageCode 
		WHEN null THEN
			AddSectionLinkText 
		WHEN '' THEN
			AddSectionLinkText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestionSectionOption.QuestionID AND
			LanguageMessageTypeID = 6 AND
			LanguageCode = @LanguageCode), AddSectionLinkText)	
		END,
	DeleteSectionLinkText = 
		CASE @LanguageCode 
		WHEN null THEN
			DeleteSectionLinkText 
		WHEN '' THEN
			DeleteSectionLinkText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestionSectionOption.QuestionID AND
			LanguageMessageTypeID = 7 AND
			LanguageCode = @LanguageCode), DeleteSectionLinkText)	
		END,

	EditSectionLinkText = 
		CASE @LanguageCode 
		WHEN null THEN
			EditSectionLinkText 
		WHEN '' THEN
			EditSectionLinkText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestionSectionOption.QuestionID AND
			LanguageMessageTypeID = 9 AND
			LanguageCode = @LanguageCode), EditSectionLinkText)	
		END,
	UpdateSectionLinkText = 
		CASE @LanguageCode 
		WHEN null THEN
			UpdateSectionLinkText 
		WHEN '' THEN
			UpdateSectionLinkText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestionSectionOption.QuestionID AND
			LanguageMessageTypeID = 8 AND
			LanguageCode = @LanguageCode), UpdateSectionLinkText)
		END,
	MaxSections
FROM vts_tbQuestionSectionOption
WHERE QuestionID = @QuestionID



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionSectionOptionUpdate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Get the section options for questions
/// </summary>
/// <param Name="@QuestionID">
/// ID of the question to retrieve section options from
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionSectionOptionUpdate] 
						@QuestionID int, 
						@RepeatableSectionModeID int = 0,
						@AddSectionLinkText NVARCHAR(255),
						@DeleteSectionLinkText NVARCHAR(255),
						@EditSectionLinkText NVARCHAR(255),
						@UpdateSectionLinkText NVARCHAR(255),
						@MaxSections int,
						@LanguageCode NVARCHAR(50)


AS
UPDATE vts_tbQuestionSectionOption SET 
	RepeatableSectionModeID = @RepeatableSectionModeID,
	MaxSections = @MaxSections
WHERE QuestionID = @QuestionID

-- creates new options
IF @@RowCount = 0
BEGIN
	INSERT INTO vts_tbQuestionSectionOption (QuestionID, RepeatableSectionModeID, MaxSections)
	VALUES (@QuestionID, @RepeatableSectionModeID, @MaxSections)
END

-- Updates text
IF @LanguageCode is null OR @LanguageCode='' 
BEGIN
	UPDATE vts_tbQuestionSectionOption
	SET 	
		AddSectionLinkText = @AddSectionLinkText,
		DeleteSectionLinkText = @DeleteSectionLinkText, 
		EditSectionLinkText = @EditSectionLinkText,
		UpdateSectionLinkText = @UpdateSectionLinkText
	WHERE
		QuestionID = @QuestionID
END
ELSE
BEGIN
	-- Updates localized text
	exec vts_spMultiLanguageTextUpdate @QuestionID, @LanguageCode, 6, @AddSectionLinkText
	exec vts_spMultiLanguageTextUpdate @QuestionID, @LanguageCode, 7, @DeleteSectionLinkText
	exec vts_spMultiLanguageTextUpdate @QuestionID, @LanguageCode, 8, @UpdateSectionLinkText
	exec vts_spMultiLanguageTextUpdate @QuestionID, @LanguageCode, 9, @EditSectionLinkText
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionSelectionModeGetForSingle]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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

*/
CREATE PROCEDURE [dbo].[vts_spQuestionSelectionModeGetForSingle] AS
SELECT * FROM vts_tbQuestionSelectionMode



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionSelectionModeGetSelectable]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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

*/
CREATE PROCEDURE [dbo].[vts_spQuestionSelectionModeGetSelectable] AS
SELECT * FROM vts_tbQuestionSelectionMode WHERE TypeMode & 1 = 1



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionsGetForLibrary]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Get the questions until next page break
/// </summary>
/// <param Name="@LibraryID">
/// ID of the Library  to retrieve questions from
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionsGetForLibrary]
			@LibraryID int,@LanguageCode NVARCHAR(50)
AS
	SELECT 
	vts_tbQuestion.QuestionID,
	SurveyID,
	LibraryID,
	QuestionText = 
		CASE @LanguageCode 
		WHEN null THEN
			QuestionText 
		WHEN '' THEN
			QuestionText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestion.QuestionID AND
			LanguageMessageTypeID = 3 AND
			LanguageCode = @LanguageCode), QuestionText)		
		END,
	vts_tbQuestion.SelectionModeID,
	LayoutModeID,
	DisplayOrder,
	MinSelectionRequired,
	MaxSelectionAllowed,
	RandomizeAnswers,
	TypeNameSpace,
	TypeAssembly,
	TypeMode,
	PageNumber,
	ColumnsNumber,
	QuestionPipeAlias,
	(ISNULL(RepeatableSectionModeID, 0)) AS RepeatableSectionModeID,
	AddSectionLinkText = 
		CASE @LanguageCode 
		WHEN null THEN
			AddSectionLinkText 
		WHEN '' THEN
			AddSectionLinkText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestionSectionOption.QuestionID AND
			LanguageMessageTypeID = 6 AND
			LanguageCode = @LanguageCode), AddSectionLinkText)	
		END,
	DeleteSectionLinkText = 
		CASE @LanguageCode 
		WHEN null THEN
			DeleteSectionLinkText 
		WHEN '' THEN
			DeleteSectionLinkText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestionSectionOption.QuestionID AND
			LanguageMessageTypeID = 7 AND
			LanguageCode = @LanguageCode), DeleteSectionLinkText)	
		END,

	EditSectionLinkText = 
		CASE @LanguageCode 
		WHEN null THEN
			EditSectionLinkText 
		WHEN '' THEN
			EditSectionLinkText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestionSectionOption.QuestionID AND
			LanguageMessageTypeID = 9 AND
			LanguageCode = @LanguageCode), EditSectionLinkText)	
		END,
	UpdateSectionLinkText = 
		CASE @LanguageCode 
		WHEN null THEN
			UpdateSectionLinkText 
		WHEN '' THEN
			UpdateSectionLinkText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestionSectionOption.QuestionID AND
			LanguageMessageTypeID = 8 AND
			LanguageCode = @LanguageCode), UpdateSectionLinkText)
		END,
		HelpText = 
		CASE @LanguageCode 
		WHEN null THEN
			HelpText 
		WHEN '' THEN
			HelpText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestion.QuestionID AND
			LanguageMessageTypeID = 11 AND
			LanguageCode = @LanguageCode), HelpText)		
		END,
	ShowHelpText,
	Alias= 
		CASE @LanguageCode 
		WHEN null THEN
			Alias 
		WHEN '' THEN
			Alias
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestion.QuestionID AND
			LanguageMessageTypeID = 12 AND
			LanguageCode = @LanguageCode), Alias)		
		END,
	(ISNULL(MaxSections, 0)) as MaxSections, 
	QuestionIDText
	FROM vts_tbQuestion
	LEFT JOIN vts_tbQuestionSectionOption
		ON vts_tbQuestionSectionOption.QuestionID = vts_tbQuestion.QuestionID
	INNER JOIN vts_tbQuestionSelectionMode 
		ON vts_tbQuestionSelectionMode.QuestionSelectionModeID = vts_tbQuestion.SelectionModeID
	WHERE 
		LibraryID=@LibraryID AND ParentQuestionID is null
	ORDER BY vts_tbQuestion.DisplayOrder



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionsGetForSurvey]    Script Date: 19-8-2014 22:01:40 ******/
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
/// get all questions for a survey
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey  to retrieve questions
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionsGetForSurvey] @SurveyID int, @LanguageCode NVARCHAR(50)  AS
SELECT 
	vts_tbQuestion.QuestionID,
	SurveyID,
	LibraryID,
	QuestionText = 
		CASE @LanguageCode 
		WHEN null THEN
			QuestionText 
		WHEN '' THEN
			QuestionText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestion.QuestionID AND
			LanguageMessageTypeID = 3 AND
			LanguageCode = @LanguageCode), QuestionText)		
		END,
	vts_tbQuestion.SelectionModeID,
	LayoutModeID,
	DisplayOrder,
	MinSelectionRequired,
	MaxSelectionAllowed,
	RandomizeAnswers,
	TypeNameSpace,
	TypeAssembly,
	TypeMode,
	PageNumber,
	ColumnsNumber,
	QuestionPipeAlias,
	(ISNULL(RepeatableSectionModeID, 0)) AS RepeatableSectionModeID,
	AddSectionLinkText = 
		CASE @LanguageCode 
		WHEN null THEN
			AddSectionLinkText 
		WHEN '' THEN
			AddSectionLinkText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestionSectionOption.QuestionID AND
			LanguageMessageTypeID = 6 AND
			LanguageCode = @LanguageCode), AddSectionLinkText)	
		END,
	DeleteSectionLinkText = 
		CASE @LanguageCode 
		WHEN null THEN
			DeleteSectionLinkText 
		WHEN '' THEN
			DeleteSectionLinkText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestionSectionOption.QuestionID AND
			LanguageMessageTypeID = 7 AND
			LanguageCode = @LanguageCode), DeleteSectionLinkText)	
		END,

	EditSectionLinkText = 
		CASE @LanguageCode 
		WHEN null THEN
			EditSectionLinkText 
		WHEN '' THEN
			EditSectionLinkText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestionSectionOption.QuestionID AND
			LanguageMessageTypeID = 9 AND
			LanguageCode = @LanguageCode), EditSectionLinkText)	
		END,
	UpdateSectionLinkText = 
		CASE @LanguageCode 
		WHEN null THEN
			UpdateSectionLinkText 
		WHEN '' THEN
			UpdateSectionLinkText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestionSectionOption.QuestionID AND
			LanguageMessageTypeID = 8 AND
			LanguageCode = @LanguageCode), UpdateSectionLinkText)
		END,
	(ISNULL(MaxSections, 0)) as MaxSections, 
	QuestionIDText,
	Alias = 
		CASE @LanguageCode 
		WHEN null THEN
			Alias 
		WHEN '' THEN
			Alias
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestion.QuestionID AND
			LanguageMessageTypeID = 12 AND
			LanguageCode = @LanguageCode), Alias)
		END,
		HelpText = 
		CASE @LanguageCode 
		WHEN null THEN
			HelpText 
		WHEN '' THEN
			HelpText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestion.QuestionID AND
			LanguageMessageTypeID = 11 AND
			LanguageCode = @LanguageCode), HelpText)
		END,
		ShowHelpText
FROM vts_tbQuestion
LEFT JOIN vts_tbQuestionSectionOption
		ON vts_tbQuestionSectionOption.QuestionID = vts_tbQuestion.QuestionID
INNER JOIN vts_tbQuestionSelectionMode 
	ON vts_tbQuestionSelectionMode.QuestionSelectionModeID = vts_tbQuestion.SelectionModeID
WHERE 
	SurveyID=@SurveyID AND
	ParentQuestionID is null
ORDER BY PageNumber, DisplayOrder ASC



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionsGetListForLibrary]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Get a list of all question templates that are available in the given Library
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionsGetListForLibrary] @LibraryID int  AS

SELECT 
QuestionID,
 QuestionText,
 HelpText,
 QuestionIDText
FROM vts_tbQuestion 
WHERE 
	LibraryID = @LibraryID



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionsGetListForLibraryWithoutChilds]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Get a list of all question templates that are available in the given Library
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionsGetListForLibraryWithoutChilds] @LibraryID int  AS

SELECT QuestionID, QuestionText
FROM vts_tbQuestion 
WHERE 
	LibraryID = @LibraryID AND 
	ParentQuestionID is null



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionsGetMatrixChilds]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// get all child questions for a matrix questionl
/// </summary>
/// <param Name="@QuestionID">
/// ID of the question to retrieve child questions from
/// </param>
/// <returns>
/// A multiple resultset of questions and answers
/// </returns>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionsGetMatrixChilds] @ParentQuestionID int, @LanguageCode NVARCHAR(50)  AS
SELECT
	SurveyID,
	QuestionID, 
	QuestionText = 
		CASE @LanguageCode 
		WHEN null THEN
			QuestionText 
		WHEN '' THEN
			QuestionText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestion.QuestionID AND
			LanguageMessageTypeID = 3 AND
			LanguageCode = @LanguageCode), QuestionText)		
		END,
	SelectionModeID,
	LayoutModeID,
	DisplayOrder,
	QuestionPipeAlias
FROM vts_tbQuestion
WHERE 
	ParentQuestionID=@ParentQuestionID
ORDER BY DisplayOrder
SELECT 
	AnswerID,
	vts_tbAnswer.QuestionID,
	AnswerText = 
		CASE @LanguageCode
		WHEN null THEN
			AnswerText 
		WHEN '' THEN
			AnswerText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbAnswer.AnswerID AND
			LanguageMessageTypeID = 1 AND
			LanguageCode = @LanguageCode), AnswerText)		
		END,
	ImageURL,
	(SELECT count(*) FROM vts_tbVoterAnswers WHERE AnswerID = vts_tbAnswer.AnswerID) as VoteCount,
	FieldWidth,
	FieldHeight,
	FieldLength,
	TypeMode,
	XMLDatasource,
	DataSource,
	TypeNameSpace,
	TypeAssembly,
	TypeMode,	
	JavascriptCode,
	JavascriptFunctionName,
	JavascriptErrorMessage,
	Selected,
	DefaultText = 
		CASE @LanguageCode 
		WHEN null THEN
			DefaultText 
		WHEN '' THEN
			DefaultText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbAnswer.AnswerID AND
			LanguageMessageTypeID = 2 AND
			LanguageCode = @LanguageCode), null)		
		END,
	Mandatory
FROM vts_tbAnswer
INNER JOIN vts_tbQuestion 
	ON vts_tbAnswer.QuestionID = vts_tbQuestion.QuestionID
INNER JOIN vts_tbAnswerType 
	ON vts_tbAnswer.AnswerTypeID = vts_tbAnswerType.AnswerTypeID
WHERE 
	vts_tbQuestion.ParentQuestionID = @ParentQuestionID
ORDER BY AnswerID;



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionsGetPagedForSurvey]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Get the questions until next page break
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey  to retrieve questions
/// </param>
/// <param Name="@LastDisplayOrder">
/// Question at which we whish to stop
/// </param>
/// <returns>
/// 	QuestionID, 
/// 	QuestionText, 
/// 	QuestionLayoutID,
///	LayoutModeID,
///	DisplayOrder
///	TypeNameSpace,
///	TypeAssembly
/// </returns>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionsGetPagedForSurvey]
			@SurveyID int,
			@PageNumber  int = 1,
			@LanguageCode NVARCHAR(50)
AS
	SELECT 
		vts_tbQuestion.QuestionID,
		SurveyID,
		LibraryID,
		QuestionText = 
		CASE @LanguageCode 
		WHEN null THEN
			QuestionText 
		WHEN '' THEN
			QuestionText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestion.QuestionID AND
			LanguageMessageTypeID = 3 AND
			LanguageCode = @LanguageCode), QuestionText)		
		END,
		vts_tbQuestion.SelectionModeID,
		LayoutModeID,
		DisplayOrder,
		MinSelectionRequired,
		MaxSelectionAllowed,
		RandomizeAnswers,
		TypeNameSpace,
		TypeAssembly,
		TypeMode,
		ColumnsNumber,
		QuestionPipeAlias,
		(ISNULL(RepeatableSectionModeID, 0)) as RepeatableSectionModeID,
	AddSectionLinkText = 
		CASE @LanguageCode 
		WHEN null THEN
			AddSectionLinkText 
		WHEN '' THEN
			AddSectionLinkText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestionSectionOption.QuestionID AND
			LanguageMessageTypeID = 6 AND
			LanguageCode = @LanguageCode), AddSectionLinkText)	
		END,
	DeleteSectionLinkText = 
		CASE @LanguageCode 
		WHEN null THEN
			DeleteSectionLinkText 
		WHEN '' THEN
			DeleteSectionLinkText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestionSectionOption.QuestionID AND
			LanguageMessageTypeID = 7 AND
			LanguageCode = @LanguageCode), DeleteSectionLinkText)	
		END,

	EditSectionLinkText = 
		CASE @LanguageCode 
		WHEN null THEN
			EditSectionLinkText 
		WHEN '' THEN
			EditSectionLinkText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestionSectionOption.QuestionID AND
			LanguageMessageTypeID = 9 AND
			LanguageCode = @LanguageCode), EditSectionLinkText)	
		END,
	UpdateSectionLinkText = 
		CASE @LanguageCode 
		WHEN null THEN
			UpdateSectionLinkText 
		WHEN '' THEN
			UpdateSectionLinkText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestionSectionOption.QuestionID AND
			LanguageMessageTypeID = 8 AND
			LanguageCode = @LanguageCode), UpdateSectionLinkText)
		END,
		(ISNULL(MaxSections, 0)) as MaxSections,
		HelpText = 
		CASE @LanguageCode 
		WHEN null THEN
			HelpText 
		WHEN '' THEN
			HelpText 
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = vts_tbQuestion.QuestionID AND
			LanguageMessageTypeID = 11 AND
			LanguageCode = @LanguageCode), HelpText)
		END,
		ShowHelpText
	FROM vts_tbQuestion
	LEFT JOIN vts_tbQuestionSectionOption
		ON vts_tbQuestionSectionOption.QuestionID = vts_tbQuestion.QuestionID
	INNER JOIN vts_tbQuestionSelectionMode 
		ON vts_tbQuestionSelectionMode.QuestionSelectionModeID = vts_tbQuestion.SelectionModeID
	WHERE 
		SurveyID=@SurveyID AND
		PageNumber = @PageNumber AND
		ParentQuestionID is null
	ORDER BY DisplayOrder



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionsGetPageRangeForSurvey]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Get a list of all questions in the given page range for a survey
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey  to retrieve questions
/// </param>
/// <returns>
///	QuestionID,
///	QuestionText, 
/// </returns>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionsGetPageRangeForSurvey] 
			@SurveyID int,
			@StartPage int,
			@EndPage int
AS

SELECT 
	QuestionID,
	QuestionText 
FROM vts_tbQuestion
WHERE 
	SurveyID=@SurveyID AND PageNumber BETWEEN @StartPage AND @EndPage



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionSingleAnswerableListWithoutChilds]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Get a list of all parent questions that can have any type of answers, except those that have child questions 
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey  to retrieve questions
/// </param>
/// <returns>
///	QuestionID,
///	SurveyID
///	QuestionText, 
/// </returns>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionSingleAnswerableListWithoutChilds] @SurveyID int  AS
SELECT DISTINCT Q.QuestionID, Q.QuestionText, Q.DisplayOrder 
FROM vts_tbQuestion Q
INNER JOIN vts_tbQuestionSelectionMode
	ON  QuestionSelectionModeID= Q.SelectionModeID
WHERE 
	SurveyID = @SurveyID AND 
	TypeMode & 4 > 1 AND 
	ParentQuestionID is null
	AND QuestionID NOT IN (SELECT ParentQuestionID FROM vts_tbQuestion WHERE ParentQuestionID = Q.QuestionID)
ORDER BY Q.DisplayOrder, Q.QuestionID



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionSkipLogicRuleAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Adds a new skip rule to the question
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionSkipLogicRuleAddNew] 	
			@SkipQuestionID int,
			@QuestionID int,
			@AnswerID int,
			@TextFilter NVARCHAR(4000),
			@ConditionalOperator int,
			@ExpressionOperator int,
			@Score	int,
			@ScoreMax int,
			@SkipLogicRuleID int OUTPUT
AS
INSERT INTO vts_tbSkipLogicRule
	(SkipQuestionID,
	QuestionID,
	AnswerID,
	ConditionalOperator,
	ExpressionOperator,
	TextFilter,
	Score,
	ScoreMax)
VALUES
	(@SkipQuestionID,
	@QuestionID,
	@AnswerID,
	@ConditionalOperator,
	@ExpressionOperator,
	@TextFilter,
	@Score,
	@ScoreMax)
set @SkipLogicRuleID = SCOPE_IDENTITY()



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionSkipLogicRuleDeleteByID]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Deletes the given rule
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionSkipLogicRuleDeleteByID] @SkipLogicRuleID int AS
DELETE FROM vts_tbSkipLogicRule WHERE SkipLogicRuleID = @SkipLogicRuleID



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionSkipLogicRuleGetAll]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Retrieve all skip rules for the given question
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionSkipLogicRuleGetAll] @SkipQuestionID int AS

SELECT vts_tbSkipLogicRule.*,  AnswerText, QuestionText
FROM vts_tbSkipLogicRule 
INNER JOIN vts_tbQuestion 	
	ON vts_tbQuestion.QuestionID = vts_tbSkipLogicRule.QuestionID
LEFT JOIN vts_tbAnswer 
	ON vts_tbAnswer.AnswerID = vts_tbSkipLogicRule.AnswerID
WHERE SkipQuestionID  = @SkipQuestionID
ORDER BY SkipLogicRuleID



GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionUpdate]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Updates a question
/// </summary>
/// <param Name="QuestionID">
/// ID of the question to update
/// </param>
/// <param Name="@QuestionText">
/// Question's text
/// </param>
/// <param Name="@SelectionModeID">
/// selection type of the question (checkbox, radio, matrix ..)
/// </param>
/// <param Name="@LayoutModeID">
/// Layout of the question (horizontal, vertical ..)
/// </param>
/// <param Name="@MinSelectionRequired">
/// Number of selection that question requires
/// </param>
/// <param Name="@MaxSelectionRequired">
/// Number of max selection that question allows
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionUpdate] 
			@QuestionID int, 
			@QuestionText NVARCHAR(4000),
			@SelectionModeID int,
			@LayoutModeID int,
			@ColumnsNumber int,
			@MinSelectionRequired int,
			@MaxSelectionAllowed int,
			@RandomizeAnswers bit,
			@RatingEnabled bit,
			@QuestionPipeAlias NVARCHAR(255),
			@LanguageCode NVARCHAR(50) = null,
			@QuestionGroupID int, 
			@ShowHelpText	bit,
			@HelpText	NVARCHAR(4000),
			@Alias	NVARCHAR(255),
			@QuestionIDText NVARCHAR(255)
AS
BEGIN TRAN UpdateQuestion

UPDATE vts_tbQuestion  
SET 	SelectionModeID=@SelectionModeID,
	LayoutModeID = @LayoutModeID,
	MinSelectionRequired = @MinSelectionRequired,
	MaxSelectionAllowed = @MaxSelectionAllowed,
	RandomizeAnswers = @RandomizeAnswers,
	RatingEnabled = @RatingEnabled,
	ColumnsNumber = @ColumnsNumber,
	QuestionPipeAlias = @QuestionPipeAlias,
	QuestionGroupID =case when  @QuestionGroupID=-1 then null else @QuestionGroupID end,
	ShowHelpText = @ShowHelpText,
	QuestionIDText=@QuestionIDText
WHERE
	QuestionID = @QuestionID

-- Updates Child question's options
UPDATE vts_tbQuestion  
SET 	SelectionModeID=@SelectionModeID,
	LayoutModeID = @LayoutModeID,
	MinSelectionRequired = @MinSelectionRequired,
	MaxSelectionAllowed = @MaxSelectionAllowed,
	RandomizeAnswers = @RandomizeAnswers,
	RatingEnabled = @RatingEnabled
WHERE
	ParentQuestionID = @QuestionID

-- Updates text
IF @LanguageCode is null OR @LanguageCode='' 
BEGIN
	UPDATE vts_tbQuestion
	SET 	QuestionText = @QuestionText,
		    HelpText =     @HelpText,
	        Alias =        @Alias
	WHERE
		QuestionID = @QuestionID
END
ELSE
BEGIN
	-- Updates localized text
	exec vts_spMultiLanguageTextUpdate @QuestionID, @LanguageCode, 3, @QuestionText
    exec vts_spMultiLanguageTextUpdate @QuestionID, @LanguageCode, 11, @HelpText
	exec vts_spMultiLanguageTextUpdate @QuestionID, @LanguageCode, 12, @Alias
	
END

COMMIT TRAN UpdateQuestion



GO
/****** Object:  StoredProcedure [dbo].[vts_spRegularExpressionAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Adds a new regular expression
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spRegularExpressionAddNew]
			@UserID int,
			@Description varchar(255) = null,
			@RegExpression varchar(2000) = null, 
			@RegExMessage varchar(2000) = null,
			@RegularExpressionID int OUTPUT
AS

SELECT 
	@RegularExpressionID = RegularExpressionID
FROM vts_tbRegularExpression
WHERE
	RegExpression = @RegExpression AND
	Description = @Description

if @@RowCount = 0
BEGIN
	INSERT INTO vts_tbRegularExpression
		(Description,
		RegExpression, 
		RegExMessage)
	VALUES
		(@Description,
		@RegExpression, 
		@RegExMessage)

	set @RegularExpressionID = SCOPE_IDENTITY()
	IF @UserID > 0
	BEGIN
		exec vts_spUserRegularExpressionAssignUser @RegularExpressionID, @UserID
	END
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spRegularExpressionDelete]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Deletes the given regex
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spRegularExpressionDelete] @RegularExpressionID int AS
DELETE FROM vts_tbRegularExpression WHERE RegularExpressionID = @RegularExpressionID



GO
/****** Object:  StoredProcedure [dbo].[vts_spRegularExpressionGetDetails]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Retrieves all the infos of the given regular expression  id
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spRegularExpressionGetDetails] @RegularExpressionID int AS
SELECT 
	RegularExpressionID,
	Description, 
	RegExMessage,
	RegExpression,
	BuiltIn
 FROM vts_tbRegularExpression 
 WHERE RegularExpressionID = @RegularExpressionID



GO
/****** Object:  StoredProcedure [dbo].[vts_spRegularExpressionGetEditableListForUser]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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

*/
CREATE PROCEDURE [dbo].[vts_spRegularExpressionGetEditableListForUser] @UserID int AS
SELECT 
	vts_tbRegularExpression.RegularExpressionID,
	vts_tbRegularExpression.Description
 FROM vts_tbRegularExpression 
INNER JOIN vts_tbUserRegularExpression
	ON vts_tbUserRegularExpression.RegularExpressionID = vts_tbRegularExpression.RegularExpressionID
WHERE vts_tbUserRegularExpression.UserID = @UserID
ORDER BY Description



GO
/****** Object:  StoredProcedure [dbo].[vts_spRegularExpressionGetList]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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

*/
CREATE PROCEDURE [dbo].[vts_spRegularExpressionGetList] AS
SELECT 
	RegularExpressionID,
	Description
 FROM vts_tbRegularExpression 
ORDER BY Description



GO
/****** Object:  StoredProcedure [dbo].[vts_spRegularExpressionGetListForUser]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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

*/
CREATE PROCEDURE [dbo].[vts_spRegularExpressionGetListForUser] @UserID int, @SurveyID int
AS

SELECT 
	DISTINCT vts_tbRegularExpression.RegularExpressionID,
	vts_tbRegularExpression.Description
FROM vts_tbRegularExpression 
LEFT JOIN vts_tbUserRegularExpression
	ON vts_tbUserRegularExpression.RegularExpressionID = vts_tbRegularExpression.RegularExpressionID
WHERE vts_tbUserRegularExpression.UserID = @UserID OR 
	BuiltIn<>0 OR 
	vts_tbRegularExpression.RegularExpressionID in 
		(SELECT RegularExpressionID 
		FROM vts_tbAnswer 
		INNER JOIN vts_tbQuestion 
			ON vts_tbAnswer.QuestionID = vts_tbQuestion.QuestionID 
		WHERE SurveyID = @SurveyID)
ORDER BY Description



GO
/****** Object:  StoredProcedure [dbo].[vts_spRegularExpressionSetBuiltIn]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Set  answer type to be built in
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spRegularExpressionSetBuiltIn] @RegularExpressionID int AS

BEGIN TRAN SetBuiltIn

DELETE FROM vts_tbUserRegularExpression WHERE RegularExpressionID = @RegularExpressionID

UPDATE vts_tbRegularExpression SET BuiltIn = 1 WHERE RegularExpressionID = @RegularExpressionID

COMMIT TRAN SetBuiltIn



GO
/****** Object:  StoredProcedure [dbo].[vts_spRegularExpressionUpdate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Updates the regular expression data
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spRegularExpressionUpdate]
			@RegularExpressionID int, 
			@Description varchar(255), 
			@RegExpression varchar(2000),
			@RegExMessage NVARCHAR(2000)		
AS
UPDATE vts_tbRegularExpression SET
	Description = @Description, 
	RegExpression = @RegExpression,
	RegExMessage = @RegExMessage
WHERE RegularExpressionID = @RegularExpressionID



GO
/****** Object:  StoredProcedure [dbo].[vts_spResumeModeGetAll]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spResumeModeGetAll] AS
SELECT * FROM vts_tbResumeMode



GO
/****** Object:  StoredProcedure [dbo].[vts_spRoleAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// adds a new role
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spRoleAddNew]
			@RoleID int out,
			@RoleName NVARCHAR(255)
			
AS

INSERT INTO vts_tbRole (RoleName)
VALUES (@RoleName)

SELECT @RoleID = SCOPE_IDENTITY()



GO
/****** Object:  StoredProcedure [dbo].[vts_spRoleDelete]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// get all details of the roles
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spRoleDelete] @RoleID int
AS

DELETE FROM vts_tbRoleSecurityRight WHERE RoleID = @RoleID
DELETE FROM vts_tbUserRole WHERE RoleID = @RoleID
DELETE FROM vts_tbRole WHERE RoleID = @RoleID



GO
/****** Object:  StoredProcedure [dbo].[vts_spRoleGetDetails]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// get all details of the roles
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spRoleGetDetails] @RoleID int
AS

SELECT RoleID, RoleName FROM vts_tbRole WHERE RoleID = @RoleID

SELECT SecurityRightID  FROM vts_tbRoleSecurityRight WHERE RoleID = @RoleID



GO
/****** Object:  StoredProcedure [dbo].[vts_spRoleGetList]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Get a list of all roles
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spRoleGetList]
AS

SELECT RoleID, RoleName FROM vts_tbRole



GO
/****** Object:  StoredProcedure [dbo].[vts_spRoleSecurityRightAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// adds a new role
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spRoleSecurityRightAddNew]
			@RoleID int,
			@SecurityRightID int
			
AS

SELECT RoleID FROM vts_tbRoleSecurityRight WHERE RoleID = @RoleID AND SecurityRightID = @SecurityRightID

IF @@RowCount = 0
BEGIN
	INSERT INTO vts_tbRoleSecurityRight(RoleID, SecurityRightID) VALUES (@RoleID, @SecurityRightID)
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spRoleSecurityRightDeleteAll]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Deletes all associated security rights of a role
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spRoleSecurityRightDeleteAll] @RoleID int
AS

DELETE FROM vts_tbRoleSecurityRight WHERE RoleID = @RoleID



GO
/****** Object:  StoredProcedure [dbo].[vts_spRoleUpdate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// adds a new role
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spRoleUpdate]
			@RoleID int,
			@RoleName NVARCHAR(255)
			
AS

UPDATE vts_tbRole SET RoleName = @RoleName WHERE RoleID = @RoleID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSecurityRightGetList]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Get a list of all security rights
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSecurityRightGetList]
AS

SELECT SecurityRightID, Description FROM vts_tbSecurityRight



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyAccessPasswordGet]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spSurveyAccessPasswordGet] @SurveyID int AS
SELECT AccessPassword FROM vts_tbSurvey WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyAccessPasswordUpdate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spSurveyAccessPasswordUpdate] @SurveyID int, @AccessPassword NVARCHAR(255)
AS
UPDATE vts_tbSurvey  SET AccessPassword = @AccessPassword
WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyAddNew]    Script Date: 19-8-2014 22:01:40 ******/
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
CREATE PROCEDURE [dbo].[vts_spSurveyAddNew] 
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
	ThankYouMessage) 
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
	@ThankYouMessage)
	
SET @SurveyID = SCOPE_IDENTITY()



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyAllowMultipleASPNetVotes]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spSurveyAllowMultipleASPNetVotes] @SurveyID int AS
SELECT AllowMultipleUserNameSubmissions FROM vts_tbSurvey WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyAllowMultipleNSurveyVotes]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spSurveyAllowMultipleNSurveyVotes] @SurveyID int AS
SELECT AllowMultipleNSurveySubmissions FROM vts_tbSurvey WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyBranchingRuleAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Adds a new branching rule to a survey page
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyBranchingRuleAddNew] 
			@PageNumber int,
			@ExpressionOperator int,
			@QuestionID int,
			@AnswerID int,
			@TextFilter NVARCHAR(4000),
			@TargetPageNumber int,
			@ConditionalOperator int,
			@Score	int,
			@ScoreMax int,
			@BranchingRuleID int OUTPUT
AS
INSERT INTO vts_tbBranchingRule
	(PageNumber,
	ExpressionOperator,
	QuestionID,
	AnswerID,
	TargetPageNumber,
	ConditionalOperator,
	TextFilter,
	Score,
	ScoreMax)
VALUES
	(@PageNumber,
	@ExpressionOperator,
	@QuestionID,
	@AnswerID,
	@TargetPageNumber,
	@ConditionalOperator,
	@TextFilter,
	@Score,
	@ScoreMax)
set @BranchingRuleID = SCOPE_IDENTITY()



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyBranchingRuleCopyToSurvey]    Script Date: 19-8-2014 22:01:40 ******/
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
/// clone all branchings of a survey to another
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyBranchingRuleCopyToSurvey] @SurveyID int, @ClonedSurveyID int AS


DECLARE @ConditionalOperator int,
	@ExpressionOperator int,
	@PageNumber int,
	@TargetPageNumber int,
	@AnswerID int,
	@ClonedAnswerID int,
	@QuestionID int,
	@ClonedQuestionID int,
	@TextFilter varchar(4000),
	@Score int,
	@ScoreMax int


DECLARE BranchingCursor CURSOR FOR
	SELECT 
		ConditionalOperator,
		ExpressionOperator,
		vts_tbBranchingRule.PageNumber,
		TargetPageNumber,
		AnswerID,
		vts_tbBranchingRule.QuestionID,
		TextFilter,
		Score,
		ScoreMax
	FROM vts_tbBranchingRule
	INNER JOIN vts_tbQuestion
		ON vts_tbQuestion.QuestionID = vts_tbBranchingRule.QuestionID
	WHERE SurveyID = @SurveyID


OPEN BranchingCursor
FETCH BranchingCursor INTO 
	@ConditionalOperator,
	@ExpressionOperator,
	@PageNumber,
	@TargetPageNumber,
	@AnswerID,
	@QuestionID,
	@TextFilter,
	@Score,
	@ScoreMax
WHILE @@FETCH_STATUS = 0
BEGIN

	-- Get question id that is in the cloned survey
	SELECT @ClonedQuestionID =	(select QuestionID  from vts_tbQuestion WHERE SurveyID = @ClonedSurveyID AND QuestionText = (select QuestionText FROM vts_tbQuestion WHERE QuestionID = @QuestionID) AND 
				DisplayOrder = (select DisplayOrder FROM vts_tbQuestion WHERE QuestionID = @QuestionID)) 
	
	IF @AnswerID is not NULL
	BEGIN
		-- Get answer id from the cloned survey
		SELECT @ClonedAnswerID = 
				(select AnswerID from vts_tbAnswer WHERE QuestionID = @ClonedQuestionID AND 
				DisplayOrder = (select DisplayOrder FROM vts_tbAnswer WHERE AnswerID = @AnswerID))
	END
	ELSE
	BEGIN
		SET @ClonedAnswerID = null
	END

	INSERT INTO vts_tbBranchingRule(ConditionalOperator, ExpressionOperator, PageNumber, TargetPageNumber, AnswerID, QuestionID, TextFilter, Score, ScoreMax)
	VALUES (@ConditionalOperator, @ExpressionOperator, @PageNumber, @TargetPageNumber, @ClonedAnswerID, @ClonedQuestionID, @TextFilter, @Score, @ScoreMax)

	FETCH BranchingCursor INTO 
		@ConditionalOperator,
		@ExpressionOperator,
		@PageNumber,
		@TargetPageNumber,
		@AnswerID,
		@QuestionID,
		@TextFilter,
		@Score,
		@ScoreMax
END

CLOSE BranchingCursor 
DEALLOCATE BranchingCursor



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyBranchingRuleDeleteByID]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Deletes the given rule
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyBranchingRuleDeleteByID] @BranchingRuleID int AS
DELETE FROM vts_tbBranchingRule WHERE BranchingRuleID = @BranchingRuleID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyBranchingRuleGetAll]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Retrieve all branching rule for the given survey
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyBranchingRuleGetAll] @SurveyID int AS
SELECT vts_tbBranchingRule.*,  AnswerText, QuestionText
FROM vts_tbBranchingRule 
INNER JOIN vts_tbQuestion 	
	ON vts_tbQuestion.QuestionID = vts_tbBranchingRule.QuestionID
LEFT JOIN vts_tbAnswer 
	ON vts_tbAnswer.AnswerID = vts_tbBranchingRule.AnswerID
WHERE vts_tbQuestion.SurveyID = @SurveyID
ORDER BY BranchingRuleID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyBranchingRuleGetDetailsForPage]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Retrieve all branching rule for the given survey
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyBranchingRuleGetDetailsForPage] @SurveyID int, @PageNumber int AS
SELECT vts_tbBranchingRule.*,  AnswerText, QuestionText
FROM vts_tbBranchingRule 
INNER JOIN vts_tbQuestion 	
	ON vts_tbQuestion.QuestionID = vts_tbBranchingRule.QuestionID
LEFT JOIN vts_tbAnswer 
	ON vts_tbAnswer.AnswerID = vts_tbBranchingRule.AnswerID
WHERE
	vts_tbQuestion.SurveyID = @SurveyID AND 
	vts_tbBranchingRule.PageNumber = @PageNumber
ORDER BY BranchingRuleID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyBranchingRuleGetForPage]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Retrieve all branching rule for the given page of the survey
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyBranchingRuleGetForPage] @SurveyID int, @PageNumber int AS
SELECT * FROM vts_tbBranchingRule 
INNER JOIN vts_tbQuestion
	ON vts_tbQuestion.QuestionID = vts_tbBranchingRule.QuestionID
WHERE vts_tbQuestion.SurveyID = @SurveyID AND vts_tbBranchingRule.PageNumber = @PageNumber
ORDER BY BranchingRuleID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyCheckUserAssigned]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Check if the survey is assigned to this user
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyCheckUserAssigned] @SurveyID int, @UserID int AS
SELECT SurveyID FROM vts_tbUserSurvey WHERE SurveyID = @SurveyID AND UserID = @UserID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyClone]    Script Date: 19-8-2014 22:01:40 ******/
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
*/
CREATE PROCEDURE [dbo].[vts_spSurveyClone] @SurveyID int 
AS
BEGIN TRANSACTION CloneSurvey
DECLARE @ClonedSurveyID int
-- Clone the survey
INSERT INTO vts_tbSurvey 
	(CreationDate, 
	OpenDate,
	CloseDate,
	Title,
	RedirectionURL,
	ThankYouMessage,
	AccessPassword,
	SurveyDisplayTimes,
	ResultsDisplayTimes,
	Archive,
	Activated,
	IPExpires,
	CookieExpires,
	OnlyInvited,
	NavigationEnabled,
	ProgressDisplayModeID,
	NotificationModeID,
	ResumeModeID,
	Scored,
	AllowMultipleUserNameSubmissions,
	QuestionNumberingDisabled,
	AllowMultipleNSurveySubmissions,
	MultiLanguageModeID,
	MultiLanguageVariable,
	FolderID)
SELECT      
	getdate(), 
	OpenDate,
	CloseDate,
	Title + ' - cloned',
	RedirectionURL,
	ThankYouMessage,
	AccessPassword,
	0,
	0,
	Archive,
	0,
	IPExpires,
	CookieExpires,
	OnlyInvited,
	NavigationEnabled,
	ProgressDisplayModeID,
	NotificationModeID,
	ResumeModeID,
	Scored,
	AllowMultipleUserNameSubmissions,
	QuestionNumberingDisabled,
	AllowMultipleNSurveySubmissions,
	MultiLanguageModeID,
	MultiLanguageVariable,
    FolderID
FROM vts_tbSurvey WHERE SurveyID = @SurveyID
-- Check if the cloned survey was created
IF @@RowCount <> 0
BEGIN
	-- Clone the survey's questions
	set @ClonedSurveyID = SCOPE_IDENTITY()
	INSERT INTO vts_tbEmailNotificationSettings(SurveyID, EmailFrom, EmailTo, EmailSubject) 
		SELECT @ClonedSurveyID as SurveyID,EmailFrom, Emailto, EmailSubject 
		FROM vts_tbEmailNotificationSettings 
		WHERE SurveyID = @SurveyID
	
	INSERT INTO vts_tbSurveyWebSecurity(WebSecurityAddInID, SurveyID, AddInOrder, Disabled)
		SELECT WebSecurityAddInID, @ClonedSurveyID as SurveyID, AddInOrder, Disabled
		FROM vts_tbSurveyWebSecurity
		WHERE SurveyID = @SurveyID
	
	INSERT INTO vts_tbPageOption (SurveyID, PageNumber, RandomizeQuestions, EnableSubmitButton)
		SELECT @ClonedSurveyID as SurveyID, PageNumber, RandomizeQuestions, EnableSubmitButton
		FROM vts_tbPageOption
		WHERE SurveyID = @SurveyID
	
	INSERT INTO vts_tbSurveyLanguage(SurveyID, LanguageCode, DefaultLanguage)
		SELECT @ClonedSurveyID as SurveyID, LanguageCode, DefaultLanguage
		FROM vts_tbSurveyLanguage
		WHERE SurveyID = @SurveyID

	-- Clone multi languages messages
	INSERT INTO vts_tbMultiLanguageText(LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText)
		SELECT @ClonedSurveyID as LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText
		FROM vts_tbMultiLanguageText
		WHERE LanguageItemID = @SurveyID AND LanguageMessageTypeID = 4

	INSERT INTO vts_tbMultiLanguageText(LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText)
		SELECT @ClonedSurveyID as LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText
		FROM vts_tbMultiLanguageText
		WHERE LanguageItemID = @SurveyID AND LanguageMessageTypeID = 5

	exec vts_spQuestionCopyToSurvey @SurveyID, @ClonedSurveyID
	exec vts_spSurveyBranchingRuleCopyToSurvey @SurveyID, @ClonedSurveyID
	exec vts_spSurveySkipLogicRuleCopyToSurvey @SurveyID, @ClonedSurveyID
END
COMMIT TRANSACTION CloneQuestion
exec vts_spSurveyGetDetails @ClonedSurveyID, null



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyCookieExpirationUpdate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spSurveyCookieExpirationUpdate] @SurveyID int, @CookieExpires int AS
UPDATE vts_tbSurvey SET CookieExpires = @CookieExpires FROM vts_tbSurvey WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyDeleteByID]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Delete a survey
/// </summary>
/// <param Name="@iSurveyID">
/// ID of the survey to delete
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyDeleteByID] @SurveyID int AS
BEGIN TRAN DeleteSurvey

-- deletes all multi languages messages available for this survey
EXEC vts_spMultiLanguageTextDeleteForSurvey @SurveyID

DELETE FROM vts_tbFile WHERE FileID IN (
	SELECT FileID FROM vts_tbFile INNER JOIN vts_tbVoterAnswers ON 
		AnswerText like GroupGuid
	INNER JOIN vts_tbVoter ON
		vts_tbVoter.VoterID = vts_tbVoterAnswers.VoterID
	WHERE vts_tbVoter.SurveyID = @SurveyID)
DELETE FROM vts_tbVoter WHERE SurveyID = @SurveyID
DELETE FROM vts_tbSurveyLayout WHERE SurveyID = @SurveyID
DELETE FROM vts_tbSurveyToken WHERE SurveyID=@SurveyID
DELETE FROM vts_tbSurveyIPRange Where SurveyID=@SurveyID
DELETE FROM vts_tbSurvey WHERE SurveyID = @SurveyID
COMMIT TRAN DeleteSurvey



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyDeletePageBreak]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// deletes the given page break and reorder the PageNumber 
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyDeletePageBreak] @SurveyID int, @PageNumber int AS
UPDATE vts_tbQuestion SET PageNumber = PageNumber -1 
WHERE 
	PageNumber>1 AND 
	PageNumber>=@PageNumber AND 
	SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyEntryDelete]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Deletes the entry quota allowed for a survey
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyEntryDelete] 	@SurveyID int 


AS
DELETE FROM vts_tbSurveyEntryQuota WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyEntryQuotaGetDetails]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// get all the entry quota settings 
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyEntryQuotaGetDetails]
						@SurveyID int 


AS
SELECT * FROM vts_tbSurveyEntryQuota WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyEntryQuotaIncreaseEntry]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Updates the entry quota allowed for a survey
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyEntryQuotaIncreaseEntry] @SurveyID int


AS

UPDATE vts_tbSurveyEntryQuota SET EntryCount = EntryCount + 1 WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyEntryQuotaReset]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Updates the entry quota allowed for a survey
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyEntryQuotaReset] @SurveyID int


AS

UPDATE vts_tbSurveyEntryQuota SET EntryCount = 0 WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyEntryQuotaUpdate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Updates the entry quota allowed for a survey
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyEntryQuotaUpdate] 
						@SurveyID int, 
						@MaxReachedMessage NVARCHAR(4000),
						@MaxEntries int


AS
UPDATE vts_tbSurveyEntryQuota SET 
	MaxReachedMessage = @MaxReachedMessage,
	MaxEntries = @MaxEntries
WHERE SurveyID = @SurveyID

-- creates a new survey quota 
IF @@RowCount = 0
BEGIN
	INSERT INTO vts_tbSurveyEntryQuota(SurveyID, MaxReachedMessage, MaxEntries) VALUES (@SurveyID, @MaxReachedMessage, @MaxEntries)
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyExists]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Check if the given survey exists in the DB
/// </summary>
/// <param Name="@Title">
/// Title of the survey to check for
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyExists] @Title NVARCHAR(255) AS
SELECT 
	SurveyID
FROM vts_tbSurvey 
WHERE Title = @Title



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyGetActivated]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Get survey  that is currently marked as Activated
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyGetActivated] AS
SELECT
	SurveyID
FROM vts_tbSurvey 
ORDER BY 
	Activated DESC, 
	SurveyID DESC



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyGetAllDetails]    Script Date: 19-8-2014 22:01:40 ******/
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
/// get all surveys available in the database
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyGetAllDetails] AS
SELECT 
	vts_tbSurvey.SurveyID,
	UnAuthentifiedUserActionID,
	CreationDate,
	Title,
	AccessPassword,
	RedirectionURL, 
	OpenDate,
	CloseDate,
	LastEntryDate,
	(Select count(*) FROM vts_tbVoter WHERE Validated<>0 AND SurveyID = vts_tbSurvey.SurveyID) as VoterNumber,
	(Select max(PageNumber) FROM vts_tbQuestion WHERE SurveyID = vts_tbSurvey.SurveyID) as TotalPageNumber,
	SurveyDisplayTimes,
	ResultsDisplayTimes,
	Archive,
	Activated,
	IPExpires,
	CookieExpires,
	OnlyInvited,
	NavigationEnabled,
	ProgressDisplayModeID,
	NotificationModeID,
	ResumeModeID,
	EmailFrom,
	EmailTo,
	EmailSubject,
	Scored,
	AllowMultipleUserNameSubmissions,
	QuestionNumberingDisabled,
	AllowMultipleNSurveySubmissions,
	MultiLanguageModeID,
	MultiLanguageVariable
FROM vts_tbSurvey
LEFT JOIN vts_tbEmailNotificationSettings 
	ON vts_tbSurvey.SurveyID = vts_tbEmailNotificationSettings.SurveyID
ORDER BY vts_tbSurvey.SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyGetAssignedListForUser]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// gets the full list of survey that are assigned to the given user
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyGetAssignedListForUser] @UserID int AS
if exists( select * from vts_tbUserSetting s 
     where s.UserID=@UserID AND (s.IsAdmin=1 OR s.GlobalSurveyAccess=1))
select SurveyID,Title,DefaultSurvey
FROM vts_tbSurvey
ELSE
SELECT s.SurveyID, Title ,DefaultSurvey
FROM vts_tbSurvey s
INNER JOIN vts_tbUserSurvey us
	ON s.SurveyID = us.SurveyID
WHERE 
	us.UserID=@UserID
ORDER BY s.SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyGetCookieExpiration]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spSurveyGetCookieExpiration] @SurveyID int AS
SELECT CookieExpires FROM vts_tbSurvey WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyGetDetails]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Get the details of a survey
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey to get details of
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyGetDetails] @SurveyID int, @LanguageCode NVARCHAR(50)  AS
SELECT 
	vts_tbSurvey.SurveyID,
	UnAuthentifiedUserActionID,
	CreationDate,
	Title,
	AccessPassword,
	RedirectionURL = 
	CASE @LanguageCode 
	WHEN null THEN
		RedirectionURL 
	WHEN '' THEN
		RedirectionURL 
	ELSE
		ISNULL((SELECT ItemText FROM 
		vts_tbMultiLanguageText WHERE
		LanguageItemID =vts_tbSurvey.SurveyID AND
		LanguageMessageTypeID = 5 AND
		LanguageCode = @LanguageCode), RedirectionURL)		
	END,
	OpenDate,
	CloseDate,
	LastEntryDate,
	(Select count(*) FROM vts_tbVoter WHERE Validated<>0 AND SurveyID = @SurveyID) as VoterNumber,
	(Select max(PageNumber) FROM vts_tbQuestion WHERE SurveyID = @SurveyID) as TotalPageNumber,
	SurveyDisplayTimes,
	ResultsDisplayTimes,
	Archive,
	Activated,
	IPExpires,
	CookieExpires,
	ThankYouMessage =
	CASE @LanguageCode 
	WHEN null THEN
		ThankYouMessage 
	WHEN '' THEN
		ThankYouMessage 
	ELSE
		ISNULL((SELECT ItemText FROM 
		vts_tbMultiLanguageText WHERE
		LanguageItemID =vts_tbSurvey.SurveyID AND
		LanguageMessageTypeID = 4 AND
		LanguageCode = @LanguageCode), ThankYouMessage)		
	END,
	OnlyInvited,
	NavigationEnabled,
	ProgressDisplayModeID,
	NotificationModeID,
	ResumeModeID,
	EmailFrom,
	EmailTo,
	EmailSubject,
	Scored,
	AllowMultipleUserNameSubmissions,
	QuestionNumberingDisabled,
	AllowMultipleNSurveySubmissions,
	MultiLanguageModeID,
	MultiLanguageVariable,
	SurveyGuid,
	FriendlyName,defaultSurvey
FROM vts_tbSurvey 
LEFT JOIN vts_tbEmailNotificationSettings 
	ON vts_tbSurvey.SurveyID = vts_tbEmailNotificationSettings.SurveyID
WHERE vts_tbSurvey.SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyGetFirstID]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Retrieved the first suvey id available in the DB
/// </summary>
/// <returns>
/// 	SurveyID
/// </returns>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyGetFirstID] AS
SELECT TOP 1 SurveyID FROM vts_tbSurvey 
ORDER BY SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyGetFirstIDForUser]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Retrieved the first suvey id available in the DB
/// </summary>
/// <returns>
/// 	SurveyID
/// </returns>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyGetFirstIDForUser] @UserID int AS
SELECT TOP 1 vts_tbSurvey.SurveyID FROM vts_tbSurvey 
INNER JOIN vts_tbUserSurvey ON vts_tbSurvey.SurveyID = vts_tbUserSurvey.SurveyID
WHERE vts_tbUserSurvey.UserID = @UserID
ORDER BY vts_tbSurvey.SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyGetForExport]    Script Date: 26-4-2016 9:54:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
	Survey Project changes: copyright (c) 2016, Fryslan Webservices TM (http://survey.codeplex.com)	

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
/// returns the full survey form's questions
/// </summary>
*/

CREATE PROCEDURE [dbo].[vts_spSurveyGetForExport]  @SurveyID int AS


SELECT DISTINCT vts_tbAnswerType.* FROM vts_tbAnswerType
INNER JOIN vts_tbAnswer 
	ON vts_tbAnswer.AnswerTypeID = vts_tbAnswerType.AnswerTypeID
INNER JOIN vts_tbQuestion 
	ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID  
WHERE vts_tbQuestion.SurveyID = @SurveyID

SELECT DISTINCT vts_tbRegularExpression.* FROM vts_tbRegularExpression
INNER JOIN vts_tbAnswer 
	ON vts_tbAnswer.RegularExpressionID = vts_tbRegularExpression.RegularExpressionID
INNER JOIN vts_tbQuestion 
	ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID  
WHERE vts_tbQuestion.SurveyID = @SurveyID

SELECT 
	SurveyID,
	Title,
	RedirectionURL, 
	OpenDate,
	CloseDate,
	ThankYouMessage,
	NavigationEnabled,
	ProgressDisplayModeID,
	ResumeModeID,
	Scored,
	Activated,
	Archive,
	ResultsDisplayTimes,
	SurveyDisplayTimes,
	CreationDate,
	QuestionNumberingDisabled,
	MultiLanguageModeID,
MultiLanguageVariable
FROM vts_tbSurvey WHERE SurveyID = @SurveyID

-- Get main questions and answers
SELECT 
	QuestionID,
	SurveyID,
	ParentQuestionID,
	QuestionText, 
	vts_tbQuestion.SelectionModeID,
	LayoutModeID,
	MinSelectionRequired,
	MaxSelectionAllowed,
	RandomizeAnswers,
	RatingEnabled,
	ColumnsNumber,
	QuestionPipeAlias,
	PageNumber,
	DisplayOrder,
	QuestionIDText,
	HelpText,
	Alias,
	ShowHelpText,
	QuestionID as OldQuestionID,
	QuestionGroupID
FROM vts_tbQuestion 
WHERE SurveyID = @SurveyID AND ParentQuestionID is null

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
	RegularExpressionID,
	Mandatory,
	AnswerIDText,
	AnswerAlias,
    SliderRange,
	SliderValue,
	SliderMin,
	SliderMax,
	SliderAnimate,
	SliderStep,
    vts_tbAnswer.AnswerID as OldAnswerID,
	CssClass
FROM vts_tbAnswer
INNER JOIN vts_tbQuestion 
	ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID  
WHERE vts_tbQuestion.SurveyID = @SurveyID AND vts_tbQuestion.ParentQuestionID is null

SELECT 
	PublisherAnswerID,
	SubscriberAnswerID,
	vts_tbAnswer.QuestionID
FROM vts_tbAnswerConnection
INNER JOIN vts_tbAnswer
	ON vts_tbAnswer.AnswerID = PublisherAnswerID
INNER JOIN vts_tbQuestion 
	ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID  
WHERE vts_tbQuestion.SurveyID = @SurveyID AND vts_tbQuestion.ParentQuestionID is null

-- Retrieves all child questions and their answers
SELECT 
	ParentQuestionID,
	QuestionText
FROM vts_tbQuestion 
WHERE SurveyID = @SurveyID AND ParentQuestionID is not null

SELECT vts_tbAnswerProperty.AnswerID, Properties
FROM vts_tbAnswerProperty
INNER JOIN vts_tbAnswer
	ON vts_tbAnswerProperty.AnswerID = vts_tbAnswer.AnswerID  
INNER JOIN vts_tbQuestion 
	ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID  
WHERE vts_tbQuestion.SurveyID = @SurveyID AND vts_tbQuestion.ParentQuestionID is null


SELECT 
	DeleteSectionLinkText,
	EditSectionLinkText,
	UpdateSectionLinkText,
	AddSectionLinkText,
	vts_tbQuestionSectionOption.QuestionID,
	MaxSections,
	RepeatableSectionModeID
FROM vts_tbQuestionSectionOption
INNER JOIN vts_tbQuestion 
	ON vts_tbQuestion.QuestionID = vts_tbQuestionSectionOption.QuestionID  
WHERE vts_tbQuestion.SurveyID = @SurveyID AND vts_tbQuestion.ParentQuestionID is null


SELECT vts_tbQuestionSectionGridAnswer.QuestionID, AnswerID 
FROM vts_tbQuestionSectionGridAnswer 
INNER JOIN vts_tbQuestion 
	ON vts_tbQuestion.QuestionID = vts_tbQuestionSectionGridAnswer.QuestionID  
WHERE vts_tbQuestion.SurveyID = @SurveyID AND vts_tbQuestion.ParentQuestionID is null

SELECT SurveyID,LanguageCode,DefaultLanguage 
FROM  vts_tbSurveyLanguage WHERE SurveyID=@SurveyID;

SELECT [LanguageItemID]
      ,[LanguageCode]
      ,[LanguageMessageTypeID]
      ,[ItemText]
  FROM [dbo].[vts_tbMultiLanguageText]
  where( 
   LanguageMessageTypeID=10 or
  ([LanguageItemID]=@SurveyID and [LanguageMessageTypeID] in(4,5))
  OR( [LanguageItemID] in (SELECT QuestionID from vts_tbQuestion where SurveyID=@SurveyID) and
  [LanguageMessageTypeID] in(3,11,12))
  OR( [LanguageItemID] in (SELECT AnswerID from 
  vts_tbQuestion as q inner join 
  vts_tbAnswer as ans on  q.QuestionID=ans.QuestionID where q.SurveyID=@SurveyID ) and
  [LanguageMessageTypeID] in(1,2,13)))
 and len(ItemText) !=0
 or LanguageItemID in(
  --
  SELECT g.ID
   FROM vts_tbQuestionGroups AS g
  WHERE g.ID  IN(
  SELECT q.QuestionGroupID FROM vts_tbQuestion AS  q WHERE SurveyID=@SurveyID)
  UNION
  SELECT g.ID FROM vts_tbQuestionGroups AS g
  WHERE g.ID IN(
  SELECT g.ParentGroupID FROM vts_tbQuestionGroups AS g
  WHERE g.ID  IN(
  SELECT q.QuestionGroupID FROM vts_tbQuestion AS  q WHERE SurveyID=@SurveyID)
  )
  
 )
  
  --
  -- Select all required groups and their parent groups
  --
  SELECT g.ID,g.ParentGroupID,g.GroupName,g.DisplayOrder,g.ID OldID
   FROM vts_tbQuestionGroups AS g
  WHERE g.ID  IN(
  SELECT q.QuestionGroupID FROM vts_tbQuestion AS  q WHERE SurveyID=@SurveyID)
  UNION
  SELECT g.ID,g.ParentGroupID,g.GroupName,g.DisplayOrder ,g.ID OldID FROM vts_tbQuestionGroups AS g
  WHERE g.ID IN(
  SELECT g.ParentGroupID FROM vts_tbQuestionGroups AS g
  WHERE g.ID  IN(
  SELECT q.QuestionGroupID FROM vts_tbQuestion AS  q WHERE SurveyID=@SurveyID)
  )
  

GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyGetLibraryList]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// get all surveys available with only the SurveyID and Title in the database
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyGetLibraryList] @LibraryID int AS
SELECT 
	SurveyID,
	Title
FROM vts_tbSurvey
ORDER BY SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyGetList]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// get all surveys available with only the SurveyID and Title in the database
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyGetList] AS
SELECT 
	SurveyID,
	Title
FROM vts_tbSurvey
ORDER BY SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyGetListByTitle]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
/*
/// <summary>
/// get all surveys from specified folder with SurveyID, Title, FolderID and ParentFolderID in the database
/// </summary>
*/
-- =============================================
CREATE PROCEDURE [dbo].[vts_spSurveyGetListByTitle] 
	@SurveyTitle varchar(200),
	@FolderID int = null,
	@UserID int 
AS
BEGIN
	SET NOCOUNT ON;

/* JJ  If @FolderID is null then select from all Folders.select @FolderID = FolderID from vts_tbFolders where @FolderID is null and ParentFolderID is null
 * Restrict Surveys by User
 * Recursively get foldera under given folder
*/
    -- Insert statements for procedure here
     with FolderList as (
    select FolderID,ParentFolderID from
    vts_tbFolders where FolderID=@FolderID
    union all
    select e.FolderID,e.ParentFolderID from
    vts_tbFolders e inner join FolderList as cte on
    e.ParentFolderID=cte.FolderID 
    )
    SELECT 	sv.SurveyID,
	sv.UnAuthentifiedUserActionID,
	sv.CreationDate,
	sv.Title,
	sv.AccessPassword,
	sv.RedirectionURL, 
	sv.OpenDate,
	sv.CloseDate,
	sv.LastEntryDate,
	(Select count(*) FROM vts_tbVoter WHERE Validated<>0 AND SurveyID = sv.SurveyID) as VoterNumber,
	(Select max(PageNumber) FROM vts_tbQuestion WHERE SurveyID = sv.SurveyID) as TotalPageNumber,
	sv.SurveyDisplayTimes,
	sv.ResultsDisplayTimes,
	sv.Archive,
	sv.Activated,
	sv.IPExpires,
	sv.CookieExpires,
	sv.OnlyInvited,
	sv.NavigationEnabled,
	sv.ProgressDisplayModeID,
	sv.NotificationModeID,
	sv.ResumeModeID,
	em.EmailFrom,
	em.EmailTo,
	em.EmailSubject,
	sv.Scored,
	sv.AllowMultipleUserNameSubmissions,
	sv.QuestionNumberingDisabled,
	sv.AllowMultipleNSurveySubmissions,
	sv.MultiLanguageModeID,
	sv.MultiLanguageVariable
	FROM vts_tbSurvey AS sv
	LEFT JOIN vts_tbEmailNotificationSettings em
	ON sv.SurveyID = em.SurveyID
	WHERE sv.Title like '%' + ISNULL(@SurveyTitle, '') + '%'
	and( sv.FolderID in (select FolderID from folderlist) or @FolderID is null)
	AND (exists(
	    select 1 from vts_tbUserSurvey as usr
	    where usr.SurveyID=sv.SurveyID and usr.UserID=@UserID )
	    or exists (
	    select 1 from vts_tbUserSetting st
	    where st.UserID=@UserID
	    and (st.IsAdmin=1 or st.GlobalSurveyAccess=1)
	    )
	    )
	
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyGetPagesNumber]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spSurveyGetPagesNumber] @SurveyID int
AS
Select max(PageNumber) As TotalPages FROM vts_tbQuestion WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyGetPipeDataFromQuestionID]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// get all pipe informations for the survey needed to handle 
/// answer / question piping
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyGetPipeDataFromQuestionID] @QuestionID int  AS

DECLARE @SurveyID int 

SELECT @SurveyID = SurveyID FROM vts_tbQuestion WHERE QuestionID = @QuestionID

SELECT
	QuestionID, 
	QuestionPipeAlias
FROM vts_tbQuestion
WHERE 
	SurveyID = @SurveyID AND 
	QuestionPipeAlias<>'' AND 
	QuestionPipeAlias is not null

SELECT 
	AnswerID,
	AnswerText,
	vts_tbAnswer.QuestionID,
	AnswerPipeAlias
FROM vts_tbAnswer
INNER JOIN vts_tbQuestion 
	ON vts_tbAnswer.QuestionID = vts_tbQuestion.QuestionID
WHERE 
	vts_tbQuestion.SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyGetSurveyIDFromFriendlyName]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spSurveyGetSurveyIDFromFriendlyName]
 @FriendlyName as varchar(200)
AS
Select max(SurveyID) As SurveyID FROM vts_tbSurvey WHERE FriendlyName = @FriendlyName



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyGetSurveyIDFromGuid]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spSurveyGetSurveyIDFromGuid] @SurveyGuid uniqueidentifier
AS
Select max(SurveyID) As SurveyID FROM vts_tbSurvey WHERE SurveyGuid = @SurveyGuid



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyGetUnAssignedListForUser]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// gets the full list of survey that aren't assigned to the given user
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyGetUnAssignedListForUser] @UserID int AS

SELECT s.SurveyID, Title 
FROM vts_tbSurvey s
WHERE SurveyID not in (select SurveyID from vts_tbUserSurvey where UserID = @UserID)
ORDER BY s.SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyGetUnAuthentifiedUserAction]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
///  Gets the unauthenticated action
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyGetUnAuthentifiedUserAction] @SurveyID int
AS
	SELECT UnAuthentifiedUserActionID FROM vts_tbSurvey WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyHasPageBranching]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Check if the given page of the survey has branching rules associated with it
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyHasPageBranching] @SurveyID int, @PageNumber int 
AS

SELECT top 1 vts_tbBranchingRule.BranchingRuleID FROM vts_tbBranchingRule 
INNER JOIN vts_tbQuestion
	ON vts_tbQuestion.QuestionID = vts_tbBranchingRule.QuestionID
WHERE vts_tbQuestion.SurveyID = @SurveyID AND vts_tbBranchingRule.PageNumber = @PageNumber



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyIncrementResultsViews]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
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
/// Increments the number of times a survey's results has been Displayed
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey that will increment its results view count
/// </param>
/// <param Name="@CountNumber">
/// Increment number
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyIncrementResultsViews]
					@SurveyID int, 
					@CountNumber int
AS
UPDATE vts_tbSurvey
SET ResultsDisplayTimes = ResultsDisplayTimes+@CountNumber
WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyIncrementViews]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
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
/// Increments the number of times a survey has been Displayed
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey that will increment its view count
/// </param>
/// <param Name="@CountNumber">
/// Increment number
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyIncrementViews]
					@SurveyID int, 
					@CountNumber int			
AS
UPDATE vts_tbSurvey
SET SurveyDisplayTimes = SurveyDisplayTimes+@CountNumber
WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyInsertPageBreak]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Insert a page break at the given position and updates 
/// the PageNumber to match the new break position
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyInsertPageBreak] @SurveyID int, @DisplayOrder int AS
-- Update the question page order
UPDATE vts_tbQuestion SET PageNumber = PageNumber + 1 
WHERE 
	SurveyID = @SurveyID AND
	DisplayOrder >= @DisplayOrder



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyIPExpirationGet]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spSurveyIPExpirationGet] @SurveyID int AS
SELECT IPExpires FROM vts_tbSurvey WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyIPExpirationUpdate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spSurveyIPExpirationUpdate] @SurveyID int, @IPExpires int AS
UPDATE vts_tbSurvey SET IPExpires = @IPExpires FROM vts_tbSurvey WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyIPRangeAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[vts_spSurveyIPRangeAddNew]	
	@SurveyID int,
	@IPStart varchar(100),
	@IPEnd  varchar(100)
AS
BEGIN
INSERT INTO vts_tbSurveyIPRange
(SurveyID,IPStart,IPEnd)
VALUES (@SurveyID,@IPStart,@IPEnd);
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyIPRangeDelete]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[vts_spSurveyIPRangeDelete]	
	@SurveyIPRangeID int
AS
BEGIN
DELETE FROM  vts_tbSurveyIPRange
WHERE SurveyIPRangeID=@SurveyIPRangeID;
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyIPRangeUpdate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[vts_spSurveyIPRangeUpdate]	
    @SurveyIPRangeID int,
	@SurveyID int,
	@IPStart varchar(100),
	@IPEnd  varchar(100)
AS
BEGIN
UPDATE vts_tbSurveyIPRange
 SET IPStart=@IPStart,
     IPEnd=@IPEnd
 WHERE SurveyIPRangeID=@SurveyIPRangeID
 AND   SurveyID=@SurveyID
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyIsScored]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spSurveyIsScored] @SurveyID int 
AS
SELECT Scored FROM vts_tbSurvey WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyLayoutGet]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Gets the settings of the user
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyLayoutGet] (@SurveyID  int,@LanguageCode as NVARCHAR(30))
 AS

SELECT [SurveyID]
      ,[SurveyHeaderText]=
		CASE @LanguageCode 
		WHEN null THEN
			[SurveyHeaderText]
		WHEN '' THEN
			[SurveyHeaderText]
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = @SurveyID AND
			LanguageMessageTypeID = 14 AND
			LanguageCode = @LanguageCode), [SurveyHeaderText])		
		END
      ,[SurveyFooterText]=
      CASE @LanguageCode 
		WHEN null THEN
			[SurveyFooterText]
		WHEN '' THEN
			[SurveyFooterText]
		ELSE
			ISNULL((SELECT ItemText FROM 
			vts_tbMultiLanguageText WHERE
			LanguageItemID = @SurveyID AND
			LanguageMessageTypeID = 15 AND
			LanguageCode = @LanguageCode), [SurveyFooterText])		
		END
      ,[SurveyCss]
FROM vts_tbSurveyLayout
WHERE SurveyID = @SurveyID




GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyLayoutUpdate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

/*
JJ Created Moving Layout data from User to Survey
    Survey changes: copyright (c) 2010, Fryslan Webservices TM (http://survey.codeplex.com)    

    NSurvey - The web survey and form engine
    Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.
*/
CREATE PROCEDURE [dbo].[vts_spSurveyLayoutUpdate]
@SurveyID int =null,
            @SurveyHeaderText NVARCHAR(max) = null,
            @SurveyFooterText NVARCHAR(max) = null,
            @SurveyCss NVARCHAR(255) = null,
            @LanguageCode NVARCHAR(30)=null
            
AS

begin TRANSACTION
UPDATE vts_tbSurveyLayout
     SET 
     SurveyCss=@SurveyCss
     WHERE SurveyID=@SurveyID;
IF(@@RowCount>0) 
BEGIN
     IF @LanguageCode is null OR @LanguageCode='' 
     BEGIN
    	UPDATE vts_tbSurveyLayout
	  SET 	
		   SurveyHeaderText    =     @SurveyHeaderText,
	        SurveyFooterText =        @SurveyFooterText
	  WHERE
		 SurveyID = @SurveyID
      END 
      ELSE
      BEGIN
	    exec vts_spMultiLanguageTextUpdate @SurveyID, @LanguageCode, 14, @SurveyHeaderText
        exec vts_spMultiLanguageTextUpdate @SurveyID, @LanguageCode, 15, @SurveyFooterText
      END
END
ELSE
BEGIN
INSERT INTO vts_tbSurveyLayout
              (SurveyID, SurveyHeaderText, SurveyFooterText, SurveyCss) 
              VALUES (@SurveyID,@SurveyHeaderText,@SurveyFooterText,@SurveyCss)
END
commit TRANSACTION



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyMessageConditionAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Adds a new message condition  to the survey 
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyMessageConditionAddNew] 
			@SurveyID int,
			@MessageConditionalOperator int,
			@QuestionID int,
			@AnswerID int,
			@TextFilter NVARCHAR(4000),
			@ThankYouMessage NVARCHAR(4000),
			@ConditionalOperator int,
			@ExpressionOperator int,			
			@Score	int,
			@ScoreMax int,
			@MessageConditionID int OUTPUT
AS
INSERT INTO vts_tbMessageCondition
	(SurveyID,
	MessageConditionalOperator,
	QuestionID,
	AnswerID,
	ConditionalOperator,
	ExpressionOperator,
	TextFilter,
	ThankYouMessage,
	Score,
	ScoreMax)
VALUES
	(@SurveyID,
	@MessageConditionalOperator,
	@QuestionID,
	@AnswerID,
	@ConditionalOperator,
	@ExpressionOperator,
	@TextFilter,
	@ThankYouMessage,
	@Score,
	@ScoreMax)

set @MessageConditionID = SCOPE_IDENTITY()



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyMessageConditionDeleteByID]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Deletes the given condition
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyMessageConditionDeleteByID] @MessageConditionID int AS
DELETE FROM vts_tbMessageCondition WHERE MessageConditionID = @MessageConditionID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyMessageConditionsGetAll]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Retrieve all message condition rules for the given survey
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyMessageConditionsGetAll] @SurveyID int AS
SELECT vts_tbMessageCondition.*,  AnswerText, QuestionText
FROM vts_tbMessageCondition
LEFT JOIN vts_tbQuestion 	
	ON vts_tbQuestion.QuestionID =  vts_tbMessageCondition.QuestionID
LEFT JOIN vts_tbAnswer 
	ON vts_tbAnswer.AnswerID = vts_tbMessageCondition.AnswerID
WHERE vts_tbMessageCondition.SurveyID = @SurveyID
ORDER BY MessageConditionID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyMessageConditionsGetDetails]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Retrieve  a condition rules details
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyMessageConditionsGetDetails] @MessageConditionID int AS
SELECT * FROM vts_tbMessageCondition
WHERE MessageConditionID = @MessageConditionID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyMessageConditionUpdate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// updates a new message condition  to the survey 
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyMessageConditionUpdate] 
			@MessageConditionID  int,
			@MessageConditionalOperator int,
			@ExpressionOperator int,			
			@QuestionID int,
			@AnswerID int,
			@TextFilter NVARCHAR(4000),
			@ThankYouMessage NVARCHAR(4000),
			@ConditionalOperator int,
			@Score	int,
			@ScoreMax int
AS
UPDATE vts_tbMessageCondition SET
	MessageConditionalOperator = @MessageConditionalOperator,
	QuestionID = @QuestionID,
	AnswerID = @AnswerID,
	ConditionalOperator = @ConditionalOperator,
	TextFilter = @TextFilter,
	ThankYouMessage = @ThankYouMessage,
	Score = @Score,
	ScoreMax = @ScoreMax,
	ExpressionOperator = @ExpressionOperator			

WHERE MessageConditionID = @MessageConditionID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyMovePageBreakDown]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
///  Moves a page break down
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey that contains the page break
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyMovePageBreakDown] 
				@SurveyID int,
				@PageNumber int
AS
DECLARE 
	@NewPage int,
	@QuestionID int
	/*
SELECT TOP 1  
	@NewPage = PageNumber,
	@QuestionID = QuestionID
FROM 
	vts_tbQuestion
WHERE
	SurveyID = @SurveyID AND
	ParentQuestionID is null AND
	PageNumber > @PageNumber
	ORDER BY PageNumber ASC, DisplayOrder ASC
if @@RowCount <>0
BEGIN
	-- Move page break down from one question
	IF @PageNumber+1 = @NewPage 
	BEGIN
		UPDATE vts_tbQuestion 
			set PageNumber = @PageNumber 
		WHERE QuestionID = @QuestionID OR ParentQuestionID = @QuestionID
	END
END
*/
DECLARE @LastPage int

SELECT @LastPage=MAX(PageNumber) 
FROM vts_tbQuestion WHERE SurveyID=@SurveyID;

IF @@RowCount =0 OR @LastPage=@PageNumber  return;


UPDATE vts_tbQuestion
SET PageNumber= CASE When PageNumber=@PageNumber   THEN PageNumber+1
                     When PageNumber=@PageNumber+1 THEN PageNumber-1
                     Else PageNumber
                 END 
    WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyMovePageBreakUp]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
///  Moves a page break up
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey that contains the page break
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyMovePageBreakUp] 
				@SurveyID int,
				@PageNumber int
AS
DECLARE 
	@NewPage int,
	@QuestionID int
	/*
SELECT TOP 1  
	@NewPage = PageNumber,
	@QuestionID = QuestionID
FROM 
	vts_tbQuestion
WHERE
	SurveyID = @SurveyID AND
	ParentQuestionID is null AND
	PageNumber < @PageNumber
	ORDER BY PageNumber DESC, DisplayOrder DESC
if @@RowCount <>0
BEGIN
	-- Move page break up from one question
	IF @PageNumber-1 = @NewPage 
	BEGIN
		UPDATE vts_tbQuestion 
			set PageNumber = @PageNumber 
		WHERE QuestionID = @QuestionID OR ParentQuestionID = @QuestionID
	END
END
*/
if(@PageNumber=1) return;

UPDATE vts_tbQuestion
SET PageNumber= CASE When PageNumber=@PageNumber   THEN PageNumber-1
                     When PageNumber=@PageNumber-1 THEN PageNumber+1
                     Else PageNumber
                 END 
    WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyMultiLanguageModeClearSettings]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spSurveyMultiLanguageModeClearSettings] @SurveyID int
AS

BEGIN TRAN ClearMultiLanguage

UPDATE vts_tbSurvey  SET MultiLanguageModeID = 0 WHERE SurveyID = @SurveyID

DELETE FROM vts_tbSurveyLanguage WHERE SurveyID = @SurveyID
DELETE FROM vts_tbMultiLanguageText WHERE (LanguageMessageTypeID = 1 OR LanguageMessageTypeID = 2) AND 
	LanguageItemID in (SELECT AnswerID FROM vts_tbAnswer A inner join vts_tbQuestion Q ON Q.QuestionID = A.QuestionID Where SurveyID = @SurveyID)
DELETE FROM vts_tbMultiLanguageText WHERE LanguageMessageTypeID = 3 AND 
	LanguageItemID in (SELECT QuestionID FROM vts_tbQuestion Where SurveyID = @SurveyID)

COMMIT TRAN ClearMultiLanguage



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyMultiLanguageModeGet]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spSurveyMultiLanguageModeGet] @SurveyID int
AS

	SELECT MultiLanguageModeID FROM vts_tbSurvey WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyMultiLanguageModeUpdate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spSurveyMultiLanguageModeUpdate] @SurveyID int, @MultiLanguageModeID int, @MultiLanguageVariable NVARCHAR(50)
AS

UPDATE vts_tbSurvey  SET MultiLanguageModeID = @MultiLanguageModeID, MultiLanguageVariable = @MultiLanguageVariable
WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyOnlyInvitedGet]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spSurveyOnlyInvitedGet] @SurveyID int 
AS
SELECT OnlyInvited FROM vts_tbSurvey WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyOnlyInvitedUpdate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spSurveyOnlyInvitedUpdate] @SurveyID int, @OnlyInvited bit 
AS
UPDATE vts_tbSurvey SET OnlyInvited = @OnlyInvited FROM vts_tbSurvey WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveySaveTokenUserDataGet]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spSurveySaveTokenUserDataGet] @SurveyID int 
AS
SELECT SaveTokenUserData FROM vts_tbSurvey WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveySaveTokenUserDataUpdate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spSurveySaveTokenUserDataUpdate] @SurveyID int, @SaveData bit 
AS
UPDATE vts_tbSurvey SET SaveTokenUserData = @SaveData FROM vts_tbSurvey WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveySetFolder]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[vts_spSurveySetFolder]
	@ParentFolderID int,
	@SurveyID int
AS
BEGIN
	SET NOCOUNT ON;
	declare @PID int
	-- we set parent to point to home folder
	select @PID = FolderID from vts_tbFolders where ParentFolderID IS NULL
	if exists(
          select 1 from vts_tbSurvey where FolderID=ISNULL(@ParentFolderID, @PID) and 
          Title =(select Title from vts_tbSurvey where SurveyID=@SurveyID)
          and SurveyID!=@SurveyID )
         begin
           raiserror('DUPLICATEFOLDER',16,4);
           return;
         end;
    Update vts_tbSurvey
	set FolderID = ISNULL(@ParentFolderID, @PID)
	where SurveyID = @SurveyID
	
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveySkipLogicRuleCopyToSurvey]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
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
/// clone all branchings of a survey to another
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveySkipLogicRuleCopyToSurvey] @SurveyID int, @ClonedSurveyID int AS


DECLARE @ConditionalOperator int,
	@ExpressionOperator int,
	@AnswerID int,
	@ClonedAnswerID int,
	@QuestionID int,
	@SkipQuestionID int,
	@ClonedSkipQuestionID int,
	@ClonedQuestionID int,
	@TextFilter varchar(4000),
	@Score int,
	@ScoreMax int


DECLARE SkipCursor CURSOR FOR
	SELECT 
		ConditionalOperator,
		ExpressionOperator,
		AnswerID,
		SkipQuestionID,
		vts_tbSkipLogicRule.QuestionID,
		TextFilter,
		Score,
		ScoreMax
	FROM vts_tbSkipLogicRule
	INNER JOIN vts_tbQuestion
		ON vts_tbQuestion.QuestionID = vts_tbSkipLogicRule.SkipQuestionID
	WHERE SurveyID = @SurveyID


OPEN SkipCursor
FETCH SkipCursor INTO 
	@ConditionalOperator,
	@ExpressionOperator,
	@AnswerID,
	@SkipQuestionID,
	@QuestionID,
	@TextFilter,
	@Score,
	@ScoreMax
WHILE @@FETCH_STATUS = 0
BEGIN

	-- Get question id that is in the cloned survey
	SELECT @ClonedSkipQuestionID = (select QuestionID  from vts_tbQuestion WHERE SurveyID = @ClonedSurveyID AND QuestionText = (select QuestionText FROM vts_tbQuestion WHERE QuestionID = @SkipQuestionID) AND 
				DisplayOrder = (select DisplayOrder FROM vts_tbQuestion WHERE QuestionID = @SkipQuestionID)) 

	-- Get question id that is in the cloned survey
	SELECT @ClonedQuestionID =	(select QuestionID  from vts_tbQuestion WHERE SurveyID = @ClonedSurveyID AND QuestionText = (select QuestionText FROM vts_tbQuestion WHERE QuestionID = @QuestionID) AND 
				DisplayOrder = (select DisplayOrder FROM vts_tbQuestion WHERE QuestionID = @QuestionID)) 
	
	IF @AnswerID is not NULL
	BEGIN
		-- Get answer id from the cloned survey
		SELECT @ClonedAnswerID = 
				(select AnswerID from vts_tbAnswer WHERE QuestionID = @ClonedQuestionID AND 
				DisplayOrder = (select DisplayOrder FROM vts_tbAnswer WHERE AnswerID = @AnswerID))
	END
	ELSE
	BEGIN
		SET @ClonedAnswerID = null
	END

	INSERT INTO vts_tbSkipLogicRule(ConditionalOperator, ExpressionOperator, AnswerID, SkipQuestionID, QuestionID, TextFilter, Score, ScoreMax)
	VALUES (@ConditionalOperator, @ExpressionOperator, @ClonedAnswerID, @ClonedSkipQuestionID, @ClonedQuestionID, @TextFilter, @Score, @ScoreMax)

	FETCH SkipCursor INTO 
		@ConditionalOperator,
		@ExpressionOperator,
		@AnswerID,
		@SkipQuestionID,
		@QuestionID,
		@TextFilter,
		@Score,
		@ScoreMax
END

CLOSE SkipCursor 
DEALLOCATE SkipCursor



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyTokenAddMultiple]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURe [dbo].[vts_spSurveyTokenAddMultiple]
 @SurveyID int,
 @CreationDate date,
 @tblTokenList VarcharTableType readonly
	
AS
BEGIN
	
	SET NOCOUNT ON;
INSERT INTO vts_tbSurveyToken(SurveyID,CreationDate,Token,Used)
SELECT @SurveyID,@CreationDate,list.value,0
FROM @tblTokenList as list;

END



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyTokenDeleteMultiple]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURe [dbo].[vts_spSurveyTokenDeleteMultiple]
 @tblTokenIDList IntTableType readonly
	
AS
BEGIN
	
SET NOCOUNT ON;
DELETE FROM vts_tbSurveyToken
WHERE TokenID in(
SELECT value
FROM @tblTokenIDList 
);
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyTokenUpdate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Check if the given UID is valid and returns its survey id
/// </summary>
/// <param Name="@UID">
/// UID to check
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyTokenUpdate] 
@SurveyID int,@Token varchar(40),
@VoterID int
AS

UPDATE vts_tbSurveyToken
SET Used=1,
    VoterID=case when @VoterID<=0 then null else @VoterID end
WHERE SurveyID =@SurveyID
AND   Token=@Token



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyTokenValidate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Check if the given UID is valid and returns its survey id
/// </summary>
/// <param Name="@UID">
/// UID to check
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyTokenValidate] 
@SurveyID int,@Token varchar(40)
AS

select 1 from vts_tbSurveyToken
WHERE SurveyID =@SurveyID
AND   Token=@Token
AND   Used=0;



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyUpdate]    Script Date: 19-8-2014 22:01:40 ******/
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
///  Updates a survey
/// </summary>
/// <remarks>
/// Only one survey can be Activated at one time
/// </remarks>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyUpdate] 

				@LanguageCode NVARCHAR(50),
				@SurveyID int,
				@OpenDate datetime,
				@CloseDate datetime,
				@Title NVARCHAR(255),
				@ThankYouMessage NVARCHAR(4000),
				@RedirectionURL varchar(1024),
				@Archive bit,
				@Activated bit,
				@ResumeModeID int,
				@NavigationEnabled bit,
				@ProgressDisplayModeID int,
				@NotificationModeID int,
				@Scored bit,
				@QuestionNumberingDisabled bit,
			    @DefaultSurvey bit=0
AS

if exists(
          select 1 from vts_tbSurvey where FolderID=
          (select FolderID from vts_tbSurvey where SurveyID=@SurveyID)
          and Title =@Title  and SurveyID!=@SurveyID)
         begin
    
           raiserror('DUPLICATEFOLDER',16,4);
           return;
         end;
         
if @DefaultSurvey <> 0
-- Only one survey can be Activated at one time
UPDATE vts_tbSurvey SET DefaultSurvey = 0 WHERE DefaultSurvey<>0


         
         
UPDATE vts_tbSurvey 
SET 
	ProgressDisplayModeID = @ProgressDisplayModeID,
	NotificationModeID = @NotificationModeID,
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
	exec vts_spMultiLanguageTextUpdate @SurveyID, @LanguageCode, 4, @ThankYouMessage
	exec vts_spMultiLanguageTextUpdate @SurveyID, @LanguageCode, 5, @RedirectionURL
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyUpdateASPNetSubmissions]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spSurveyUpdateASPNetSubmissions] @SurveyID int, @AllowMultipleSubmissions  bit 
AS
UPDATE vts_tbSurvey SET AllowMultipleUserNameSubmissions = @AllowMultipleSubmissions FROM vts_tbSurvey WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyUpdateFriendlyName]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[vts_spSurveyUpdateFriendlyName]
	@SurveyID int,
	@FriendlyName varchar(200)
AS
BEGIN
	SET NOCOUNT ON;
	
	
    Update vts_tbSurvey
	set FriendlyName=@FriendlyName
	where SurveyID = @SurveyID
	
END

GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyDeleteFriendlyName]    Script Date: 7-2-2017 16:47:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	<W3DevPro>
-- Create date: <2017/02/07>
-- Description:	<update to "remove" FriendlyUrl from Survey table i.e. set it to null>
-- =============================================
CREATE PROCEDURE [dbo].[vts_spSurveyDeleteFriendlyName]
	@SurveyID int
AS
BEGIN
	SET NOCOUNT ON;
	
    Update vts_tbSurvey
	set FriendlyName= null
	where SurveyID = @SurveyID
	
END


GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyUpdateNSurveySubmissions]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spSurveyUpdateNSurveySubmissions] @SurveyID int, @AllowMultipleNSurveySubmissions bit 
AS
UPDATE vts_tbSurvey SET AllowMultipleNSurveySubmissions = @AllowMultipleNSurveySubmissions FROM vts_tbSurvey WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyUpdateUnAuthentifiedUserAction]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
///  Updates the unauthenticated action
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyUpdateUnAuthentifiedUserAction] @SurveyID int, @UnAuthentifiedUserActionID int
AS
	UPDATE vts_tbSurvey SET UnAuthentifiedUserActionID = @UnAuthentifiedUserActionID WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spSurveyValidatePassword]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Check if the given password match the survey password that was setup
/// </summary>
/// <param Name="@SurveyID">
/// ID of the protected survey
/// </param>
/// <param Name="@Password">
/// Password to validate
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spSurveyValidatePassword]  @SurveyID int, @Password NVARCHAR(255) AS
SELECT 
	SurveyID
FROM vts_tbSurvey 
WHERE SurveyID = @SurveyID AND (AccessPassword = @Password)



GO
/****** Object:  StoredProcedure [dbo].[vts_spTokenGetForSurvey]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Get the questions until next page break
/// </summary>
/// <param Name="@LibraryID">
/// ID of the Library  to retrieve questions from
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spTokenGetForSurvey]
			@SurveyID int
AS
	SELECT * from vts_tbSurveyToken
	WHERE SurveyID=@SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spTreeGetFolders]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[vts_spTreeGetFolders]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT sv.SurveyID, sv.Title, fs.FolderID, fs.ParentFolderID, fs.FolderName FROM vts_tbSurvey AS sv
	RIGHT OUTER JOIN vts_tbFolders AS fs ON fs.FolderID = sv.FolderID

END



GO
/****** Object:  StoredProcedure [dbo].[vts_spTreeNodesGetAll]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
-- JJ Modified to restrict to Surveys to which the User has access
--
CREATE PROCEDURE [dbo].[vts_spTreeNodesGetAll](
@UserID int)
AS
BEGIN	
	SET NOCOUNT ON;
	WITH cte As
	(
	SELECT sv.FolderID , 
		 fs.FolderName , 
		fs.ParentFolderID 
		FROM vts_tbSurvey AS sv
		JOIN vts_tbFolders AS fs ON fs.FolderID = sv.FolderID 
		WHERE exists(
	    select 1 from vts_tbUserSurvey as usr
	    where usr.SurveyID=sv.SurveyID and usr.UserID=@UserID )
	    OR exists(
	    select 1 from vts_tbUserSetting us
	      where us.UserID=@UserID
	      and(us.GlobalSurveyAccess=1 OR us.IsAdmin=1)
	    )
	    UNION ALL
	    SELECT fs.FolderID   , 
	           fs.FolderName , 
		       fs.ParentFolderID  
		from vts_tbFolders as fs JOIN cte 
		on cte.ParentFolderID=fs.FolderID
		where cte.ParentFolderID IS NOT NULL
	    )
	SELECT 's' + CONVERT(varchar, sv.SurveyID) as ItemID, 
		ISNULL(sv.Title, fs.FolderName) as NodeName, 
		'f' + CONVERT(varchar, sv.FolderID) as ParentFolderID
		FROM vts_tbSurvey AS sv 
		JOIN vts_tbFolders AS fs ON fs.FolderID = sv.FolderID 
		WHERE (exists(
	    select 1 from vts_tbUserSurvey as usr
	    where usr.SurveyID=sv.SurveyID and usr.UserID=@UserID )
	    or exists (
	    select 1 from vts_tbUserSetting st
	    where st.UserID=@UserID
	    and (st.IsAdmin=1 or st.GlobalSurveyAccess=1)
	    )
	    )
	  
	UNION 
	SELECT 'f' + CONVERT(varchar, fs.FolderID)  as ItemID , FolderName  as NodeName, 
		'f' + CONVERT(varchar, fs.ParentFolderID)  as ParentFolderID
		from cte as fs 
     UNION
	SELECT 'f' + CONVERT(varchar, fs.FolderID)  as ItemID , FolderName  as NodeName, 
		'f' + CONVERT(varchar, fs.ParentFolderID)  as ParentFolderID
		from vts_tbFolders as fs 
	WHERE exists(
	    select 1 from vts_tbUserSetting st
	    where st.UserID=@UserID
	    and (st.IsAdmin=1 or st.GlobalSurveyAccess=1))
		ORDER BY NodeName		
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spUnAuthentifiedUserActionGetAll]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spUnAuthentifiedUserActionGetAll] AS
SELECT * FROM vts_tbUnAuthentifiedUserAction



GO
/****** Object:  StoredProcedure [dbo].[vts_spUserAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// adds a new nsurvey user
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spUserAddNew]
			@UserID int out,
			@UserName NVARCHAR(255),
			@Password NVARCHAR(255),
	        @PasswordSalt NVARCHAR(255),
			@LastName NVARCHAR(255),
			@FirstName NVARCHAR(255),  
			@Email NVARCHAR(255)
			
AS

INSERT INTO vts_tbUser (UserName,
	Password,
    PasswordSalt,
	FirstName,
	LastName,
	Email)
VALUES (@UserName,
	@Password,
	@PasswordSalt,
	@FirstName,
	@LastName,
	@Email)



SELECT @UserID = SCOPE_IDENTITY()




GO
/****** Object:  StoredProcedure [dbo].[vts_spUserAnswerTypeAssignUser]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Assign an answer type to the user
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spUserAnswerTypeAssignUser] @AnswerTypeID int, @UserID int AS

SELECT AnswerTypeID FROM vts_tbUserAnswerType WHERE AnswerTypeID = @AnswerTypeID AND UserID = @UserID
IF @@RowCount = 0
BEGIN
	INSERT INTO vts_tbUserAnswerType(AnswerTypeID, UserID) VALUES (@AnswerTypeID, @UserID)
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spUserAnswerTypeUnAssignAllUser]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Assign an answer type to the user
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spUserAnswerTypeUnAssignAllUser] @AnswerTypeID int AS

DELETE FROM vts_tbUserAnswerType WHERE AnswerTypeID =@AnswerTypeID



GO
/****** Object:  StoredProcedure [dbo].[vts_spUserDelete]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// deletes a user from the database
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spUserDelete]
			@UserID int
			
AS
BEGIN TRAN DeleteUserTRAN
DELETE FROM vts_tbUserRole WHERE UserID = @UserID
DELETE FROM vts_tbUserSurvey WHERE UserID = @UserID
DELETE FROM vts_tbUserSetting WHERE UserID = @UserID
DELETE FROM vts_tbUser WHERE UserID = @UserID
COMMIT TRAN DeleteUserTRAN



GO
/****** Object:  StoredProcedure [dbo].[vts_spUserGetAdminCount]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Returns the number of admins in the database
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spUserGetAdminCount]
			
AS

SELECT count(*) FROM vts_tbUserSetting WHERE IsAdmin<>0



GO
/****** Object:  StoredProcedure [dbo].[vts_spUserGetData]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Gets the details of a user
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spUserGetData] @UserName NVARCHAR(255), @Password NVARCHAR(255) AS

SELECT 
	UserID, UserName, 
	Password, FirstName, 
	LastName, Email, 
	CreationDate, LastLogin
FROM vts_tbUser
WHERE UserName = @UserName AND Password = @Password

IF @@RowCount > 0
BEGIN
	UPDATE vts_tbUser SET
		LastLogin = getdate() 
	WHERE UserName = @UserName AND Password = @Password
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spUserGetDetails]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Gets the details of a user
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spUserGetDetails] @UserID int AS

SELECT 
	UserID, UserName, 
	Password, PasswordSalt,FirstName, 
	LastName, Email, 
	CreationDate, LastLogin 
FROM vts_tbUser
WHERE UserID = @UserID




GO
/****** Object:  StoredProcedure [dbo].[vts_spUserGetList]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// gets the full list of nsurvey users
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spUserGetList] AS

SELECT 
	UserID, UserName, 
	Password, FirstName, 
	LastName, Email, 
	CreationDate, LastLogin
FROM vts_tbUser
ORDER BY UserName



GO
/****** Object:  StoredProcedure [dbo].[vts_spUserGetListByFilter]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[vts_spUserGetListByFilter]
	@UserName varchar(200) = null,
	@FirstName varchar(200) = null,
	@LastName varchar(200) = null,
	@Email varchar(200) = null,
	@Administrator int = null
AS
BEGIN
	SET NOCOUNT ON;

	/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 u.[UserID]
      ,u.[UserName]
      ,u.[Password]
      ,u.[FirstName]
      ,u.[LastName]
      ,u.[Email]
      ,u.[CreationDate]
      ,u.[LastLogin]
      ,us.IsAdmin
  FROM [dbo].[vts_tbUser] u
  INNER JOIN [dbo].[vts_tbUserSetting] us ON us.UserID = u.[UserID]
  WHERE (u.UserName LIKE '%'+ ISNULL(@UserName, '') + '%' OR u.UserName is NULL)
  AND (u.FirstName LIKE '%'+ ISNULL(@FirstName, '') +'%' OR u.FirstName IS NULL)
  AND (u.LastName LIKE '%'+ ISNULL(@LastName, '') +'%' OR u.LastName IS NULL)
  AND (u.Email LIKE '%'+ ISNULL(@Email, '') +'%' or u.Email is null)
  AND (@Administrator is null OR us.IsAdmin = @Administrator)
   Order by u.[UserName]
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spUserGetUserIDFromUserName]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Returns the user id of the userName
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spUserGetUserIDFromUserName]
			@UserName NVARCHAR(255)
			
AS

SELECT UserID FROM vts_tbUser WHERE UserName = @UserName



GO
/****** Object:  StoredProcedure [dbo].[vts_spUserIsAdmin]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// check if the user is an Administrator
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spUserIsAdmin] @UserID int AS

SELECT IsAdmin FROM vts_tbUserSetting WHERE UserID = @UserID AND IsAdmin<>0



GO
/****** Object:  StoredProcedure [dbo].[vts_spUserRegularExpressionAssignUser]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Assign an regex to the user
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spUserRegularExpressionAssignUser] @RegularExpressionID int, @UserID int AS

SELECT RegularExpressionID FROM vts_tbUserRegularExpression WHERE RegularExpressionID = @RegularExpressionID AND UserID = @UserID
IF @@RowCount = 0
BEGIN
	INSERT INTO vts_tbUserRegularExpression(RegularExpressionID, UserID) VALUES (@RegularExpressionID, @UserID)
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spUserRoleAssignUser]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Assign a new role to the user
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spUserRoleAssignUser] @RoleID int, @UserID int AS

SELECT RoleID FROM vts_tbUserRole WHERE RoleID = @RoleID AND UserID = @UserID
IF @@RowCount = 0
BEGIN
	INSERT INTO vts_tbUserRole(RoleID, UserID) VALUES (@RoleID, @UserID)
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spUserRoleGetAssignedList]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Get user's roles
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spUserRoleGetAssignedList] @UserID int AS

SELECT ur.RoleID, RoleName FROM vts_tbUserRole ur
INNER JOIN vts_tbRole 
	ON vts_tbRole.RoleID = ur.RoleID 
WHERE ur.UserID = @UserID



GO
/****** Object:  StoredProcedure [dbo].[vts_spUserRoleGetUnAssignedList]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Get user's roles
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spUserRoleGetUnAssignedList] @UserID int AS

SELECT vts_tbRole.RoleID, RoleName FROM vts_tbRole 
WHERE vts_tbRole.RoleID not in (SELECT RoleID FROM vts_tbUserRole WHERE UserID = @UserID)



GO
/****** Object:  StoredProcedure [dbo].[vts_spUserRoleUnAssignUser]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Removes a user survey assignement
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spUserRoleUnAssignUser] @RoleID int, @UserID int 
AS
	DELETE FROM vts_tbUserRole WHERE RoleID=@RoleID AND UserID = @UserID



GO
/****** Object:  StoredProcedure [dbo].[vts_spUserSecurityRightGet]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Gets the details of a user
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spUserSecurityRightGet] @UserID int AS

-- get rights, if a role forbit a right forbidden right will win 
SELECT distinct rs.SecurityRightID
FROM vts_tbRoleSecurityRight rs
INNER JOIN vts_tbUserRole ur
	ON ur.RoleID = rs.RoleID
WHERE 
	ur.UserID = @UserID
	/*
	 AND
	((select count(*) FROM vts_tbUserRole WHERE UserID=@UserID AND vts_tbUserRole.RoleID <> ur.RoleID) = 0 OR 
		((select count(*) FROM vts_tbUserRole WHERE UserID=@UserID AND vts_tbUserRole.RoleID <> ur.RoleID) > 0 AND 
	rs.SecurityRightID IN (
		SELECT distinct SecurityRightID 
		FROM vts_tbRoleSecurityRight 
		INNER JOIN vts_tbUserRole
			ON vts_tbUserRole.RoleID = vts_tbRoleSecurityRight.RoleID
		WHERE 
		ur.UserID = @UserID AND 
		vts_tbUserRole.RoleID <> ur.RoleID)))


*/



GO
/****** Object:  StoredProcedure [dbo].[vts_spUserSettingAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spUserSettingAddNew]
			@UserID int,
			@IsAdmin bit,
			@GlobalSurveyAccess bit
AS

INSERT INTO vts_tbUserSetting (UserID, IsAdmin, GlobalSurveyAccess) VALUES (@UserID, @IsAdmin, @GlobalSurveyAccess)



GO
/****** Object:  StoredProcedure [dbo].[vts_spUserSettingGet]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Gets the settings of the user
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spUserSettingGet] @UserID int AS

SELECT UserID, IsAdmin, GlobalSurveyAccess
FROM vts_tbUserSetting
WHERE UserID = @UserID



GO
/****** Object:  StoredProcedure [dbo].[vts_spUserSettingUpdate]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spUserSettingUpdate]
			@UserID int,
			@IsAdmin bit,
			@GlobalSurveyAccess bit
AS

UPDATE vts_tbUserSetting SET
	IsAdmin = @IsAdmin,
	GlobalSurveyAccess = @GlobalSurveyAccess
WHERE UserID = @UserID

if @@RowCount = 0
BEGIN
	exec vts_spUserSettingAddNew @UserID, @IsAdmin, @GlobalSurveyAccess
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spUserSurveyAssignUser]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Assign a new user to the survey
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spUserSurveyAssignUser] @SurveyID int, @UserID int AS

SELECT SurveyID FROM vts_tbUserSurvey WHERE SurveyID = @SurveyID AND UserID = @UserID
IF @@RowCount = 0
BEGIN
	INSERT INTO vts_tbUserSurvey(SurveyID, UserID) VALUES (@SurveyID, @UserID)
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spUserSurveyUnAssignUser]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Removes a user survey assignement
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spUserSurveyUnAssignUser] @SurveyID int, @UserID int 
AS
	DELETE FROM vts_tbUserSurvey WHERE SurveyID=@SurveyID AND UserID = @UserID



GO
/****** Object:  StoredProcedure [dbo].[vts_spUserUpdate]    Script Date: 3/21/2017 08:36:36 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[vts_spUserUpdate]
			@UserID int,
			@UserName NVARCHAR(255),
			@Password NVARCHAR(255),
		    @PasswordSalt NVARCHAR(255),
			@LastName NVARCHAR(255),
			@FirstName NVARCHAR(255),  
			@Email NVARCHAR(255),
			@LastLogin datetime
			
AS

UPDATE vts_tbUser SET
	UserName = @UserName,
	FirstName = @FirstName,
	LastName = @LastName,
	Email = @Email
WHERE UserID = @UserID

if @Password is not null or @PasswordSalt is not null
BEGIN
	UPDATE vts_tbUser SET
		Password = @Password,
	    PasswordSalt = @PasswordSalt
	WHERE UserID = @UserID
END

if @LastLogin is not null
BEGIN
	UPDATE vts_tbUser SET
		LastLogin = @LastLogin
	WHERE UserID = @UserID
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Adds a new voter
/// </summary>
*/

CREATE PROCEDURE [dbo].[vts_spVoterAddNew]
			@SurveyID int,
			@IPSource NVARCHAR(50),
			@VoteDate datetime,
			@StartDate datetime,
			@UID varchar(50),
			@Validated bit = 1, 
			@ResumeUID varchar(50) ,
			@ProgressSaveDate datetime,
			@ResumeAtPageNumber int ,
			@ResumeQuestionNumber int,
			@ResumeHighestPageNumber int,
			@LanguageCode NVARCHAR(50),
			@VoterID int OUTPUT
AS
INSERT INTO vts_tbVoter
	(SurveyID,
	UID,
	IPSource , 
	VoteDate,
	StartDate,
	Validated,
	ResumeUID,
	ProgressSaveDate,
	ResumeAtPageNumber,
	ResumeQuestionNumber,
	ResumeHighestPageNumber,
	LanguageCode)
VALUES
	 (@SurveyID,
	@UID,
	@IPSource,
	@VoteDate,
	@StartDate,
	@Validated,
	@ResumeUID,
	@ProgressSaveDate,
	@ResumeAtPageNumber,
	@ResumeQuestionNumber,
	@ResumeHighestPageNumber,
	@LanguageCode)

set @VoterID = SCOPE_IDENTITY()
if @UID is not null
BEGIN 
	exec vts_spVoterUIDAddNew @VoterID, @UID
END
Update vts_tbSurvey set LastEntryDate = GetDate() WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterAnswersAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Adds a new answer given by a voter
/// </summary>
/// <param Name="@VoterID">
/// Voter's owner of the answer ID
/// </param>
/// <param Name="@AnswerID">
/// Which answer has been answered
/// </param>
/// <param Name="@AnswerText">
/// Text if any entered by the voter
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spVoterAnswersAddNew]
			@VoterID int,
			@AnswerID int,
			@SectionNumber int,
			@AnswerText ntext
AS
INSERT INTO vts_tbVoterAnswers
	(VoterID , 
	AnswerID,
	AnswerText,
	SectionNumber)
VALUES
	 (@VoterID,
	@AnswerID,
	@AnswerText,
	@SectionNumber)



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterAnswersImport]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Adds a new answer given by a voter
/// </summary>
/// <param Name="@VoterID">
/// Voter's owner of the answer ID
/// </param>
/// <param Name="@AnswerID">
/// Which answer has been answered
/// </param>
/// <param Name="@AnswerText">
/// Text if any entered by the voter
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spVoterAnswersImport]
            @SurveyID int,-- Ensure Question is in right survey
			@VoterID int,
			@SectionNumber int,
			@VoterAnswer ntext=null,
		    @Answer NVARCHAR(400)=null,
		    @QuestionText NVARCHAR(4000),
			@QuestionDisplayOrder int,
			@AnswerDisplayOrder int as

declare @AnswerID int,
        @AnswerTypeID int
BEGIN TRY

SELECT @AnswerID=AnswerID,
       @AnswerTypeID=AnswerTypeID
FROM vts_tbAnswer as a inner Join
     vts_tbQuestion as q
ON   q.QuestionID=a.QuestionID
WHERE q.DisplayOrder=@QuestionDisplayOrder
AND   q.SurveyID=@SurveyID
AND   replace(dbo.fnStriptags(q.QuestionText),' ','')
             =replace(@QuestionText,' ','')
AND   a.DisplayOrder=@AnswerDisplayOrder;

IF (@@RowCount =0) 
RAISERROR
    (N'No Question / Answer Combination with %d/%d/%s',
    16, -- Severity.
    1, -- State.
    @QuestionDisplayOrder, -- First substitution argument.
    @AnswerDisplayOrder,
    @QuestionText)  -- Second substitution argument.
        
INSERT INTO vts_tbVoterAnswers
	(VoterID , 
	AnswerID,
	AnswerText,
	SectionNumber)
VALUES
	 (@VoterID,
	@AnswerID,
	CASE WHEN
	  @AnswerTypeID IN(1) THEN NULL
	ELSE @VoterAnswer END,
	@SectionNumber)

END TRY
BEGIN CATCH
DECLARE
@ERROR_SEVERITY int = ERROR_SEVERITY(),
@ERROR_STATE int = ERROR_STATE(),
@ERROR_NUMBER int = ERROR_NUMBER(),
@ERROR_LINE int = ERROR_LINE(),
@ERROR_MESSAGE varchar(245) = ERROR_MESSAGE();
RAISERROR('Msg %d, Line %d: %s',
@ERROR_SEVERITY,
@ERROR_STATE,
@ERROR_NUMBER,
@ERROR_LINE,
@ERROR_MESSAGE);
END CATCH



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterCheckIfIPExists]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Check if the ip has already been registered in a vote 
/// in the expiration time lapse
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey to which the ip belongs
/// </param>
/// <param Name="@IP">
/// IP to check for
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spVoterCheckIfIPExists] 
				@SurveyID int,  
				@IP varchar(50)
AS
SELECT VoterID 
FROM vts_tbVoter 
INNER JOIN vts_tbSurvey
	ON vts_tbSurvey.SurveyID = vts_tbVoter.SurveyID
WHERE 
	vts_tbVoter.SurveyID = @SurveyID AND 
	IPSource = @IP AND
	DateAdd(Minute,IPExpires,VoteDate)>GetDate() ORDER BY VoteDate DESC



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterCheckIfUIDExists]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Check if the UID has already been registered in a vote 
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey to which the ip belongs
/// </param>
/// <param Name="@UID">
/// UID to check for
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spVoterCheckIfUIDExists] 
				@SurveyID int,  
				@UID varchar(40)
AS
SELECT VoterID 
FROM vts_tbVoter 
WHERE 
	SurveyID = @SurveyID AND 
	UID = @UID



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterDelete]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Deletes the voter from the DB
/// </summary>
/// <param Name="@VoterID">
/// The id of the voter to delete
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spVoterDelete] @VoterID int
AS
DELETE FROM vts_tbVoterEmail WHERE VoterID =@VoterID
DELETE FROM vts_tbVoter WHERE VoterID = @VoterID



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterDeleteAll]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Deletes all voters from the DB
/// </summary>
/// <param Name="@SurveyID">
/// Survey id from which to delete the voters
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spVoterDeleteAll] @SurveyID int
AS
DELETE FROM vts_tbVoterEmail WHERE VoterID IN (SELECT VoterID FROM vts_tbVoter WHERE SurveyID = @SurveyID)
DELETE FROM vts_tbVoter WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterDeleteAnswers]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Deletes all answers that a voter gave to the survey question
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spVoterDeleteAnswers]  @VoterID int
AS
	DELETE FROM vts_tbVoterAnswers WHERE VoterID = @VoterID



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterDeletePageAnswers]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Deletes all answers that a voter gave to the questions of the page
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spVoterDeletePageAnswers]   @SurveyID int, @VoterID int,@PageNumber  int 
AS
	DELETE FROM vts_tbVoterAnswers WHERE VoterID = @VoterID AND AnswerID IN (
	SELECT AnswerID FROM vts_tbAnswer WHERE QuestionID in 
		(SELECT QuestionID FROM vts_tbQuestion 
		WHERE SurveyID = @SurveyID AND PageNumber = @PageNumber))



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterDeleteQuestionAnswers]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Deletes all answers that a voter gave to the question
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spVoterDeleteQuestionAnswers]  @VoterID int, @QuestionID int 
AS

	DELETE FROM vts_tbVoterAnswers WHERE VoterID = @VoterID AND AnswerID IN (
	SELECT AnswerID FROM vts_tbAnswer WHERE QuestionID in 
		(SELECT QuestionID FROM vts_tbQuestion 
		WHERE QuestionID = @QuestionID OR ParentQuestionID = @QuestionID))



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterDeleteResumeSession]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Deletes a resume session of a voter from the DB
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spVoterDeleteResumeSession] @SurveyID int, @ResumeUID NVARCHAR(40) 
AS

DECLARE @VoterID int

DELETE FROM vts_tbVoterEmail WHERE VoterID IN (SELECT VoterID FROM vts_tbVoter WHERE SurveyID = @SurveyID AND ResumeUID = @ResumeUID)
DELETE FROM vts_tbVoter WHERE SurveyID = @SurveyID AND ResumeUID = @ResumeUID



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterDeleteUnValidated]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Deletes all saved progress entries that have not 
/// been Validated
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spVoterDeleteUnValidated]
				@SurveyID int
AS

DELETE FROM vts_tbVoter WHERE SurveyID = @SurveyID AND Validated = 0



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterExportCSVData]    Script Date: 3/21/2017 15:14:06 ******/
SET ANSI_NULLS OFF
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
/// Return the data needed to export a CSV  file
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spVoterExportCSVData]
				@SurveyID int,
				@StartDate datetime ,
				@EndDate datetime
AS

SELECT  SUBSTRING(Q.QuestionText,1,20) as QuestionText,Q.QuestionID,
 AnswerID,SelectionModeID,AnswerTypeID, 
SUBSTRING(Q.QuestionText,1,20)+'...'+' | '+ AnswerText   as ColumnHeader ,
AnswerText,
Q.DisplayOrder QuestionDisplayOrder,
Q.QuestionID,
Q.Alias QuestionAlias,
Q.QuestionIDText QuestionIDText,
A.DisplayOrder AnswerDisplayOrder,
A.AnswerID ,
A.AnswerAlias,Q.ParentQuestionID,
	case when q.ParentQuestionID is null then null
	else (select count(*)+1 from vts_tbQuestion q1 
	         where q1.ParentQuestionID=q.ParentQuestionID
	         and   q1.QuestionID<q.QuestionID
	         ) 
	end as roworder,
	case when q.ParentQuestionID is null then null
	else (select QuestionText from vts_tbQuestion q1 
	         where q1.QuestionID=q.ParentQuestionID
	         ) 
	end as ParentQuestionText,
	case when q.ParentQuestionID is null then null
	else (select QuestionIDText from vts_tbQuestion q1 
	         where q1.QuestionID=q.ParentQuestionID
	         ) 
	end as ParentQuestionIDText,
	case when q.ParentQuestionID is null then null
	else (select Alias from vts_tbQuestion q1 
	         where q1.QuestionID=q.ParentQuestionID
	         ) 
	end as ParentQuestionAliastext,
A.AnswerIDText AnswerIDText
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
	V.ContextUserName as userName,
	(SELECT sum(ScorePoint) FROM vts_tbVoter 
		INNER JOIN vts_tbVoterAnswers
			ON vts_tbVoterAnswers.VoterID = vts_tbVoter.VoterID
		INNER JOIN vts_tbAnswer
			ON vts_tbAnswer.AnswerID = vts_tbVoterAnswers.AnswerID
		WHERE vts_tbVoter.VoterID = V.VoterID) AS Score,
	E.Email as Email
	FROM vts_tbVoter V

		LEFT JOIN vts_tbVoterEmail 
		ON V.VoterID = vts_tbVoterEmail.VoterID
	LEFT JOIN vts_tbEmail E
		ON E.EmailID = vts_tbVoterEmail.EmailID

	WHERE 
		V.SurveyID = @SurveyID AND
		V.Validated <> 0 AND
		DATEDIFF (d,@StartDate,V.VoteDate) >= 0 AND DATEDIFF (d,@EndDate,V.VoteDate) <= 0
	ORDER BY V.VoterID DESC

SELECT
	V.VoterID,
	VA.AnswerID,
	SectionNumber,
	VA.AnswerText,
	AnswerTypeID,
	SelectionModeID,
	Q.QuestionID,
	A.AnswerText AnswerAnswerText,
	A.DisplayOrder AnswerDisplayOrder,
A.AnswerAlias,
A.AnswerIDText AnswerIDAlias
FROM vts_tbVoterAnswers VA
INNER JOIN vts_tbVoter V
	ON V.VoterID = VA.VoterID
INNER JOIN vts_tbAnswer A
    ON VA.AnswerID=A.AnswerID
INNER JOIN vts_tbQuestion Q
     ON A.QuestionID=Q.QuestionID
WHERE 
	V.SurveyID = @SurveyID AND
	V.Validated <> 0 AND
	DATEDIFF (d,@StartDate,V.VoteDate) >= 0 AND DATEDIFF (d,@EndDate,V.VoteDate) <= 0
ORDER BY V.VoterID DESC



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterFilter]    Script Date: 19-8-2014 22:01:40 ******/
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

*/
CREATE PROCEDURE [dbo].[vts_spVoterFilter] 
			@FilterID int, 
			@QuestionID  int,
			@SortOrder varchar(4) = 'ANS',
			@StartDate datetime,
			@EndDate datetime,
			@LanguageCode NVARCHAR(50)
AS
SET NOCOUNT ON
CREATE TABLE #FilteredVoters (VoterID int )
DECLARE @FilterRuleID int,
		@FilterAnswerID int,
		@FilterQuestionID int,
		@SurveyID int,
		@TextFilter NVARCHAR(4000),
		@LogicalOperatorTypeID  int
SELECT @LogicalOperatorTypeID = vts_tbFilter.LogicalOperatorTypeID FROM vts_tbFilter WHERE FilterID = @FilterID
-- Get the filters
DECLARE GetRules CURSOR LOCAL READ_ONLY  FOR 
SELECT 
	vts_tbFilterRule.FilterRuleID, vts_tbFilterRule.AnswerID, 
	vts_tbFilterRule.QuestionID, vts_tbFilter.SurveyID, 
	vts_tbFilterRule.TextFilter 
FROM vts_tbFilterRule 
INNER JOIN vts_tbFilter 
	ON vts_tbFilterRule.FilterID = vts_tbFilter.FilterID 
WHERE vts_tbFilter.FilterID=@FilterID ORDER BY vts_tbFilterRule.FilterRuleID
-- CHECKING OF AnswerID = -1 THEN SELECT VOTERS WITH ANY ANSWER 
OPEN GetRules
FETCH NEXT FROM GetRules INTO @FilterRuleID,  @FilterAnswerID, @FilterQuestionID, @SurveyID, @TextFilter
-- Apply the first filter rule
IF @FilterAnswerID is NULL AND @FilterRuleID is not NULL
BEGIN
	-- get voters who answered any answer of the question
	INSERT INTO #FilteredVoters 
		SELECT DISTINCT vts_tbVoterAnswers.VoterID 
		FROM (SELECT AnswerID FROM vts_tbAnswer WHERE QuestionID=@FilterQuestionID) 
		AS AllAnswers, vts_tbVoterAnswers
		INNER JOIN vts_tbVoter 
			ON vts_tbVoter.VoterID =  vts_tbVoterAnswers.VoterID 
		WHERE 
			vts_tbVoter.Validated<>0 AND  
			vts_tbVoterAnswers.AnswerID = AllAnswers.AnswerID AND 
			vts_tbVoterAnswers.VoterID NOT IN (SELECT VoterID FROM #FilteredVoters)
END 
ELSE
	IF @TextFilter is NULL
	BEGIN
    -- no text filter setup    
		INSERT INTO #FilteredVoters 
		SELECT vts_tbVoterAnswers.VoterID 
		FROM vts_tbVoterAnswers
		INNER JOIN vts_tbVoter 
			ON vts_tbVoter.VoterID =  vts_tbVoterAnswers.VoterID 
		WHERE 
			vts_tbVoter.Validated<>0 AND  
			vts_tbVoterAnswers.AnswerID = @FilterAnswerID 
	END    
	ELSE
    -- text filter setup
		INSERT INTO #FilteredVoters 
		SELECT vts_tbVoterAnswers.VoterID 
		FROM vts_tbVoterAnswers
		INNER JOIN vts_tbVoter 
			ON vts_tbVoter.VoterID =  vts_tbVoterAnswers.VoterID 
		WHERE 
			vts_tbVoter.Validated<>0 AND  
			vts_tbVoterAnswers.AnswerID = @FilterAnswerID AND 
			vts_tbVoterAnswers.AnswerText LIKE  '%'+replace(@TextFilter,' ','%')+'%'  
-- Retrieve next rule
FETCH NEXT FROM GetRules INTO @FilterRuleID,  @FilterAnswerID, @FilterQuestionID, @SurveyID, @TextFilter
-- Parse all remaining rules and apply logical operation
WHILE @@FETCH_STATUS = 0
BEGIN      
	IF @LogicalOperatorTypeID = 1     
	-- OR Operation
	BEGIN
		IF @FilterAnswerID is NULL  AND @FilterRuleID is not NULL
		BEGIN
			print 'get all'
			-- get voters who answered any answer of the question
			INSERT INTO #FilteredVoters 
				SELECT DISTINCT vts_tbVoterAnswers.VoterID 
				FROM (SELECT AnswerID FROM vts_tbAnswer WHERE QuestionID=@FilterQuestionID) 
				AS AllAnswers, vts_tbVoterAnswers
				INNER JOIN vts_tbVoter 
					ON vts_tbVoter.VoterID =  vts_tbVoterAnswers.VoterID 
				WHERE 
					vts_tbVoter.Validated<>0 AND  
					vts_tbVoterAnswers.AnswerID = AllAnswers.AnswerID AND 
					vts_tbVoterAnswers.VoterID NOT IN (SELECT VoterID FROM #FilteredVoters)
          END 
       ELSE
			IF @TextFilter is NULL
			BEGIN
			-- no text filter setup    
			--  Insert answers type fields
			INSERT INTO #FilteredVoters 
				SELECT vts_tbVoterAnswers.VoterID 
				FROM vts_tbVoterAnswers
				INNER JOIN vts_tbVoter 
					ON vts_tbVoter.VoterID =  vts_tbVoterAnswers.VoterID 
				WHERE 
					vts_tbVoter.Validated<>0 AND  
					vts_tbVoterAnswers.AnswerID = @FilterAnswerID AND 
					vts_tbVoterAnswers.VoterID NOT IN (SELECT VoterID FROM #FilteredVoters)
			END    
			ELSE
			-- text filter setup
			INSERT INTO #FilteredVoters 
				SELECT vts_tbVoterAnswers.VoterID 
				FROM vts_tbVoterAnswers
				INNER JOIN vts_tbVoter 
					ON vts_tbVoter.VoterID =  vts_tbVoterAnswers.VoterID 
				WHERE 
					vts_tbVoter.Validated<>0 AND  
					vts_tbVoterAnswers.AnswerID = @FilterAnswerID AND 
					vts_tbVoterAnswers.AnswerText LIKE  '%'+replace(@TextFilter,' ','%')+'%'  AND
					vts_tbVoterAnswers.VoterID NOT IN (SELECT VoterID FROM #FilteredVoters)
      END 
	-- AND Operation
	ELSE
		IF @FilterAnswerID is NULL  AND @FilterRuleID is not NULL
		BEGIN 
		-- remove voters who did not have answered any answer of the question */
             DELETE FROM #FilteredVoters WHERE 
				VoterID NOT IN (
				SELECT DISTINCT vts_tbVoterAnswers.VoterID 
				FROM (SELECT AnswerID FROM vts_tbAnswer WHERE QuestionID=@FilterQuestionID) AS AllAnswers, vts_tbVoterAnswers  WHERE vts_tbVoterAnswers.AnswerID = AllAnswers.AnswerID)
            END 
		ELSE
		-- remove voters who did not have answered the right answer of the question */
		IF @TextFilter is NULL
		BEGIN
		-- no filter setup
			DELETE FROM #FilteredVoters WHERE 
				VoterID NOT IN (SELECT VoterID FROM vts_tbVoterAnswers WHERE vts_tbVoterAnswers.AnswerID = @FilterAnswerID)
		END
        ELSE           
		-- filter on
			DELETE FROM #FilteredVoters WHERE 
				VoterID NOT IN (SELECT VoterID FROM vts_tbVoterAnswers WHERE vts_tbVoterAnswers.AnswerID = @FilterAnswerID AND vts_tbVoterAnswers.AnswerText LIKE '%'+replace(@TextFilter,' ','%')+'%')
	FETCH NEXT FROM GetRules INTO @FilterRuleID,  @FilterAnswerID, @FilterQuestionID, @SurveyID, @TextFilter   
END 
CLOSE GetRules
DEALLOCATE GetRules
SELECT 
	AnswerID,
	AnswerText,
	 (select count(*) FROM vts_tbVoterAnswers 
	INNER JOIN #FilteredVoters ON vts_tbVoterAnswers.VoterID = #FilteredVoters.VoterID
	INNER JOIN vts_tbVoter ON vts_tbVoterAnswers.VoterID = vts_tbVoter.VoterID 
			WHERE Validated<>0 AND AnswerID = vts_tbAnswer.AnswerID AND
			DATEDIFF (d,@StartDate,vts_tbVoter.VoteDate) >= 0 AND DATEDIFF (d,@EndDate,vts_tbVoter.VoteDate) <= 0
			AND (vts_tbVoter.LanguageCode = @LanguageCode OR
			 ((@LanguageCode is null OR @LanguageCode = '') AND (LanguageCode is null OR LanguageCode ='')) OR
			(@LanguageCode = '-1' AND (LanguageCode is not null OR LanguageCode is null)))

	) as VoterCount,
	vts_tbAnswer.QuestionID,
	vts_tbAnswer.AnswerTypeID,
	SelectionModeID,
	TypeMode,
	RatePart
FROM vts_tbAnswer
INNER JOIN vts_tbQuestion
	ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID
INNER JOIN vts_tbAnswerType
	ON vts_tbAnswerType.AnswerTypeID = vts_tbAnswer.AnswerTypeID
WHERE 
	vts_tbQuestion.QuestionID=@QuestionID OR vts_tbQuestion.ParentQuestionID = @QuestionID
ORDER BY 
		case when @SortOrder = 'ANS' then vts_tbAnswer.DisplayOrder end ,
		case when @SortOrder = 'ASC' then (select count(*) FROM vts_tbVoterAnswers INNER JOIN #FilteredVoters ON vts_tbVoterAnswers.VoterID = #FilteredVoters.VoterID WHERE AnswerID = vts_tbAnswer.AnswerID) end ASC ,
		case when @SortOrder = 'DESC' then  (select count(*) FROM vts_tbVoterAnswers INNER JOIN #FilteredVoters ON vts_tbVoterAnswers.VoterID = #FilteredVoters.VoterID WHERE AnswerID = vts_tbAnswer.AnswerID) end DESC



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterForExport]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spVoterForExport] 
					@SurveyID int,
					@StartDate datetime,
					@EndDate datetime 

AS
	SELECT 
		vts_tbVoter.VoterID,
		SurveyID,
		VoteDate,
		IPSource,
		StartDate,
		Email,
		ContextUserName,
		LanguageCode
	FROM vts_tbVoter
	LEFT JOIN vts_tbVoterEmail 
		ON vts_tbVoter.VoterID = vts_tbVoterEmail.VoterID
	LEFT JOIN vts_tbEmail
		ON vts_tbEmail.EmailID = vts_tbVoterEmail.EmailID
	WHERE 
		vts_tbVoter.SurveyID = @SurveyID AND
		DATEDIFF (d,@StartDate,vts_tbVoter.VoteDate) >= 0 AND DATEDIFF (d,@EndDate,vts_tbVoter.VoteDate) <= 0

	SELECT DISTINCT va.VoterID, QuestionText, q.QuestionID,
	                q.QuestionIDText,q.Alias QuestionAlias
	FROM vts_tbVoterAnswers va	
	INNER JOIN vts_tbAnswer a
		ON a.AnswerID = va.AnswerID 
	INNER JOIN vts_tbQuestion q
		ON q.QuestionID = a.QuestionID
	INNER JOIN vts_tbVoter v
		ON v.VoterID = va.VoterID
	WHERE 
		v.SurveyID = @SurveyID AND
		DATEDIFF (d,@StartDate,V.VoteDate) >= 0 AND DATEDIFF (d,@EndDate,V.VoteDate) <= 0


	
	SELECT va.VoterID, va.SectionNumber, va.AnswerID, va.AnswerText as VoterAnswer, 
	q.QuestionID, q.DisplayOrder as QuestionDisplayOrder,
	a.AnswerText as Answer,a.DisplayOrder as AnswerDisplayOrder,
	A.AnswerAlias
	FROM vts_tbVoterAnswers va	
	INNER JOIN vts_tbAnswer a
		ON a.AnswerID = va.AnswerID 
	INNER JOIN vts_tbQuestion q
		ON q.QuestionID = a.QuestionID
	INNER JOIN vts_tbVoter v
		ON v.VoterID = va.VoterID
	WHERE 
		v.SurveyID = @SurveyID AND
		DATEDIFF (d,@StartDate,V.VoteDate) >= 0 AND DATEDIFF (d,@EndDate,V.VoteDate) <= 0



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterGetAnswers]    Script Date: 19-8-2014 22:01:40 ******/
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
*/
CREATE PROCEDURE [dbo].[vts_spVoterGetAnswers] @VoterID int
AS
	SELECT 
		vts_tbVoter.VoterID,
		SurveyID,
		VoteDate,
		IPSource,
		Validated,
		StartDate,
		Email,
		ResumeUID,
		ProgressSaveDate,
		ResumeAtPageNumber,
		ResumeQuestionNumber,
		ContextUserName,
		LanguageCode
	FROM vts_tbVoter
	LEFT JOIN vts_tbVoterEmail 
		ON vts_tbVoter.VoterID = vts_tbVoterEmail.VoterID
	LEFT JOIN vts_tbEmail
		ON vts_tbEmail.EmailID = vts_tbVoterEmail.EmailID
	WHERE vts_tbVoter.VoterID = @VoterID
	
	SELECT va.VoterID, va.AnswerID, va.SectionNumber, va.AnswerText, a.QuestionID, ast.TypeMode
	FROM vts_tbVoterAnswers va	
	INNER JOIN vts_tbAnswer a
		ON a.AnswerID = va.AnswerID
	INNER JOIN vts_tbAnswerType ast
		ON ast.AnswerTypeID = a.AnswerTypeID
	WHERE VoterID = @VoterID



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterGetByUserName]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Retrieves the voter id which has the given user Name from the DB
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spVoterGetByUserName] @SurveyID int,  @UserName NVARCHAR(255)
					
AS
SELECT VoterID FROM vts_tbVoter WHERE SurveyID = @SurveyID AND ContextUserName = @UserName



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterGetDailyStat]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Returns the number of voters for a given date
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spVoterGetDailyStat] @SurveyID int, @StatDay datetime AS

SELECT count(*) as TotalCount
FROM vts_tbVoter V
WHERE V.SurveyID = @SurveyID AND
	DATEDIFF (d, @StatDay,V.VoteDate) = 0



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterGetFullPivot]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Pivots the voter  entries into a column / row format
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey to pivot answers
/// </param>
/// <param Name="@CurrentPage">
/// Current page number
/// </param>
/// <param Name="@PageSize">
/// Page size
/// </param>
/// <return>
/// returns the paged pivoted resultset
/// </return>
*/
CREATE PROCEDURE [dbo].[vts_spVoterGetFullPivot]
				@SurveyID int,
				@CurrentPage int = 1,
				@PageSize int=10,
				@StartDate datetime,
				@EndDate datetime 
AS
DECLARE @TotalRecords int
CREATE TABLE #VoterEntries (VoterID int NOT NULL, EndDate dateTime, StartDate datetime, IP varchar(50), Score int)
-- Get voter range
INSERT INTO #VoterEntries (VoterID, EndDate, StartDate, IP, Score)  
	EXEC vts_spVoterGetPaged @SurveyID, @CurrentPage, @PageSize, @StartDate, @EndDate, @TotalRecords output
-- Start pivot
DECLARE @AnswerText NVARCHAR(4000)
DECLARE @QuestionText NVARCHAR(4000)
DECLARE @AnswerID varchar(16)
DECLARE @VoterID varchar(16)
DECLARE @BuildColumnSQL varchar(4000)
DECLARE @UpdateVotersRowSQL varchar(4000)
-- Get the answers text to generate the column
DECLARE AnswerColumnCursor  CURSOR FOR
	SELECT AnswerText, AnswerID, left(vts_tbQuestion.QuestionText, 64) FROM vts_tbAnswer
	INNER JOIN vts_tbQuestion 
		ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID
	INNER JOIN vts_tbAnswerType
		ON vts_tbAnswerType.AnswerTypeID = vts_tbAnswer.AnswerTypeID 	
	WHERE SurveyID = @SurveyID 
	ORDER BY vts_tbQuestion.DisplayOrder, vts_tbQuestion.QuestionID, vts_tbAnswer.DisplayOrder
OPEN AnswerColumnCursor
FETCH AnswerColumnCursor INTO @AnswerText, @AnswerID, @QuestionText
WHILE @@FETCH_STATUS = 0
BEGIN
	-- creates the new column
	SET @BuildColumnSQL = N'ALTER TABLE #VoterEntries ADD ['+@QuestionText+'_'+@AnswerText+'] NVARCHAR(4000)'
	EXEC (@BuildColumnSQL)
	-- Assign voters entry to the column
	SET @UpdateVotersRowSQL =N'UPDATE #VoterEntries SET ['+@QuestionText+'_'+@AnswerText+'] = (SELECT AnswerText FROM vts_tbVoterAnswers (nolock) WHERE AnswerID='+@AnswerID+' AND vts_tbVoterAnswers.VoterID=Voters.VoterID) FROM (SELECT VoterID FROM #VoterEntries (nolock)) as Voters WHERE #VoterEntries.VoterID=Voters.VoterID'
	EXEC (@UpdateVotersRowSQL)
	-- Assign voters choice
	SET @UpdateVotersRowSQL =N'UPDATE #VoterEntries SET ['+@QuestionText+'_'+@AnswerText+'] = (SELECT ''1'' FROM vts_tbVoterAnswers (nolock) INNER JOIN vts_tbAnswer ON vts_tbVoterAnswers.AnswerID = vts_tbAnswer.AnswerID WHERE vts_tbVoterAnswers.AnswerText is null AND vts_tbVoterAnswers.AnswerID='+@AnswerID+' AND vts_tbVoterAnswers.VoterID=Voters.VoterID) FROM (SELECT VoterID FROM #VoterEntries (nolock)) as Voters WHERE #VoterEntries.VoterID=Voters.VoterID AND #VoterEntries.['+@QuestionText+'_'+@AnswerText+'] is  null'
	EXEC (@UpdateVotersRowSQL)
	FETCH AnswerColumnCursor INTO @AnswerText, @AnswerID, @QuestionText
END
CLOSE AnswerColumnCursor
DEALLOCATE AnswerColumnCursor
SELECT *  FROM #VoterEntries
DROP TABLE #VoterEntries



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterGetMonthlyStats]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spVoterGetMonthlyStats] 
				@SurveyID int, 
				@Month int, 
				@Year int 
AS
	SELECT
		 count(*) as VoterCount,
		 CAST(ROUND(CAST(VoteDate AS FLOAT),0,1) AS DATETIME) as VotesDate
	FROM vts_tbVoter 
	WHERE 
		SurveyID=@SurveyID AND
		Month(VoteDate) = @Month AND 
		Year(VoteDate) = @Year AND
		Validated<>0
	GROUP BY CAST(ROUND(CAST(VoteDate AS FLOAT),0,1) AS DATETIME)



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterGetPaged]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Return a paged results of available voters
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey to pivot answers
/// </param>
/// <param Name="@CurrentPage">
/// Current page number
/// </param>
/// <param Name="@PageSize">
/// Page size
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spVoterGetPaged]
				@SurveyID int,
				@CurrentPage int = 1,
				@PageSize int=10,
				@StartDate datetime ,
				@EndDate datetime,
				@TotalRecords int OUTPUT
AS
-- Turn off count return.
Set NOCOUNT On
-- Declare variables.
DECLARE @FirstRec int
DECLARE @LastRec int
-- Initialize variables.
SET @FirstRec = (@CurrentPage - 1) * @PageSize
SET @LastRec = (@CurrentPage * @PageSize + 1)
-- Create a temp table to hold the current page of data
-- Add an ID column to count the records
CREATE TABLE #TempTable (RowID int IDENTITY PRIMARY KEY, VoterID int NOT NULL, [Date] dateTime, StartDate datetime, IP varchar(50), Score int)
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
		DATEDIFF (d,@StartDate,V.VoteDate) >= 0 AND DATEDIFF (d,@EndDate,V.VoteDate) <= 0
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
		RowID > @FirstRec AND
		RowID < @LastRec
END
DROP TABLE #TempTable



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterGetPivotTextEntries]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Pivots the voter  entries into a column / row format
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey to pivot answers
/// </param>
/// <param Name="@CurrentPage">
/// Current page number
/// </param>
/// <param Name="@PageSize">
/// Page size
/// </param>
/// <return>
/// returns the paged pivoted resultset
/// </return>
*/
CREATE PROCEDURE [dbo].[vts_spVoterGetPivotTextEntries] 
				@SurveyID int,
				@CurrentPage int = 1,
				@PageSize int=10,
				@StartDate datetime,
				@EndDate datetime
AS
DECLARE @TotalRecords int
CREATE TABLE #VoterEntries (VoterID int NOT NULL, [Date] dateTime, StartDate datetime, IP varchar(50), Score int)
-- Get voter range
INSERT INTO #VoterEntries (VoterID, [Date], StartDate, IP, Score)  
	EXEC vts_spVoterGetPaged @SurveyID, @CurrentPage, @PageSize, @StartDate, @EndDate, @TotalRecords output
-- Start pivot
DECLARE @AnswerText NVARCHAR(4000)
DECLARE @AnswerID varchar(16)
DECLARE @VoterID varchar(16)
DECLARE @BuildColumnSQL varchar(4000)
DECLARE @UpdateVotersRowSQL varchar(4000)
-- Get the fields to generate the column
DECLARE AnswerColumnCursor  CURSOR FOR
	SELECT AnswerText, AnswerID FROM vts_tbAnswer
	INNER JOIN vts_tbQuestion 
		ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID
	INNER JOIN vts_tbAnswerType
		ON vts_tbAnswerType.AnswerTypeID = vts_tbAnswer.AnswerTypeID 
	WHERE SurveyID = @SurveyID AND (TypeMode & 2 = 2 OR TypeMode & 8 =8 OR TypeMode & 4 =4)
	ORDER BY vts_tbQuestion.DisplayOrder, vts_tbQuestion.QuestionID, vts_tbAnswer.DisplayOrder
OPEN AnswerColumnCursor
FETCH AnswerColumnCursor INTO @AnswerText, @AnswerID
WHILE @@FETCH_STATUS = 0
BEGIN
	-- creates the new column
	SET @BuildColumnSQL = N'ALTER TABLE #VoterEntries ADD ['+@AnswerText+'_'+@AnswerID+'] NVARCHAR(4000)'
	EXEC (@BuildColumnSQL)
	-- Assign voters entry to the column
	SET @UpdateVotersRowSQL =N'UPDATE #VoterEntries SET ['+@AnswerText+'_'+@AnswerID+'] = (SELECT SUBSTRING(AnswerText,1, 40)  as AnswerText FROM vts_tbVoterAnswers  WHERE AnswerID='+@AnswerID+' AND SectionNumber=0 AND vts_tbVoterAnswers.VoterID=Voters.VoterID) FROM (SELECT VoterID FROM #VoterEntries (nolock)) as Voters WHERE #VoterEntries.VoterID=Voters.VoterID'
	EXEC (@UpdateVotersRowSQL)
	FETCH AnswerColumnCursor INTO @AnswerText, @AnswerID
END
CLOSE AnswerColumnCursor
DEALLOCATE AnswerColumnCursor
SELECT *, TotalRecords =@TotalRecords FROM #VoterEntries
DROP TABLE #VoterEntries



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterGetUnValidatedCount]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
///  returns the number of unValidated entries that 
/// have been saved
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spVoterGetUnValidatedCount]
				@SurveyID int
AS

SELECT count(VoterID) as UnValidatedCount FROM vts_tbVoter
WHERE SurveyID = @SurveyID AND Validated = 0



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterImport]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Adds a new voter
/// </summary>
*/

CREATE PROCEDURE [dbo].[vts_spVoterImport]
			@SurveyID int,
			@IPSource NVARCHAR(50),
			@VoteDate datetime,
			@StartDate datetime,
			@UID varchar(50)=null,
			@Validated bit =1, 
			@ResumeUID varchar(50) =null,
			@ProgressSaveDate datetime=null,
			@ResumeAtPageNumber int =null,
			@ResumeQuestionNumber int=null,
			@ResumeHighestPageNumber int=null,
			@LanguageCode NVARCHAR(50)=null,
			@VoterID int OUTPUT
AS
INSERT INTO vts_tbVoter
	(SurveyID,
	UID,
	IPSource , 
	VoteDate,
	StartDate,
	Validated,
	ResumeUID,
	ProgressSaveDate,
	ResumeAtPageNumber,
	ResumeQuestionNumber,
	ResumeHighestPageNumber,
	LanguageCode)
VALUES
	 (@SurveyID,
	@UID,
	@IPSource,
	@VoteDate,
	@StartDate,
	@Validated,
	@ResumeUID,
	@ProgressSaveDate,
	@ResumeAtPageNumber,
	@ResumeQuestionNumber,
	@ResumeHighestPageNumber,
	@LanguageCode)

set @VoterID = SCOPE_IDENTITY()

GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterInvitationAnsweredGetAll]    Script Date: 04/01/2015 20:59:48 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
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
///  Get pending invited Emails for the survey
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spVoterInvitationAnsweredGetAll]
				@SurveyID int,
				@CurrentPage int = 1,
				@PageSize int=10,
				@TotalRecords int OUTPUT
AS
-- Turn off count return.
Set NOCOUNT On
-- Declare variables.
DECLARE @FirstRec int
DECLARE @LastRec int
-- Initialize variables.
SET @FirstRec = @PageSize*@CurrentPage
SET @LastRec= @FirstRec+@PageSize + 1
-- Create a temp table to hold the current page of data
-- Add an ID column to count the records
CREATE TABLE #TempTable (RowID int IDENTITY PRIMARY KEY, SurveyID int NOT NULL, VoterID int NOT NULL, Email varchar(150), VoteDate DateTime)
--Fill the temp table with the reminders
INSERT INTO #TempTable (SurveyID, VoterID, Email, VoteDate)
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
	RowID > @FirstRec AND
	RowID < @LastRec
DROP TABLE #TempTable


GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterInvitationQueueAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
///  Insert the given Email in the invitation queue
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spVoterInvitationQueueAddNew]
				@SurveyID int,
				@Email varchar(150),
				@AnonymousEntry bit,
				@UID varchar(50)
AS
DECLARE @EmailID int
exec vts_spEmailAddNew @Email, @EmailID out
INSERT INTO vts_tbInvitationQueue(SurveyID, EmailID, UID, AnonymousEntry) VALUES (@SurveyID, @EmailID, @UID, @AnonymousEntry)



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterInvitationQueueDelete]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
///  Delete given invitiation
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spVoterInvitationQueueDelete]
				@SurveyID int,
				@EmailID int
AS
DELETE FROM vts_tbInvitationQueue WHERE SurveyID = @SurveyID AND EmailID = @EmailID



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterInvitationQueueDeleteByEmail]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
///  Delete given invitiation
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spVoterInvitationQueueDeleteByEmail]
				@SurveyID int,
				@Email NVARCHAR(155)
AS
DELETE FROM vts_tbInvitationQueue 
WHERE SurveyID = @SurveyID AND EmailID IN (SELECT EmailID FROM vts_tbEmail WHERE Email = @Email)

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
Set NOCOUNT On
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
CREATE TABLE #TempTable (RowID int IDENTITY PRIMARY KEY, SurveyID int NOT NULL, EmailID int NOT NULL, Email varchar(150), AnonymousEntry bit, UID varchar(50), RequestDate DateTime)
--Fill the temp table with the reminders
INSERT INTO #TempTable (SurveyID, EmailID, Email, AnonymousEntry, UID, RequestDate)
	SELECT SurveyID, vts_tbInvitationQueue.EmailID, Email, AnonymousEntry, UID, RequestDate
	FROM vts_tbInvitationQueue
	INNER JOIN vts_tbEmail
		ON vts_tbEmail.EmailID = vts_tbInvitationQueue.EmailID
	WHERE SurveyID =@SurveyID
	ORDER BY RequestDate DESC
SELECT SurveyID, EmailID, Email, AnonymousEntry, UID, RequestDate
FROM #TempTable
WHERE 
	RowID > @FirstRec AND
	RowID < @LastRec
DROP TABLE #TempTable

GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterInvitationQueueGetUID]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
///  Insert the given Email in the invitation queue
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spVoterInvitationQueueGetUID]
				@SurveyID int,
				@Email varchar(150)
AS
SELECT UID FROM vts_tbInvitationQueue 
INNER JOIN vts_tbEmail ON vts_tbEmail.EmailID = vts_tbInvitationQueue.EmailID  
WHERE vts_tbEmail.Email = @Email AND SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterResumeSession]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Returns the voter info and his answers for the 
/// session to resume
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spVoterResumeSession] 
			@SurveyID int, 
			@ResumeUID NVARCHAR(40)
AS

DECLARE @VoterID int

	SELECT @VoterID = VoterID
	FROM vts_tbVoter
	WHERE SurveyID  = @SurveyID AND ResumeUID = @ResumeUID AND Validated = 0

	if @@RowCount > 0
	BEGIN
		exec vts_spVoterGetAnswers @VoterID
	END



GO
/****** Object:  StoredProcedure [dbo].[vts_spVotersDeleteForSurvey]    Script Date: 19-8-2014 22:01:40 ******/
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
/// Delete all  the voters
/// </summary>
/// <param Name="@SurveyID">
/// ID of the survey that will reset its results
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spVotersDeleteForSurvey] @SurveyID int AS

DELETE fROM vts_tbFile WHERE FileID IN (
	SELECT FileID FROM vts_tbFile INNER JOIN vts_tbVoterAnswers ON 
		AnswerText like GroupGuid
	INNER JOIN vts_tbVoter ON
		vts_tbVoter.VoterID = vts_tbVoterAnswers.VoterID
	WHERE vts_tbVoter.SurveyID = @SurveyID)
DELETE FROM vts_tbVoter WHERE SurveyID = @SurveyID



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterUIDAddNew]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
/// Removes the given UID from the queue and 
/// Insert the corresponding Email for the voter if 
/// its state is not anonymous
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spVoterUIDAddNew] 
				@VoterID int,
				@UID  varchar(40)
AS
DECLARE @EmailID int
SELECT @EmailID = EmailID 
FROM vts_tbInvitationQueue 
WHERE UID = @UID AND AnonymousEntry = 0
IF @@RowCount <> 0
BEGIN 
	INSERT INTO vts_tbVoterEmail(VoterID, EmailID) VALUES (@VoterID, @EmailID)
	UPDATE vts_tbVoter SET UID = @UID WHERE VoterID = @VoterID 
END
DELETE FROM vts_tbInvitationQueue WHERE UID = @UID



GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterUpdateUserName]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
/// Updates the voter id with the given user Name
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spVoterUpdateUserName] @VoterID int,  @UserName NVARCHAR(255)
					
AS
UPDATE vts_tbVoter SET ContextUserName = @UserName WHERE VoterID = @VoterID



GO
/****** Object:  StoredProcedure [dbo].[vts_spWebSecurityAddInsAddForSurvey]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
///  Add a new addin to the survey 
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spWebSecurityAddInsAddForSurvey] @SurveyID int, @AddInID int, @AddInOrder int
AS
BEGIN TRANSACTION AddAddIn
UPDATE vts_tbSurveyWebSecurity
SET AddInOrder = AddInOrder + 1 
WHERE SurveyID = @SurveyID AND AddInOrder >= @AddInOrder
INSERT INTO vts_tbSurveyWebSecurity (WebSecurityAddInID, SurveyID, AddInOrder, Disabled) VALUES (@AddInID, @SurveyID, @AddInOrder, 0)
COMMIT TRANSACTION AddAddIn



GO
/****** Object:  StoredProcedure [dbo].[vts_spWebSecurityAddInsDeleteForSurvey]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
///  Deletes an addin from the survey 
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spWebSecurityAddInsDeleteForSurvey] @SurveyID int, @AddInID int
AS
DECLARE @AddInOrder int	
BEGIN TRANSACTION DeleteAddIn
-- Retrieve the current Display order
SELECT @AddInOrder  = AddInOrder FROM vts_tbSurveyWebSecurity WHERE SurveyID = @SurveyID AND WebSecurityAddInID = @AddInID
-- Deletes the addin from the survey
DELETE FROM vts_tbSurveyWebSecurity WHERE SurveyID = @SurveyID AND WebSecurityAddInID = @AddInID
-- Updates the addin Display order
UPDATE vts_tbSurveyWebSecurity
SET AddInOrder   = AddInOrder   - 1 
WHERE 
	SurveyID = @SurveyID AND
	AddInOrder >= @AddInOrder 
COMMIT TRANSACTION DeleteAddIn



GO
/****** Object:  StoredProcedure [dbo].[vts_spWebSecurityAddInsDisableForSurvey]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
///  Disables an addin from the survey 
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spWebSecurityAddInsDisableForSurvey] @SurveyID int, @AddInID int
AS
-- Updates the addin status
UPDATE vts_tbSurveyWebSecurity
SET Disabled   = 1 
WHERE 
	SurveyID = @SurveyID AND WebSecurityAddInID = @AddInID



GO
/****** Object:  StoredProcedure [dbo].[vts_spWebSecurityAddInsEnableForSurvey]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
///  Enables an addin from the survey 
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spWebSecurityAddInsEnableForSurvey] @SurveyID int, @AddInID int
AS
-- Updates the addin status
UPDATE vts_tbSurveyWebSecurity
SET Disabled   = 0 
WHERE 
	SurveyID = @SurveyID AND WebSecurityAddInID = @AddInID



GO
/****** Object:  StoredProcedure [dbo].[vts_spWebSecurityAddInsGetAll]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spWebSecurityAddInsGetAll] @SurveyID int
AS
SELECT 
	vts_tbWebSecurityAddIn.WebSecurityAddInID,
	SurveyID,
	Disabled,
	BuiltIn,
	Description,
	TypeNameSpace,
	TypeAssembly,
	TypeMode,
	AddInOrder
FROM vts_tbWebSecurityAddIn
INNER JOIN vts_tbSurveyWebSecurity
	ON vts_tbWebSecurityAddIn.WebSecurityAddInID = vts_tbSurveyWebSecurity.WebSecurityAddInID
WHERE SurveyID = @SurveyID  ORDER BY AddInOrder



GO
/****** Object:  StoredProcedure [dbo].[vts_spWebSecurityAddInsGetAvailableList]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spWebSecurityAddInsGetAvailableList] @SurveyID int
AS
SELECT WebSecurityAddInID, Description
FROM vts_tbWebSecurityAddIn
WHERE WebSecurityAddInID NOT IN (Select WebSecurityAddInID FROM vts_tbSurveyWebSecurity WHERE SurveyID = @SurveyID)



GO
/****** Object:  StoredProcedure [dbo].[vts_spWebSecurityAddInsGetEnabled]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spWebSecurityAddInsGetEnabled] @SurveyID int
AS
SELECT 
	vts_tbWebSecurityAddIn.WebSecurityAddInID,
	SurveyID,
	Disabled,
	BuiltIn,
	Description,
	TypeNameSpace,
	TypeAssembly,
	TypeMode,
	AddInOrder
FROM vts_tbWebSecurityAddIn
INNER JOIN vts_tbSurveyWebSecurity
	ON vts_tbWebSecurityAddIn.WebSecurityAddInID = vts_tbSurveyWebSecurity.WebSecurityAddInID
WHERE SurveyID = @SurveyID  AND Disabled=0 ORDER BY AddInOrder



GO
/****** Object:  StoredProcedure [dbo].[vts_spWebSecurityAddInsMoveDown]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
///  Moves a survey's addin positions down 
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spWebSecurityAddInsMoveDown] @SurveyID int, @AddInID int 
AS
DECLARE
	@OldAddInOrder int,
	@NewAddInOrder int,
	@NewAddInID int
SELECT 
	@OldAddInOrder = AddInOrder
FROM 
	vts_tbSurveyWebSecurity
WHERE
	SurveyID = @SurveyID AND
	WebSecurityAddInID = @AddInID
SELECT TOP 1  
	@NewAddInOrder = AddInOrder,
	@NewAddInID = WebSecurityAddInID
FROM 
	vts_tbSurveyWebSecurity
WHERE
	SurveyID = @SurveyID AND
	AddInOrder > @OldAddInOrder
	ORDER BY AddInOrder ASC
-- Is this already the last addin
IF @@RowCount <>0
BEGIN
	-- Move up previous addin
	UPDATE vts_tbSurveyWebSecurity
		set AddInOrder = @OldAddInOrder 
	WHERE 
		AddInOrder = @NewAddInOrder AND
		SurveyID = @SurveyID AND 
		WebSecurityAddInID = @NewAddInID
	-- Move down current addin
	UPDATE vts_tbSurveyWebSecurity
		set AddInOrder = @NewAddInOrder 
	WHERE SurveyID = @SurveyID AND WebSecurityAddInID = @AddInID
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spWebSecurityAddInsMoveUp]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
///  Moves a survey's addin positions up 
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spWebSecurityAddInsMoveUp] @SurveyID int, @AddInID int 
AS
DECLARE
	@OldAddInOrder int,
	@NewAddInOrder int,
	@NewAddInID int
SELECT 
	@OldAddInOrder = AddInOrder
FROM 
	vts_tbSurveyWebSecurity
WHERE
	SurveyID = @SurveyID AND
	WebSecurityAddInID = @AddInID
SELECT TOP 1  
	@NewAddInOrder = AddInOrder,
	@NewAddInID = WebSecurityAddInID
FROM 
	vts_tbSurveyWebSecurity
WHERE
	SurveyID = @SurveyID AND
	AddInOrder < @OldAddInOrder
	ORDER BY AddInOrder DESC
-- Is this already the last addin
IF @@RowCount <>0
BEGIN
	-- Move up down addin
	UPDATE vts_tbSurveyWebSecurity
		set AddInOrder = @OldAddInOrder 
	WHERE 
		AddInOrder = @NewAddInOrder AND
		SurveyID = @SurveyID AND 
		WebSecurityAddInID = @NewAddInID
	-- Move up current addin
	UPDATE vts_tbSurveyWebSecurity
		set AddInOrder = @NewAddInOrder 
	WHERE SurveyID = @SurveyID AND WebSecurityAddInID = @AddInID
END



GO
/****** Object:  StoredProcedure [dbo].[vts_spWebSecurityAddInsSurveyGetDetails]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
*/
CREATE PROCEDURE [dbo].[vts_spWebSecurityAddInsSurveyGetDetails] @SurveyID int, @AddInID int
AS
SELECT 
	vts_tbWebSecurityAddIn.WebSecurityAddInID,
	SurveyID,
	Disabled,
	BuiltIn,
	Description,
	TypeNameSpace,
	TypeAssembly,
	TypeMode,
	AddInOrder
FROM vts_tbWebSecurityAddIn
INNER JOIN vts_tbSurveyWebSecurity
	ON vts_tbWebSecurityAddIn.WebSecurityAddInID = vts_tbSurveyWebSecurity.WebSecurityAddInID
WHERE SurveyID = @SurveyID AND vts_tbWebSecurityAddIn.WebSecurityAddInID = @AddInID



GO
/****** Object:  UserDefinedFunction [dbo].[fnStripTags]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Function [dbo].[fnStripTags]
    (@Dirty varchar(4000))
    Returns varchar(4000)
As

Begin
    Declare @Start int,
        @End int,
        @Length int

    While CharIndex('<', @Dirty) > 0 And CharIndex('>', @Dirty, CharIndex('<', @Dirty)) > 0
     Begin
        Select @Start = CharIndex('<', @Dirty),
         @End = CharIndex('>', @Dirty, CharIndex('<', @Dirty))
        Select @Length = (@End - @Start) + 1
        If @Length > 0
         Begin
            Select @Dirty = Stuff(@Dirty, @Start, @Length, '')
         End
     End

    return @Dirty
End



GO
/****** Object:  UserDefinedFunction [dbo].[vts_fnGetFolderRootCount]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[vts_fnGetFolderRootCount]
(
)
RETURNS INT 
AS
BEGIN
	DECLARE @ret int;

	SELECT @ret = count(*) from vts_tbFolders where ParentFolderID is null

	RETURN @ret
END



GO
/****** Object:  Table [dbo].[vts_tbAnswer]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbAnswer](
	[AnswerID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionID] [int] NOT NULL,
	[AnswerTypeID] [smallint] NULL,
	[RegularExpressionID] [int] NULL,
	[AnswerText] [NVARCHAR](4000) NULL,
	[ImageURL] [NVARCHAR](255) NULL,
	[RatePart] [bit] NOT NULL,
	[DisplayOrder] [int] NULL,
	[Selected] [bit] NULL,
	[DefaultText] [NVARCHAR](4000) NULL,
	[ScorePoint] [int] NULL,
	[AnswerPipeAlias] [NVARCHAR](50) NULL,
	[Mandatory] [bit] NULL,
	[AnswerIDText] [NVARCHAR](255) NULL,
	[AnswerAlias] [NVARCHAR](255) NULL,
	[SliderRange] [NVARCHAR](3) NULL,
	[SliderValue] [int] NULL,
	[SliderMin] [int] NULL,
	[SliderMax] [int] NULL,
	[SliderAnimate] [bit] NULL,
	[SliderStep] [int] NULL,
	[CssClass] [NVARCHAR](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbAnswerConnection]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbAnswerConnection](
	[PublisherAnswerID] [int] NOT NULL,
	[SubscriberAnswerID] [int] NOT NULL,
 CONSTRAINT [PK_vts_tbAnswerSubscriber] PRIMARY KEY CLUSTERED 
(
	[PublisherAnswerID] ASC,
	[SubscriberAnswerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbAnswerProperty]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbAnswerProperty](
	[AnswerID] [int] NOT NULL,
	[Properties] [image] NULL,
 CONSTRAINT [PK_vts_tbAnswerProperty] PRIMARY KEY CLUSTERED 
(
	[AnswerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbAnswerType]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[vts_tbAnswerType](
	[AnswerTypeID] [smallint] IDENTITY(1,1) NOT NULL,
	[BuiltIn] [bit] NULL,
	[Description] [NVARCHAR](200) NOT NULL,
	[FieldWidth] [int] NULL,
	[FieldHeight] [int] NULL,
	[FieldLength] [int] NULL,
	[TypeMode] [int] NOT NULL,
	[XMLDatasource] [varchar](200) NULL,
	[PublicFieldResults] [bit] NULL,
	[JavascriptFunctionName] [varchar](1000) NULL,
	[JavascriptCode] [varchar](8000) NULL,
	[JavascriptErrorMessage] [varchar](1000) NULL,
	[TypeNameSpace] [varchar](200) NOT NULL,
	[TypeAssembly] [varchar](200) NOT NULL,
	[DataSource] [NVARCHAR](4000) NULL,
 CONSTRAINT [PK_vts_tbAnswerType] PRIMARY KEY CLUSTERED 
(
	[AnswerTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[vts_tbBranchingRule]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[vts_tbBranchingRule](
	[BranchingRuleID] [int] IDENTITY(1,1) NOT NULL,
	[ConditionalOperator] [int] NULL,
	[ExpressionOperator] [int] NULL,
	[PageNumber] [int] NULL,
	[TargetPageNumber] [int] NULL,
	[AnswerID] [int] NULL,
	[QuestionID] [int] NULL,
	[TextFilter] [varchar](4000) NULL,
	[Score] [int] NULL,
	[ScoreMax] [int] NULL,
 CONSTRAINT [PK_vts_tbBranchingRule] PRIMARY KEY CLUSTERED 
(
	[BranchingRuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[vts_tbEmail]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[vts_tbEmail](
	[EmailID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](150) NULL,
 CONSTRAINT [PK_vts_tbEmail] PRIMARY KEY CLUSTERED 
(
	[EmailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[vts_tbEmailNotificationSettings]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbEmailNotificationSettings](
	[SurveyID] [int] NOT NULL,
	[EmailFrom] [NVARCHAR](255) NULL,
	[EmailTo] [NVARCHAR](255) NULL,
	[EmailSubject] [NVARCHAR](255) NULL,
 CONSTRAINT [PK_vts_tbEmailNotificationSettings] PRIMARY KEY CLUSTERED 
(
	[SurveyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbFile]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbFile](
	[FileID] [int] IDENTITY(1,1) NOT NULL,
	[GroupGuid] [NVARCHAR](40) NOT NULL,
	[SaveDate] [datetime] NULL,
	[FileName] [NVARCHAR](1024) NOT NULL,
	[FileSize] [int] NULL,
	[FileType] [NVARCHAR](1024) NULL,
	[FileData] [image] NULL,
 CONSTRAINT [PK_vts_tbFile] PRIMARY KEY CLUSTERED 
(
	[GroupGuid] ASC,
	[FileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbFilter]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[vts_tbFilter](
	[FilterID] [int] IDENTITY(1,1) NOT NULL,
	[SurveyID] [int] NULL,
	[Description] [varchar](200) NULL,
	[LogicalOperatorTypeID] [smallint] NULL,
	[ParentFilterID] [int] NULL,
 CONSTRAINT [PK_vts_tbFilter] PRIMARY KEY CLUSTERED 
(
	[FilterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[vts_tbFilterRule]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbFilterRule](
	[FilterRuleID] [int] IDENTITY(1,1) NOT NULL,
	[FilterID] [int] NOT NULL,
	[QuestionID] [int] NULL,
	[AnswerID] [int] NULL,
	[TextFilter] [NVARCHAR](4000) NULL,
 CONSTRAINT [PK_vts_tbFilterRule] PRIMARY KEY CLUSTERED 
(
	[FilterRuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbFolders]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[vts_tbFolders](
	[FolderID] [int] IDENTITY(1,1) NOT NULL,
	[FolderName] [varchar](100) NOT NULL,
	[ParentFolderID] [int] NULL,
 CONSTRAINT [PK_vts_tbFolders] PRIMARY KEY CLUSTERED 
(
	[FolderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[vts_tbInvitationLog]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbInvitationLog](
	[InvitationLogID] [int] IDENTITY(1,1) NOT NULL,
	[SurveyID] [int] NULL,
	[EmailID] [int] NULL,
	[ExceptionMessage] [NVARCHAR](1024) NULL,
	[ExceptionType] [NVARCHAR](255) NULL,
	[ErrorDate] [datetime] NULL,
 CONSTRAINT [PK_vts_InvitationLog] PRIMARY KEY CLUSTERED 
(
	[InvitationLogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbInvitationQueue]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbInvitationQueue](
	[EmailID] [int] NOT NULL,
	[SurveyID] [int] NOT NULL,
	[UID] [NVARCHAR](40) NULL,
	[RequestDate] [datetime] NULL,
	[AnonymousEntry] [bit] NULL,
 CONSTRAINT [PK_vts_tbInvitationQueue] PRIMARY KEY CLUSTERED 
(
	[EmailID] ASC,
	[SurveyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbLanguageMessageType]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbLanguageMessageType](
	[LanguageMessageTypeID] [int] NOT NULL,
	[TypeDescription] [NVARCHAR](255) NULL,
 CONSTRAINT [PK_vts_LanguageMessageType] PRIMARY KEY CLUSTERED 
(
	[LanguageMessageTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbLayoutMode]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[vts_tbLayoutMode](
	[LayoutModeID] [tinyint] NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_vts_tbLayoutMode] PRIMARY KEY CLUSTERED 
(
	[LayoutModeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[vts_tbLibrary]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbLibrary](
	[LibraryID] [int] IDENTITY(1,1) NOT NULL,
	[LibraryName] [NVARCHAR](255) NULL,
	[Description] [ntext] NULL,
	[DefaultLanguageCode] [NVARCHAR](50) NULL,
 CONSTRAINT [PK_vts_tbLibrary] PRIMARY KEY CLUSTERED 
(
	[LibraryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbLibraryLanguage]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbLibraryLanguage](
	[LibraryID] [int] NOT NULL,
	[LanguageCode] [NVARCHAR](50) NOT NULL,
	[DefaultLanguage] [bit] NULL,
 CONSTRAINT [PK_vts_tbLibraryLanguages] PRIMARY KEY CLUSTERED 
(
	[LibraryID] ASC,
	[LanguageCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbMessageCondition]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbMessageCondition](
	[MessageConditionID] [int] IDENTITY(1,1) NOT NULL,
	[MessageConditionalOperator] [int] NULL,
	[SurveyID] [int] NULL,
	[QuestionID] [int] NULL,
	[ConditionalOperator] [int] NULL,
	[AnswerID] [int] NULL,
	[TextFilter] [NVARCHAR](4000) NULL,
	[ThankYouMessage] [NVARCHAR](4000) NULL,
	[Score] [int] NULL,
	[ScoreMax] [int] NULL,
	[ExpressionOperator] [int] NULL,
 CONSTRAINT [PK_vts_tbMessageCondition] PRIMARY KEY CLUSTERED 
(
	[MessageConditionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbMultiLanguage]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbMultiLanguage](
	[LanguageCode] [NVARCHAR](50) NOT NULL,
	[LanguageDescription] [NVARCHAR](255) NULL,
 CONSTRAINT [PK_vts_tbMultiLanguage] PRIMARY KEY CLUSTERED 
(
	[LanguageCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbMultiLanguageMode]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbMultiLanguageMode](
	[MultiLanguageModeID] [int] NOT NULL,
	[ModeDescription] [NVARCHAR](255) NULL,
 CONSTRAINT [PK_vts_MultiLanguageMode] PRIMARY KEY CLUSTERED 
(
	[MultiLanguageModeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbMultiLanguageText]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbMultiLanguageText](
	[LanguageItemID] [int] NOT NULL,
	[LanguageCode] [NVARCHAR](50) NOT NULL,
	[LanguageMessageTypeID] [int] NOT NULL,
	[ItemText] [NVARCHAR](4000) NULL,
 CONSTRAINT [PK_vts_LanguageText] PRIMARY KEY CLUSTERED 
(
	[LanguageItemID] ASC,
	[LanguageCode] ASC,
	[LanguageMessageTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbNotificationMode]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbNotificationMode](
	[NotificationModeID] [int] NOT NULL,
	[Description] [NVARCHAR](50) NULL,
 CONSTRAINT [PK_vts_tbNotificationMode] PRIMARY KEY CLUSTERED 
(
	[NotificationModeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbPageOption]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbPageOption](
	[SurveyID] [int] NOT NULL,
	[PageNumber] [int] NOT NULL,
	[RandomizeQuestions] [bit] NULL,
	[EnableSubmitButton] [bit] NULL,
 CONSTRAINT [PK_vts_tbPageOptions] PRIMARY KEY CLUSTERED 
(
	[SurveyID] ASC,
	[PageNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbProgressDisplayMode]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbProgressDisplayMode](
	[ProgressDisplayModeID] [int] NOT NULL,
	[Description] [NVARCHAR](50) NULL,
 CONSTRAINT [PK_ProgressDisplayMode] PRIMARY KEY CLUSTERED 
(
	[ProgressDisplayModeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbQuestion]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbQuestion](
	[QuestionID] [int] IDENTITY(1,1) NOT NULL,
	[ParentQuestionID] [int] NULL,
	[SurveyID] [int] NULL,
	[LibraryID] [int] NULL,
	[LayoutModeID] [tinyint] NULL,
	[SelectionModeID] [tinyint] NULL,
	[ColumnsNumber] [int] NULL,
	[QuestionText] [NVARCHAR](4000) NULL,
	[DisplayOrder] [int] NULL,
	[MinSelectionRequired] [int] NULL,
	[MaxSelectionAllowed] [int] NULL,
	[RatingEnabled] [bit] NOT NULL,
	[RandomizeAnswers] [bit] NOT NULL,
	[PageNumber] [int] NULL,
	[QuestionPipeAlias] [NVARCHAR](50) NULL,
	[QuestionIDText] [NVARCHAR](255) NULL,
	[HelpText] [NVARCHAR](4000) NULL,
	[Alias] [NVARCHAR](255) NULL,
	[QuestionGroupID] [int] NULL,
	[ShowHelpText] [bit] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbQuestionGroups]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbQuestionGroups](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [NVARCHAR](250) NOT NULL,
	[ParentGroupID] [int] NULL,
	[DisplayOrder] [int] NOT NULL,
 CONSTRAINT [PK_vts_tbQuestionGroups] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbQuestionSectionGridAnswer]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbQuestionSectionGridAnswer](
	[QuestionID] [int] NOT NULL,
	[AnswerID] [int] NOT NULL,
 CONSTRAINT [PK_vts_tbSectionGridAnswers] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC,
	[AnswerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbQuestionSectionOption]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbQuestionSectionOption](
	[QuestionID] [int] NOT NULL,
	[RepeatableSectionModeID] [int] NOT NULL,
	[AddSectionLinkText] [NVARCHAR](255) NULL,
	[DeleteSectionLinkText] [NVARCHAR](255) NULL,
	[EditSectionLinkText] [NVARCHAR](255) NULL,
	[UpdateSectionLinkText] [NVARCHAR](255) NULL,
	[MaxSections] [int] NULL,
 CONSTRAINT [PK_vts_tbQuestionSectionOption] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbQuestionSelectionMode]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[vts_tbQuestionSelectionMode](
	[QuestionSelectionModeID] [tinyint] NOT NULL,
	[Description] [NVARCHAR](50) NULL,
	[TypeNameSpace] [varchar](200) NOT NULL,
	[TypeAssembly] [varchar](200) NOT NULL,
	[TypeMode] [int] NULL,
 CONSTRAINT [PK_vts_tbQuestionSelectionMode] PRIMARY KEY CLUSTERED 
(
	[QuestionSelectionModeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[vts_tbRegularExpression]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbRegularExpression](
	[RegularExpressionID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [NVARCHAR](255) NULL,
	[RegExpression] [NVARCHAR](2000) NULL,
	[RegExMessage] [NVARCHAR](2000) NULL,
	[BuiltIn] [bit] NULL,
 CONSTRAINT [PK_vts_tbRegularExpression] PRIMARY KEY CLUSTERED 
(
	[RegularExpressionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbRepeatableSection]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[vts_tbRepeatableSection](
	[RepeatableSectionModeID] [int] NOT NULL,
	[ModeDescription] [varchar](255) NULL,
 CONSTRAINT [PK_vts_tbRepeatableSection] PRIMARY KEY CLUSTERED 
(
	[RepeatableSectionModeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[vts_tbResumeMode]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbResumeMode](
	[ResumeModeID] [tinyint] NOT NULL,
	[Description] [NVARCHAR](50) NULL,
 CONSTRAINT [PK_tbResumeMode] PRIMARY KEY CLUSTERED 
(
	[ResumeModeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbRole]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[vts_tbRole](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](255) NULL,
 CONSTRAINT [PK_vts_tbRole] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[vts_tbRoleSecurityRight]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbRoleSecurityRight](
	[RoleID] [int] NOT NULL,
	[SecurityRightID] [int] NOT NULL,
 CONSTRAINT [PK_vts_tbRoleSecurityRight] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC,
	[SecurityRightID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbSecurityRight]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbSecurityRight](
	[SecurityRightID] [int] NOT NULL,
	[Description] [NVARCHAR](255) NULL,
 CONSTRAINT [PK_vts_tbSecurityRights] PRIMARY KEY CLUSTERED 
(
	[SecurityRightID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbSkipLogicRule]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[vts_tbSkipLogicRule](
	[SkipLogicRuleID] [int] IDENTITY(1,1) NOT NULL,
	[SkipQuestionID] [int] NULL,
	[ConditionalOperator] [int] NULL,
	[ExpressionOperator] [int] NULL,
	[AnswerID] [int] NULL,
	[QuestionID] [int] NULL,
	[TextFilter] [varchar](4000) NULL,
	[Score] [int] NULL,
	[ScoreMax] [int] NULL,
 CONSTRAINT [PK_vts_tbSkipLogicRule] PRIMARY KEY CLUSTERED 
(
	[SkipLogicRuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[vts_tbSurvey]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[vts_tbSurvey](
	[SurveyID] [int] IDENTITY(1,1) NOT NULL,
	[ProgressDisplayModeID] [int] NULL,
	[NotificationModeID] [int] NULL,
	[ResumeModeID] [tinyint] NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastEntryDate] [datetime] NULL,
	[OpenDate] [datetime] NULL,
	[CloseDate] [datetime] NULL,
	[Title] [NVARCHAR](255) NULL,
	[RedirectionURL] [varchar](1024) NULL,
	[ThankYouMessage] [NVARCHAR](4000) NULL,
	[AccessPassword] [NVARCHAR](255) NULL,
	[SurveyDisplayTimes] [int] NOT NULL,
	[ResultsDisplayTimes] [int] NOT NULL,
	[NavigationEnabled] [bit] NULL,
	[Archive] [bit] NOT NULL,
	[Activated] [bit] NOT NULL,
	[Scored] [bit] NULL,
	[IPExpires] [int] NULL,
	[CookieExpires] [int] NULL,
	[SaveTokenUserData] [int] NULL,
	[OnlyInvited] [bit] NULL,
	[UnAuthentifiedUserActionID] [int] NULL,
	[AllowMultipleUserNameSubmissions] [bit] NULL,
	[QuestionNumberingDisabled] [bit] NULL,
	[AllowMultipleNSurveySubmissions] [bit] NULL,
	[MultiLanguageModeID] [int] NULL,
	[MultiLanguageVariable] [NVARCHAR](50) NULL,
	[FolderID] [int] NOT NULL,
	[SurveyGuid] [uniqueidentifier] NULL,
	[FriendlyName] [NVARCHAR](200) NULL,
	[DefaultSurvey] [bit] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[vts_tbSurveyAsset]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbSurveyAsset](
	[AssetID] [int] IDENTITY(1,1) NOT NULL,
	[SurveyID] [int] NOT NULL,
	[AssetType] [nchar](30) NOT NULL,
	[Name] [nchar](300) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AssetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbSurveyEntryQuota]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbSurveyEntryQuota](
	[SurveyID] [int] NOT NULL,
	[MaxEntries] [int] NULL,
	[EntryCount] [int] NULL,
	[MaxReachedMessage] [NVARCHAR](4000) NULL,
 CONSTRAINT [PK_vts_tbSurveyEntryQuota] PRIMARY KEY CLUSTERED 
(
	[SurveyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbSurveyIPRange]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[vts_tbSurveyIPRange](
	[SurveyIPRangeID] [int] IDENTITY(1,1) NOT NULL,
	[SurveyID] [int] NOT NULL,
	[IPStart] [varchar](50) NOT NULL,
	[IPEnd] [varchar](50) NOT NULL,
 CONSTRAINT [PK_vts_tbSurveyIPRange] PRIMARY KEY CLUSTERED 
(
	[SurveyIPRangeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[vts_tbSurveyLanguage]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbSurveyLanguage](
	[SurveyID] [int] NOT NULL,
	[LanguageCode] [NVARCHAR](50) NOT NULL,
	[DefaultLanguage] [bit] NULL,
 CONSTRAINT [PK_vts_tbSurveyLanguage] PRIMARY KEY CLUSTERED 
(
	[SurveyID] ASC,
	[LanguageCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbSurveyLayout]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbSurveyLayout](
	[SurveyID] [int] NOT NULL,
	[SurveyHeaderText] [NVARCHAR](max) NULL,
	[SurveyFooterText] [NVARCHAR](max) NULL,
	[SurveyCss] [NVARCHAR](255) NULL,
 CONSTRAINT [PK_SurveyLayout] PRIMARY KEY CLUSTERED 
(
	[SurveyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbSurveyToken]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[vts_tbSurveyToken](
	[TokenID] [int] IDENTITY(1,1) NOT NULL,
	[SurveyID] [int] NOT NULL,
	[Token] [varchar](40) NOT NULL,
	[CreationDate] [date] NOT NULL,
	[Used] [bit] NOT NULL,
	[VoterID] [int] NULL,
 CONSTRAINT [PK_vts_tbSurveyToken] PRIMARY KEY CLUSTERED 
(
	[TokenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[vts_tbSurveyWebSecurity]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbSurveyWebSecurity](
	[WebSecurityAddInID] [int] NOT NULL,
	[SurveyID] [int] NOT NULL,
	[AddInOrder] [int] NULL,
	[Disabled] [bit] NULL,
 CONSTRAINT [PK_vts_tbSurveyWebSecurity] PRIMARY KEY CLUSTERED 
(
	[WebSecurityAddInID] ASC,
	[SurveyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbUnAuthentifiedUserAction]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[vts_tbUnAuthentifiedUserAction](
	[UnAuthentifiedUserActionID] [int] NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_tbNonAuthentifiedUserAction] PRIMARY KEY CLUSTERED 
(
	[UnAuthentifiedUserActionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[vts_tbUser]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbUser](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [NVARCHAR](255) NULL,
	[Password] [NVARCHAR](255) NULL,
	[FirstName] [NVARCHAR](255) NULL,
	[LastName] [NVARCHAR](255) NULL,
	[Email] [NVARCHAR](255) NULL,
	[CreationDate] [datetime] NULL,
	[LastLogin] [datetime] NULL,
	[PasswordSalt] [NVARCHAR](255) NULL,
 CONSTRAINT [PK_vts_tbUser] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbUserAnswerType]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbUserAnswerType](
	[UserID] [int] NOT NULL,
	[AnswerTypeID] [smallint] NOT NULL,
 CONSTRAINT [PK_vts_tbUserAnswerType] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[AnswerTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbUserRegularExpression]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbUserRegularExpression](
	[UserID] [int] NOT NULL,
	[RegularExpressionID] [int] NOT NULL,
 CONSTRAINT [PK_vts_tbUserRegularExpression] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[RegularExpressionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbUserRole]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbUserRole](
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_vts_tbUserRole] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbUserSetting]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbUserSetting](
	[UserID] [int] NOT NULL,
	[IsAdmin] [bit] NULL,
	[GlobalSurveyAccess] [bit] NULL,
 CONSTRAINT [PK_vts_tbUserSettings] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbUserSurvey]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbUserSurvey](
	[UserID] [int] NOT NULL,
	[SurveyID] [int] NOT NULL,
 CONSTRAINT [PK_vts_tbUserSurvey] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[SurveyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbVoter]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbVoter](
	[VoterID] [int] IDENTITY(1,1) NOT NULL,
	[UID] [NVARCHAR](40) NULL,
	[SurveyID] [int] NULL,
	[ContextUserName] [NVARCHAR](255) NULL,
	[VoteDate] [datetime] NULL,
	[StartDate] [datetime] NULL,
	[IPSource] [NVARCHAR](50) NULL,
	[Validated] [bit] NULL,
	[ResumeUID] [NVARCHAR](40) NULL,
	[ResumeAtPageNumber] [int] NULL,
	[ProgressSaveDate] [datetime] NULL,
	[ResumeQuestionNumber] [int] NULL,
	[ResumeHighestPageNumber] [int] NULL,
	[LanguageCode] [NVARCHAR](50) NULL,
 CONSTRAINT [PK_vts_tbVoter] PRIMARY KEY CLUSTERED 
(
	[VoterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbVoterAnswers]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbVoterAnswers](
	[VoterID] [int] NOT NULL,
	[AnswerID] [int] NOT NULL,
	[SectionNumber] [int] NOT NULL,
	[AnswerText] [ntext] NULL,
 CONSTRAINT [PK_vts_tbVoterAnswers] PRIMARY KEY CLUSTERED 
(
	[VoterID] ASC,
	[AnswerID] ASC,
	[SectionNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbVoterEmail]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbVoterEmail](
	[VoterID] [int] NOT NULL,
	[EmailID] [int] NOT NULL,
 CONSTRAINT [PK_vts_tbVoterEmail] PRIMARY KEY CLUSTERED 
(
	[VoterID] ASC,
	[EmailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vts_tbWebSecurityAddIn]    Script Date: 19-8-2014 22:01:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[vts_tbWebSecurityAddIn](
	[WebSecurityAddInID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [NVARCHAR](50) NULL,
	[BuiltIn] [bit] NULL,
	[TypeNameSpace] [varchar](200) NULL,
	[TypeAssembly] [varchar](50) NULL,
	[TypeMode] [int] NULL,
 CONSTRAINT [PK_tbSecurityAddIn] PRIMARY KEY CLUSTERED 
(
	[WebSecurityAddInID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[vts_tbAnswerType] ON 

GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (1, 1, N'SelectionTextType', 0, 0, 0, 1, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerSelectionItem', N'Votations.NSurvey.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (2, 1, N'SelectionOtherType', 20, 0, 300, 211, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerOtherFieldItem', N'Votations.NSurvey.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (3, 1, N'FieldBasicType', 20, 1, 100, 210, NULL, 0, N'', N'', N'', N'Votations.NSurvey.WebControls.UI.AnswerFieldItem', N'Votations.NSurvey.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (4, 1, N'XMLCountryList', NULL, NULL, NULL, 88, N'countries.xml', 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerXmlListItem', N'Votations.NSurvey.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (20, 1, N'BooleanType', 0, 0, 0, 20, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerBooleanItem', N'Votations.NSurvey.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (21, 1, N'FieldRequiredType', 20, 0, 255, 210, NULL, 0, N'isFilled', N'function isFilled(sender)
{
if (sender.value.length == 0)
{
   sender.focus();
   return false;
}
else
{
   return true;
}
}', N'FieldRequiredType', N'Votations.NSurvey.WebControls.UI.AnswerFieldItem', N'Votations.NSurvey.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (22, 1, N'FieldCalendarType', 10, 0, 10, 86, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerFieldCalendarItem', N'Votations.NSurvey.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (24, 1, N'FieldLargeType', 70, 10, 255, 210, NULL, 0, N'', N'', N'', N'Votations.NSurvey.WebControls.UI.AnswerFieldItem', N'Votations.NSurvey.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (26, 1, N'FieldEmailType', 20, 0, 255, 210, NULL, 0, N'EmailValid', N'function EmailValid(sender) 
{
if (sender.value.length == 0)
{
   sender.focus();
   return false;
}
else
{
  var EmailRegEx = /^[a-zA-Z0-9._-]+@([a-zA-Z0-9.-]+\.)+[a-zA-Z0-9.-]{2,4}$/;
  if (sender.value.match(EmailRegEx) != null)
  {
   return true;
  }
  else
  {
   sender.focus();
   return false;
  }
}
}', N'JSInvalidEmail', N'Votations.NSurvey.WebControls.UI.AnswerFieldItem', N'Votations.NSurvey.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (27, 1, N'FieldHiddenType', 0, 0, 0, 20, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerFieldHiddenItem', N'Votations.NSurvey.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (28, 1, N'FieldPasswordType', 20, 0, 0, 214, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerFieldPasswordItem', N'Votations.NSurvey.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (29, 1, N'SubscriberXMLList', 0, 0, 0, 116, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerSubscriberXmlListItem', N'Votations.NSurvey.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (30, 1, N'ExtendedFileUploadType', 0, 0, 0, 832, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerUploadItem', N'Votations.NSurvey.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (31, 1, N'ExtendedFreeTextBoxType', 500, 100, 255, 722, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.ThirdPartyItems.FreeTextBoxAnswerItem', N'Votations.NSurvey.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (55, 1, N'FieldAddressType', 20, 0, 100, 210, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerFieldAddressItem', N'Votations.NSurvey.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (56, 1, N'FieldSliderType', 20, 0, 100, 1090, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerFieldSliderItem', N'Votations.NSurvey.WebControls', NULL)
GO
SET IDENTITY_INSERT [dbo].[vts_tbAnswerType] OFF
GO
SET IDENTITY_INSERT [dbo].[vts_tbFolders] ON 

GO
INSERT [dbo].[vts_tbFolders] ([FolderID], [FolderName], [ParentFolderID]) VALUES (33, N'Root', NULL)
GO
SET IDENTITY_INSERT [dbo].[vts_tbFolders] OFF
GO
INSERT [dbo].[vts_tbLanguageMessageType] ([LanguageMessageTypeID], [TypeDescription]) VALUES (1, N'Answer text')
GO
INSERT [dbo].[vts_tbLanguageMessageType] ([LanguageMessageTypeID], [TypeDescription]) VALUES (2, N'Answer''s default value')
GO
INSERT [dbo].[vts_tbLanguageMessageType] ([LanguageMessageTypeID], [TypeDescription]) VALUES (3, N'Question text')
GO
INSERT [dbo].[vts_tbLanguageMessageType] ([LanguageMessageTypeID], [TypeDescription]) VALUES (4, N'Thank you message')
GO
INSERT [dbo].[vts_tbLanguageMessageType] ([LanguageMessageTypeID], [TypeDescription]) VALUES (5, N'Redirection url')
GO
INSERT [dbo].[vts_tbLanguageMessageType] ([LanguageMessageTypeID], [TypeDescription]) VALUES (6, N'Add section')
GO
INSERT [dbo].[vts_tbLanguageMessageType] ([LanguageMessageTypeID], [TypeDescription]) VALUES (7, N'Delete section')
GO
INSERT [dbo].[vts_tbLanguageMessageType] ([LanguageMessageTypeID], [TypeDescription]) VALUES (8, N'Update section')
GO
INSERT [dbo].[vts_tbLanguageMessageType] ([LanguageMessageTypeID], [TypeDescription]) VALUES (9, N'Edit section')
GO
INSERT [dbo].[vts_tbLanguageMessageType] ([LanguageMessageTypeID], [TypeDescription]) VALUES (10, N'Question Groups')
GO
INSERT [dbo].[vts_tbLanguageMessageType] ([LanguageMessageTypeID], [TypeDescription]) VALUES (11, N'Question Help Text')
GO
INSERT [dbo].[vts_tbLanguageMessageType] ([LanguageMessageTypeID], [TypeDescription]) VALUES (12, N'Question Alias')
GO
INSERT [dbo].[vts_tbLanguageMessageType] ([LanguageMessageTypeID], [TypeDescription]) VALUES (13, N'AnswerAlias')
GO
INSERT [dbo].[vts_tbLanguageMessageType] ([LanguageMessageTypeID], [TypeDescription]) VALUES (14, N'Layout Header')
GO
INSERT [dbo].[vts_tbLanguageMessageType] ([LanguageMessageTypeID], [TypeDescription]) VALUES (15, N'Layout Footer')
GO
INSERT [dbo].[vts_tbLayoutMode] ([LayoutModeID], [Description]) VALUES (1, N'VerticalItemText')
GO
INSERT [dbo].[vts_tbLayoutMode] ([LayoutModeID], [Description]) VALUES (2, N'HorizontalItemText')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'af-ZA', N'AfrikaansLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'ar-SA', N'ArabicLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'az-AZ-Cyrl', N'AzeriLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'be-BY', N'BelarusianLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'bg-BG', N'BulgarianLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'ca-ES', N'CatalanLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'cs-CZ', N'CzechLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'da-DK', N'DanishLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'de-CH', N'de-CH')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'de-DE', N'GermanLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'div-MV', N'DhivehiLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'el-GR', N'GreekLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'en-US', N'EnglishLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'es-ES', N'SpanishLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'et-EE', N'EstonianLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'eu-ES', N'BasqueLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'fa-IR', N'FarsiLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'fi-FI', N'FinishLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'fo-FO', N'FaroeseLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'fr-FR', N'FrenchLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'gl-ES', N'GalicianLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'gu-IN', N'GujaratiLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'he-IL', N'HebrewLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'hi-IN', N'HindiLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'hr-HR', N'CroatianLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'hu-HU', N'HungarianLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'hy-AM', N'ArmenianLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'id-ID', N'IndonesianLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'is-IS', N'IcelandicLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'it-IT', N'ItalianLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'ja-JP', N'JapaneseLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'ka-GE', N'GeorgianLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'kk-KZ', N'KazakhLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'kn-IN', N'KannadaLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'kok-IN', N'KonkaniLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'ko-KR', N'KoreanLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'ky-KZ', N'KyrgyzLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'LanguageCode', N'LanguageDescription')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'Lt-LT', N'LithuanianLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'lv-LV', N'LatvianLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'mk-ML', N'MacedonianLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'mn-MN', N'MongolianLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'mr-IN', N'MarathiLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'ms-BN', N'MalayLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'nb-NO', N'NorwegianLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'nl-NL', N'DutchLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'pa-IN', N'PunjabiLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'pl-PL', N'PolishLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'pt-PT', N'PortugueseLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'ro-RO', N'RomanianLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'ru-RU', N'RussianLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'sa-IN', N'SanskritLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'sk-SK', N'SlovakLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'sl-SI', N'SlovenianLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'sq-AL', N'AlbanianLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'sr-SP-Latn', N'SerbianLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'sv-FI', N'SwedishLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'sw-KE', N'SwahiliLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'syr-SY', N'SyriacLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'ta-IN', N'TamilLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'te-IN', N'TeluguLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'th-TH', N'ThaiLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'tr-TR', N'TurkishLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'tt-RU', N'TatarLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'uk-UA', N'UkrainianLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'ur-PK', N'UrduLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'uz-UZ-Latn', N'UzbekLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'vi-VN', N'VietNameseLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES (N'zh-CN', N'ChineseLanguage')
GO
INSERT [dbo].[vts_tbMultiLanguageMode] ([MultiLanguageModeID], [ModeDescription]) VALUES (0, N'None')
GO
INSERT [dbo].[vts_tbMultiLanguageMode] ([MultiLanguageModeID], [ModeDescription]) VALUES (1, N'UserSelectionOption')
GO
INSERT [dbo].[vts_tbMultiLanguageMode] ([MultiLanguageModeID], [ModeDescription]) VALUES (2, N'BrowserDetectionOption')
GO
INSERT [dbo].[vts_tbMultiLanguageMode] ([MultiLanguageModeID], [ModeDescription]) VALUES (3, N'QueryStringLanguageOption')
GO
INSERT [dbo].[vts_tbMultiLanguageMode] ([MultiLanguageModeID], [ModeDescription]) VALUES (4, N'CookieLanguageOption')
GO
INSERT [dbo].[vts_tbMultiLanguageMode] ([MultiLanguageModeID], [ModeDescription]) VALUES (5, N'SessionLanguageOption')
GO
INSERT [dbo].[vts_tbNotificationMode] ([NotificationModeID], [Description]) VALUES (1, N'NoneNotificationText')
GO
INSERT [dbo].[vts_tbNotificationMode] ([NotificationModeID], [Description]) VALUES (2, N'ShortNotificationText')
GO
INSERT [dbo].[vts_tbNotificationMode] ([NotificationModeID], [Description]) VALUES (3, N'ReportNotificationText')
GO
INSERT [dbo].[vts_tbNotificationMode] ([NotificationModeID], [Description]) VALUES (4, N'AnswerReportNotificationText')
GO
INSERT [dbo].[vts_tbProgressDisplayMode] ([ProgressDisplayModeID], [Description]) VALUES (1, N'NoProgressText')
GO
INSERT [dbo].[vts_tbProgressDisplayMode] ([ProgressDisplayModeID], [Description]) VALUES (2, N'PageNumberText')
GO
INSERT [dbo].[vts_tbProgressDisplayMode] ([ProgressDisplayModeID], [Description]) VALUES (3, N'ProgressPercentageText')
GO
INSERT [dbo].[vts_tbQuestionSelectionMode] ([QuestionSelectionModeID], [Description], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (1, N'RadioButtonSelection', N'Votations.NSurvey.WebControls.UI.RadioButtonQuestion', N'Votations.NSurvey.WebControls', 7)
GO
INSERT [dbo].[vts_tbQuestionSelectionMode] ([QuestionSelectionModeID], [Description], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (2, N'CheckBoxSelection', N'Votations.NSurvey.WebControls.UI.CheckBoxQuestion', N'Votations.NSurvey.WebControls', 23)
GO
INSERT [dbo].[vts_tbQuestionSelectionMode] ([QuestionSelectionModeID], [Description], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (3, N'DropDownListSelection', N'Votations.NSurvey.WebControls.UI.DropDownQuestion', N'Votations.NSurvey.WebControls', 7)
GO
INSERT [dbo].[vts_tbQuestionSelectionMode] ([QuestionSelectionModeID], [Description], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (4, N'MatrixSingleSelection', N'Votations.NSurvey.WebControls.UI.MatrixQuestion', N'Votations.NSurvey.WebControls', 14)
GO
INSERT [dbo].[vts_tbQuestionSelectionMode] ([QuestionSelectionModeID], [Description], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (5, N'StatictextSelection', N'Votations.NSurvey.WebControls.UI.StaticQuestion', N'Votations.NSurvey.WebControls', 3)
GO
INSERT [dbo].[vts_tbQuestionSelectionMode] ([QuestionSelectionModeID], [Description], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (6, N'MatrixMultipleSelection', N'Votations.NSurvey.WebControls.UI.CheckBoxMatrixQuestion', N'Votations.NSurvey.WebControls', 30)
GO
SET IDENTITY_INSERT [dbo].[vts_tbRegularExpression] ON 

GO
INSERT [dbo].[vts_tbRegularExpression] ([RegularExpressionID], [Description], [RegExpression], [RegExMessage], [BuiltIn]) VALUES (1, N'UnsignedIntegerRegEx', N'^\d*$', N'InvalidIntegerRegExMessage', 1)
GO
INSERT [dbo].[vts_tbRegularExpression] ([RegularExpressionID], [Description], [RegExpression], [RegExMessage], [BuiltIn]) VALUES (2, N'EmailRegEx', N'^((?>[a-zA-Z\d!#$%&''*+\-/=?^_`{|}~]+\x20*|"((?=[\x01-\x7f])[^"\\]|\\[\x01-\x7f])*"\x20*)*(?<angle><))?((?!\.)(?>\.?[a-zA-Z\d!#$%&''*+\-/=?^_`{|}~]+)+|"((?=[\x01-\x7f])[^"\\]|\\[\x01-\x7f])*")@(((?!-)[a-zA-Z\d\-]+(?<!-)\.)+[a-zA-Z]{2,}|\[(((?(?<!\[)\.)(25[0-5]|2[0-4]\d|[01]?\d?\d)){4}|[a-zA-Z\d\-]*[a-zA-Z\d]:((?=[\x01-\x7f])[^\\\[\]]|\\[\x01-\x7f])+)\])(?(angle)>)$', N'InvalidEmailRegExMessage', 1)
GO
INSERT [dbo].[vts_tbRegularExpression] ([RegularExpressionID], [Description], [RegExpression], [RegExMessage], [BuiltIn]) VALUES (3, N'EuroDateRegEx', N'^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((1[6-9]|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$', N'InvalidEuroDateRegExMessage', 1)
GO
INSERT [dbo].[vts_tbRegularExpression] ([RegularExpressionID], [Description], [RegExpression], [RegExMessage], [BuiltIn]) VALUES (4, N'USDateRegEx', N'^(?:(?:(?:0?[13578]|1[02])(\/|-|\.)31)\1|(?:(?:0?[1,3-9]|1[0-2])(\/|-|\.)(?:29|30)\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:0?2(\/|-|\.)29\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:(?:0?[1-9])|(?:1[0-2]))(\/|-|\.)(?:0?[1-9]|1\d|2[0-8])\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$', N'InvalidUSDateRegExMessage', 1)
GO
INSERT [dbo].[vts_tbRegularExpression] ([RegularExpressionID], [Description], [RegExpression], [RegExMessage], [BuiltIn]) VALUES (5, N'InternetWebURLRegEx', N'http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?', N'InvalidInternetUrlRegExMessage', 1)
GO
SET IDENTITY_INSERT [dbo].[vts_tbRegularExpression] OFF
GO
INSERT [dbo].[vts_tbRepeatableSection] ([RepeatableSectionModeID], [ModeDescription]) VALUES (0, N'NoRepeatOption')
GO
INSERT [dbo].[vts_tbRepeatableSection] ([RepeatableSectionModeID], [ModeDescription]) VALUES (1, N'FullRepeatOption')
GO
INSERT [dbo].[vts_tbRepeatableSection] ([RepeatableSectionModeID], [ModeDescription]) VALUES (2, N'GridRepeatOption')
GO
INSERT [dbo].[vts_tbResumeMode] ([ResumeModeID], [Description]) VALUES (1, N'ResumeNotAllowedText')
GO
INSERT [dbo].[vts_tbResumeMode] ([ResumeModeID], [Description]) VALUES (2, N'AutomaticResumeText')
GO
INSERT [dbo].[vts_tbResumeMode] ([ResumeModeID], [Description]) VALUES (3, N'ManualResumeText')
GO
SET IDENTITY_INSERT [dbo].[vts_tbRole] ON 

GO
INSERT [dbo].[vts_tbRole] ([RoleID], [RoleName]) VALUES (1, N'Report Viewers')
GO
INSERT [dbo].[vts_tbRole] ([RoleID], [RoleName]) VALUES (6, N'Survey Respondents')
GO
INSERT [dbo].[vts_tbRole] ([RoleID], [RoleName]) VALUES (7, N'Survey Creator')
GO
SET IDENTITY_INSERT [dbo].[vts_tbRole] OFF
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (1, 14)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (1, 16)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (1, 19)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (1, 40)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (6, 30)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (7, 1)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (7, 2)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (7, 3)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (7, 4)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (7, 5)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (7, 6)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (7, 7)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (7, 8)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (7, 12)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (7, 22)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (7, 25)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (7, 29)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (7, 34)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (7, 35)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (7, 38)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (7, 39)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (7, 40)
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (1, N'CreateSurveyRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (2, N'DeleteSurveyRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (3, N'AccessSurveySettingsRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (4, N'ExportSurveyXmlRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (5, N'ApplySurveySettingsRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (6, N'CloneSurveyRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (7, N'AccessPrivacySettingsRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (8, N'AccessSecuritySettingsRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (9, N'AccessStatsRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (10, N'ResetVotesRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (11, N'DeleteUnvalidateEntriesRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (12, N'AccessFormBuilderRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (13, N'AccessAnswerTypeEditorRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (14, N'AccessReportsRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (15, N'CreateResultsFilterRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (16, N'AccessFieldEntriesRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (17, N'EditVoterEntriesRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (18, N'DeleteVoterEntriesRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (19, N'AccessCrossTabRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (20, N'AccessExportRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (21, N'AccessMailingRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (22, N'AccessASPNetCodeRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (23, N'AccessUserManagerRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (24, N'AccessLibraryRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (25, N'CopyQuestionFromLibraryRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (26, N'CopyQuestionFromAllSurveyRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (27, N'ManageLibraryRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (28, N'SqlAnswerTypesEditionRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (29, N'AllowXmlImportRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (30, N'TakeSurveyRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (31, N'AccessRegExEditorRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (32, N'AccessFileManagerRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (33, N'ExportFilesRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (34, N'AccessMultiLanguagesRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (35, N'AccessHelpFiles')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (36, N'AccessQuestionGroupRight')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (37, N'DataImport')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (38, N'TokenSecurity')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (39, N'SurveyLayout')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (40, N'AccessSurveyList')
GO
INSERT [dbo].[vts_tbUnAuthentifiedUserAction] ([UnAuthentifiedUserActionID], [Description]) VALUES (1, N'ShowThankYouText')
GO
INSERT [dbo].[vts_tbUnAuthentifiedUserAction] ([UnAuthentifiedUserActionID], [Description]) VALUES (2, N'HideSurveyText')
GO
SET IDENTITY_INSERT [dbo].[vts_tbUser] ON 

GO
INSERT [dbo].[vts_tbUser] ([UserID], [UserName], [Password], [FirstName], [LastName], [Email], [CreationDate], [LastLogin], [PasswordSalt]) VALUES (1, N'admin', N'D42F0C00B41782AABA9CDEE44D6B41BCF05F8693', N'Survey Project', N'Administrator', N'', CAST(0x00009EC80144F992 AS DateTime), CAST(0x0000A17F0137E615 AS DateTime), N'imqxFRQ=')
GO
SET IDENTITY_INSERT [dbo].[vts_tbUser] OFF
GO
INSERT [dbo].[vts_tbUserSetting] ([UserID], [IsAdmin], [GlobalSurveyAccess]) VALUES (1, 1, 1)
GO

SET IDENTITY_INSERT [dbo].[vts_tbWebSecurityAddIn] ON 

GO
INSERT [dbo].[vts_tbWebSecurityAddIn] ([WebSecurityAddInID], [Description], [BuiltIn], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (1, N'PasswordProtection', 1, N'Votations.NSurvey.Security.PasswordWebSecurityAddIn', N'Votations.NSurvey.WebControls', 0)
GO
INSERT [dbo].[vts_tbWebSecurityAddIn] ([WebSecurityAddInID], [Description], [BuiltIn], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (2, N'IPProtection', 1, N'Votations.NSurvey.Security.IPWebSecurityAddIn', N'Votations.NSurvey.WebControls', 0)
GO
INSERT [dbo].[vts_tbWebSecurityAddIn] ([WebSecurityAddInID], [Description], [BuiltIn], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (3, N'CookieProtection', 1, N'Votations.NSurvey.Security.CookieWebSecurityAddIn', N'Votations.NSurvey.WebControls', 0)
GO
INSERT [dbo].[vts_tbWebSecurityAddIn] ([WebSecurityAddInID], [Description], [BuiltIn], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (4, N'EmailCodeProtection', 1, N'Votations.NSurvey.Security.EmailWebSecurityAddIn', N'Votations.NSurvey.WebControls', 0)
GO
INSERT [dbo].[vts_tbWebSecurityAddIn] ([WebSecurityAddInID], [Description], [BuiltIn], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (5, N'ASPNETSecurityContextProtection', 1, N'Votations.NSurvey.Security.ASPNetContextSecurityAddIn', N'Votations.NSurvey.WebControls', 0)
GO
INSERT [dbo].[vts_tbWebSecurityAddIn] ([WebSecurityAddInID], [Description], [BuiltIn], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (6, N'NSurveySecurityContextProtection', 1, N'Votations.NSurvey.Security.NSurveyContextSecurityAddIn', N'Votations.NSurvey.WebControls', 0)
GO
INSERT [dbo].[vts_tbWebSecurityAddIn] ([WebSecurityAddInID], [Description], [BuiltIn], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (7, N'EntryQuotaProtection', 1, N'Votations.NSurvey.Security.EntryQuotaSecurityAddIn', N'Votations.NSurvey.WebControls', 0)
GO
INSERT [dbo].[vts_tbWebSecurityAddIn] ([WebSecurityAddInID], [Description], [BuiltIn], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (8, N'Token Protection', 1, N'Votations.NSurvey.Security.TokenSecurityAddIn', N'Votations.NSurvey.WebControls', 0)
GO
INSERT [dbo].[vts_tbWebSecurityAddIn] ([WebSecurityAddInID], [Description], [BuiltIn], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (9, N'IP Filter', 1, N'Votations.NSurvey.Security.IPRangeSecurityAddIn', N'Votations.NSurvey.WebControls', 0)
GO
SET IDENTITY_INSERT [dbo].[vts_tbWebSecurityAddIn] OFF
GO
/****** Object:  Index [PK_vts_tbAnswer]    Script Date: 19-8-2014 22:01:40 ******/
ALTER TABLE [dbo].[vts_tbAnswer] ADD  CONSTRAINT [PK_vts_tbAnswer] PRIMARY KEY NONCLUSTERED 
(
	[AnswerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
GO
/****** Object:  Index [IX_Answer]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_Answer] ON [dbo].[vts_tbAnswer]
(
	[DisplayOrder] ASC,
	[ScorePoint] ASC
)
INCLUDE ( 	[AnswerID]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_AnswerAlias]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_AnswerAlias] ON [dbo].[vts_tbAnswer]
(
	[AnswerAlias] ASC,
	[DisplayOrder] ASC,
	[ScorePoint] ASC
)
INCLUDE ( 	[AnswerID]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
GO
/****** Object:  Index [IX_QuestionID]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_QuestionID] ON [dbo].[vts_tbAnswer]
(
	[QuestionID] ASC,
	[DisplayOrder] ASC,
	[ScorePoint] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
GO
/****** Object:  Index [IX_ScorePoint]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_ScorePoint] ON [dbo].[vts_tbAnswer]
(
	[ScorePoint] ASC
)
INCLUDE ( 	[AnswerID]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
GO
/****** Object:  Index [PK_vts_tbQuestion]    Script Date: 19-8-2014 22:01:40 ******/
ALTER TABLE [dbo].[vts_tbQuestion] ADD  CONSTRAINT [PK_vts_tbQuestion] PRIMARY KEY NONCLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_DisplayOrder]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_DisplayOrder] ON [dbo].[vts_tbQuestion]
(
	[DisplayOrder] ASC,
	[QuestionIDText] ASC
)
INCLUDE ( 	[QuestionID]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Question]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_Question] ON [dbo].[vts_tbQuestion]
(
	[QuestionIDText] ASC,
	[QuestionGroupID] ASC,
	[DisplayOrder] ASC
)
INCLUDE ( 	[QuestionID]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_QuestionAlias]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_QuestionAlias] ON [dbo].[vts_tbQuestion]
(
	[Alias] ASC,
	[QuestionIDText] ASC
)
INCLUDE ( 	[QuestionID]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
GO
/****** Object:  Index [IX_QuestionGroupID]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_QuestionGroupID] ON [dbo].[vts_tbQuestion]
(
	[QuestionGroupID] ASC
)
INCLUDE ( 	[QuestionID]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RatingEnabled]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_RatingEnabled] ON [dbo].[vts_tbQuestion]
(
	[RatingEnabled] ASC
)
INCLUDE ( 	[QuestionID],
	[QuestionText],
	[DisplayOrder],
	[QuestionIDText],
	[Alias],
	[QuestionGroupID]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SurveyID]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_SurveyID] ON [dbo].[vts_tbQuestion]
(
	[SurveyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_GroupName]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_GroupName] ON [dbo].[vts_tbQuestionGroups]
(
	[GroupName] ASC,
	[ParentGroupID] ASC
)
INCLUDE ( 	[ID]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
GO
/****** Object:  Index [IX_ParentGroupID]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_ParentGroupID] ON [dbo].[vts_tbQuestionGroups]
(
	[ParentGroupID] ASC
)
INCLUDE ( 	[ID]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
GO
/****** Object:  Index [IX_QuestionGroups]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_QuestionGroups] ON [dbo].[vts_tbQuestionGroups]
(
	[DisplayOrder] ASC
)
INCLUDE ( 	[ID]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
GO
/****** Object:  Index [PK_vts_tbSurvey]    Script Date: 19-8-2014 22:01:40 ******/
ALTER TABLE [dbo].[vts_tbSurvey] ADD  CONSTRAINT [PK_vts_tbSurvey] PRIMARY KEY NONCLUSTERED 
(
	[SurveyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
GO
/****** Object:  Index [IDX_Voter]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IDX_Voter] ON [dbo].[vts_tbVoter]
(
	[Validated] ASC,
	[SurveyID] ASC
)
INCLUDE ( 	[VoterID]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
GO
/****** Object:  Index [IX_SurveyID]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_SurveyID] ON [dbo].[vts_tbVoter]
(
	[SurveyID] ASC
)
INCLUDE ( 	[VoterID],
	[Validated]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Answer]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_Answer] ON [dbo].[vts_tbVoterAnswers]
(
	[AnswerID] ASC
)
INCLUDE ( 	[VoterID]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
GO
ALTER TABLE [dbo].[vts_tbAnswer] ADD  CONSTRAINT [DF_vts_tbAnswer_iAnswerTypeID]  DEFAULT ((1)) FOR [AnswerTypeID]
GO
ALTER TABLE [dbo].[vts_tbAnswer] ADD  CONSTRAINT [DF_vts_tbAnswer_RatingPart]  DEFAULT ((0)) FOR [RatePart]
GO
ALTER TABLE [dbo].[vts_tbAnswer] ADD  CONSTRAINT [DF_vts_tbAnswer_DisplayOrder]  DEFAULT ((1)) FOR [DisplayOrder]
GO
ALTER TABLE [dbo].[vts_tbAnswer] ADD  CONSTRAINT [DF_vts_tbAnswer_Selected]  DEFAULT ((0)) FOR [Selected]
GO
ALTER TABLE [dbo].[vts_tbAnswer] ADD  CONSTRAINT [DF_vts_tbAnswer_ScorePoint]  DEFAULT ((0)) FOR [ScorePoint]
GO
ALTER TABLE [dbo].[vts_tbAnswer] ADD  CONSTRAINT [DF_vts_tbAnswer_Mandatory]  DEFAULT ((0)) FOR [Mandatory]
GO
ALTER TABLE [dbo].[vts_tbAnswerType] ADD  CONSTRAINT [DF_vts_tbAnswerType_BuiltIn]  DEFAULT ((0)) FOR [BuiltIn]
GO
ALTER TABLE [dbo].[vts_tbAnswerType] ADD  CONSTRAINT [DF_vts_tbAnswerType_FieldWidth]  DEFAULT ((0)) FOR [FieldWidth]
GO
ALTER TABLE [dbo].[vts_tbAnswerType] ADD  CONSTRAINT [DF_vts_tbAnswerType_FieldHeight]  DEFAULT ((0)) FOR [FieldHeight]
GO
ALTER TABLE [dbo].[vts_tbAnswerType] ADD  CONSTRAINT [DF_vts_tbAnswerType_FieldLength]  DEFAULT ((0)) FOR [FieldLength]
GO
ALTER TABLE [dbo].[vts_tbAnswerType] ADD  CONSTRAINT [DF_vts_tbAnswerType_TypeMode]  DEFAULT ((0)) FOR [TypeMode]
GO
ALTER TABLE [dbo].[vts_tbAnswerType] ADD  CONSTRAINT [DF_vts_tbAnswerType_PublicFieldResults]  DEFAULT ((0)) FOR [PublicFieldResults]
GO
ALTER TABLE [dbo].[vts_tbBranchingRule] ADD  CONSTRAINT [DF_vts_tbBranchingRule_ExpressionOperatorID]  DEFAULT ((1)) FOR [ExpressionOperator]
GO
ALTER TABLE [dbo].[vts_tbFile] ADD  CONSTRAINT [DF_vts_tbFile_SaveDate]  DEFAULT (getdate()) FOR [SaveDate]
GO
ALTER TABLE [dbo].[vts_tbFile] ADD  CONSTRAINT [DF_vts_tbFile_FileName]  DEFAULT (getdate()) FOR [FileName]
GO
ALTER TABLE [dbo].[vts_tbInvitationLog] ADD  CONSTRAINT [DF_vts_InvitationLog_ErrorDate]  DEFAULT (getdate()) FOR [ErrorDate]
GO
ALTER TABLE [dbo].[vts_tbInvitationQueue] ADD  CONSTRAINT [DF_vts_tbInvitationQueue_RequestDate]  DEFAULT (getdate()) FOR [RequestDate]
GO
ALTER TABLE [dbo].[vts_tbLibraryLanguage] ADD  CONSTRAINT [DF_vts_tbLibraryLanguage_DefaultLanguage]  DEFAULT ((0)) FOR [DefaultLanguage]
GO
ALTER TABLE [dbo].[vts_tbPageOption] ADD  CONSTRAINT [DF_vts_tbPageOptions_RandomizeQuestions]  DEFAULT ((0)) FOR [RandomizeQuestions]
GO
ALTER TABLE [dbo].[vts_tbQuestion] ADD  CONSTRAINT [DF_vts_tbQuestion_iQuestionLayoutID]  DEFAULT ((0)) FOR [SelectionModeID]
GO
ALTER TABLE [dbo].[vts_tbQuestion] ADD  CONSTRAINT [DF_vts_tbQuestion_ColumnsNumber]  DEFAULT ((0)) FOR [ColumnsNumber]
GO
ALTER TABLE [dbo].[vts_tbQuestion] ADD  CONSTRAINT [DF_vts_tbQuestion_MinSelectionRequired]  DEFAULT ((0)) FOR [MinSelectionRequired]
GO
ALTER TABLE [dbo].[vts_tbQuestion] ADD  CONSTRAINT [DF_vts_tbQuestion_MaxSelectionAllowed]  DEFAULT ((0)) FOR [MaxSelectionAllowed]
GO
ALTER TABLE [dbo].[vts_tbQuestion] ADD  CONSTRAINT [DF_vts_tbQuestion_RatingEnabled]  DEFAULT ((0)) FOR [RatingEnabled]
GO
ALTER TABLE [dbo].[vts_tbQuestion] ADD  CONSTRAINT [DF_vts_tbQuestion_RandomizeAnswers]  DEFAULT ((0)) FOR [RandomizeAnswers]
GO
ALTER TABLE [dbo].[vts_tbQuestion] ADD  CONSTRAINT [DF_vts_tbQuestion_PageNumber]  DEFAULT ((1)) FOR [PageNumber]
GO
ALTER TABLE [dbo].[vts_tbQuestion] ADD  CONSTRAINT [DF_vts_tbQuestion_ShowHelpText]  DEFAULT ((0)) FOR [ShowHelpText]
GO
ALTER TABLE [dbo].[vts_tbQuestionSectionOption] ADD  CONSTRAINT [DF_vts_tbQuestionSectionOption_RepeatableSectionModeID]  DEFAULT ((0)) FOR [RepeatableSectionModeID]
GO
ALTER TABLE [dbo].[vts_tbQuestionSectionOption] ADD  CONSTRAINT [DF_vts_tbQuestionSectionOption_MaxSections]  DEFAULT ((0)) FOR [MaxSections]
GO
ALTER TABLE [dbo].[vts_tbRegularExpression] ADD  CONSTRAINT [DF_vts_tbRegularExpression_BuiltIn]  DEFAULT ((0)) FOR [BuiltIn]
GO
ALTER TABLE [dbo].[vts_tbSurvey] ADD  CONSTRAINT [DF_vts_tbSurvey_ProgressDisplayModeID]  DEFAULT ((2)) FOR [ProgressDisplayModeID]
GO
ALTER TABLE [dbo].[vts_tbSurvey] ADD  CONSTRAINT [DF_vts_tbSurvey_NotificationModeID]  DEFAULT ((1)) FOR [NotificationModeID]
GO
ALTER TABLE [dbo].[vts_tbSurvey] ADD  CONSTRAINT [DF_vts_tbSurvey_ResumeModeID]  DEFAULT ((1)) FOR [ResumeModeID]
GO
ALTER TABLE [dbo].[vts_tbSurvey] ADD  CONSTRAINT [DF_vts_tbSurvey_dCreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[vts_tbSurvey] ADD  CONSTRAINT [DF_vts_tbSurvey_iPollBoxDisplayTimes]  DEFAULT ((0)) FOR [SurveyDisplayTimes]
GO
ALTER TABLE [dbo].[vts_tbSurvey] ADD  CONSTRAINT [DF_vts_tbSurvey_iResultsDisplayTimes]  DEFAULT ((0)) FOR [ResultsDisplayTimes]
GO
ALTER TABLE [dbo].[vts_tbSurvey] ADD  CONSTRAINT [DF_vts_tbSurvey_NavigationEnabled]  DEFAULT ((0)) FOR [NavigationEnabled]
GO
ALTER TABLE [dbo].[vts_tbSurvey] ADD  CONSTRAINT [DF_vts_tbSurvey_bArchive]  DEFAULT ((0)) FOR [Archive]
GO
ALTER TABLE [dbo].[vts_tbSurvey] ADD  CONSTRAINT [DF_vts_tbSurvey_bActivated]  DEFAULT ((0)) FOR [Activated]
GO
ALTER TABLE [dbo].[vts_tbSurvey] ADD  CONSTRAINT [DF_vts_tbSurvey_Scored]  DEFAULT ((0)) FOR [Scored]
GO
ALTER TABLE [dbo].[vts_tbSurvey] ADD  CONSTRAINT [DF_vts_tbSurvey_IPExpires]  DEFAULT ((1440)) FOR [IPExpires]
GO
ALTER TABLE [dbo].[vts_tbSurvey] ADD  CONSTRAINT [DF_vts_tbSurvey_CookieExpires]  DEFAULT ((1440)) FOR [CookieExpires]
GO
ALTER TABLE [dbo].[vts_tbSurvey] ADD  CONSTRAINT [DF_vts_tbSurvey_OnyInvited]  DEFAULT ((0)) FOR [OnlyInvited]
GO
ALTER TABLE [dbo].[vts_tbSurvey] ADD  CONSTRAINT [DF_vts_tbSurvey_AllowMultipleUserNameSubmissions]  DEFAULT ((0)) FOR [AllowMultipleUserNameSubmissions]
GO
ALTER TABLE [dbo].[vts_tbSurvey] ADD  CONSTRAINT [DF_vts_tbSurvey_DisableQuestionNumbering]  DEFAULT ((0)) FOR [QuestionNumberingDisabled]
GO
ALTER TABLE [dbo].[vts_tbSurvey] ADD  CONSTRAINT [DF_vts_tbSurvey_AllowMultipleNSurveySubmissions]  DEFAULT ((0)) FOR [AllowMultipleNSurveySubmissions]
GO
ALTER TABLE [dbo].[vts_tbSurvey] ADD  CONSTRAINT [DF_vts_tbSurvey_MultiLanguageModeID]  DEFAULT ((0)) FOR [MultiLanguageModeID]
GO
ALTER TABLE [dbo].[vts_tbSurvey] ADD  CONSTRAINT [DF_vts_tbSurvey_SurveyGuid]  DEFAULT (newid()) FOR [SurveyGuid]
GO
ALTER TABLE [dbo].[vts_tbSurvey] ADD  CONSTRAINT [DF_vts_tbSurvey_DefaultSurvey]  DEFAULT ((0)) FOR [DefaultSurvey]
GO
ALTER TABLE [dbo].[vts_tbSurveyEntryQuota] ADD  CONSTRAINT [DF_vts_tbSurveyEntryQuota_MaxEntries]  DEFAULT ((1)) FOR [MaxEntries]
GO
ALTER TABLE [dbo].[vts_tbSurveyEntryQuota] ADD  CONSTRAINT [DF_vts_tbSurveyEntryQuota_EntryCount]  DEFAULT ((0)) FOR [EntryCount]
GO
ALTER TABLE [dbo].[vts_tbSurveyLanguage] ADD  CONSTRAINT [DF_vts_tbSurveyLanguage_DefaultLanguage]  DEFAULT ((0)) FOR [DefaultLanguage]
GO
ALTER TABLE [dbo].[vts_tbSurveyWebSecurity] ADD  CONSTRAINT [DF_vts_tbSurveyWebSecurity_AddInOrder]  DEFAULT ((1)) FOR [AddInOrder]
GO
ALTER TABLE [dbo].[vts_tbSurveyWebSecurity] ADD  CONSTRAINT [DF_vts_tbSurveyWebSecurity_Disabled]  DEFAULT ((0)) FOR [Disabled]
GO
ALTER TABLE [dbo].[vts_tbUser] ADD  CONSTRAINT [DF_vts_tbUser_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[vts_tbUserSetting] ADD  CONSTRAINT [DF_vts_tbUserSettings_IsAdmin]  DEFAULT ((0)) FOR [IsAdmin]
GO
ALTER TABLE [dbo].[vts_tbUserSetting] ADD  CONSTRAINT [DF_vts_tbUserSettings_GlobalSurveyAccess]  DEFAULT ((0)) FOR [GlobalSurveyAccess]
GO
ALTER TABLE [dbo].[vts_tbVoter] ADD  CONSTRAINT [DF_vts_tbVoter_VoteDate]  DEFAULT (getdate()) FOR [VoteDate]
GO
ALTER TABLE [dbo].[vts_tbVoter] ADD  CONSTRAINT [DF_vts_tbVoter_Validated]  DEFAULT ((0)) FOR [Validated]
GO
ALTER TABLE [dbo].[vts_tbVoterAnswers] ADD  CONSTRAINT [DF_vts_tbVoterAnswers_SectionID]  DEFAULT ((0)) FOR [SectionNumber]
GO
ALTER TABLE [dbo].[vts_tbWebSecurityAddIn] ADD  CONSTRAINT [DF_vts_tbWebSecurityAddIn_BuiltIn]  DEFAULT ((0)) FOR [BuiltIn]
GO
ALTER TABLE [dbo].[vts_tbAnswer]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbAnswer_vts_tbAnswerType] FOREIGN KEY([AnswerTypeID])
REFERENCES [dbo].[vts_tbAnswerType] ([AnswerTypeID])
GO
ALTER TABLE [dbo].[vts_tbAnswer] CHECK CONSTRAINT [FK_vts_tbAnswer_vts_tbAnswerType]
GO
ALTER TABLE [dbo].[vts_tbAnswer]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbAnswer_vts_tbQuestion] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[vts_tbQuestion] ([QuestionID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbAnswer] CHECK CONSTRAINT [FK_vts_tbAnswer_vts_tbQuestion]
GO
ALTER TABLE [dbo].[vts_tbAnswer]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbAnswer_vts_tbRegularExpression] FOREIGN KEY([RegularExpressionID])
REFERENCES [dbo].[vts_tbRegularExpression] ([RegularExpressionID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbAnswer] CHECK CONSTRAINT [FK_vts_tbAnswer_vts_tbRegularExpression]
GO
ALTER TABLE [dbo].[vts_tbAnswerConnection]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbAnswerSubscriber_vts_tbAnswer] FOREIGN KEY([SubscriberAnswerID])
REFERENCES [dbo].[vts_tbAnswer] ([AnswerID])
GO
ALTER TABLE [dbo].[vts_tbAnswerConnection] CHECK CONSTRAINT [FK_vts_tbAnswerSubscriber_vts_tbAnswer]
GO
ALTER TABLE [dbo].[vts_tbAnswerConnection]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbAnswerSubscriber_vts_tbAnswer1] FOREIGN KEY([PublisherAnswerID])
REFERENCES [dbo].[vts_tbAnswer] ([AnswerID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbAnswerConnection] CHECK CONSTRAINT [FK_vts_tbAnswerSubscriber_vts_tbAnswer1]
GO
ALTER TABLE [dbo].[vts_tbAnswerProperty]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbAnswerProperty_vts_tbAnswer] FOREIGN KEY([AnswerID])
REFERENCES [dbo].[vts_tbAnswer] ([AnswerID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbAnswerProperty] CHECK CONSTRAINT [FK_vts_tbAnswerProperty_vts_tbAnswer]
GO
ALTER TABLE [dbo].[vts_tbBranchingRule]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbBranchingRule_vts_tbQuestion] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[vts_tbQuestion] ([QuestionID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbBranchingRule] CHECK CONSTRAINT [FK_vts_tbBranchingRule_vts_tbQuestion]
GO
ALTER TABLE [dbo].[vts_tbEmailNotificationSettings]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbEmailNotificationSettings_vts_tbSurvey] FOREIGN KEY([SurveyID])
REFERENCES [dbo].[vts_tbSurvey] ([SurveyID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbEmailNotificationSettings] CHECK CONSTRAINT [FK_vts_tbEmailNotificationSettings_vts_tbSurvey]
GO
ALTER TABLE [dbo].[vts_tbFilter]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbFilter_vts_tbSurvey] FOREIGN KEY([SurveyID])
REFERENCES [dbo].[vts_tbSurvey] ([SurveyID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbFilter] CHECK CONSTRAINT [FK_vts_tbFilter_vts_tbSurvey]
GO
ALTER TABLE [dbo].[vts_tbFilterRule]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbFilterRule_vts_tbAnswer] FOREIGN KEY([AnswerID])
REFERENCES [dbo].[vts_tbAnswer] ([AnswerID])
GO
ALTER TABLE [dbo].[vts_tbFilterRule] CHECK CONSTRAINT [FK_vts_tbFilterRule_vts_tbAnswer]
GO
ALTER TABLE [dbo].[vts_tbFilterRule]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbFilterRule_vts_tbFilter] FOREIGN KEY([FilterID])
REFERENCES [dbo].[vts_tbFilter] ([FilterID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbFilterRule] CHECK CONSTRAINT [FK_vts_tbFilterRule_vts_tbFilter]
GO
ALTER TABLE [dbo].[vts_tbFilterRule]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbFilterRule_vts_tbQuestion] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[vts_tbQuestion] ([QuestionID])
GO
ALTER TABLE [dbo].[vts_tbFilterRule] CHECK CONSTRAINT [FK_vts_tbFilterRule_vts_tbQuestion]
GO
ALTER TABLE [dbo].[vts_tbFolders]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbFolders_vts_tbFolders] FOREIGN KEY([ParentFolderID])
REFERENCES [dbo].[vts_tbFolders] ([FolderID])
GO
ALTER TABLE [dbo].[vts_tbFolders] CHECK CONSTRAINT [FK_vts_tbFolders_vts_tbFolders]
GO
ALTER TABLE [dbo].[vts_tbInvitationLog]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbInvitationLog_vts_tbEmail] FOREIGN KEY([EmailID])
REFERENCES [dbo].[vts_tbEmail] ([EmailID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbInvitationLog] CHECK CONSTRAINT [FK_vts_tbInvitationLog_vts_tbEmail]
GO
ALTER TABLE [dbo].[vts_tbInvitationLog]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbInvitationLog_vts_tbSurvey] FOREIGN KEY([SurveyID])
REFERENCES [dbo].[vts_tbSurvey] ([SurveyID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbInvitationLog] CHECK CONSTRAINT [FK_vts_tbInvitationLog_vts_tbSurvey]
GO
ALTER TABLE [dbo].[vts_tbInvitationQueue]  WITH CHECK ADD  CONSTRAINT [FK_tbMailingQueue_vts_tbEmail] FOREIGN KEY([EmailID])
REFERENCES [dbo].[vts_tbEmail] ([EmailID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbInvitationQueue] CHECK CONSTRAINT [FK_tbMailingQueue_vts_tbEmail]
GO
ALTER TABLE [dbo].[vts_tbInvitationQueue]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbInvitationQueue_vts_tbSurvey] FOREIGN KEY([SurveyID])
REFERENCES [dbo].[vts_tbSurvey] ([SurveyID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbInvitationQueue] CHECK CONSTRAINT [FK_vts_tbInvitationQueue_vts_tbSurvey]
GO
ALTER TABLE [dbo].[vts_tbLibraryLanguage]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbLibraryLanguage_vts_tbLibrary] FOREIGN KEY([LibraryID])
REFERENCES [dbo].[vts_tbLibrary] ([LibraryID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbLibraryLanguage] CHECK CONSTRAINT [FK_vts_tbLibraryLanguage_vts_tbLibrary]
GO
ALTER TABLE [dbo].[vts_tbLibraryLanguage]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbLibraryLanguage_vts_tbMultiLanguage] FOREIGN KEY([LanguageCode])
REFERENCES [dbo].[vts_tbMultiLanguage] ([LanguageCode])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbLibraryLanguage] CHECK CONSTRAINT [FK_vts_tbLibraryLanguage_vts_tbMultiLanguage]
GO
ALTER TABLE [dbo].[vts_tbMessageCondition]  WITH CHECK ADD  CONSTRAINT [FK_tbMessageCondition_vts_tbSurvey] FOREIGN KEY([SurveyID])
REFERENCES [dbo].[vts_tbSurvey] ([SurveyID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbMessageCondition] CHECK CONSTRAINT [FK_tbMessageCondition_vts_tbSurvey]
GO
ALTER TABLE [dbo].[vts_tbMultiLanguageText]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbMultiLanguageText_vts_tbLanguageMessageType] FOREIGN KEY([LanguageMessageTypeID])
REFERENCES [dbo].[vts_tbLanguageMessageType] ([LanguageMessageTypeID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbMultiLanguageText] CHECK CONSTRAINT [FK_vts_tbMultiLanguageText_vts_tbLanguageMessageType]
GO
ALTER TABLE [dbo].[vts_tbPageOption]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbPageOption_vts_tbSurvey] FOREIGN KEY([SurveyID])
REFERENCES [dbo].[vts_tbSurvey] ([SurveyID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbPageOption] CHECK CONSTRAINT [FK_vts_tbPageOption_vts_tbSurvey]
GO
ALTER TABLE [dbo].[vts_tbQuestion]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbQuestion_vts_tbLayoutMode] FOREIGN KEY([LayoutModeID])
REFERENCES [dbo].[vts_tbLayoutMode] ([LayoutModeID])
GO
ALTER TABLE [dbo].[vts_tbQuestion] CHECK CONSTRAINT [FK_vts_tbQuestion_vts_tbLayoutMode]
GO
ALTER TABLE [dbo].[vts_tbQuestion]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbQuestion_vts_tbLibrary] FOREIGN KEY([LibraryID])
REFERENCES [dbo].[vts_tbLibrary] ([LibraryID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbQuestion] CHECK CONSTRAINT [FK_vts_tbQuestion_vts_tbLibrary]
GO
ALTER TABLE [dbo].[vts_tbQuestion]  WITH NOCHECK ADD  CONSTRAINT [FK_vts_tbQuestion_vts_tbQuestionGroups] FOREIGN KEY([QuestionGroupID])
REFERENCES [dbo].[vts_tbQuestionGroups] ([ID])
GO
ALTER TABLE [dbo].[vts_tbQuestion] CHECK CONSTRAINT [FK_vts_tbQuestion_vts_tbQuestionGroups]
GO
ALTER TABLE [dbo].[vts_tbQuestion]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbQuestion_vts_tbQuestionSelectionMode] FOREIGN KEY([SelectionModeID])
REFERENCES [dbo].[vts_tbQuestionSelectionMode] ([QuestionSelectionModeID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbQuestion] CHECK CONSTRAINT [FK_vts_tbQuestion_vts_tbQuestionSelectionMode]
GO
ALTER TABLE [dbo].[vts_tbQuestion]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbQuestion_vts_tbSurvey] FOREIGN KEY([SurveyID])
REFERENCES [dbo].[vts_tbSurvey] ([SurveyID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbQuestion] CHECK CONSTRAINT [FK_vts_tbQuestion_vts_tbSurvey]
GO
ALTER TABLE [dbo].[vts_tbQuestionSectionGridAnswer]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbQuestionSectionGridAnswers_vts_tbAnswer] FOREIGN KEY([AnswerID])
REFERENCES [dbo].[vts_tbAnswer] ([AnswerID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbQuestionSectionGridAnswer] CHECK CONSTRAINT [FK_vts_tbQuestionSectionGridAnswers_vts_tbAnswer]
GO
ALTER TABLE [dbo].[vts_tbQuestionSectionOption]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbQuestionSectionOption_vts_tbQuestion] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[vts_tbQuestion] ([QuestionID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbQuestionSectionOption] CHECK CONSTRAINT [FK_vts_tbQuestionSectionOption_vts_tbQuestion]
GO
ALTER TABLE [dbo].[vts_tbQuestionSectionOption]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbQuestionSectionOption_vts_tbRepeatableSection] FOREIGN KEY([RepeatableSectionModeID])
REFERENCES [dbo].[vts_tbRepeatableSection] ([RepeatableSectionModeID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbQuestionSectionOption] CHECK CONSTRAINT [FK_vts_tbQuestionSectionOption_vts_tbRepeatableSection]
GO
ALTER TABLE [dbo].[vts_tbRoleSecurityRight]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbRoleSecurityRight_vts_tbRole] FOREIGN KEY([RoleID])
REFERENCES [dbo].[vts_tbRole] ([RoleID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbRoleSecurityRight] CHECK CONSTRAINT [FK_vts_tbRoleSecurityRight_vts_tbRole]
GO
ALTER TABLE [dbo].[vts_tbRoleSecurityRight]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbRoleSecurityRight_vts_tbSecurityRight] FOREIGN KEY([SecurityRightID])
REFERENCES [dbo].[vts_tbSecurityRight] ([SecurityRightID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbRoleSecurityRight] CHECK CONSTRAINT [FK_vts_tbRoleSecurityRight_vts_tbSecurityRight]
GO
ALTER TABLE [dbo].[vts_tbSkipLogicRule]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbSkipLogicRule_vts_tbQuestion] FOREIGN KEY([SkipQuestionID])
REFERENCES [dbo].[vts_tbQuestion] ([QuestionID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbSkipLogicRule] CHECK CONSTRAINT [FK_vts_tbSkipLogicRule_vts_tbQuestion]
GO
ALTER TABLE [dbo].[vts_tbSurvey]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbSurvey_vts_tbFolders] FOREIGN KEY([FolderID])
REFERENCES [dbo].[vts_tbFolders] ([FolderID])
GO
ALTER TABLE [dbo].[vts_tbSurvey] CHECK CONSTRAINT [FK_vts_tbSurvey_vts_tbFolders]
GO
ALTER TABLE [dbo].[vts_tbSurvey]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbSurvey_vts_tbMultiLanguageMode] FOREIGN KEY([MultiLanguageModeID])
REFERENCES [dbo].[vts_tbMultiLanguageMode] ([MultiLanguageModeID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbSurvey] CHECK CONSTRAINT [FK_vts_tbSurvey_vts_tbMultiLanguageMode]
GO
ALTER TABLE [dbo].[vts_tbSurvey]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbSurvey_vts_tbNotificationMode] FOREIGN KEY([NotificationModeID])
REFERENCES [dbo].[vts_tbNotificationMode] ([NotificationModeID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[vts_tbSurvey] CHECK CONSTRAINT [FK_vts_tbSurvey_vts_tbNotificationMode]
GO
ALTER TABLE [dbo].[vts_tbSurvey]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbSurvey_vts_tbProgressDisplayMode] FOREIGN KEY([ProgressDisplayModeID])
REFERENCES [dbo].[vts_tbProgressDisplayMode] ([ProgressDisplayModeID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[vts_tbSurvey] CHECK CONSTRAINT [FK_vts_tbSurvey_vts_tbProgressDisplayMode]
GO
ALTER TABLE [dbo].[vts_tbSurvey]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbSurvey_vts_tbResumeMode] FOREIGN KEY([ResumeModeID])
REFERENCES [dbo].[vts_tbResumeMode] ([ResumeModeID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[vts_tbSurvey] CHECK CONSTRAINT [FK_vts_tbSurvey_vts_tbResumeMode]
GO
ALTER TABLE [dbo].[vts_tbSurvey]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbSurvey_vts_tbUnAuthentifiedUserAction] FOREIGN KEY([UnAuthentifiedUserActionID])
REFERENCES [dbo].[vts_tbUnAuthentifiedUserAction] ([UnAuthentifiedUserActionID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[vts_tbSurvey] CHECK CONSTRAINT [FK_vts_tbSurvey_vts_tbUnAuthentifiedUserAction]
GO
ALTER TABLE [dbo].[vts_tbSurveyAsset]  WITH CHECK ADD FOREIGN KEY([SurveyID])
REFERENCES [dbo].[vts_tbSurvey] ([SurveyID])
GO
ALTER TABLE [dbo].[vts_tbSurveyEntryQuota]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbSurveyEntryQuota_vts_tbSurvey] FOREIGN KEY([SurveyID])
REFERENCES [dbo].[vts_tbSurvey] ([SurveyID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbSurveyEntryQuota] CHECK CONSTRAINT [FK_vts_tbSurveyEntryQuota_vts_tbSurvey]
GO
ALTER TABLE [dbo].[vts_tbSurveyIPRange]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbSurveyIPRange_vts_tbSurvey] FOREIGN KEY([SurveyID])
REFERENCES [dbo].[vts_tbSurvey] ([SurveyID])
GO
ALTER TABLE [dbo].[vts_tbSurveyIPRange] CHECK CONSTRAINT [FK_vts_tbSurveyIPRange_vts_tbSurvey]
GO
ALTER TABLE [dbo].[vts_tbSurveyLanguage]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbSurveyLanguage_vts_tbMultiLanguage] FOREIGN KEY([LanguageCode])
REFERENCES [dbo].[vts_tbMultiLanguage] ([LanguageCode])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbSurveyLanguage] CHECK CONSTRAINT [FK_vts_tbSurveyLanguage_vts_tbMultiLanguage]
GO
ALTER TABLE [dbo].[vts_tbSurveyLanguage]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbSurveyLanguage_vts_tbSurvey] FOREIGN KEY([SurveyID])
REFERENCES [dbo].[vts_tbSurvey] ([SurveyID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbSurveyLanguage] CHECK CONSTRAINT [FK_vts_tbSurveyLanguage_vts_tbSurvey]
GO
ALTER TABLE [dbo].[vts_tbSurveyLayout]  WITH CHECK ADD  CONSTRAINT [FK_SurveyLayout_vts_tbSurvey] FOREIGN KEY([SurveyID])
REFERENCES [dbo].[vts_tbSurvey] ([SurveyID])
GO
ALTER TABLE [dbo].[vts_tbSurveyLayout] CHECK CONSTRAINT [FK_SurveyLayout_vts_tbSurvey]
GO
ALTER TABLE [dbo].[vts_tbSurveyToken]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbSurveyToken_vts_tbSurvey] FOREIGN KEY([SurveyID])
REFERENCES [dbo].[vts_tbSurvey] ([SurveyID])
GO
ALTER TABLE [dbo].[vts_tbSurveyToken] CHECK CONSTRAINT [FK_vts_tbSurveyToken_vts_tbSurvey]
GO
ALTER TABLE [dbo].[vts_tbSurveyWebSecurity]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbSurveyWebSecurity_vts_tbSurvey] FOREIGN KEY([SurveyID])
REFERENCES [dbo].[vts_tbSurvey] ([SurveyID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbSurveyWebSecurity] CHECK CONSTRAINT [FK_vts_tbSurveyWebSecurity_vts_tbSurvey]
GO
ALTER TABLE [dbo].[vts_tbSurveyWebSecurity]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbSurveyWebSecurity_vts_tbWebSecurityAddIn] FOREIGN KEY([WebSecurityAddInID])
REFERENCES [dbo].[vts_tbWebSecurityAddIn] ([WebSecurityAddInID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbSurveyWebSecurity] CHECK CONSTRAINT [FK_vts_tbSurveyWebSecurity_vts_tbWebSecurityAddIn]
GO
ALTER TABLE [dbo].[vts_tbUserAnswerType]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbUserAnswerType_vts_tbAnswerType] FOREIGN KEY([AnswerTypeID])
REFERENCES [dbo].[vts_tbAnswerType] ([AnswerTypeID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbUserAnswerType] CHECK CONSTRAINT [FK_vts_tbUserAnswerType_vts_tbAnswerType]
GO
ALTER TABLE [dbo].[vts_tbUserRegularExpression]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbUserRegularExpression_vts_tbRegularExpression] FOREIGN KEY([RegularExpressionID])
REFERENCES [dbo].[vts_tbRegularExpression] ([RegularExpressionID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbUserRegularExpression] CHECK CONSTRAINT [FK_vts_tbUserRegularExpression_vts_tbRegularExpression]
GO
ALTER TABLE [dbo].[vts_tbUserSurvey]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbUserSurvey_vts_tbSurvey] FOREIGN KEY([SurveyID])
REFERENCES [dbo].[vts_tbSurvey] ([SurveyID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbUserSurvey] CHECK CONSTRAINT [FK_vts_tbUserSurvey_vts_tbSurvey]
GO
ALTER TABLE [dbo].[vts_tbVoterAnswers]  WITH CHECK ADD  CONSTRAINT [FK_VoterAnswers_vts_tbAnswer] FOREIGN KEY([AnswerID])
REFERENCES [dbo].[vts_tbAnswer] ([AnswerID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbVoterAnswers] CHECK CONSTRAINT [FK_VoterAnswers_vts_tbAnswer]
GO
ALTER TABLE [dbo].[vts_tbVoterAnswers]  WITH CHECK ADD  CONSTRAINT [FK_VoterAnswers_vts_tbVoter] FOREIGN KEY([VoterID])
REFERENCES [dbo].[vts_tbVoter] ([VoterID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbVoterAnswers] CHECK CONSTRAINT [FK_VoterAnswers_vts_tbVoter]
GO
ALTER TABLE [dbo].[vts_tbVoterEmail]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbVoterEmail_vts_tbEmail] FOREIGN KEY([EmailID])
REFERENCES [dbo].[vts_tbEmail] ([EmailID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbVoterEmail] CHECK CONSTRAINT [FK_vts_tbVoterEmail_vts_tbEmail]
GO
ALTER TABLE [dbo].[vts_tbFolders]  WITH CHECK ADD  CONSTRAINT [chk_Folder] CHECK  (([ParentFolderID] IS NULL AND [dbo].[vts_fnGetFolderRootCount]()<(2) OR [ParentFolderID] IS NOT NULL))
GO
ALTER TABLE [dbo].[vts_tbFolders] CHECK CONSTRAINT [chk_Folder]
GO
