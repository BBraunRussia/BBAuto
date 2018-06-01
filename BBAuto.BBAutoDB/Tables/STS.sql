CREATE TABLE [dbo].[STS](
	[car_id] [int] NOT NULL,
	[sts_number] [varchar](50) NOT NULL,
	[sts_date] [datetime] NOT NULL,
	[sts_giveOrg] [varchar](100) NULL,
	[sts_file] [varchar](200) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[STS]  WITH CHECK ADD  CONSTRAINT [FK_STS_Car] FOREIGN KEY([car_id])
REFERENCES [dbo].[Car] ([car_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[STS] CHECK CONSTRAINT [FK_STS_Car]
GO
