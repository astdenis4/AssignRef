USE [AssignRef]
GO
/****** Object:  Table [dbo].[PageSection]    Script Date: 6/7/2023 3:30:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageSection](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PageTranslationId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Component] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_PageSection] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PageSection] ADD  CONSTRAINT [DF_PageSection_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[PageSection]  WITH CHECK ADD  CONSTRAINT [FK_PageSection_PageTranslations] FOREIGN KEY([PageTranslationId])
REFERENCES [dbo].[PageTranslations] ([Id])
GO
ALTER TABLE [dbo].[PageSection] CHECK CONSTRAINT [FK_PageSection_PageTranslations]
GO
