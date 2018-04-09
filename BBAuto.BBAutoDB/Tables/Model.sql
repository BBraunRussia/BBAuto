CREATE TABLE [dbo].[Model](
	[model_id] [int] IDENTITY(1,1) NOT NULL,
	[model_name] [varchar](50) NOT NULL,
	[mark_id] [int] NOT NULL,
 CONSTRAINT [PK_Model] PRIMARY KEY CLUSTERED 
(
	[model_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Model]  WITH CHECK ADD  CONSTRAINT [FK_Model_Mark] FOREIGN KEY([mark_id])
REFERENCES [dbo].[Mark] ([mark_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Model] CHECK CONSTRAINT [FK_Model_Mark]
GO
