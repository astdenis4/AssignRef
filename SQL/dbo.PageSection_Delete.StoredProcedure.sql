USE [AssignRef]
GO
/****** Object:  StoredProcedure [dbo].[PageSection_Delete]    Script Date: 6/7/2023 3:30:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: <Alicia St. Denis>
-- Create date: <5/08/2023>
-- Description: <Delete proc for PageSection>
-- Code Reviewer:William Kuheleloa

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer: 
-- Note:
-- =============================================

CREATE proc [dbo].[PageSection_Delete]

	@Id int

as

/*

Declare @Id int = 1 

Select * 
From dbo.PageSection
Where Id = @Id

Execute [dbo].[PageSection_Delete] @Id

Select * 
From dbo.PageSection 
Where Id = @Id

*/

BEGIN 

UPDATE [dbo].[PageSection]
   SET [IsActive] = 0
 WHERE Id = @Id 

END
GO
