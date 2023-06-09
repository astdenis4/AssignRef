USE [AssignRef]
GO
/****** Object:  StoredProcedure [dbo].[PageTranslations_Select_PageByLanguage]    Script Date: 6/7/2023 3:30:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Alicia St. Denis>
-- Create date: <5/08/2023>
-- Description: <Select Page By Language proc for PageTranslations>
-- Code Reviewer:William Kuheleloa

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================


CREATE proc [dbo].[PageTranslations_Select_PageByLanguage]

			@link nvarchar(255),
			@LanguageId int
/*

	Declare @link nvarchar(255) = 'mno'
	Declare @LanguageId int = 2
	Execute dbo.PageTranslations_Select_PageByLanguage @link, @LanguageId
	
	select * 
	FROM dbo.PageTranslations

*/

as

BEGIN

SELECT pt.Id
      ,l.Id
	  ,l.Name
      ,[Link]
      ,pt.Name
      ,[CreatedBy]
      ,[IsActive]
  FROM [dbo].[PageTranslations] as pt inner join dbo.Languages as l 
  on pt.LanguageId = l.Id 

  Where link = @link AND LanguageId = @LanguageId AND IsActive = 1

END
GO
