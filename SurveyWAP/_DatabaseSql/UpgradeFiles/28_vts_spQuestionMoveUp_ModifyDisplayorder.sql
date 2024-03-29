USE [sp25dev]
GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionMoveUp]    Script Date: 3/23/2020 16:42:03 ******/
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
///  Moves a question positions up 
/// </summary>
/// <param name="@QuestionID">
/// ID of the questions to move one position up
/// </param>
*/
ALTER PROCEDURE [dbo].[vts_spQuestionMoveUp] @QuestionID int 
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
	ParentQuestionID is NULL AND
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



