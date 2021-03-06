USE [dbname]
GO
/****** Object:  StoredProcedure [dbo].[vts_spTreeNodesGetAll]    Script Date: 11/27/2017 14:56:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<SurveyProject, W3DevPro>
-- Create date: <2017, Nov 27th>
-- Description:	<Adjustment to SP Single UserMode Options>
-- =============================================
-- JJ Modified to restrict to Surveys to which the User has access
--
ALTER PROCEDURE [dbo].[vts_spTreeNodesGetAll]
				(
				@UserId int
				)
AS

-- UserID in case of Single User Mode
If @UserID = 0 
set @UserID = 1

BEGIN	
	SET NOCOUNT ON;
	WITH CTE As
	(
	SELECT sv.FolderID , 
		 fs.FolderName , 
		fs.ParentFolderId 
		FROM vts_tbSurvey AS sv
		JOIN vts_tbFolders AS fs ON fs.FolderId = sv.FolderId 
		WHERE exists(
	    select 1 from vts_tbUserSurvey as usr
	    where usr.surveyId=sv.surveyId and usr.UserId=@UserId )
	    OR exists(
	    select 1 from vts_tbusersetting us
	      where us.userid=@UserId
	      and(us.GlobalSurveyAccess=1 OR us.IsAdmin=1)
	    )
	    UNION ALL
	    SELECT fs.FolderId   , 
	           fs.FolderName , 
		       fs.ParentFolderId  
		from vts_tbFolders as fs JOIN cte 
		on cte.ParentFolderId=fs.FolderId
		where cte.parentFolderId IS NOT NULL
	    )
	SELECT 's' + CONVERT(varchar, sv.SurveyID) as ItemId, 
		ISNULL(sv.Title, fs.FolderName) as NodeName, 
		'f' + CONVERT(varchar, sv.FolderId) as ParentFolderId
		FROM vts_tbSurvey AS sv 
		JOIN vts_tbFolders AS fs ON fs.FolderId = sv.FolderId 
		WHERE (exists(
	    select 1 from vts_tbUserSurvey as usr
	    where usr.surveyId=sv.surveyId and usr.UserId=@UserId )
	    or exists (
	    select 1 from vts_tbUserSetting st
	    where st.userid=@userid
	    and (st.Isadmin=1 or st.GlobalSurveyAccess=1)
	    )
	    )
	  
	UNION 
	SELECT 'f' + CONVERT(varchar, fs.FolderId)  as ItemId , FolderName  as NodeName, 
		'f' + CONVERT(varchar, fs.ParentFolderId)  as ParentFolderId
		from cte as fs 
     UNION
	SELECT 'f' + CONVERT(varchar, fs.FolderId)  as ItemId , FolderName  as NodeName, 
		'f' + CONVERT(varchar, fs.ParentFolderId)  as ParentFolderId
		from vts_tbFolders as fs 
	WHERE exists(
	    select 1 from vts_tbUserSetting st
	    where st.userid=@userid
	    and (st.Isadmin=1 or st.GlobalSurveyAccess=1))
		ORDER BY NodeName		
END



