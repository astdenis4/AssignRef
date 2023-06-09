USE [AssignRef]
GO
/****** Object:  StoredProcedure [dbo].[PageSection_Update]    Script Date: 6/7/2023 3:30:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Alicia St. Denis>
-- Create date: <5/08/2023>
-- Description: <Update proc for PageSection>
-- Code Reviewer:William Kuheleloa

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[PageSection_Update]

@Id int
,@PageTranslationId int 
,@Name nvarchar(50)
,@Component nvarchar(50)
,@IsActive bit

/*

Declare @Id int = 1;

Declare @PageTranslationId int = 14
,@Name nvarchar(50) = 'Soccer'
,@Component nvarchar(50) = 'Soccer G'
,@IsActive bit = 1

Select * 
From dbo.PageSection
Where Id = @Id

Execute dbo.PageSection_Update

@Id 
,@PageTranslationId 
,@Name 
,@Component 
,@IsActive 

select * 
FROM dbo.PageSection
Where Id = @Id

*/

as

BEGIN 

UPDATE [dbo].[PageSection]
   SET 
      [PageTranslationId] = @PageTranslationId
      ,[Name] = @Name
      ,[Component] = @Component
      ,[IsActive] = @IsActive
 WHERE Id = @Id

END
GO
