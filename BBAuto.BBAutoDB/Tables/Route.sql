CREATE TABLE [dbo].[Route](
	[route_id] [int] IDENTITY(1,1) NOT NULL,
	[mypoint1_id] [int] NOT NULL,
	[mypoint2_id] [int] NOT NULL,
	[route_distance] [int] NOT NULL,
 CONSTRAINT [PK_Route] PRIMARY KEY CLUSTERED 
(
	[route_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Route]  WITH CHECK ADD  CONSTRAINT [FK_Route_Point] FOREIGN KEY([mypoint1_id])
REFERENCES [dbo].[MyPoint] ([mypoint_id])
GO

ALTER TABLE [dbo].[Route] CHECK CONSTRAINT [FK_Route_Point]
GO

ALTER TABLE [dbo].[Route]  WITH CHECK ADD  CONSTRAINT [FK_Route_Point1] FOREIGN KEY([mypoint2_id])
REFERENCES [dbo].[MyPoint] ([mypoint_id])
GO

ALTER TABLE [dbo].[Route] CHECK CONSTRAINT [FK_Route_Point1]
GO
