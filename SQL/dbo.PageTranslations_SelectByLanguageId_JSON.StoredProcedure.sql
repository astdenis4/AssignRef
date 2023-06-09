USE [AssignRef]
GO
/****** Object:  StoredProcedure [dbo].[PageTranslations_SelectByLanguageId_JSON]    Script Date: 6/7/2023 3:30:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dru Rivas Martin
-- Create date: 06/01/2023
-- Description: PageTranslations_SelectByLanguagesId



-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer: Leonardo Murillo
-- Note: Looks Good
--
-- =============================================
CREATE proc [dbo].[PageTranslations_SelectByLanguageId_JSON]

			--PageTranslations_SelectByLanguageId_JSON
			@LanguageId int
			,@PageTranslationId  int = NULL
			

as
/*  

DECLARE   @LanguageId  int	= 2
		 ,@PageTranslationId int = 14
		 			
EXECUTE	[dbo].[PageTranslations_SelectByLanguageId_JSON]
		@LanguageId,
		 @PageTranslationId;
	    
	SELECT	*
	FROM	dbo.Languages

	SELECT	*
	FROM	dbo.PageTranslations

	SELECT	*
	FROM dbo.PageSectionContent

	SELECT *
	FROM dbo.PageSection
*/
BEGIN


SELECT pt.Id
      ,l.Id
	  ,l.[Name]
      ,pt.Link
      ,pt.[Name] as pageTranslationName
      ,pt.CreatedBy
      ,pt.IsActive
	  ,PageSections = 
		(
		SELECT 
			ps.Id
			,ps.PageTranslationId
			,ps.[Name] as pageSectionName
			,ps.Component
			,ps.IsActive
			,PageSectionKeys = 
				(
				SELECT
					psk.Id
					,psk.PageSectionId
					,psk.KeyName
					,PageSectionContents =
						(
						SELECT 
							psc.Id
							,psc.PageSectionKeyId
							,psc.Text
							,psc.LanguageId
						FROM dbo.PageSectionContent as psc 
						WHERE psc.PageSectionKeyId = psk.Id
						FOR JSON PATH										
						)
				FROM dbo.PageSectionKeys as psk
				inner join dbo.PageSectionContent as psc 
					ON psc.PageSectionKeyId = psk.Id
				WHERE psk.PageSectionId = ps.Id
				AND	psc.LanguageId = l.Id
				FOR JSON PATH										
				)
		FROM dbo.PageSection as ps 
		WHERE ps.PageTranslationId = pt.Id
		FOR JSON PATH
		)

  FROM [dbo].[PageTranslations] as pt 
  INNER JOIN dbo.Languages as l 
  ON pt.LanguageId = l.Id 
  WHERE (pt.LanguageId = @LanguageId )


END
GO
