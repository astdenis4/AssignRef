USE [AssignRef]
GO
/****** Object:  StoredProcedure [dbo].[PageTranslations_Update]    Script Date: 6/7/2023 3:30:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author: <Alicia St. Denis>
-- Create date: <5/08/2023>
-- Description: <Update proc for PageTranslations>
-- Code Reviewer:William Kuheleloa

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[PageTranslations_Update]

	  @LanguageId int
      ,@Link nvarchar (255)
      ,@Name nvarchar(100)
      ,@CreatedBy int
      ,@IsActive bit
	  ,@Id int

/*

	Declare @Id int = 15;

	Declare @LanguageId int = 3
		   ,@Link nvarchar (255) = 'llll'
	       ,@Name nvarchar(100) = 'Kean'
		   ,@CreatedBy int = 7
		   ,@IsActive bit = 0

	Select *
    From dbo.PageTranslations
    Where Id = @Id

	Execute dbo.PageTranslations_Update

		  @LanguageId 
         ,@Link 
         ,@Name 
         ,@CreatedBy 
         ,@IsActive 
	     ,@Id 

	select * 
	FROM dbo.PageTranslations
		
*/

as

BEGIN

UPDATE [dbo].[PageTranslations]
   SET [LanguageId] = @LanguageId
      ,[Link] = @Link
      ,[Name] = @Name
      ,[CreatedBy] = @CreatedBy
      ,[IsActive] = @IsActive
 WHERE Id = @Id

 END


GO
