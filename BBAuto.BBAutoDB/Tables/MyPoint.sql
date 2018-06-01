CREATE TABLE [dbo].[MyPoint](
	[mypoint_id] [int] IDENTITY(1,1) NOT NULL,
	[region_id] [int] NOT NULL,
	[mypoint_name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Point] PRIMARY KEY CLUSTERED 
(
	[mypoint_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MyPoint]  WITH CHECK ADD  CONSTRAINT [FK_Region] FOREIGN KEY([region_id])
REFERENCES [dbo].[Region] ([region_id])
GO

ALTER TABLE [dbo].[MyPoint] CHECK CONSTRAINT [FK_Region]
GO
