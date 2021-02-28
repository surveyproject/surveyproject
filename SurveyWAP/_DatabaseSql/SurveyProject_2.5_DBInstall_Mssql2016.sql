--SURVEY PROJECT v2.5 - MsSql2016 / AZURE compatible DB script - compatible with: Collation Turkish CI AS

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

-- Retrieve the current display order
SELECT @QuestionID = QuestionID, @DisplayOrder  = DisplayOrder
FROM vts_tbAnswer 
WHERE AnswerID = @AnswerID

-- Deletes the answer
DELETE FROM vts_tbAnswer WHERE AnswerID = @AnswerID

-- Updates the answers display order
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
	Survey Project changes: copyright (c) 2016, W3DevPro TM (http://github.com/surveyproject)	

	NSurvey - The web survey and form engine
	Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)

	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
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
/// Get all question's answers
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
/// Get all question's answers
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
/// The ID of the parent matrix question to which you will add the column
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
/// The created answer's ID
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
/// The ID of the parent matrix question to get the child question IDs
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
	Survey Project changes: copyright (c) 2016, W3DevPro TM (http://github.com/surveyproject)	

	NSurvey - The web survey and form engine
	Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)
	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
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
/// Deletes the given answer type ID
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
/// Retrieves all the infos of the given answer type ID
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
	Survey Project changes: copyright (c) 2016, W3DevPro TM (http://github.com/surveyproject)	

	NSurvey - The web survey and form engine
	Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)

	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
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
/// Get all details about the requested file
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
/// Get all files of the given group using the Guid
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

If (@CurrentPage = -1 and @PageSize = -1)
(SELECT FileId, VoterId, GroupGuid, FileName, FileType, FileSize, SaveDate
FROM #TempTable)
else 
(SELECT FileId, VoterId, GroupGuid, FileName, FileType, FileSize, SaveDate
FROM #TempTable
WHERE 
	RowId > @FirstRec AND
	RowId < @LastRec
	)

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
declare @Folders as table(FID int, PFID int);
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
///  Get all logs
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
/// Check if the given UID is valID and returns its survey ID
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
/// ID of the library  to retrieve questions from
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
			@Description NVARCHAR(max),
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
	l.LibraryId,
	l.LibraryName,
	l.Description,
	l.DefaultLanguageCode,
	(select count(q.QuestionId) from vts_tbQuestion q where q.LibraryID=l.LibraryId and q.ParentQuestionId is NULL) as QuestionCnt
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
			@Description NVARCHAR(max),
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
@ItemText NVARCHAR(max)
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
			@ItemText NVARCHAR(max)

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
/// Get all surveys email notification modes
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
/// Get all surveys progress modes
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
			@QuestionText NVARCHAR(max),
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

-- Check if there is already a question with the same display order
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
	-- Update the display order
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
/// Get all parent questions that can have any type of answers
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
			@QuestionText nvarchar(4000), 
			@DisplayOrder int,
			@QuestionID int OUTPUT
AS

BEGIN TRAN InsertChildQuestion

--1a Get parent default values
DECLARE 
	@SurveyID int,
	-- @DisplayOrder int,
	@SelectionModeID int,
	@PageNumber int,
	@LibraryID int,
	@RatingEnabled bit

--1b parent question values
SELECT 
	@SurveyID = SurveyID, 
	@LibraryID = LibraryID, 
	@PageNumber = PageNumber, 
	--@DisplayOrder = DisplayOrder, 
	@SelectionModeID = SelectionModeID, 
	@RatingEnabled = RatingEnabled
FROM vts_tbQuestion WHERE QuestionID = @ParentQuestionID


--2a Determine if DisplayOrder is null (new child) or already set (import child)
if @DisplayOrder is NULL

-- 2b if DisplayOrder is null (new Child) set to max DO
BEGIN
--2c Set displayorder to new child
 select @DisplayOrder = max(ISNULL(DisplayOrder, 0)) + 1 
 from vts_tbQuestion 
 where ParentQuestionID = @ParentQuestionID or QuestionID =  @ParentQuestionID
 END 

--3 insert values
INSERT INTO vts_tbQuestion
	(ParentQuestionID,
	SurveyID,
	LibraryID,
	SelectionModeId,
	DisplayOrder,
	PageNumber,
	RatingEnabled,
	QuestionText)
VALUES
	( @ParentQuestionID,
	@SurveyId,
	@LibraryId,
	@SelectionModeID,
	@DisplayOrder,
	@PageNumber,
	@RatingEnabled,
	@QuestionText)

-- 4 add columns(answers) to new row (childquestion)
IF @@rowcount<>0
BEGIN
	set @QuestionID = Scope_Identity()
	-- Assign the same columns to the row
	-- as the parent question
	exec vts_spAnswersCloneByQuestionId @ParentQuestionID,@QuestionID
END

COMMIT TRAN InsertChildQuestion


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
			@QuestionText NVARCHAR(max),
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

	-- Update the display order
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
	(ParentQuestionId, 
	SurveyID,
	LibraryID,
	SelectionModeId, 
	LayoutModeId, 
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
	QuestiongroupID,
	ShowHelpText)
SELECT      
	ParentQuestionId, 
	@NewSurveyID,
	null, 
	SelectionModeId, 
	LayoutModeId, 
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
IF @@rowCount <> 0
BEGIN
	-- Clone the question's answers
	set @QuestionCopyID = Scope_Identity()

	INSERT INTO vts_tbMultiLanguageText(LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText)
		SELECT @QuestionCopyID as LanguageItemID, LanguageCode, LanguageMessageTypeID, ItemText
		FROM vts_tbMultiLanguageText
		WHERE LanguageItemID = @QuestionID AND LanguageMessageTypeID in(3,10,11,12)	

	exec vts_spQuestionChildsClone @QuestionID, @QuestionCopyID, @NewSurveyID
	
	UPDATE vts_tbQuestion 
		SET -- DisplayOrder = @DisplayOrder, 
			LibraryID = NULL,
			PageNumber = @PageNumber 
	WHERE SurveyID = @NewSurveyID AND ParentQuestionid = @QuestionCopyID

	exec vts_spAnswersCloneByQuestionId @QuestionID, @QuestionCopyID

	exec vts_spQuestionSectionOptionClone @QuestionId, @QuestionCopyId

	-- Update the display order
	UPDATE vts_tbQuestion 
	SET DisplayOrder = DisplayOrder + 1 
	WHERE 
		SurveyID = @NewSurveyID 
		AND ( (QuestionID<>@QuestionCopyID AND ParentQuestionID is null) 
		-- OR (ParentQuestionID is not null AND ParentQuestionID <> @QuestionCopyID)
		) 
		AND DisplayOrder >= @DisplayOrder
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
/// Copy an existing question to a library
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
/// Copy all question of a survey to another survey, mainly used in the clone survey process
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

-- Retrieve the current display order
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
	-- Updates the questions display order if a parent question is deleted
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
	Survey Project changes: copyright (c) 2016, W3DevPro TM (http://github.com/surveyproject)	

	NSurvey - The web survey and form engine
	Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)

	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
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
/// Get all question's answers
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
	vts_tbQuestion.DisplayOrder,
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
	QuestionText, 
	vts_tbQuestion.DisplayOrder
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
/// Get all questions and child questions
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
/// Get all question's answers that can be Selected
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
  select ID from vts_tbQuestionGroups
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
/// Get a list from the library of all parent questions that can have any type of answers, except those that have child questions 
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
/// Parent matrix question ID to find child questions
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
	ParentQuestionID is NULL AND
	DisplayOrder > @OldDisplayOrder
	ORDER BY DisplayOrder ASC	

if @@ROWCOUNT <>0
BEGIN
	-- Are we just changing the page or are we moving the question behind another one ?
	IF @OldPageNumber = @NewPageNumber 
	BEGIN
		-- Move down previous question
		UPDATE vts_tbQuestion 
			set DisplayOrder = @OldDisplayOrder 
		WHERE 
			DisplayOrder = @NewDisplayOrder AND
			SurveyID = @SurveyID AND
			ParentQuestionID is NULL

		-- Move up current question
		UPDATE vts_tbQuestion set DisplayOrder = @NewDisplayOrder WHERE QuestionID = @QuestionID AND ParentQuestionID is NULL
	END
	ELSE IF @OldPageNumber +1 < @NewPageNumber 
	BEGIN
		-- Move one page down
		UPDATE vts_tbQuestion 
			set PageNumber = PageNumber+1 
		WHERE QuestionID = @QuestionID AND ParentQuestionID is NULL
	END 
	ELSE
	BEGIN
		-- Move one page down
		UPDATE vts_tbQuestion 
			set PageNumber = @NewPageNumber 
		WHERE QuestionID = @QuestionID AND ParentQuestionID is NULL
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
IF @@ROWCOUNT <>0
BEGIN
	-- Are we just changing the page or are we moving the question in front of another one ?
	IF @OldPageNumber = @NewPageNumber 
	BEGIN
		-- Move down previous question
		UPDATE vts_tbQuestion 
			set DisplayOrder = @OldDisplayOrder 
		WHERE 
			DisplayOrder = @NewDisplayOrder AND
			SurveyID = @SurveyID AND
			ParentQuestionID is NULL 

		-- Move up current question
		UPDATE vts_tbQuestion 
			set DisplayOrder = @NewDisplayOrder 
		WHERE 
			QuestionID = @QuestionID AND ParentQuestionID is NULL
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
	IF @OldPageNumber > 1
	BEGIN
		UPDATE vts_tbQuestion 
			set DisplayOrder = 1, PageNumber = PageNumber-1 
		WHERE 
		QuestionID = @QuestionID AND ParentQuestionID is NULL
	END
END

GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionOrderUpdate]    Script Date: 19-8-2014 22:01:40 ******/
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
///  Reorder question positions in Library 
/// </summary>
/// <param Name="@QuestionID">
/// ID of the questions to move one position up
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spQuestionOrderUpdate] 
@QuestionID int, 
@UpdateUp  bit = 0 -- 1 to move up, or zero to move down
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

declare @NewDisplayOrder int, @LibId int, @OldQuestionId int
declare @OrderIndex int, @CurrentDisplayOrder int, @MaxOrderId int
declare @QID int

select @LibId = LibraryID from vts_tbQuestion where QuestionId = @QuestionID

create table #tempQuestions
    (QuestionId int, DisplayOrder int)

insert #tempQuestions 
	select QuestionId, DisplayOrder 
	from vts_tbQuestion 
	where LibraryID = @LibId AND ParentQuestionId is NULL
	order by DisplayOrder 

DECLARE cursorQuestions CURSOR for
 SELECT QuestionId, DisplayOrder FROM #tempQuestions order by DisplayOrder

 -- we make reorder of displayorder in case of duplicating displayorderid
 set @OrderIndex = 0
 OPEN cursorQuestions
 FETCH NEXT FROM cursorQuestions
 INTO @QID, @CurrentDisplayOrder
 While @@FETCH_STATUS = 0
 Begin
	set @OrderIndex = @OrderIndex + 1
	UPDATE vts_tbQuestion SET DisplayOrder = @OrderIndex WHERE QuestionId = @QID
	FETCH NEXT FROM cursorQuestions
		INTO @QID, @CurrentDisplayOrder
 End
 CLOSE cursorQuestions;
 DEALLOCATE cursorQuestions;
 drop table #tempQuestions;

 select @MaxOrderId = MAX(DisplayOrder) from vts_tbQuestion where LibraryID = @LibId AND ParentQuestionId is NULL
 select @CurrentDisplayOrder = DisplayOrder, @LibId = LibraryID from vts_tbQuestion where QuestionId = @QuestionID
 
 if @UpdateUp > 0
	set @NewDisplayOrder = @CurrentDisplayOrder - 1
 else
	set @NewDisplayOrder = @CurrentDisplayOrder + 1 
       
 if @NewDisplayOrder < 1
	set @NewDisplayOrder = 1
 if @NewDisplayOrder >= @MaxOrderId
	set @NewDisplayOrder = @MaxOrderId

 select @OldQuestionId = QuestionId from vts_tbQuestion where DisplayOrder = @NewDisplayOrder and LibraryID = @LibId
 
 update vts_tbQuestion set DisplayOrder = @NewDisplayOrder 
	where QuestionId = @QuestionId 
 
 update vts_tbQuestion set DisplayOrder = @CurrentDisplayOrder 
	where QuestionId = @OldQuestionId 
  
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
/// Get all questions with their answers
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
/// adds a new grID answer
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
/// Delete a grID answer
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
/// returns all answers to be shown in the section grID
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
	-- Clone answers to be shown in grID section mode
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
/// ID of the library  to retrieve questions from
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
/// Get all questions for a survey
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
/// Get a list of all question templates that are available in the given library
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
/// Get a list of all question templates that are available in the given library
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
/// Get all child questions for a matrix questionl
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
			@QuestionText NVARCHAR(max),
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
/// Retrieves all the infos of the given regular expression  ID
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

/****** Object:  StoredProcedure [dbo].[vts_spRegularExpressionIsInUse]    Script Date: 10/12/2018 12:40:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
	Survey Project: (c) 2018, W3DevPro TM (http://www.w3devpro.com)

	NSurvey - The web survey and form engine
	Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)

	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
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
/// Check if the Regular Expression is in use
/// by an answer
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spRegularExpressionIsInUse] @RegularExpressionID int AS
SELECT
(
SELECT TOP 1 AnswerID FROM vts_tbAnswer WHERE RegularExpressionId = @RegularExpressionID
) AnswerID

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
/****** Object:  StoredProcedure [dbo].[vts_spReportGetScores]    Script Date: 12/29/2017 09:17:57 ******/
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

/*
	Start of Custom Calculations 
	----------------------------------------------------------------------------------
	-- Nr of output values must match nr of cells in customreport.cs code
	-- VoterID = mandatory: used as key to datagrid in .aspx file - NEVER LEAVE OUT
	----------------------------------------------------------------------------------

	Optional use of:

	'blank1' as cell1,
	'blank2' as cell2,
	'blank3' as cell3,
	'blank4' as cell4,
	'blank5' as cell5,

	If any of these values ('blankx') and/ or fieldsnames (cellx) is used in the right order [1-5]
	instead of calculated values, it will not show on the datagrid (visilble = false) without having to 
	adjust the number of cells[x] in the .cs codebehind file.
*/

