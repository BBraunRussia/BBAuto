CREATE TABLE [dbo].[Proxy](
	[proxy_id] [int] IDENTITY(1,1) NOT NULL,
	[proxy_number] NVARCHAR(50) NOT NULL,
	[proxy_date] [datetime] NOT NULL,
	[proxyType_id] [int] NOT NULL,
	[driver_id] [int] NOT NULL,
	[car_id] [int] NOT NULL,
 CONSTRAINT [PK_Proxy] PRIMARY KEY CLUSTERED 
(
	[proxy_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Proxy]  WITH CHECK ADD  CONSTRAINT [FK_Proxy_Car] FOREIGN KEY([car_id])
REFERENCES [dbo].[Car] ([Id])
GO

ALTER TABLE [dbo].[Proxy] CHECK CONSTRAINT [FK_Proxy_Car]
GO

ALTER TABLE [dbo].[Proxy]  WITH CHECK ADD  CONSTRAINT [FK_Proxy_Driver] FOREIGN KEY([driver_id])
REFERENCES [dbo].[Driver] ([Id])
GO

ALTER TABLE [dbo].[Proxy] CHECK CONSTRAINT [FK_Proxy_Driver]
GO

ALTER TABLE [dbo].[Proxy]  WITH CHECK ADD  CONSTRAINT [FK_Proxy_proxyType] FOREIGN KEY([proxyType_id])
REFERENCES [dbo].[proxyType] ([proxyType_id])
GO

ALTER TABLE [dbo].[Proxy] CHECK CONSTRAINT [FK_Proxy_proxyType]
GO
