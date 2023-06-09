USE [AssignRef]
GO
/****** Object:  StoredProcedure [dbo].[PageTranslations_SelectAll]    Script Date: 6/7/2023 3:30:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author: <Alicia St. Denis>
-- Create date: <5/08/2023>
-- Description: <SelectAll proc for PageTranslations>
-- Code Reviewer:William Kuheleloa

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================


CREATE proc [dbo].[PageTranslations_SelectAll]  

           	@PageIndex int 
			,@PageSize int
			
as

/*
Declare  @PageIndex int = 0, @PageSize int = 10

Execute  dbo.PageTranslations_SelectAll @PageIndex, @PageSize

Select * 
FROM PageTranslations

*/

BEGIN 

Declare @offset int = @PageIndex * @PageSize

SELECT pt.Id
	  ,l.Id
	  ,l.Name
      ,[Link]
      ,pt.Name
      ,[CreatedBy]
      ,[IsActive]

	,TotalCount = COUNT(1) OVER()

  FROM [dbo].[PageTranslations] as pt inner join dbo.Languages as l
  on pt.LanguageId = l.Id

	ORDER BY pt.Id

	OFFSET @offSet Rows
	Fetch Next @PageSize Rows ONLY

END
GO
