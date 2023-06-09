USE [AssignRef]
GO
/****** Object:  StoredProcedure [dbo].[PageTranslations_Delete]    Script Date: 6/7/2023 3:30:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Alicia St. Denis>
-- Create date: <5/08/2023>
-- Description: <Delete proc for PageTranslations>
-- Code Reviewer:William Kuheleloa

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================





CREATE proc [dbo].[PageTranslations_Delete]

	@Id int

as 

/*

Declare @Id int = 1

Select *
From dbo.PageTranslations
Where Id = @Id; 

Execute dbo.PageTranslations_Delete @Id

Select * 
From dbo.PageTranslations
Where Id = @Id 

*/

BEGIN

UPDATE [dbo].[PageTranslations]
   SET [IsActive] = 0
 WHERE Id = @Id 

END
GO