-- STEP 1.

select
	-- VoterID mandatory
	(select VoterId = @VoterID) as VoterID,

-- STEP 2.
	-- custom values: must match number of cells in .cs file

	-- Example: values and header visible: not equal to 'blankx' as 'cellx'
	'test1' as cell01,
	'test2' as cell02,
	'test3' as cell03,
	'test4' as cell04,
	'test5' as cell05

	-- Example: values and header NOT visible
	-- 'blank1' as cell1,
	-- 'blank2' as cell2,
	-- 'blank3' as cell3,
	-- 'blank4' as cell4,
	-- 'blank5' as cell5

-- Finalize calculations
COMMIT TRANSACTION CalculateScores

GO
/****** Object:  StoredProcedure [dbo].[vts_spReportSsrsVoterGetAll]    Script Date: 1/1/2018 11:19:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		W3DevPro
-- Create date: 2017-09-27
-- Description:	Stored Procedure to get all Voter Data for SSRS reports Test
-- =============================================
CREATE PROCEDURE [dbo].[vts_spReportSsrsVoterGetAll] 

	-- Add the parameters for the stored procedure here
	-- @SurveyID int,
	-- @VoterID int

AS
BEGIN TRANSACTION GetVoterData
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

/* 	Start of Query to fetch all voterdata */
-- STEP 1.

SELECT 
VoterID, 
UID, 
SurveyID, 
ContextUserName, 
VoteDate, 
StartDate, 
IPSource, 
Validated, 
ResumeUID, 
ResumeAtPageNumber, 
ProgressSaveDate, 
ResumeQuestionNumber, 
ResumeHighestPageNumber, 
LanguageCode 

FROM vts_tbvoter

-- Finalize calculations
COMMIT TRANSACTION GetVoterData

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
/// Get all details of the roles
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
/// Get all details of the roles
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

-- SELECT SecurityRightID, Description FROM vts_tbSecurityRight

select
sr.SecurityRightID,

cast(

cast(m.Code as nchar(4)) + '&nbsp;&nbsp;' +
m.main +  '/ ' +
isnull(m.subone, ' - ') + '/ ' +
isnull(m.subtwo, ' - ') + '/ ' +
isnull(m.subthree, ' - / ') 

as nchar(100)) +

cast(sr.description as nchar(35)) as [description]
-- len(sr.description) length

from vts_tbsecurityright sr left join 
dbo.vts_tbmenusecurityright msr on sr.SecurityRightID = msr.securityrightID
left join vts_tbmenu m on msr.menuID = m.MenuID
where msr.securityrightID is not null
order by m.code

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
/****** Object:  StoredProcedure [dbo].[vts_spSurveyAddNew]    Script Date: 1/19/2018 10:19:46 ******/
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
					@UnAuthentifiedUserActionID int = 0,		
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
	ThankYouMessage,
	UnAuthentifiedUserActionID) 
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
	@ThankYouMessage,
	@UnAuthentifiedUserActionID)
	
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

	-- Get question ID that is in the cloned survey
	SELECT @ClonedQuestionID =	(select QuestionID  from vts_tbQuestion WHERE SurveyID = @ClonedSurveyID AND QuestionText = (select QuestionText FROM vts_tbQuestion WHERE QuestionID = @QuestionID) AND 
				DisplayOrder = (select DisplayOrder FROM vts_tbQuestion WHERE QuestionID = @QuestionID)) 
	
	IF @AnswerID is not NULL
	BEGIN
		-- Get answer ID from the cloned survey
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
/****** Object:  StoredProcedure [dbo].[vts_spSurveyClone]    Script Date: 1/24/2018 14:33:14 ******/
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
	UnAuthentifiedUserActionID,	
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
	UnAuthentifiedUserActionID,
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
/// deletes the given page break and reorder the pagenumber 
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
/// Get all the entry quota settings 
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
/// Get survey  that is currently marked as activated
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
/// Get all surveys available in the database
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
/// Retrieved the first suvey ID available in the DB
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
/// Retrieved the first suvey ID available in the DB
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
	Survey Project changes: copyright (c) 2016, W3DevPro TM (http://github.com/surveyproject)	

	NSurvey - The web survey and form engine
	Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)

	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
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
	ProgressDisplayModeId,
	ResumeModeId,
	Scored,
	Activated,
	Archive,
	ResultsDisplayTimes,
	SurveyDisplayTimes,
	CreationDate,
	QuestionNumberingDisabled,
	MultiLanguageModeId,
MultiLanguageVariable
FROM vts_tbSurvey WHERE SurveyID = @SurveyID

-- Get main questions and answers
SELECT 
	QuestionID,
	SurveyID,
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
	PageNumber,
	DisplayOrder,
	QuestionIDText,
	HelpText,
	Alias,
	ShowHelpText,
	QuestionId as OldQuestionId,
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
	RegularExpressionId,
	Mandatory,
	AnswerIDText,
	AnswerAlias,
    SliderRange,
	SliderValue,
	SliderMin,
	SliderMax,
	SliderAnimate,
	SliderStep,
    vts_tbAnswer.AnswerID as OldAnswerId,
	CssClass
FROM vts_tbAnswer
INNER JOIN vts_tbQuestion 
	ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID  
WHERE vts_tbQuestion.SurveyID = @SurveyID AND vts_tbQuestion.ParentQuestionID is null

SELECT 
	PublisherAnswerID,
	SubscriberAnswerID,
	vts_tbAnswer.QuestionId
FROM vts_tbAnswerConnection
INNER JOIN vts_tbAnswer
	ON vts_tbAnswer.AnswerId = PublisherAnswerID
INNER JOIN vts_tbQuestion 
	ON vts_tbQuestion.QuestionID = vts_tbAnswer.QuestionID  
WHERE vts_tbQuestion.SurveyID = @SurveyID AND vts_tbQuestion.ParentQuestionID is null

-- Retrieves all child questions and their answers
SELECT 
	ParentQuestionID,
	QuestionText,
	DisplayOrder
FROM vts_tbQuestion 
WHERE SurveyID = @SurveyID AND ParentQuestionID is not null

SELECT vts_tbAnswerProperty.AnswerId, Properties
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
	vts_tbQuestionSectionOption.QuestionId,
	MaxSections,
	RepeatableSectionModeId
FROM vts_tbQuestionSectionOption
INNER JOIN vts_tbQuestion 
	ON vts_tbQuestion.QuestionID = vts_tbQuestionSectionOption.QuestionID  
WHERE vts_tbQuestion.SurveyID = @SurveyID AND vts_tbQuestion.ParentQuestionID is null


SELECT vts_tbQuestionSectionGridAnswer.QuestionID, AnswerID 
FROM vts_tbQuestionSectionGridAnswer 
INNER JOIN vts_tbQuestion 
	ON vts_tbQuestion.QuestionID = vts_tbQuestionSectionGridAnswer.QuestionID  
WHERE vts_tbQuestion.SurveyID = @SurveyID AND vts_tbQuestion.ParentQuestionID is null

SELECT SurveyId,LanguageCode,DefaultLanguage 
FROM  vts_tbSurveyLanguage WHERE surveyId=@SurveyId;

