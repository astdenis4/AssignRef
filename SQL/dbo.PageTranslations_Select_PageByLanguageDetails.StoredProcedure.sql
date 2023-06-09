USE [AssignRef]
GO
/****** Object:  StoredProcedure [dbo].[PageTranslations_Select_PageByLanguageDetails]    Script Date: 6/7/2023 3:30:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Alicia St. Denis>
-- Create date: <5/08/2023>
-- Description: <Select Page By Language Details proc for PageTranslations>
-- Code Reviewer:William Kuheleloa

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[PageTranslations_Select_PageByLanguageDetails]

	        @link nvarchar(255),
			@LanguageId int

/*

Declare @link nvarchar(255) = 'qqqq',
			@LanguageId int = 3

Execute [dbo].[PageTranslations_Select_PageByLanguageDetails] @link, @LanguageId




*/

as

BEGIN

SELECT pt.Id
      ,l.Id
  	 ,l.Name
	  ,Link
      ,pt.Name
      ,CreatedBy
      ,IsActive
  FROM [dbo].[PageTranslations] as pt inner join dbo.Languages as l 
  on pt.LanguageId = l.Id

   Where link = @link AND LanguageId = @LanguageId AND IsActive = 1

END 

GO
