CREATE TABLE [dbo].[CarBuy](
	[car_id] [int] NOT NULL,
	[owner_id] [int] NULL,
	[region_id_buy] [int] NULL,
	[region_id_using] [int] NULL,
	[driver_id] [int] NULL,
	[carBuy_dateOrder] [datetime] NULL,
	[carBuy_isGet] [int] NOT NULL,
	[carBuy_dateGet] [datetime] NULL,
	[carBuy_cost] [float] NULL,
	[carBuy_dop] NVARCHAR(100) NULL,
	[carBuy_events] NVARCHAR(500) NULL,
	[dealerId] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CarBuy]  WITH CHECK ADD  CONSTRAINT [FK_CarBuy_Car] FOREIGN KEY([car_id])
REFERENCES [dbo].[Car] ([Id])
GO

ALTER TABLE [dbo].[CarBuy] CHECK CONSTRAINT [FK_CarBuy_Car]
GO
