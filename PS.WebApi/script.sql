USE [ApiRestPostDb]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 08/03/2015 12:01:05 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Posts](
	[Text] [varchar](max) NOT NULL,
	[WordsThatEndWithN] [int] NOT NULL,
	[SentencesWithMoreThan15Words] [int] NOT NULL,
	[Paragraphs] [int] NOT NULL,
	[CharactersDifferensToN] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
