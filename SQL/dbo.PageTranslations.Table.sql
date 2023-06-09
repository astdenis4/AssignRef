USE [AssignRef]
GO
/****** Object:  Table [dbo].[PageTranslations]    Script Date: 6/7/2023 3:30:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageTranslations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Link] [nvarchar](255) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_PageTranslations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PageTranslations] ADD  CONSTRAINT [DF_PageTranslations_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[PageTranslations] ADD  CONSTRAINT [DF_PageTranslations_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[PageTranslations] ADD  CONSTRAINT [DF_PageTranslations_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[PageTranslations]  WITH CHECK ADD  CONSTRAINT [FK_PageTranslations_Languages] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[PageTranslations] CHECK CONSTRAINT [FK_PageTranslations_Languages]
GO
