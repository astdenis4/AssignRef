USE [AssignRef]
GO
/****** Object:  StoredProcedure [dbo].[PageSection_Insert]    Script Date: 6/7/2023 3:30:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Alicia St. Denis>
-- Create date: <5/08/2023>
-- Description: <Insert proc for PageSection>
-- Code Reviewer: William Kuheleloa

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer: 
-- Note:
-- =============================================


CREATE proc [dbo].[PageSection_Insert]

@PageTranslationId int
,@Name nvarchar(50)
,@Component nvarchar(50)
,@IsActive bit
,@Id int OUTPUT

/*

Declare @Id int = 0

Declare @PageTranslationId int = 14
,@Name nvarchar(50) = 'Boxing'
,@Component nvarchar(50) = 'Boxing' 
,@IsActive bit = 1

Execute dbo.PageSection_Insert

@PageTranslationId 
,@Name 
,@Component 
,@IsActive 
,@Id OUTPUT

*/

as

Begin 

INSERT INTO [dbo].[PageSection]
           (
           [PageTranslationId]
           ,[Name]
           ,[Component]
           ,[IsActive])
     VALUES
           (
           @PageTranslationId
           ,@Name
           ,@Component
           ,@IsActive
		   )
End
GO
