USE [AssignRef]
GO
/****** Object:  StoredProcedure [dbo].[PageSection_SelectById]    Script Date: 6/7/2023 3:30:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Alicia St. Denis>
-- Create date: <5/08/2023>
-- Description: <Select By Id proc for PageSection>
-- Code Reviewer: William Kuheleloa

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================



CREATE proc [dbo].[PageSection_SelectById]

   @Id int

/*

Declare @Id int = 1;

Execute dbo.PageSection_SelectById @Id

*/

as

Begin

SELECT [Id]
      ,[PageTranslationId]
      ,[Name]
      ,[Component]
      ,[IsActive]
  FROM [dbo].[PageSection]
  Where Id = @Id 

End 
GO
