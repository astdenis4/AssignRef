USE [AssignRef]
GO
/****** Object:  StoredProcedure [dbo].[PageTranslations_Insert]    Script Date: 6/7/2023 3:30:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Alicia St. Denis>
-- Create date: <5/08/2023>
-- Description: <Insert proc for PageTranslations>
-- Code Reviewer:William Kuheleloa

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[PageTranslations_Insert]

	   @LanguageId int
      ,@Link nvarchar (255)
      ,@Name nvarchar(100)
      ,@CreatedBy int
      ,@IsActive bit
	  ,@Id int OUTPUT

/*
	
Declare @Id  int = 0;

Declare  @LanguageId int = 7
      ,@Link nvarchar (255) = 'dfg'
      ,@Name nvarchar(100) = 'Chika'
      ,@CreatedBy int = 7
      ,@IsActive bit = 1
	 
 Execute dbo.PageTranslations_Insert

	   @LanguageId 
      ,@Link 
      ,@Name 
      ,@CreatedBy
      ,@IsActive 
	  ,@Id OUTPUT

	select * 
	FROM dbo.PageTranslations
		
*/

as

BEGIN

INSERT INTO [dbo].[PageTranslations]
           ([LanguageId]
           ,[Link]
           ,[Name]
           ,[CreatedBy]
           ,[IsActive])
     VALUES
           (@LanguageId
           ,@Link
           ,@Name
           ,@CreatedBy
           ,@IsActive)

SET @Id = SCOPE_IDENTITY()

END


GO
