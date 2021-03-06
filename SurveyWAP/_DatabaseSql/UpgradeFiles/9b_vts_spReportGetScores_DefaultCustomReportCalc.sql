USE [dbname]
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

