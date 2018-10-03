USE [dbname]
GO

/****** Object:  Index [IX_RatingEnabled]    Script Date: 8/17/2017 14:32:38 ******/
DROP INDEX [IX_RatingEnabled] ON [dbo].[vts_tbQuestion]
GO


ALTER TABLE vts_tbquestion
ALTER COLUMN questiontext nvarchar(max);


/****** Object:  Index [IX_RatingEnabled]    Script Date: 8/17/2017 14:32:38 ******/
CREATE NONCLUSTERED INDEX [IX_RatingEnabled] ON [dbo].[vts_tbQuestion]
(
	[RatingEnabled] ASC
)
INCLUDE ( 	[QuestionId],
	[QuestionText],
	[DisplayOrder],
	[QuestionIdText],
	[Alias],
	[QuestionGroupID]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