SELECT [LanguageItemId]
      ,[LanguageCode]
      ,[LanguageMessageTypeId]
      ,[ItemText]
  FROM [dbo].[vts_tbMultiLanguageText]
  where( 
   languageMessageTypeId=10 or
  ([LanguageItemId]=@SurveyID and [LanguageMessageTypeId] in(4,5))
  OR( [LanguageItemId] in (SELECT questionid from vts_tbQuestion where SurveyId=@SurveyID) and
  [LanguageMessageTypeId] in(3,11,12))
  OR( [LanguageItemId] in (SELECT answerid from 
  vts_tbQuestion as q inner join 
  vts_tbAnswer as ans on  q.QuestionId=ans.QuestionId where q.SurveyId=@SurveyID ) and
  [LanguageMessageTypeId] in(1,2,13)))
 and len(ItemText) !=0
 or LanguageItemId in(
  --
  SELECT g.ID
   FROM vts_tbQuestionGroups AS g
  WHERE g.ID  IN(
  SELECT q.QuestionGroupID FROM vts_tbQuestion AS  q WHERE SurveyId=@SurveyID)
  UNION
  SELECT g.ID FROM vts_tbQuestionGroups AS g
  WHERE g.ID IN(
  SELECT g.ParentGroupID FROM vts_tbQuestionGroups AS g
  WHERE g.ID  IN(
  SELECT q.QuestionGroupID FROM vts_tbQuestion AS  q WHERE SurveyId=@SurveyID)
  )
  
 )
  
  --
  -- Select all required groups and their parent groups
  --
  SELECT g.ID,g.ParentGroupID,g.GroupName,g.DisplayOrder,g.ID OldId
   FROM vts_tbQuestionGroups AS g
  WHERE g.ID  IN(
  SELECT q.QuestionGroupID FROM vts_tbQuestion AS  q WHERE SurveyId=@SurveyID)
  UNION
  SELECT g.ID,g.ParentGroupID,g.GroupName,g.DisplayOrder ,g.ID OldId FROM vts_tbQuestionGroups AS g
  WHERE g.ID IN(
  SELECT g.ParentGroupID FROM vts_tbQuestionGroups AS g
  WHERE g.ID  IN(
  SELECT q.QuestionGroupID FROM vts_tbQuestion AS  q WHERE SurveyId=@SurveyID)
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
/// Get all surveys available with only the SurveyID and title in the database
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
/// Get all surveys available with only the SurveyID and title in the database
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
/*
/// <summary>
/// Get all surveys from specified folder with SurveyID, title, FolderID and ParentFolderID in the database
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
	sv.MultiLanguageVariable,
	ISNULL(sv.FriendlyName, '-') as FriendlyName
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
/// Get all pipe informations for the survey needed to handle 
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
/// Increments the number of times a survey's results has been displayed
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
/// Increments the number of times a survey has been displayed
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
/// the pagenumber to match the new break position
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
    Survey changes: copyright (c) 2010, W3DevPro TM (http://github.com/surveyproject)    

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

	-- Get question ID that is in the cloned survey
	SELECT @ClonedSkipQuestionID = (select QuestionID  from vts_tbQuestion WHERE SurveyID = @ClonedSurveyID AND QuestionText = (select QuestionText FROM vts_tbQuestion WHERE QuestionID = @SkipQuestionID) AND 
				DisplayOrder = (select DisplayOrder FROM vts_tbQuestion WHERE QuestionID = @SkipQuestionID)) 

	-- Get question ID that is in the cloned survey
	SELECT @ClonedQuestionID =	(select QuestionID  from vts_tbQuestion WHERE SurveyID = @ClonedSurveyID AND QuestionText = (select QuestionText FROM vts_tbQuestion WHERE QuestionID = @QuestionID) AND 
				DisplayOrder = (select DisplayOrder FROM vts_tbQuestion WHERE QuestionID = @QuestionID)) 
	
	IF @AnswerID is not NULL
	BEGIN
		-- Get answer ID from the cloned survey
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
/// Check if the given UID is valID and returns its survey ID
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
/// Check if the given UID is valID and returns its survey ID
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
/// Only one survey can be activated at one time
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
-- Only one survey can be activated at one time
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

DECLARE @test int;
SET @test = 
(select count(*) 
from vts_tbSurvey 
where FriendlyName is not null 
and FriendlyName = @FriendlyName)  ;

BEGIN
	--SET NOCOUNT ON;
	
	if (@test = 0)	
	BEGIN
    Update vts_tbSurvey
	set FriendlyName=@FriendlyName
	where SurveyID = @SurveyID
	END
	
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
/// Password to valIDate
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
/// ID of the library  to retrieve questions from
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
BEGIN TRAN DeleteUserTran
DELETE FROM vts_tbUserRole WHERE UserID = @UserID
DELETE FROM vts_tbUserSurvey WHERE UserID = @UserID
DELETE FROM vts_tbUserSetting WHERE UserID = @UserID
DELETE FROM vts_tbUser WHERE UserID = @UserID
COMMIT TRAN DeleteUserTran



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
/****** Object:  StoredProcedure [dbo].[vts_spUserGetUserIDFromUserName]    Script Date: 7/31/2017 12:56:54 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
	Survey Project: (c) 2016, W3DevPro TM (http://github.com/surveyproject)

	NSurvey - The web survey and form engine
	Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)

	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
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
/// Returns the user ID of the username
/// </summary>
*/
CREATE PROCEDURE [dbo].[vts_spUserGetUserIDFromUserName]
			@UserName NVARCHAR(255)
			
AS

select
Case 
when count(*) > 0
then

(SELECT UserID FROM vts_tbUser WHERE UserName = @UserName)

else -2
end as UserID
from vts_tbUser 


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
/// check if the user is an administrator
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

-- get rights, if a role forbit a right forbIDden right will win 
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
			@AnswerText NVARCHAR(max)
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
			@VoterAnswer NVARCHAR(max)=null,
		    @Answer NVARCHAR(400)=null,
		    @QuestionText NVARCHAR(max),
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
/// The ID of the voter to delete
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
/// Survey ID from which to delete the voters
/// </param>
*/
CREATE PROCEDURE [dbo].[vts_spVoterDeleteAll] @SurveyID int
AS
DELETE FROM vts_tbVoterEmail WHERE VoterID IN (SELECT VoterID FROM vts_tbVoter WHERE SurveyID = @SurveyID)
-- OLD: DELETE FROM vts_tbVoter WHERE SurveyID = @SurveyID

DECLARE @Deleted_Rows INT;
SET @Deleted_Rows = 1;

WHILE (@Deleted_Rows > 0) BEGIN

   BEGIN TRANSACTION TestDelete

   -- Delete small batch of rows at a time
     DELETE TOP (100) vts_tbvoter
     WHERE SurveyID = @SurveyID

     SET @Deleted_Rows = @@ROWCOUNT;

   COMMIT TRANSACTION
   CHECKPOINT -- for simple recovery model
END

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
		-- remove voters who dID not have answered any answer of the question */
             DELETE FROM #FilteredVoters WHERE 
				VoterID NOT IN (
				SELECT DISTINCT vts_tbVoterAnswers.VoterID 
				FROM (SELECT AnswerID FROM vts_tbAnswer WHERE QuestionID=@FilterQuestionID) AS AllAnswers, vts_tbVoterAnswers  WHERE vts_tbVoterAnswers.AnswerID = AllAnswers.AnswerID)
            END 
		ELSE
		-- remove voters who dID not have answered the right answer of the question */
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

-- Voter data:
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
		ON vts_tbEmail.EmailID = vts_tbVoterEmail.EmailId
	WHERE 
		vts_tbVoter.SurveyID = @SurveyID AND
		DATEDIFF (d,@startDate,vts_tbVoter.VoteDate) >= 0 AND DATEDIFF (d,@endDate,vts_tbVoter.VoteDate) <= 0

-- QuestionData:
	SELECT DISTINCT va.VoterID, QuestionText, q.QuestionID,
	                q.QuestionIdText,q.Alias QuestionAlias
	FROM vts_tbVoterAnswers va	
	INNER JOIN vts_tbAnswer a
		ON a.AnswerID = va.AnswerID 
	INNER JOIN vts_tbQuestion q
		ON q.questionID = a.questionID
	INNER JOIN vts_tbVoter v
		ON v.VoterID = va.VoterID
	WHERE 
		v.SurveyID = @SurveyID AND
		DATEDIFF (d,@startDate,V.VoteDate) >= 0 AND DATEDIFF (d,@endDate,V.VoteDate) <= 0


-- Answer data:	
	SELECT va.VoterID, va.SectionNumber, va.AnswerID, ISNULL(va.AnswerText, '-' ) as VoterAnswer, 
	q.QuestionID, q.DisplayOrder as QuestionDisplayOrder,
	a.AnswerText as Answer,a.DisplayOrder as AnswerDisplayOrder,
	A.AnswerAlias
	FROM vts_tbVoterAnswers va	
	INNER JOIN vts_tbAnswer a
		ON a.AnswerID = va.AnswerID 
	INNER JOIN vts_tbQuestion q
		ON q.questionID = a.QuestionID
	INNER JOIN vts_tbVoter v
		ON v.VoterID = va.VoterID
	WHERE 
		v.SurveyID = @SurveyID AND
		DATEDIFF (d,@startDate,V.VoteDate) >= 0 AND DATEDIFF (d,@endDate,V.VoteDate) <= 0


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
/// Retrieves the voter ID which has the given user name from the DB
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
--~ Deprecated feature 'Table hint without WITH'.  Automatically added WITH for you.
--~ Deprecated feature 'Table hint without WITH'.  Automatically added WITH for you.
--~ Deprecated feature 'Table hint without WITH'.  Automatically added WITH for you.
--~ Deprecated feature 'Table hint without WITH'.  Automatically added WITH for you.
/*
	Survey Project: (c) 2017, W3DevPro TM (https://github.com/surveyproject)

	NSurvey - The web survey and form engine
	Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)

	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
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
DECLARE @QuestionText NVARCHAR(max)
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
	SET @UpdateVotersRowSQL =N'UPDATE #VoterEntries SET ['+@QuestionText+'_'+@AnswerText+'] = (SELECT AnswerText FROM vts_tbVoterAnswers  WITH (nolock) WHERE AnswerID='+@AnswerID+' AND vts_tbVoterAnswers.VoterID=Voters.VoterID) FROM (SELECT VoterID FROM #VoterEntries  WITH (nolock)) as Voters WHERE #VoterEntries.VoterID=Voters.VoterID'
	EXEC (@UpdateVotersRowSQL)
	-- Assign voters choice
	SET @UpdateVotersRowSQL =N'UPDATE #VoterEntries SET ['+@QuestionText+'_'+@AnswerText+'] = (SELECT ''1'' FROM vts_tbVoterAnswers  WITH (nolock) INNER JOIN vts_tbAnswer ON vts_tbVoterAnswers.AnswerID = vts_tbAnswer.AnswerID WHERE vts_tbVoterAnswers.AnswerText is null AND vts_tbVoterAnswers.AnswerID='+@AnswerID+' AND vts_tbVoterAnswers.VoterID=Voters.VoterID) FROM (SELECT VoterID FROM #VoterEntries  WITH (nolock)) as Voters WHERE #VoterEntries.VoterID=Voters.VoterID AND #VoterEntries.['+@QuestionText+'_'+@AnswerText+'] is  null'
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
/****** Object:  StoredProcedure [dbo].[vts_spVoterGetPagedIndiv]    Script Date: 8/6/2017 21:38:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Survey Project: (c) 2016, W3DevPro TM (http://github.com/surveyproject)

	NSurvey - The web survey and form engine
	Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)

	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
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
		DATEDIFF (d,@StartDate,V.VoteDate) >= 0 AND DATEDIFF (d,@EndDate,V.VoteDate) <= 0 AND
		contextusername = (	select username from vts_tbuser where UserID = @UserID)
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
--~ Deprecated feature 'Table hint without WITH'.  Automatically added WITH for you.
/*
	Survey Project: (c) 2017, W3DevPro TM (https://github.com/surveyproject)

	NSurvey - The web survey and form engine
	Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)

	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
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
	SET @UpdateVotersRowSQL =N'UPDATE #VoterEntries SET ['+@AnswerText+'_'+@AnswerID+'] = (SELECT SUBSTRING(AnswerText,1, 40)  as AnswerText FROM vts_tbVoterAnswers  WHERE AnswerID='+@AnswerID+' AND SectionNumber=0 AND vts_tbVoterAnswers.VoterID=Voters.VoterID) FROM (SELECT VoterID FROM #VoterEntries  WITH (nolock)) as Voters WHERE #VoterEntries.VoterID=Voters.VoterID'
	EXEC (@UpdateVotersRowSQL)
	FETCH AnswerColumnCursor INTO @AnswerText, @AnswerID
END
CLOSE AnswerColumnCursor
DEALLOCATE AnswerColumnCursor
SELECT *, TotalRecords =@TotalRecords FROM #VoterEntries
DROP TABLE #VoterEntries

GO
/****** Object:  StoredProcedure [dbo].[vts_spVoterGetPivotTextIndivEntries]    Script Date: 8/6/2017 21:32:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Survey Project: (c) 2016, W3DevPro TM (http://github.com/surveyproject)

	NSurvey - The web survey and form engine
	Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)

	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
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
/// <param Name="@UserID">
/// ID of the user to to get sp contextusername from votertable
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
CREATE PROCEDURE [dbo].[vts_spVoterGetPivotTextIndivEntries] 
				@SurveyID int,
				@UserID int,
				@CurrentPage int = 1,
				@PageSize int=10,
				@StartDate datetime,
				@EndDate datetime
AS
DECLARE @TotalRecords int
CREATE TABLE #VoterEntriesIndiv (VoterID int NOT NULL, [Date] dateTime, StartDate datetime, IP varchar(50), Score int)
-- Get voter range
INSERT INTO #VoterEntriesIndiv (VoterID, [Date], StartDate, IP, Score)  
	EXEC vts_spVoterGetPagedIndiv @SurveyID, @UserID, @CurrentPage, @PageSize, @StartDate, @EndDate, @TotalRecords output
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
	SET @BuildColumnSQL = N'ALTER TABLE #VoterEntriesIndiv ADD ['+@AnswerText+'_'+@AnswerID+'] NVARCHAR(4000)'
	EXEC (@BuildColumnSQL)
	-- Assign voters entry to the column
	SET @UpdateVotersRowSQL =N'UPDATE #VoterEntriesIndiv SET ['+@AnswerText+'_'+@AnswerID+'] = (SELECT SUBSTRING(AnswerText,1, 40)  as AnswerText FROM vts_tbVoterAnswers  WHERE AnswerID='+@AnswerID+' AND SectionNumber=0 AND vts_tbVoterAnswers.VoterID=Voters.VoterID) FROM (SELECT VoterID FROM #VoterEntriesIndiv WITH (nolock)) as Voters WHERE #VoterEntriesIndiv.VoterID=Voters.VoterID'
	EXEC (@UpdateVotersRowSQL)
	FETCH AnswerColumnCursor INTO @AnswerText, @AnswerID
END
CLOSE AnswerColumnCursor
DEALLOCATE AnswerColumnCursor
SELECT *, TotalRecords =@TotalRecords FROM #VoterEntriesIndiv
DROP TABLE #VoterEntriesIndiv

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
///  Insert the given email in the invitation queue
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
///  Insert the given email in the invitation queue
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
/// Insert the corresponding email for the voter if 
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
/// Updates the voter ID with the given user name
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
-- Retrieve the current display order
SELECT @AddInOrder  = AddInOrder FROM vts_tbSurveyWebSecurity WHERE SurveyID = @SurveyID AND WebSecurityAddInID = @AddInID
-- Deletes the addin from the survey
DELETE FROM vts_tbSurveyWebSecurity WHERE SurveyID = @SurveyID AND WebSecurityAddInID = @AddInID
-- Updates the addin display order
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
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
	[Description] [NVARCHAR](max) NULL,
	[DefaultLanguageCode] [NVARCHAR](50) NULL,
 CONSTRAINT [PK_vts_tbLibrary] PRIMARY KEY CLUSTERED 
(
	[LibraryID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
	[ItemText] [NVARCHAR](max) NULL,
 CONSTRAINT [PK_vts_LanguageText] PRIMARY KEY CLUSTERED 
(
	[LanguageItemID] ASC,
	[LanguageCode] ASC,
	[LanguageMessageTypeID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
	[QuestionText] [NVARCHAR](max) NULL,
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
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
	[AnswerText] [NVARCHAR](max) NULL,
 CONSTRAINT [PK_vts_tbVoterAnswers] PRIMARY KEY CLUSTERED 
(
	[VoterID] ASC,
	[AnswerID] ASC,
	[SectionNumber] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[vts_tbAnswerType] ON 

GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (1, 1, N'SelectionTextType', 0, 0, 0, 1, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerSelectionItem', N'SurveyProject.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (2, 1, N'SelectionOtherType', 20, 0, 300, 211, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerOtherFieldItem', N'SurveyProject.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (3, 1, N'FieldBasicType', 20, 1, 100, 210, NULL, 0, N'', N'', N'', N'Votations.NSurvey.WebControls.UI.AnswerFieldItem', N'SurveyProject.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (4, 1, N'XMLCountryList', NULL, NULL, NULL, 88, N'countries.xml', 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerXmlListItem', N'SurveyProject.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (5, 1, N'XMLUSStatesList', NULL, NULL, NULL, 88, N'usstates.xml', 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerXmlListItem', N'SurveyProject.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (20, 1, N'BooleanType', 0, 0, 0, 20, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerBooleanItem', N'SurveyProject.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (22, 1, N'FieldCalendarType', 10, 0, 10, 86, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerFieldCalendarItem', N'SurveyProject.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (24, 1, N'FieldLargeType', 70, 10, 255, 210, NULL, 0, N'', N'', N'', N'Votations.NSurvey.WebControls.UI.AnswerFieldItem', N'SurveyProject.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (27, 1, N'FieldHiddenType', 0, 0, 0, 20, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerFieldHiddenItem', N'SurveyProject.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (28, 1, N'FieldPasswordType', 20, 0, 0, 214, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerFieldPasswordItem', N'SurveyProject.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (29, 1, N'SubscriberXMLList', 0, 0, 0, 116, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerSubscriberXmlListItem', N'SurveyProject.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (30, 1, N'ExtendedFileUploadType', 0, 0, 0, 832, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerUploadItem', N'SurveyProject.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (31, 1, N'ExtendedFreeTextBoxType', 500, 100, 255, 722, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.ThirdPartyItems.FreeTextBoxAnswerItem', N'SurveyProject.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (55, 1, N'FieldAddressType', 20, 0, 100, 210, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerFieldAddressItem', N'SurveyProject.WebControls', NULL)
GO
INSERT [dbo].[vts_tbAnswerType] ([AnswerTypeID], [BuiltIn], [Description], [FieldWidth], [FieldHeight], [FieldLength], [TypeMode], [XMLDatasource], [PublicFieldResults], [JavascriptFunctionName], [JavascriptCode], [JavascriptErrorMessage], [TypeNameSpace], [TypeAssembly], [DataSource]) VALUES (56, 1, N'FieldSliderType', 20, 0, 100, 1090, NULL, 0, NULL, NULL, NULL, N'Votations.NSurvey.WebControls.UI.AnswerFieldSliderItem', N'SurveyProject.WebControls', NULL)
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
-- NEW LANGUAGE CODES SP25 START --
INSERT [dbo].[vts_tbMultiLanguage] ([LanguageCode], [LanguageDescription]) VALUES
(N'aa', N'Afar_aa'),
(N'aa-DJ', N'Afar_Djibouti_aa-DJ'),
(N'aa-ER', N'Afar_Eritrea_aa-ER'),
(N'aa-ET', N'Afar_Ethiopia_aa-ET'),
(N'af', N'Afrikaans_af'),
(N'af-NA', N'Afrikaans_Namibia_af-NA'),
(N'af-ZA', N'Afrikaans_South Africa_af-ZA'),
(N'agq', N'Aghem_agq'),
(N'agq-CM', N'Aghem_Cameroon_agq-CM'),
(N'ak', N'Akan_ak'),
(N'ak-GH', N'Akan_Ghana_ak-GH'),
(N'sq', N'Albanian_sq'),
(N'sq-AL', N'Albanian_Albania_sq-AL'),
(N'sq-MK', N'Albanian_Macedonia, FYRO_sq-MK'),
(N'gsw', N'Alsatian_gsw'),
(N'gsw-FR', N'Alsatian_France_gsw-FR'),
(N'gsw-LI', N'Alsatian_Liechtenstein_gsw-LI'),
(N'gsw-CH', N'Alsatian_Switzerland_gsw-CH'),
(N'am', N'Amharic_am'),
(N'am-ET', N'Amharic_Ethiopia_am-ET'),
(N'ar', N'Arabic_ar'),
(N'ar-DZ', N'Arabic_Algeria_ar-DZ'),
(N'ar-BH', N'Arabic_Bahrain_ar-BH'),
(N'ar-TD', N'Arabic_Chad_ar-TD'),
(N'ar-KM', N'Arabic_Comoros_ar-KM'),
(N'ar-DJ', N'Arabic_Djibouti_ar-DJ'),
(N'ar-EG', N'Arabic_Egypt_ar-EG'),
(N'ar-ER', N'Arabic_Eritrea_ar-ER'),
(N'ar-IQ', N'Arabic_Iraq_ar-IQ'),
(N'ar-IL', N'Arabic_Israel_ar-IL'),
(N'ar-JO', N'Arabic_Jordan_ar-JO'),
(N'ar-KW', N'Arabic_Kuwait_ar-KW'),
(N'ar-LB', N'Arabic_Lebanon_ar-LB'),
(N'ar-LY', N'Arabic_Libya_ar-LY'),
(N'ar-MR', N'Arabic_Mauritania_ar-MR'),
(N'ar-MA', N'Arabic_Morocco_ar-MA'),
(N'ar-OM', N'Arabic_Oman_ar-OM'),
(N'ar-PS', N'Arabic_Palestinian Authority_ar-PS'),
(N'ar-QA', N'Arabic_Qatar_ar-QA'),
(N'ar-SA', N'Arabic_Saudi Arabia_ar-SA'),
(N'ar-SO', N'Arabic_Somalia_ar-SO'),
(N'ar-SS', N'Arabic_South Sudan_ar-SS'),
(N'ar-SD', N'Arabic_Sudan_ar-SD'),
(N'ar-SY', N'Arabic_Syria_ar-SY'),
(N'ar-TN', N'Arabic_Tunisia_ar-TN'),
(N'ar-AE', N'Arabic_U.A.E._ar-AE'),
(N'ar-001', N'Arabic_World_ar-001'),
(N'ar-YE', N'Arabic_Yemen_ar-YE'),
(N'hy', N'Armenian_hy'),
(N'hy-AM', N'Armenian_Armenia_hy-AM'),
(N'as', N'Assamese_as'),
(N'as-IN', N'Assamese_India_as-IN'),
(N'ast', N'Asturian_ast'),
(N'ast-ES', N'Asturian_Spain_ast-ES'),
(N'asa', N'Asu_asa'),
(N'asa-TZ', N'Asu_Tanzania_asa-TZ'),
(N'az-Cyrl', N'Azerbaijani (Cyrillic)_az-Cyrl'),
(N'az-Cyrl-AZ', N'Azerbaijani (Cyrillic)_Azerbaijan_az-Cyrl-AZ'),
(N'az', N'Azerbaijani (Latin)_az'),
(N'az-Latn', N'Azerbaijani (Latin)_az-Latn'),
(N'az-Latn-AZ', N'Azerbaijani (Latin)_Azerbaijan_az-Latn-AZ'),
(N'ksf', N'Bafia_ksf'),
(N'ksf-CM', N'Bafia_Cameroon_ksf-CM'),
(N'bm', N'Bamanankan_bm'),
(N'bm-Latn-ML', N'Bamanankan (Latin)_Mali_bm-Latn-ML'),
(N'bn', N'Bangla_bn'),
(N'bn-BD', N'Bangla_Bangladesh_bn-BD'),
(N'bn-IN', N'Bangla_India_bn-IN'),
(N'bas', N'Basaa_bas'),
(N'bas-CM', N'Basaa_Cameroon_bas-CM'),
(N'ba', N'Bashkir_ba'),
(N'ba-RU', N'Bashkir_Russia_ba-RU'),
(N'eu', N'Basque_eu'),
(N'eu-ES', N'Basque_Spain_eu-ES'),
(N'be', N'Belarusian_be'),
(N'be-BY', N'Belarusian_Belarus_be-BY'),
(N'bem', N'Bemba_bem'),
(N'bem-ZM', N'Bemba_Zambia_bem-ZM'),
(N'bez', N'Bena_bez'),
(N'bez-TZ', N'Bena_Tanzania_bez-TZ'),
(N'byn', N'Blin_byn'),
(N'byn-ER', N'Blin_Eritrea_byn-ER'),
(N'brx', N'Bodo_brx'),
(N'brx-IN', N'Bodo_India_brx-IN'),
(N'bs-Cyrl', N'Bosnian (Cyrillic)_bs-Cyrl'),
(N'bs-Cyrl-BA', N'Bosnian (Cyrillic)_Bosnia and Herzegovina_bs-Cyrl-BA'),
(N'bs-Latn', N'Bosnian (Latin)_bs-Latn'),
(N'bs', N'Bosnian (Latin)_bs'),
(N'bs-Latn-BA', N'Bosnian (Latin)_Bosnia and Herzegovina_bs-Latn-BA'),
(N'br', N'Breton_br'),
(N'br-FR', N'Breton_France_br-FR'),
(N'bg', N'Bulgarian_bg'),
(N'bg-BG', N'Bulgarian_Bulgaria_bg-BG'),
(N'my', N'Burmese_my'),
(N'my-MM', N'Burmese_Myanmar_my-MM'),
(N'ca', N'Catalan_ca'),
(N'ca-AD', N'Catalan_Andorra_ca-AD'),
(N'ca-FR', N'Catalan_France_ca-FR'),
(N'ca-IT', N'Catalan_Italy_ca-IT'),
(N'ca-ES', N'Catalan_Spain_ca-ES'),
(N'tzm-Latn-MA', N'Central Atlas Tamazight (Latin)_Morocco_tzm-Latn-MA'),
(N'ku', N'Central Kurdish_ku'),
(N'ku-Arab', N'Central Kurdish_ku-Arab'),
(N'ku-Arab-IQ', N'Central Kurdish_Iraq_ku-Arab-IQ'),
(N'cd-RU', N'Chechen_Russia_cd-RU'),
(N'chr', N'Cherokee_chr'),
(N'chr-Cher', N'Cherokee_chr-Cher'),
(N'chr-Cher-US', N'Cherokee_United States_chr-Cher-US'),
(N'cgg', N'Chiga_cgg'),
(N'cgg-UG', N'Chiga_Uganda_cgg-UG'),
(N'zh-Hans', N'Chinese (Simplified)_zh-Hans'),
(N'zh', N'Chinese (Simplified)_zh'),
(N'zh-CN', N'Chinese (Simplified)_People''s Republic of China_zh-CN'),
(N'zh-SG', N'Chinese (Simplified)_Singapore_zh-SG'),
(N'zh-Hant', N'Chinese (Traditional)_zh-Hant'),
(N'zh-HK', N'Chinese (Traditional)_Hong Kong S.A.R._zh-HK'),
(N'zh-MO', N'Chinese (Traditional)_Macao S.A.R._zh-MO'),
(N'zh-TW', N'Chinese (Traditional)_Taiwan_zh-TW'),
(N'cu-RU', N'Church Slavic_Russia_cu-RU'),
(N'swc', N'Congo Swahili_swc'),
(N'swc-CD', N'Congo Swahili_Congo DRC_swc-CD'),
(N'kw', N'Cornish_kw'),
(N'kw-GB', N'Cornish_United Kingdom_kw-GB'),
(N'co', N'Corsican_co'),
(N'co-FR', N'Corsican_France_co-FR'),
(N'bs, hr, or sr', N'Croatian_bs, hr, or sr'),
(N'hr-HR', N'Croatian_Croatia_hr-HR'),
(N'hr-BA', N'Croatian (Latin)_Bosnia and Herzegovina_hr-BA'),
(N'cs', N'Czech_cs'),
(N'cs-CZ', N'Czech_Czech Republic_cs-CZ'),
(N'da', N'Danish_da'),
(N'da-DK', N'Danish_Denmark_da-DK'),
(N'da-GL', N'Danish_Greenland_da-GL'),
(N'prs', N'Dari_prs'),
(N'prs-AF', N'Dari_Afghanistan_prs-AF'),
(N'dv', N'Divehi_dv'),
(N'dv-MV', N'Divehi_Maldives_dv-MV'),
(N'dua', N'Duala_dua'),
(N'dua-CM', N'Duala_Cameroon_dua-CM'),
(N'nl', N'Dutch_nl'),
(N'nl-AW', N'Dutch_Aruba_nl-AW'),
(N'nl-BE', N'Dutch_Belgium_nl-BE'),
(N'nl-BQ', N'Dutch_Bonaire, Sint Eustatius and Saba_nl-BQ'),
(N'nl-CW', N'Dutch_Curaçao_nl-CW'),
(N'nl-NL', N'Dutch_Netherlands_nl-NL'),
(N'nl-SX', N'Dutch_Sint Maarten_nl-SX'),
(N'nl-SR', N'Dutch_Suriname_nl-SR'),
(N'dz', N'Dzongkha_dz'),
(N'dz-BT', N'Dzongkha_Bhutan_dz-BT'),
(N'ebu', N'Embu_ebu'),
(N'ebu-KE', N'Embu_Kenya_ebu-KE'),
(N'en', N'English_en'),
(N'en-AS', N'English_American Samoa_en-AS'),
(N'en-AI', N'English_Anguilla_en-AI'),
(N'en-AG', N'English_Antigua and Barbuda_en-AG'),
(N'en-AU', N'English_Australia_en-AU'),
(N'en-AT', N'English_Austria_en-AT'),
(N'en-BS', N'English_Bahamas_en-BS'),
(N'en-BB', N'English_Barbados_en-BB'),
(N'en-BE', N'English_Belgium_en-BE'),
(N'en-BZ', N'English_Belize_en-BZ'),
(N'en-BM', N'English_Bermuda_en-BM'),
(N'en-BW', N'English_Botswana_en-BW'),
(N'en-IO', N'English_British Indian Ocean Territory_en-IO'),
(N'en-VG', N'English_British Virgin Islands_en-VG'),
(N'en-BI', N'English_Burundi_en-BI'),
(N'en-CM', N'English_Cameroon_en-CM'),
(N'en-CA', N'English_Canada_en-CA'),
(N'en-029', N'English_Caribbean_en-029'),
(N'en-KY', N'English_Cayman Islands_en-KY'),
(N'en-CX', N'English_Christmas Island_en-CX'),
(N'en-CC', N'English_Cocos [Keeling] Islands_en-CC'),
(N'en-CK', N'English_Cook Islands_en-CK'),
(N'en-CY', N'English_Cyprus_en-CY'),
(N'en-DK', N'English_Denmark_en-DK'),
(N'en-DM', N'English_Dominica_en-DM'),
(N'en-ER', N'English_Eritrea_en-ER'),
(N'en-150', N'English_Europe_en-150'),
(N'en-FK', N'English_Falkland Islands_en-FK'),
(N'en-FI', N'English_Finland_en-FI'),
(N'en-FJ', N'English_Fiji_en-FJ'),
(N'en-GM', N'English_Gambia_en-GM'),
(N'en-DE', N'English_Germany_en-DE'),
(N'en-GH', N'English_Ghana_en-GH'),
(N'en-GI', N'English_Gibraltar_en-GI'),
(N'en-GD', N'English_Grenada_en-GD'),
(N'en-GU', N'English_Guam_en-GU'),
(N'en-GG', N'English_Guernsey_en-GG'),
(N'en-GY', N'English_Guyana_en-GY'),
(N'en-HK', N'English_Hong Kong_en-HK'),
(N'en-IN', N'English_India_en-IN'),
(N'en-IE', N'English_Ireland_en-IE'),
(N'en-IM', N'English_Isle of Man_en-IM'),
(N'en-IL', N'English_Israel_en-IL'),
(N'en-JM', N'English_Jamaica_en-JM'),
(N'en-JE', N'English_Jersey_en-JE'),
(N'en-KE', N'English_Kenya_en-KE'),
(N'en-KI', N'English_Kiribati_en-KI'),
(N'en-LS', N'English_Lesotho_en-LS'),
(N'en-LR', N'English_Liberia_en-LR'),
(N'en-MO', N'English_Macao SAR_en-MO'),
(N'en-MG', N'English_Madagascar_en-MG'),
(N'en-MW', N'English_Malawi_en-MW'),
(N'en-MY', N'English_Malaysia_en-MY'),
(N'en-MT', N'English_Malta_en-MT'),
(N'en-MH', N'English_Marshall Islands_en-MH'),
(N'en-MU', N'English_Mauritius_en-MU'),
(N'en-FM', N'English_Micronesia_en-FM'),
(N'en-MS', N'English_Montserrat_en-MS'),
(N'en-NA', N'English_Namibia_en-NA'),
(N'en-NR', N'English_Nauru_en-NR'),
(N'en-NL', N'English_Netherlands_en-NL'),
(N'en-NZ', N'English_New Zealand_en-NZ'),
(N'en-NG', N'English_Nigeria_en-NG'),
(N'en-NU', N'English_Niue_en-NU'),
(N'en-NF', N'English_Norfolk Island_en-NF'),
(N'en-MP', N'English_Northern Mariana Islands_en-MP'),
(N'en-PK', N'English_Pakistan_en-PK'),
(N'en-PW', N'English_Palau_en-PW'),
(N'en-PG', N'English_Papua New Guinea_en-PG'),
(N'en-PN', N'English_Pitcairn Islands_en-PN'),
(N'en-PR', N'English_Puerto Rico_en-PR'),
(N'en-PH', N'English_Republic of the Philippines_en-PH'),
(N'en-RW', N'English_Rwanda_en-RW'),
(N'en-KN', N'English_Saint Kitts and Nevis_en-KN'),
(N'en-LC', N'English_Saint Lucia_en-LC'),
(N'en-VC', N'English_Saint Vincent and the Grenadines_en-VC'),
(N'en-WS', N'English_Samoa_en-WS'),
(N'en-SC', N'English_Seychelles_en-SC'),
(N'en-SL', N'English_Sierra Leone_en-SL'),
(N'en-SG', N'English_Singapore_en-SG'),
(N'en-SX', N'English_Sint Maarten_en-SX'),
(N'en-SI', N'English_Slovenia_en-SI'),
(N'en-SB', N'English_Solomon Islands_en-SB'),
(N'en-ZA', N'English_South Africa_en-ZA'),
(N'en-SS', N'English_South Sudan_en-SS'),
(N'en-SH', N'English_St Helena, Ascension, Tristan da Cunha_en-SH'),
(N'en-SD', N'English_Sudan_en-SD'),
(N'en-SZ', N'English_Swaziland_en-SZ'),
(N'en-SE', N'English_Sweden_en-SE'),
(N'en-CH', N'English_Switzerland_en-CH'),
(N'en-TZ', N'English_Tanzania_en-TZ'),
(N'en-TK', N'English_Tokelau_en-TK'),
(N'en-TO', N'English_Tonga_en-TO'),
(N'en-TT', N'English_TrinIDad and Tobago_en-TT'),
(N'en-TC', N'English_Turks and Caicos Islands_en-TC'),
(N'en-TV', N'English_Tuvalu_en-TV'),
(N'en-UG', N'English_Uganda_en-UG'),
(N'en-GB', N'English_United Kingdom_en-GB'),
(N'en-US', N'English_United States_en-US'),
(N'en-UM', N'English_US Minor Outlying Islands_en-UM'),
(N'en-VI', N'English_US Virgin Islands_en-VI'),
(N'en-VU', N'English_Vanuatu_en-VU'),
(N'en-001', N'English_World_en-001'),
(N'en-ZM', N'English_Zambia_en-ZM'),
(N'en-ZW', N'English_Zimbabwe_en-ZW'),
(N'eo', N'Esperanto_eo'),
(N'eo-001', N'Esperanto_World_eo-001'),
(N'et', N'Estonian_et'),
(N'et-EE', N'Estonian_Estonia_et-EE'),
(N'ee', N'Ewe_ee'),
(N'ee-GH', N'Ewe_Ghana_ee-GH'),
(N'ee-TG', N'Ewe_Togo_ee-TG'),
(N'ewo', N'Ewondo_ewo'),
(N'ewo-CM', N'Ewondo_Cameroon_ewo-CM'),
(N'fo', N'Faroese_fo'),
(N'fo-DK', N'Faroese_Denmark_fo-DK'),
(N'fo-FO', N'Faroese_Faroe Islands_fo-FO'),
(N'fil', N'Filipino_fil'),
(N'fil-PH', N'Filipino_Philippines_fil-PH'),
(N'fi', N'Finnish_fi'),
(N'fi-FI', N'Finnish_Finland_fi-FI'),
(N'fr', N'French_fr'),
(N'fr-DZ', N'French_Algeria_fr-DZ'),
(N'fr-BE', N'French_Belgium_fr-BE'),
(N'fr-BJ', N'French_Benin_fr-BJ'),
(N'fr-BF', N'French_Burkina Faso_fr-BF'),
(N'fr-BI', N'French_Burundi_fr-BI'),
(N'fr-CM', N'French_Cameroon_fr-CM'),
(N'fr-CA', N'French_Canada_fr-CA'),
(N'fr-CF', N'French_Central African Republic_fr-CF'),
(N'fr-TD', N'French_Chad_fr-TD'),
(N'fr-KM', N'French_Comoros_fr-KM'),
(N'fr-CG', N'French_Congo_fr-CG'),
(N'fr-CD', N'French_Congo, DRC_fr-CD'),
(N'fr-CI', N'French_Côte d''Ivoire_fr-CI'),
(N'fr-DJ', N'French_Djibouti_fr-DJ'),
(N'fr-GQ', N'French_Equatorial Guinea_fr-GQ'),
(N'fr-FR', N'French_France_fr-FR'),
(N'fr-GF', N'French_French Guiana_fr-GF'),
(N'fr-PF', N'French_French Polynesia_fr-PF'),
(N'fr-GA', N'French_Gabon_fr-GA'),
(N'fr-GP', N'French_Guadeloupe_fr-GP'),
(N'fr-GN', N'French_Guinea_fr-GN'),
(N'fr-HT', N'French_Haiti_fr-HT'),
(N'fr-LU', N'French_Luxembourg_fr-LU'),
(N'fr-MG', N'French_Madagascar_fr-MG'),
(N'fr-ML', N'French_Mali_fr-ML'),
(N'fr-MQ', N'French_Martinique_fr-MQ'),
(N'fr-MR', N'French_Mauritania_fr-MR'),
(N'fr-MU', N'French_Mauritius_fr-MU'),
(N'fr-YT', N'French_Mayotte_fr-YT'),
(N'fr-MA', N'French_Morocco_fr-MA'),
(N'fr-NC', N'French_New Caledonia_fr-NC'),
(N'fr-NE', N'French_Niger_fr-NE'),
(N'fr-MC', N'French_Principality of Monaco_fr-MC'),
(N'fr-RE', N'French_Reunion_fr-RE'),
(N'fr-RW', N'French_Rwanda_fr-RW'),
(N'fr-BL', N'French_Saint Barthélemy_fr-BL'),
(N'fr-MF', N'French_Saint Martin_fr-MF'),
(N'fr-PM', N'French_Saint Pierre and Miquelon_fr-PM'),
(N'fr-SN', N'French_Senegal_fr-SN'),
(N'fr-SC', N'French_Seychelles_fr-SC'),
(N'fr-CH', N'French_Switzerland_fr-CH'),
(N'fr-SY', N'French_Syria_fr-SY'),
(N'fr-TG', N'French_Togo_fr-TG'),
(N'fr-TN', N'French_Tunisia_fr-TN'),
(N'fr-VU', N'French_Vanuatu_fr-VU'),
(N'fr-WF', N'French_Wallis and Futuna_fr-WF'),
(N'fy', N'Frisian_Frisland_fy'),
(N'fy-NL', N'Frisian_Netherlands_fy-NL'),
(N'fur', N'Friulian_fur'),
(N'fur-IT', N'Friulian_Italy_fur-IT'),
(N'ff', N'Fulah_ff'),
(N'ff-Latn', N'Fulah_ff-Latn'),
(N'ff-CM', N'Fulah_Cameroon_ff-CM'),
(N'ff-GN', N'Fulah_Guinea_ff-GN'),
(N'ff-MR', N'Fulah_Mauritania_ff-MR'),
(N'ff-Latn-SN', N'Fulah_Senegal_ff-Latn-SN'),
(N'gl', N'Galician_gl'),
(N'gl-ES', N'Galician_Spain_gl-ES'),
(N'lg', N'Ganda_lg'),
(N'lg-UG', N'Ganda_Uganda_lg-UG'),
(N'ka', N'Georgian_ka'),
(N'ka-GE', N'Georgian_Georgia_ka-GE'),
(N'de', N'German_de'),
(N'de-AT', N'German_Austria_de-AT'),
(N'de-BE', N'German_Belgium_de-BE'),
(N'de-DE', N'German_Germany_de-DE'),
(N'de-IT', N'German_Italy_de-IT'),
(N'de-LI', N'German_Liechtenstein_de-LI'),
(N'de-LU', N'German_Luxembourg_de-LU'),
(N'de-CH', N'German_Switzerland_de-CH'),
(N'el', N'Greek_el'),
(N'el-CY', N'Greek_Cyprus_el-CY'),
(N'el-GR', N'Greek_Greece_el-GR'),
(N'kl', N'Greenlandic_kl'),
(N'kl-GL', N'Greenlandic_Greenland_kl-GL'),
(N'gn', N'Guarani_gn'),
(N'gn-PY', N'Guarani_Paraguay_gn-PY'),
(N'gu', N'Gujarati_gu'),
(N'gu-IN', N'Gujarati_India_gu-IN'),
(N'guz', N'Gusii_guz'),
(N'guz-KE', N'Gusii_Kenya_guz-KE'),
(N'ha', N'Hausa (Latin)_ha'),
(N'ha-Latn', N'Hausa (Latin)_ha-Latn'),
(N'ha-Latn-GH', N'Hausa (Latin)_Ghana_ha-Latn-GH'),
(N'ha-Latn-NE', N'Hausa (Latin)_Niger_ha-Latn-NE'),
(N'ha-Latn-NG', N'Hausa (Latin)_Nigeria_ha-Latn-NG'),
(N'haw', N'Hawaiian_haw'),
(N'haw-US', N'Hawaiian_United States_haw-US'),
(N'he', N'Hebrew_he'),
(N'he-IL', N'Hebrew_Israel_he-IL'),
(N'hi', N'Hindi_hi'),
(N'hi-IN', N'Hindi_India_hi-IN'),
(N'hu', N'Hungarian_hu'),
(N'hu-HU', N'Hungarian_Hungary_hu-HU'),
(N'is', N'Icelandic_is'),
(N'is-IS', N'Icelandic_Iceland_is-IS'),
(N'ig', N'Igbo_ig'),
(N'ig-NG', N'Igbo_Nigeria_ig-NG'),
(N'ID', N'Indonesian_ID'),
(N'ID-ID', N'Indonesian_Indonesia_ID-ID'),
(N'ia', N'Interlingua_ia'),
(N'ia-FR', N'Interlingua_France_ia-FR'),
(N'ia-001', N'Interlingua_World_ia-001'),
(N'iu', N'Inuktitut (Latin)_iu'),
(N'iu-Latn', N'Inuktitut (Latin)_iu-Latn'),
(N'iu-Latn-CA', N'Inuktitut (Latin)_Canada_iu-Latn-CA'),
(N'iu-Cans', N'Inuktitut (Syllabics)_iu-Cans'),
(N'iu-Cans-CA', N'Inuktitut (Syllabics)_Canada_iu-Cans-CA'),
(N'ga', N'Irish_ga'),
(N'ga-IE', N'Irish_Ireland_ga-IE'),
(N'it', N'Italian_it'),
(N'it-IT', N'Italian_Italy_it-IT'),
(N'it-SM', N'Italian_San Marino_it-SM'),
(N'it-CH', N'Italian_Switzerland_it-CH'),
(N'it-VA', N'Italian_Vatican City_it-VA'),
(N'ja', N'Japanese_ja'),
(N'ja-JP', N'Japanese_Japan_ja-JP'),
(N'jv', N'Javanese_jv'),
(N'jv-Latn', N'Javanese_Latin_jv-Latn'),
(N'jv-Latn-ID', N'Javanese_Latin, Indonesia_jv-Latn-ID'),
(N'dyo', N'Jola-Fonyi_dyo'),
(N'dyo-SN', N'Jola-Fonyi_Senegal_dyo-SN'),
(N'kea', N'Kabuverdianu_kea'),
(N'kea-CV', N'Kabuverdianu_Cabo Verde_kea-CV'),
(N'kab', N'Kabyle_kab'),
(N'kab-DZ', N'Kabyle_Algeria_kab-DZ'),
(N'kkj', N'Kako_kkj'),
(N'kkj-CM', N'Kako_Cameroon_kkj-CM'),
(N'kln', N'Kalenjin_kln'),
(N'kln-KE', N'Kalenjin_Kenya_kln-KE'),
(N'kam', N'Kamba_kam'),
(N'kam-KE', N'Kamba_Kenya_kam-KE'),
(N'kn', N'Kannada_kn'),
(N'kn-IN', N'Kannada_India_kn-IN'),
(N'ks', N'Kashmiri_ks'),
(N'ks-Arab', N'Kashmiri_Perso-Arabic_ks-Arab'),
(N'ks-Arab-IN', N'Kashmiri_Perso-Arabic_ks-Arab-IN'),
(N'kk', N'Kazakh_kk'),
(N'kk-KZ', N'Kazakh_Kazakhstan_kk-KZ'),
(N'km', N'Khmer_km'),
(N'km-KH', N'Khmer_Cambodia_km-KH'),
(N'quc', N'K''iche_quc'),
(N'quc-Latn-GT', N'K''iche_Guatemala_quc-Latn-GT'),
(N'ki', N'Kikuyu_ki'),
(N'ki-KE', N'Kikuyu_Kenya_ki-KE'),
(N'rw', N'Kinyarwanda_rw'),
(N'rw-RW', N'Kinyarwanda_Rwanda_rw-RW'),
(N'sw', N'Kiswahili_sw'),
(N'sw-KE', N'Kiswahili_Kenya_sw-KE'),
(N'sw-TZ', N'Kiswahili_Tanzania_sw-TZ'),
(N'sw-UG', N'Kiswahili_Uganda_sw-UG'),
(N'kok', N'Konkani_kok'),
(N'kok-IN', N'Konkani_India_kok-IN'),
(N'ko', N'Korean_ko'),
(N'ko-KR', N'Korean_Korea_ko-KR'),
(N'ko-KP', N'Korean_North Korea_ko-KP'),
(N'khq', N'Koyra Chiini_khq'),
(N'khq-ML', N'Koyra Chiini_Mali_khq-ML'),
(N'ses', N'Koyraboro Senni_ses'),
(N'ses-ML', N'Koyraboro Senni_Mali_ses-ML'),
(N'nmg', N'Kwasio_nmg'),
(N'nmg-CM', N'Kwasio_Cameroon_nmg-CM'),
(N'ky', N'Kyrgyz_ky'),
(N'ky-KG', N'Kyrgyz_Kyrgyzstan_ky-KG'),
(N'ku-Arab-IR', N'Kurdish_Perso-Arabic, Iran_ku-Arab-IR'),
(N'lkt', N'Lakota_lkt'),
(N'lkt-US', N'Lakota_United States_lkt-US'),
(N'lag', N'Langi_lag'),
(N'lag-TZ', N'Langi_Tanzania_lag-TZ'),
(N'lo', N'Lao_lo'),
(N'lo-LA', N'Lao_Lao P.D.R._lo-LA'),
(N'lv', N'Latvian_lv'),
(N'lv-LV', N'Latvian_Latvia_lv-LV'),
(N'ln', N'Lingala_ln'),
(N'ln-AO', N'Lingala_Angola_ln-AO'),
(N'ln-CF', N'Lingala_Central African Republic_ln-CF'),
(N'ln-CG', N'Lingala_Congo_ln-CG'),
(N'ln-CD', N'Lingala_Congo DRC_ln-CD'),
(N'lt', N'Lithuanian_lt'),
(N'lt-LT', N'Lithuanian_Lithuania_lt-LT'),
(N'nds', N'Low German_nds'),
(N'nds-DE', N'Low German _Germany_nds-DE'),
(N'nds-NL', N'Low German_Netherlands_nds-NL'),
(N'dsb', N'Lower Sorbian_dsb'),
(N'dsb-DE', N'Lower Sorbian_Germany_dsb-DE'),
(N'lu', N'Luba-Katanga_lu'),
(N'lu-CD', N'Luba-Katanga_Congo DRC_lu-CD'),
(N'luo', N'Luo_luo'),
(N'luo-KE', N'Luo_Kenya_luo-KE'),
(N'lb', N'Luxembourgish_lb'),
(N'lb-LU', N'Luxembourgish_Luxembourg_lb-LU'),
(N'luy', N'Luyia_luy'),
(N'luy-KE', N'Luyia_Kenya_luy-KE'),
(N'mk', N'Macedonian_mk'),
(N'mk-MK', N'Macedonian_Macedonia (Former Yugoslav Republic of Macedonia)_mk-MK'),
(N'jmc', N'Machame_jmc'),
(N'jmc-TZ', N'Machame_Tanzania_jmc-TZ'),
(N'mgh', N'Makhuwa-Meetto_mgh'),
(N'mgh-MZ', N'Makhuwa-Meetto_Mozambique_mgh-MZ'),
(N'kde', N'Makonde_kde'),
(N'kde-TZ', N'Makonde_Tanzania_kde-TZ'),
(N'mg', N'Malagasy_mg'),
(N'mg-MG', N'Malagasy_Madagascar_mg-MG'),
(N'ms', N'Malay_ms'),
(N'ms-BN', N'Malay_Brunei Darussalam_ms-BN'),
(N'ms-MY', N'Malay_Malaysia_ms-MY'),
(N'ml', N'Malayalam_ml'),
(N'ml-IN', N'Malayalam_India_ml-IN'),
(N'mt', N'Maltese_mt'),
(N'mt-MT', N'Maltese_Malta_mt-MT'),
(N'gv', N'Manx_gv'),
(N'gv-IM', N'Manx_Isle of Man_gv-IM'),
(N'mi', N'Maori_mi'),
(N'mi-NZ', N'Maori_New Zealand_mi-NZ'),
(N'arn', N'Mapudungun_arn'),
(N'arn-CL', N'Mapudungun_Chile_arn-CL'),
(N'mr', N'Marathi_mr'),
(N'mr-IN', N'Marathi_India_mr-IN'),
(N'mas', N'Masai_mas'),
(N'mas-KE', N'Masai_Kenya_mas-KE'),
(N'mas-TZ', N'Masai_Tanzania_mas-TZ'),
(N'mzn-IR', N'Mazanderani_Iran_mzn-IR'),
(N'mer', N'Meru_mer'),
(N'mer-KE', N'Meru_Kenya_mer-KE'),
(N'mgo', N'Meta''_mgo'),
(N'mgo-CM', N'Meta''_Cameroon_mgo-CM'),
(N'moh', N'Mohawk_moh'),
(N'moh-CA', N'Mohawk_Canada_moh-CA'),
(N'mn', N'Mongolian (Cyrillic)_mn'),
(N'mn-Cyrl', N'Mongolian (Cyrillic)_mn-Cyrl'),
(N'mn-MN', N'Mongolian (Cyrillic)_Mongolia_mn-MN'),
(N'mn-Mong', N'Mongolian (Traditional Mongolian)_mn-Mong'),
(N'mn-Mong-CN', N'Mongolian (Traditional Mongolian)_People''s Republic of China_mn-Mong-CN'),
(N'mn-Mong-MN', N'Mongolian (Traditional Mongolian)_Mongolia_mn-Mong-MN'),
(N'mfe', N'Morisyen_mfe'),
(N'mfe-MU', N'Morisyen_Mauritius_mfe-MU'),
(N'mua', N'Mundang_mua'),
(N'mua-CM', N'Mundang_Cameroon_mua-CM'),
(N'nqo', N'N''ko_nqo'),
(N'nqo-GN', N'N''ko_Guinea_nqo-GN'),
(N'naq', N'Nama_naq'),
(N'naq-NA', N'Nama_Namibia_naq-NA'),
(N'ne', N'Nepali_ne'),
(N'ne-IN', N'Nepali_India_ne-IN'),
(N'ne-NP', N'Nepali_Nepal_ne-NP'),
(N'nnh', N'Ngiemboon_nnh'),
(N'nnh-CM', N'Ngiemboon_Cameroon_nnh-CM'),
(N'jgo', N'Ngomba_jgo'),
(N'jgo-CM', N'Ngomba_Cameroon_jgo-CM'),
(N'lrc-IQ', N'Northern Luri_Iraq_lrc-IQ'),
(N'lrc-IR', N'Northern Luri_Iran_lrc-IR'),
(N'nd', N'North Ndebele_nd'),
(N'nd-ZW', N'North Ndebele_Zimbabwe_nd-ZW'),
(N'no', N'Norwegian (Bokmal)_no'),
(N'nb', N'Norwegian (Bokmal)_nb'),
(N'nb-NO', N'Norwegian (Bokmal)_Norway_nb-NO'),
(N'nn', N'Norwegian (Nynorsk)_nn'),
(N'nn-NO', N'Norwegian (Nynorsk)_Norway_nn-NO'),
(N'nb-SJ', N'Norwegian Bokmål_Svalbard and Jan Mayen_nb-SJ'),
(N'nus', N'Nuer_nus'),
(N'nus-SD', N'Nuer_Sudan_nus-SD'),
(N'nyn', N'Nyankole_nyn'),
(N'nyn-UG', N'Nyankole_Uganda_nyn-UG'),
(N'oc', N'Occitan_oc'),
(N'oc-FR', N'Occitan_France_oc-FR'),
(N'or', N'Odia_or'),
(N'or-IN', N'Odia_India_or-IN'),
(N'om', N'Oromo_om'),
(N'om-ET', N'Oromo_Ethiopia_om-ET'),
(N'om-KE', N'Oromo_Kenya_om-KE'),
(N'os', N'Ossetian_os'),
(N'os-GE', N'Ossetian_Cyrillic, Georgia_os-GE'),
(N'os-RU', N'Ossetian_Cyrillic, Russia_os-RU'),
(N'ps', N'Pashto_ps'),
(N'ps-AF', N'Pashto_Afghanistan_ps-AF'),
(N'fa', N'Persian_fa'),
(N'fa-AF', N'Persian_Afghanistan_fa-AF'),
(N'fa-IR', N'Persian_Iran_fa-IR'),
(N'pl', N'Polish_pl'),
(N'pl-PL', N'Polish_Poland_pl-PL'),
(N'pt', N'Portuguese_pt'),
(N'pt-AO', N'Portuguese_Angola_pt-AO'),
(N'pt-BR', N'Portuguese_Brazil_pt-BR'),
(N'pt-CV', N'Portuguese_Cabo Verde_pt-CV'),
(N'pt-GQ', N'Portuguese_Equatorial Guinea_pt-GQ'),
(N'pt-GW', N'Portuguese_Guinea-Bissau_pt-GW'),
(N'pt-LU', N'Portuguese_Luxembourg_pt-LU'),
(N'pt-MO', N'Portuguese_Macao SAR_pt-MO'),
(N'pt-MZ', N'Portuguese_Mozambique_pt-MZ'),
(N'pt-PT', N'Portuguese_Portugal_pt-PT'),
(N'pt-ST', N'Portuguese_São Tomé and Príncipe_pt-ST'),
(N'pt-CH', N'Portuguese_Switzerland_pt-CH'),
(N'pt-TL', N'Portuguese_Timor-Leste_pt-TL'),
(N'prg-001', N'Prussian_prg-001'),
(N'pa', N'Punjabi_pa'),
(N'pa-Arab', N'Punjabi_pa-Arab'),
(N'pa-IN', N'Punjabi_India_pa-IN'),
(N'pa-Arab-PK', N'Punjabi_Islamic Republic of Pakistan_pa-Arab-PK'),
(N'quz', N'Quechua_quz'),
(N'quz-BO', N'Quechua_Bolivia_quz-BO'),
(N'quz-EC', N'Quechua_Ecuador_quz-EC'),
(N'quz-PE', N'Quechua_Peru_quz-PE'),
(N'ksh', N'Ripuarian_ksh'),
(N'ksh-DE', N'Ripuarian_Germany_ksh-DE'),
(N'ro', N'Romanian_ro'),
(N'ro-MD', N'Romanian_Moldova_ro-MD'),
(N'ro-RO', N'Romanian_Romania_ro-RO'),
(N'rm', N'Romansh_rm'),
(N'rm-CH', N'Romansh_Switzerland_rm-CH'),
(N'rof', N'Rombo_rof'),
(N'rof-TZ', N'Rombo_Tanzania_rof-TZ'),
(N'rn', N'Rundi_rn'),
(N'rn-BI', N'Rundi_Burundi_rn-BI'),
(N'ru', N'Russian_ru'),
(N'ru-BY', N'Russian_Belarus_ru-BY'),
(N'ru-KZ', N'Russian_Kazakhstan_ru-KZ'),
(N'ru-KG', N'Russian_Kyrgyzstan_ru-KG'),
(N'ru-MD', N'Russian_Moldova_ru-MD'),
(N'ru-RU', N'Russian_Russia_ru-RU'),
(N'ru-UA', N'Russian_Ukraine_ru-UA'),
(N'rwk', N'Rwa_rwk'),
(N'rwk-TZ', N'Rwa_Tanzania_rwk-TZ'),
(N'ssy', N'Saho_ssy'),
(N'ssy-ER', N'Saho_Eritrea_ssy-ER'),
(N'sah', N'Sakha_sah'),
(N'sah-RU', N'Sakha_Russia_sah-RU'),
(N'saq', N'Samburu_saq'),
(N'saq-KE', N'Samburu_Kenya_saq-KE'),
(N'smn', N'Sami (Inari)_smn'),
(N'smn-FI', N'Sami (Inari)_Finland_smn-FI'),
(N'smj', N'Sami (Lule)_smj'),
(N'smj-NO', N'Sami (Lule)_Norway_smj-NO'),
(N'smj-SE', N'Sami (Lule)_Sweden_smj-SE'),
(N'se', N'Sami (Northern)_se'),
(N'se-FI', N'Sami (Northern)_Finland_se-FI'),
(N'se-NO', N'Sami (Northern)_Norway_se-NO'),
(N'se-SE', N'Sami (Northern)_Sweden_se-SE'),
(N'sms', N'Sami (Skolt)_sms'),
(N'sms-FI', N'Sami (Skolt)_Finland_sms-FI'),
(N'sma', N'Sami (Southern)_sma'),
(N'sma-NO', N'Sami (Southern)_Norway_sma-NO'),
(N'sma-SE', N'Sami (Southern)_Sweden_sma-SE'),
(N'sg', N'Sango_sg'),
(N'sg-CF', N'Sango_Central African Republic_sg-CF'),
(N'sbp', N'Sangu_sbp'),
(N'sbp-TZ', N'Sangu_Tanzania_sbp-TZ'),
(N'sa', N'Sanskrit_sa'),
(N'sa-IN', N'Sanskrit_India_sa-IN'),
(N'gd', N'Scottish Gaelic_gd'),
(N'gd-GB', N'Scottish Gaelic_United Kingdom_gd-GB'),
(N'seh', N'Sena_seh'),
(N'seh-MZ', N'Sena_Mozambique_seh-MZ'),
(N'sr-Cyrl', N'Serbian (Cyrillic)_sr-Cyrl'),
(N'sr-Cyrl-BA', N'Serbian (Cyrillic)_Bosnia and Herzegovina_sr-Cyrl-BA'),
(N'sr-Cyrl-ME', N'Serbian (Cyrillic)_Montenegro_sr-Cyrl-ME'),
(N'sr-Cyrl-RS', N'Serbian (Cyrillic)_Serbia_sr-Cyrl-RS'),
(N'sr-Cyrl-CS', N'Serbian (Cyrillic)_Serbia and Montenegro (Former)_sr-Cyrl-CS'),
(N'sr-Latn', N'Serbian (Latin)_sr-Latn'),
(N'sr', N'Serbian (Latin)_sr'),
(N'sr-Latn-BA', N'Serbian (Latin)_Bosnia and Herzegovina_sr-Latn-BA'),
(N'sr-Latn-ME', N'Serbian (Latin)_Montenegro_sr-Latn-ME'),
(N'sr-Latn-RS', N'Serbian (Latin)_Serbia_sr-Latn-RS'),
(N'sr-Latn-CS', N'Serbian (Latin)_Serbia and Montenegro (Former)_sr-Latn-CS'),
(N'nso', N'Sesotho sa Leboa_nso'),
(N'nso-ZA', N'Sesotho sa Leboa_South Africa_nso-ZA'),
(N'tn', N'Setswana_tn'),
(N'tn-BW', N'Setswana_Botswana_tn-BW'),
(N'tn-ZA', N'Setswana_South Africa_tn-ZA'),
(N'ksb', N'Shambala_ksb'),
(N'ksb-TZ', N'Shambala_Tanzania_ksb-TZ'),
(N'sn', N'Shona_sn'),
(N'sn-Latn', N'Shona_Latin_sn-Latn'),
(N'sn-Latn-ZW', N'Shona_Zimbabwe_sn-Latn-ZW'),
(N'sd', N'Sindhi_sd'),
(N'sd-Arab', N'Sindhi_sd-Arab'),
(N'sd-Arab-PK', N'Sindhi_Islamic Republic of Pakistan_sd-Arab-PK'),
(N'si', N'Sinhala_si'),
(N'si-LK', N'Sinhala_Sri Lanka_si-LK'),
(N'sk', N'Slovak_sk'),
(N'sk-SK', N'Slovak_Slovakia_sk-SK'),
(N'sl', N'Slovenian_sl'),
(N'sl-SI', N'Slovenian_Slovenia_sl-SI'),
(N'xog', N'Soga_xog'),
(N'xog-UG', N'Soga_Uganda_xog-UG'),
(N'so', N'Somali_so'),
(N'so-DJ', N'Somali_Djibouti_so-DJ'),
(N'so-ET', N'Somali_Ethiopia_so-ET'),
(N'so-KE', N'Somali_Kenya_so-KE'),
(N'so-SO', N'Somali_Somalia_so-SO'),
(N'st', N'Sotho_st'),
(N'st-ZA', N'Sotho_South Africa_st-ZA'),
(N'nr', N'South Ndebele_nr'),
(N'nr-ZA', N'South Ndebele_South Africa_nr-ZA'),
(N'st-LS', N'Southern Sotho_Lesotho_st-LS'),
(N'es', N'Spanish_es'),
(N'es-AR', N'Spanish_Argentina_es-AR'),
(N'es-BZ', N'Spanish_Belize_es-BZ'),
(N'es-VE', N'Spanish_Bolivarian Republic of Venezuela_es-VE'),
(N'es-BO', N'Spanish_Bolivia_es-BO'),
(N'es-BR', N'Spanish_Brazil_es-BR'),
(N'es-CL', N'Spanish_Chile_es-CL'),
(N'es-CO', N'Spanish_Colombia_es-CO'),
(N'es-CR', N'Spanish_Costa Rica_es-CR'),
(N'es-CU', N'Spanish_Cuba_es-CU'),
(N'es-DO', N'Spanish_Dominican Republic_es-DO'),
(N'es-EC', N'Spanish_Ecuador_es-EC'),
(N'es-SV', N'Spanish_El Salvador_es-SV'),
(N'es-GQ', N'Spanish_Equatorial Guinea_es-GQ'),
(N'es-GT', N'Spanish_Guatemala_es-GT'),
(N'es-HN', N'Spanish_Honduras_es-HN'),
(N'es-419', N'Spanish_Latin America_es-419'),
(N'es-MX', N'Spanish_Mexico_es-MX'),
(N'es-NI', N'Spanish_Nicaragua_es-NI'),
(N'es-PA', N'Spanish_Panama_es-PA'),
(N'es-PY', N'Spanish_Paraguay_es-PY'),
(N'es-PE', N'Spanish_Peru_es-PE'),
(N'es-PH', N'Spanish_Philippines_es-PH'),
(N'es-PR', N'Spanish_Puerto Rico_es-PR'),
(N'es-ES_tradnl', N'Spanish_Spain_es-ES_tradnl'),
(N'es-ES', N'Spanish_Spain_es-ES'),
(N'es-US', N'Spanish_United States_es-US'),
(N'es-UY', N'Spanish_Uruguay_es-UY'),
(N'zgh', N'Standard Moroccan Tamazight_zgh'),
(N'zgh-Tfng-MA', N'Standard Moroccan Tamazight_Morocco_zgh-Tfng-MA'),
(N'zgh-Tfng', N'Standard Moroccan Tamazight_Tifinagh_zgh-Tfng'),
(N'ss', N'Swati_ss'),
(N'ss-ZA', N'Swati_South Africa_ss-ZA'),
(N'ss-SZ', N'Swati_Swaziland_ss-SZ'),
(N'sv', N'Swedish_sv'),
(N'sv-AX', N'Swedish_Åland Islands_sv-AX'),
(N'sv-FI', N'Swedish_Finland_sv-FI'),
(N'sv-SE', N'Swedish_Sweden_sv-SE'),
(N'syr', N'Syriac_syr'),
(N'syr-SY', N'Syriac_Syria_syr-SY'),
(N'shi', N'Tachelhit_shi'),
(N'shi-Tfng', N'Tachelhit_Tifinagh_shi-Tfng'),
(N'shi-Tfng-MA', N'Tachelhit_Tifinagh, Morocco_shi-Tfng-MA'),
(N'shi-Latn', N'Tachelhit (Latin)_shi-Latn'),
(N'shi-Latn-MA', N'Tachelhit (Latin)_Morocco_shi-Latn-MA'),
(N'dav', N'Taita_dav'),
(N'dav-KE', N'Taita_Kenya_dav-KE'),
(N'tg', N'Tajik (Cyrillic)_tg'),
(N'tg-Cyrl', N'Tajik (Cyrillic)_tg-Cyrl'),
(N'tg-Cyrl-TJ', N'Tajik (Cyrillic)_Tajikistan_tg-Cyrl-TJ'),
(N'tzm', N'Tamazight (Latin)_tzm'),
(N'tzm-Latn', N'Tamazight (Latin)_tzm-Latn'),
(N'tzm-Latn-DZ', N'Tamazight (Latin)_Algeria_tzm-Latn-DZ'),
(N'ta', N'Tamil_ta'),
(N'ta-IN', N'Tamil_India_ta-IN'),
(N'ta-MY', N'Tamil_Malaysia_ta-MY'),
(N'ta-SG', N'Tamil_Singapore_ta-SG'),
(N'ta-LK', N'Tamil_Sri Lanka_ta-LK'),
(N'twq', N'Tasawaq_twq'),
(N'twq-NE', N'Tasawaq_Niger_twq-NE'),
(N'tt', N'Tatar_tt'),
(N'tt-RU', N'Tatar_Russia_tt-RU'),
(N'te', N'Telugu_te'),
(N'te-IN', N'Telugu_India_te-IN'),
(N'teo', N'Teso_teo'),
(N'teo-KE', N'Teso_Kenya_teo-KE'),
(N'teo-UG', N'Teso_Uganda_teo-UG'),
(N'th', N'Thai_th'),
(N'th-TH', N'Thai_Thailand_th-TH'),
(N'bo', N'Tibetan_bo'),
(N'bo-IN', N'Tibetan_India_bo-IN'),
(N'bo-CN', N'Tibetan_People''s Republic of China_bo-CN'),
(N'tig', N'Tigre_tig'),
(N'tig-ER', N'Tigre_Eritrea_tig-ER'),
(N'ti', N'Tigrinya_ti'),
(N'ti-ER', N'Tigrinya_Eritrea_ti-ER'),
(N'ti-ET', N'Tigrinya_Ethiopia_ti-ET'),
(N'to', N'Tongan_to'),
(N'to-TO', N'Tongan_Tonga_to-TO'),
(N'ts', N'Tsonga_ts'),
(N'ts-ZA', N'Tsonga_South Africa_ts-ZA'),
(N'tr', N'Turkish_tr'),
(N'tr-CY', N'Turkish_Cyprus_tr-CY'),
(N'tr-TR', N'Turkish_Turkey_tr-TR'),
(N'tk', N'Turkmen_tk'),
(N'tk-TM', N'Turkmen_Turkmenistan_tk-TM'),
(N'uk', N'Ukrainian_uk'),
(N'uk-UA', N'Ukrainian_Ukraine_uk-UA'),
(N'dsb or hsb', N'Upper Sorbian_dsb or hsb'),
(N'hsb-DE', N'Upper Sorbian_Germany_hsb-DE'),
(N'ur', N'Urdu_ur'),
(N'ur-IN', N'Urdu_India_ur-IN'),
(N'ur-PK', N'Urdu_Islamic Republic of Pakistan_ur-PK'),
(N'ug', N'Uyghur_ug'),
(N'ug-CN', N'Uyghur_People''s Republic of China_ug-CN'),
(N'uz-Arab', N'Uzbek_Perso-Arabic_uz-Arab'),
(N'uz-Arab-AF', N'Uzbek_Perso-Arabic, Afghanistan_uz-Arab-AF'),
(N'uz-Cyrl', N'Uzbek (Cyrillic)_uz-Cyrl'),
(N'uz-Cyrl-UZ', N'Uzbek (Cyrillic)_Uzbekistan_uz-Cyrl-UZ'),
(N'uz', N'Uzbek (Latin)_uz'),
(N'uz-Latn', N'Uzbek (Latin)_uz-Latn'),
(N'uz-Latn-UZ', N'Uzbek (Latin)_Uzbekistan_uz-Latn-UZ'),
(N'vai', N'Vai_vai'),
(N'vai-Vaii', N'Vai_vai-Vaii'),
(N'vai-Vaii-LR', N'Vai_Liberia_vai-Vaii-LR'),
(N'vai-Latn-LR', N'Vai (Latin)_ Liberia_vai-Latn-LR'),
(N'vai-Latn', N'Vai (Latin)_vai-Latn'),
(N'ca-ES-valencia', N'Valencian_Spain_ca-ES-valencia'),
(N've', N'Venda_ve'),
(N've-ZA', N'Venda_South Africa_ve-ZA'),
(N'vi', N'Vietnamese_vi'),
(N'vi-VN', N'Vietnamese_Vietnam_vi-VN'),
(N'vo', N'Volapük_vo'),
(N'vo-001', N'Volapük_World_vo-001'),
(N'vun', N'Vunjo_vun'),
(N'vun-TZ', N'Vunjo_Tanzania_vun-TZ'),
(N'wae', N'Walser_wae'),
(N'wae-CH', N'Walser_Switzerland_wae-CH'),
(N'cy', N'Welsh_cy'),
(N'cy-GB', N'Welsh_United Kingdom_cy-GB'),
(N'wal', N'Wolaytta_wal'),
(N'wal-ET', N'Wolaytta_Ethiopia_wal-ET'),
(N'wo', N'Wolof_wo'),
(N'wo-SN', N'Wolof_Senegal_wo-SN'),
(N'xh', N'Xhosa_xh'),
(N'xh-ZA', N'Xhosa_South Africa_xh-ZA'),
(N'yav', N'Yangben_yav'),
(N'yav-CM', N'Yangben_Cameroon_yav-CM'),
(N'ii', N'Yi_ii'),
(N'ii-CN', N'Yi_People''s Republic of China_ii-CN'),
(N'yo', N'Yoruba_yo'),
(N'yo-BJ', N'Yoruba_Benin_yo-BJ'),
(N'yo-NG', N'Yoruba_Nigeria_yo-NG'),
(N'dje', N'Zarma_dje'),
(N'dje-NE', N'Zarma_Niger_dje-NE'),
(N'zu', N'Zulu_zu'),
(N'zu-ZA', N'Zulu_South Africa_zu-ZA')
-- NEW LANGUAGECODES - END --
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
INSERT [dbo].[vts_tbQuestionSelectionMode] ([QuestionSelectionModeID], [Description], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (1, N'RadioButtonSelection', N'Votations.NSurvey.WebControls.UI.RadioButtonQuestion', N'SurveyProject.WebControls', 7)
GO
INSERT [dbo].[vts_tbQuestionSelectionMode] ([QuestionSelectionModeID], [Description], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (2, N'CheckBoxSelection', N'Votations.NSurvey.WebControls.UI.CheckBoxQuestion', N'SurveyProject.WebControls', 23)
GO
INSERT [dbo].[vts_tbQuestionSelectionMode] ([QuestionSelectionModeID], [Description], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (3, N'DropDownListSelection', N'Votations.NSurvey.WebControls.UI.DropDownQuestion', N'SurveyProject.WebControls', 7)
GO
INSERT [dbo].[vts_tbQuestionSelectionMode] ([QuestionSelectionModeID], [Description], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (4, N'MatrixSingleSelection', N'Votations.NSurvey.WebControls.UI.MatrixQuestion', N'SurveyProject.WebControls', 14)
GO
INSERT [dbo].[vts_tbQuestionSelectionMode] ([QuestionSelectionModeID], [Description], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (5, N'StaticTextSelection', N'Votations.NSurvey.WebControls.UI.StaticQuestion', N'SurveyProject.WebControls', 3)
GO
INSERT [dbo].[vts_tbQuestionSelectionMode] ([QuestionSelectionModeID], [Description], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (6, N'MatrixMultipleSelection', N'Votations.NSurvey.WebControls.UI.CheckBoxMatrixQuestion', N'SurveyProject.WebControls', 30)
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
INSERT [dbo].[vts_tbRegularExpression] ([RegularExpressionId], [Description], [RegExpression], [RegExMessage], [BuiltIn]) VALUES (6, N'Hours', N'^([0-9]|[1][0-9]|[2][0-4])$', N'0 - 24 hours only', 1)
GO
INSERT [dbo].[vts_tbRegularExpression] ([RegularExpressionId], [Description], [RegExpression], [RegExMessage], [BuiltIn]) VALUES (7, N'Minutes', N'^([0-9]|[1-4][0-9]|[5][0-9])$', N'0 - 59 minutes allowed', 1)
GO
INSERT [dbo].[vts_tbRegularExpression] ([RegularExpressionId], [Description], [RegExpression], [RegExMessage], [BuiltIn]) VALUES (8, N'WeekDays', N'^([0-7])$', N'0 - 7 days allowed', 1)
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
INSERT [dbo].[vts_tbRole] ([RoleID], [RoleName]) VALUES (1, N'Report Viewer')
GO
INSERT [dbo].[vts_tbRole] ([RoleID], [RoleName]) VALUES (2, N'Survey Respondent')
GO
INSERT [dbo].[vts_tbRole] ([RoleID], [RoleName]) VALUES (3, N'Survey Creator')
GO
INSERT [dbo].[vts_tbRole] ([RoleID], [RoleName]) VALUES (4, N'Registered User')
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
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (2, 30)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (3, 1)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (3, 2)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (3, 3)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (3, 4)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (3, 5)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (3, 6)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (3, 7)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (3, 8)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (3, 12)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (3, 22)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (3, 25)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (3, 29)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (3, 34)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (3, 35)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (3, 38)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (3, 39)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (3, 40)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (4, 16)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (4, 17)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (4, 23)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (4, 30)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (4, 40)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (4, 41)
GO
INSERT [dbo].[vts_tbRoleSecurityRight] ([RoleID], [SecurityRightID]) VALUES (4, 43)
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
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (41, N'AccessUserResponses')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (42, N'AccessSsrsReports')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (43, N'AccessUserAccount')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (44, N'AccessRolesManager')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (45, N'AccessImportUsers')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (46, N'AllowQuestionXmlImport')
GO
INSERT [dbo].[vts_tbSecurityRight] ([SecurityRightID], [Description]) VALUES (47, N'IpFilterSecurityRight')
GO

INSERT [dbo].[vts_tbUnAuthentifiedUserAction] ([UnAuthentifiedUserActionID], [Description]) VALUES (0, N'SelectSecurityAction')
GO
INSERT [dbo].[vts_tbUnAuthentifiedUserAction] ([UnAuthentifiedUserActionID], [Description]) VALUES (1, N'ShowThankYouText')
GO
INSERT [dbo].[vts_tbUnAuthentifiedUserAction] ([UnAuthentifiedUserActionID], [Description]) VALUES (2, N'HideSurveyText')
GO
INSERT [dbo].[vts_tbUnAuthentifiedUserAction] ([UnAuthentifiedUserActionID], [Description]) VALUES (3, N'SecurityGeneralWarningText')
GO

SET IDENTITY_INSERT [dbo].[vts_tbWebSecurityAddIn] ON 

GO
INSERT [dbo].[vts_tbWebSecurityAddIn] ([WebSecurityAddInID], [Description], [BuiltIn], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (1, N'PasswordProtection', 1, N'Votations.NSurvey.Security.PasswordWebSecurityAddIn', N'SurveyProject.WebControls', 0)
GO
INSERT [dbo].[vts_tbWebSecurityAddIn] ([WebSecurityAddInID], [Description], [BuiltIn], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (2, N'IPProtection', 1, N'Votations.NSurvey.Security.IPWebSecurityAddIn', N'SurveyProject.WebControls', 0)
GO
INSERT [dbo].[vts_tbWebSecurityAddIn] ([WebSecurityAddInID], [Description], [BuiltIn], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (3, N'CookieProtection', 1, N'Votations.NSurvey.Security.CookieWebSecurityAddIn', N'SurveyProject.WebControls', 0)
GO
INSERT [dbo].[vts_tbWebSecurityAddIn] ([WebSecurityAddInID], [Description], [BuiltIn], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (4, N'EmailCodeProtection', 1, N'Votations.NSurvey.Security.EmailWebSecurityAddIn', N'SurveyProject.WebControls', 0)
GO
INSERT [dbo].[vts_tbWebSecurityAddIn] ([WebSecurityAddInID], [Description], [BuiltIn], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (5, N'ASPNETSecurityContextProtection', 1, N'Votations.NSurvey.Security.ASPNetContextSecurityAddIn', N'SurveyProject.WebControls', 0)
GO
INSERT [dbo].[vts_tbWebSecurityAddIn] ([WebSecurityAddInID], [Description], [BuiltIn], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (6, N'NSurveySecurityContextProtection', 1, N'Votations.NSurvey.Security.NSurveyContextSecurityAddIn', N'SurveyProject.WebControls', 0)
GO
INSERT [dbo].[vts_tbWebSecurityAddIn] ([WebSecurityAddInID], [Description], [BuiltIn], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (7, N'EntryQuotaProtection', 1, N'Votations.NSurvey.Security.EntryQuotaSecurityAddIn', N'SurveyProject.WebControls', 0)
GO
INSERT [dbo].[vts_tbWebSecurityAddIn] ([WebSecurityAddInID], [Description], [BuiltIn], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (8, N'Token Protection', 1, N'Votations.NSurvey.Security.TokenSecurityAddIn', N'SurveyProject.WebControls', 0)
GO
INSERT [dbo].[vts_tbWebSecurityAddIn] ([WebSecurityAddInID], [Description], [BuiltIn], [TypeNameSpace], [TypeAssembly], [TypeMode]) VALUES (9, N'IP Filter', 1, N'Votations.NSurvey.Security.IPRangeSecurityAddIn', N'SurveyProject.WebControls', 0)
GO
SET IDENTITY_INSERT [dbo].[vts_tbWebSecurityAddIn] OFF
GO
/****** Object:  Index [PK_vts_tbAnswer]    Script Date: 19-8-2014 22:01:40 ******/
ALTER TABLE [dbo].[vts_tbAnswer] ADD  CONSTRAINT [PK_vts_tbAnswer] PRIMARY KEY NONCLUSTERED 
(
	[AnswerID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF)
GO
/****** Object:  Index [IX_Answer]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_Answer] ON [dbo].[vts_tbAnswer]
(
	[DisplayOrder] ASC,
	[ScorePoint] ASC
)
INCLUDE ( 	[AnswerID]) WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
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
INCLUDE ( 	[AnswerID]) WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
GO
/****** Object:  Index [IX_QuestionID]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_QuestionID] ON [dbo].[vts_tbAnswer]
(
	[QuestionID] ASC,
	[DisplayOrder] ASC,
	[ScorePoint] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
GO
/****** Object:  Index [IX_Scorepoint]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_Scorepoint] ON [dbo].[vts_tbAnswer]
(
	[ScorePoint] ASC
)
INCLUDE ( 	[AnswerID]) WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
GO
/****** Object:  Index [PK_vts_tbQuestion]    Script Date: 19-8-2014 22:01:40 ******/
ALTER TABLE [dbo].[vts_tbQuestion] ADD  CONSTRAINT [PK_vts_tbQuestion] PRIMARY KEY NONCLUSTERED 
(
	[QuestionID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF)
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_DisplayOrder]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_DisplayOrder] ON [dbo].[vts_tbQuestion]
(
	[DisplayOrder] ASC,
	[QuestionIDText] ASC
)
INCLUDE ( 	[QuestionID]) WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
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
INCLUDE ( 	[QuestionID]) WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_QuestionAlias]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_QuestionAlias] ON [dbo].[vts_tbQuestion]
(
	[Alias] ASC,
	[QuestionIDText] ASC
)
INCLUDE ( 	[QuestionID]) WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
GO
/****** Object:  Index [IX_QuestionGroupID]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_QuestionGroupID] ON [dbo].[vts_tbQuestion]
(
	[QuestionGroupID] ASC
)
INCLUDE ( 	[QuestionID]) WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
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
	[QuestionGroupID]) WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
GO
/****** Object:  Index [IX_SurveyID]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_SurveyID] ON [dbo].[vts_tbQuestion]
(
	[SurveyID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_GroupName]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_GroupName] ON [dbo].[vts_tbQuestionGroups]
(
	[GroupName] ASC,
	[ParentGroupID] ASC
)
INCLUDE ( 	[ID]) WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
GO
/****** Object:  Index [IX_ParentGroupID]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_ParentGroupID] ON [dbo].[vts_tbQuestionGroups]
(
	[ParentGroupID] ASC
)
INCLUDE ( 	[ID]) WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
GO
/****** Object:  Index [IX_QuestionGroups]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_QuestionGroups] ON [dbo].[vts_tbQuestionGroups]
(
	[DisplayOrder] ASC
)
INCLUDE ( 	[ID]) WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
GO
/****** Object:  Index [PK_vts_tbSurvey]    Script Date: 19-8-2014 22:01:40 ******/
ALTER TABLE [dbo].[vts_tbSurvey] ADD  CONSTRAINT [PK_vts_tbSurvey] PRIMARY KEY NONCLUSTERED 
(
	[SurveyID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF)
GO
/****** Object:  Index [IDX_Voter]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IDX_Voter] ON [dbo].[vts_tbVoter]
(
	[Validated] ASC,
	[SurveyID] ASC
)
INCLUDE ( 	[VoterID]) WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
GO
/****** Object:  Index [IX_SurveyID]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_SurveyID] ON [dbo].[vts_tbVoter]
(
	[SurveyID] ASC
)
INCLUDE ( 	[VoterID],
	[Validated]) WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
GO
/****** Object:  Index [IX_Answer]    Script Date: 19-8-2014 22:01:40 ******/
CREATE NONCLUSTERED INDEX [IX_Answer] ON [dbo].[vts_tbVoterAnswers]
(
	[AnswerID] ASC
)
INCLUDE ( 	[VoterID]) WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
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
/*ADDTIONS SP 2.5 - Menu MenuSecurityRights */
/****** Object:  Table [dbo].[vts_tbMenu]    Script Date: 9/12/2018 11:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbMenu](
	[MenuID] [int] NOT NULL,
	[Code] [int] NOT NULL,
	[Main] [nvarchar](50) NOT NULL,
	[SubOne] [nvarchar](50) NULL,
	[SubTwo] [nvarchar](50) NULL,
	[SubThree] [nvarchar](75) NULL,
 CONSTRAINT [PK_vts_tbMenus] PRIMARY KEY CLUSTERED 
(
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[vts_tbMenuSecurityRight]    Script Date: 9/12/2018 11:54:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vts_tbMenuSecurityRight](
	[MenuID] [int] NOT NULL,
	[SecurityRightID] [int] NOT NULL,
 CONSTRAINT [PK_vts_tbMenuSecurityRight] PRIMARY KEY CLUSTERED 
(
	[MenuID] ASC,
	[SecurityRightID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (1, 1000, N'Surveys', NULL, NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (2, 1100, N'Surveys', N'SurveyList', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (3, 1200, N'Surveys', N'New Survey', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (4, 1201, N'Surveys', N'New Survey', NULL, N'Button - import survey xml')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (5, 1300, N'Surveys', N'Statistics', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (6, 1301, N'Surveys', N'Statistics', NULL, N'Link – reset votes')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (7, 1302, N'Surveys', N'Statistics', NULL, N'Button - delete unvalidated')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (8, 1400, N'Surveys', N'Settings', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (9, 1410, N'Surveys', N'Settings', N'System Settings', NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (10, 1411, N'Surveys', N'Settings', N'System Settings', N'Error Log')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (11, 1420, N'Surveys', N'Settings', N'Survey Settings', NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (12, 1421, N'Surveys', N'Settings', N'Survey Settings', N'Button - delete survey')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (13, 1422, N'Surveys', N'Settings', N'Survey Settings', N'Button - export survey')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (14, 1423, N'Surveys', N'Settings', N'Survey Settings', N'Button - apply changes')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (15, 1424, N'Surveys', N'Settings', N'Survey Settings', N'Button - clone survey')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (16, 1430, N'Surveys', N'Settings', N'Multi Language', NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (17, 1440, N'Surveys', N'Settings', N'Completion', NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (18, 1500, N'Surveys', N'Security', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (19, 1510, N'Surveys', N'Security', N'Form Security', NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (20, 1520, N'Surveys', N'Security', N'Token Security', NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (21, 1530, N'Surveys', N'Security', N'IP Filter Security', NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (22, 2000, N'Designer', NULL, NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (23, 2100, N'Designer', N'Form Builder', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (24, 2101, N'Designer', N'Form Builder', N'[Insert Question]', N'Copy question from Library')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (25, 2102, N'Designer', N'Form Builder', N'[Insert Question]', N'Copy question from Survey')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (26, 2103, N'Designer', N'Form Builder', N'[Insert Question]', N'Button – Import XML')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (27, 2200, N'Designer', N'Question Libraries', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (28, 2210, N'Designer', N'Question Libraries', N'Library List', NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (29, 2211, N'Designer', N'Question Libraries', N'Library List', N'Button – Import XML')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (30, 2220, N'Designer', N'Question Libraries', N'Library New', NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (31, 2300, N'Designer', N'Question Groups', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (32, 2400, N'Designer', N'Answer Type Editor', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (33, 2401, N'Designer', N'Answer Type Editor', NULL, N'Button – Create SQL answertype')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (34, 2500, N'Designer', N'Regular Expressions', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (35, 2600, N'Designer', N'Layout', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (36, 3000, N'Results', NULL, NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (37, 3100, N'Results', N'Reports', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (38, 3101, N'Results', N'Reports', NULL, N'Link – Edit/ create filter link')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (39, 3102, N'Results', N'Reports', NULL, N'Button – Cross Tabulation Reports')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (40, 3103, N'Results', N'Reports', NULL, N'Button – SSRS Reports')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (41, 3110, N'Results', N'Reports', N'Filters', NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (42, 3200, N'Results', N'Individual Responses', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (43, 3201, N'Results', N'Individual Responses', NULL, N'Filtered Responses List')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (44, 3202, N'Results', N'Individual Responses', NULL, N'Responses List – Button  - Delete')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (45, 3203, N'Results', N'Individual Responses', NULL, N'Voter Report – Button – Edit')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (46, 3300, N'Results', N'File Manager', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (47, 3301, N'Results', N'File Manager', NULL, N'Button – export files')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (48, 3400, N'Results', N'Data Export', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (49, 3500, N'Results', N'Data import', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (50, 4000, N'Campaigns', NULL, NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (51, 4100, N'Campaigns', N'Preview', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (52, 4200, N'Campaigns', N'Mailing', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (53, 4300, N'Campaigns', N'Take Survey', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (54, 5000, N'Accounts', NULL, NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (55, 5100, N'Accounts', N'UserList', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (56, 5101, N'Accounts', N'UserList', NULL, N'Users Edit Options')
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (57, 5200, N'Accounts', N'User Roles', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (58, 5300, N'Accounts', N'Import Users', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (59, 6000, N'Help', NULL, NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (60, 6100, N'Help', N'Help options', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (61, 6200, N'Help', N'Help Files', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (62, 6300, N'Help', N'About SP', NULL, NULL)
GO
INSERT [dbo].[vts_tbMenu] ([MenuID], [Code], [Main], [SubOne], [SubTwo], [SubThree]) VALUES (63, 2212, N'Designer', N'Question Libraries', N'Library List', N'Button – Insert Question')
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (2, 40)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (3, 1)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (4, 29)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (5, 9)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (6, 10)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (7, 11)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (11, 3)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (12, 2)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (13, 4)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (14, 5)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (15, 6)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (16, 34)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (17, 7)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (19, 8)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (20, 38)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (21, 47)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (23, 12)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (24, 25)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (25, 26)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (26, 46)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (28, 24)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (29, 46)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (30, 27)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (31, 36)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (32, 13)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (33, 28)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (34, 31)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (35, 39)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (37, 14)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (38, 15)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (39, 19)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (40, 42)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (42, 16)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (43, 41)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (44, 18)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (45, 17)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (46, 32)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (47, 33)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (48, 20)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (49, 37)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (51, 22)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (52, 21)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (53, 30)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (55, 23)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (56, 43)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (57, 44)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (58, 45)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (60, 35)
GO
INSERT [dbo].[vts_tbMenuSecurityRight] ([MenuID], [SecurityRightID]) VALUES (63, 12)
GO
ALTER TABLE [dbo].[vts_tbMenuSecurityRight]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbMenuSecurityRight_vts_tbMenu] FOREIGN KEY([MenuID])
REFERENCES [dbo].[vts_tbMenu] ([MenuID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbMenuSecurityRight] CHECK CONSTRAINT [FK_vts_tbMenuSecurityRight_vts_tbMenu]
GO
ALTER TABLE [dbo].[vts_tbMenuSecurityRight]  WITH CHECK ADD  CONSTRAINT [FK_vts_tbMenuSecurityRight_vts_tbSecurityRight] FOREIGN KEY([SecurityRightID])
REFERENCES [dbo].[vts_tbSecurityRight] ([SecurityRightID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[vts_tbMenuSecurityRight] CHECK CONSTRAINT [FK_vts_tbMenuSecurityRight_vts_tbSecurityRight]
GO