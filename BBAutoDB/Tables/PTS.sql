CREATE TABLE [dbo].[PTS](
	[car_id] [int] NOT NULL,
	[pts_number] NVARCHAR(50) NOT NULL,
	[pts_date] [datetime] NOT NULL,
	[pts_giveOrg] NVARCHAR(100) NULL,
	[pts_file] NVARCHAR(200) NULL,
 CONSTRAINT [PK_PTS] PRIMARY KEY CLUSTERED 
(
	[car_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PTS]  WITH CHECK ADD  CONSTRAINT [FK_PTS_Car] FOREIGN KEY([car_id])
REFERENCES [dbo].[Car] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PTS] CHECK CONSTRAINT [FK_PTS_Car]
GO
