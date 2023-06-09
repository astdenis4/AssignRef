USE [AssignRef]
GO
/****** Object:  StoredProcedure [dbo].[PageTranslations_SelectByLanguagesId]    Script Date: 6/7/2023 3:30:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dru Rivas Martin
-- Create date: 5/16/2023
-- Description: PageTranslations_SelectByLanguagesId



-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer: Shane RM
-- Note: Looks Good
--
-- =============================================
CREATE proc [dbo].[PageTranslations_SelectByLanguagesId]

			
			@LanguageId int
			,@PageIndex  int 
			,@PageSize  int 

as
/*  
f
DECLARE   @LanguageId  int	= 2
		 ,@PageIndex	    int = 0
		 ,@PageSize		    int	= 4
		 
			

EXECUTE	[dbo].[PageTranslations_SelectByLanguagesId]
		@LanguageId
		 ,@PageIndex	
	     ,@PageSize	
	    

	SELECT	*
	FROM	dbo.Languages

	SELECT	*
	FROM	dbo.PageTranslations

	SELECT	*
	FROM dbo.PageSectionContent
*/
BEGIN
DECLARE	@Offset	int	=	@PageIndex*@PageSize

SELECT pt.Id

      ,l.Id
	  ,l.Name

      ,pt.Link
      ,pt.Name
      ,pt.CreatedBy
      ,pt.IsActive

	  ,ps.Id
	  ,ps.PageTranslationId
	  ,ps.Name
	  ,ps.Component
	  ,ps.IsActive
	  
	  ,psk.Id
	  ,psk.PageSectionId
	  ,psk.KeyName
	  
	  ,psc.Id
	 
	  ,psc.Text
	  ,psc.LanguageId
	  ,TotalCount = COUNT(1) OVER()

  FROM [dbo].[PageTranslations] as pt inner join dbo.Languages as l 
  on pt.LanguageId = l.Id 

	 inner join dbo.PageSection as ps 
  ON ps.PageTranslationId = pt.Id

	inner join dbo.PageSectionKeys as psk
	ON psk.PageSectionId = ps.Id

	inner join dbo.PageSectionContent as psc 
	ON psc.LanguageId = l.Id

  Where (pt.LanguageId = @LanguageId )
  ORDER By pt.LanguageId
  	OFFSET		@Offset ROWS
  FETCH NEXT	@PageSize ROWS ONLY


END
GO
