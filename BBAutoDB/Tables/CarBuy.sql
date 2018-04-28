CREATE TABLE [dbo].[CarBuy](
	[CarId] [int] NOT NULL,
	[OwnerId] [int] NULL,
	[RegionIdBuy] [int] NULL,
	[RegionIdUsing] [int] NULL,
	[DriverId] [int] NULL,
	[DateOrder] [datetime] NULL,
	[IsGet] BIT NOT NULL,
	[DateGet] [datetime] NULL,
	[Cost] DECIMAL NULL,
	[Dop] NVARCHAR(100) NULL,
	[Events] NVARCHAR(500) NULL,
	[DealerId] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CarBuy]  WITH CHECK ADD  CONSTRAINT [FK_CarBuy_Car] FOREIGN KEY([CarId])
REFERENCES [dbo].[Car] ([Id])
GO

ALTER TABLE [dbo].[CarBuy] CHECK CONSTRAINT [FK_CarBuy_Car]
GO
