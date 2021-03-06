USE [sp25dev]
GO
/****** Object:  StoredProcedure [dbo].[vts_spQuestionChildAddNew]    Script Date: 3/23/2020 10:14:43 ******/
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
/// Adds a new question to a survey
/// </summary>
/// <param name="@ParentQuestionID">
/// Question  to which the child question will be added
/// </param>
/// <param name="@QuestionText">
/// Question's text
/// </param>
/// <param name="@QuestionID">
/// Created child question's ID
/// </param>
*/
ALTER PROCEDURE [dbo].[vts_spQuestionChildAddNew]
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

