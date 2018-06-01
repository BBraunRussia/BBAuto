CREATE TABLE [dbo].[Car](
	[car_id] [int] IDENTITY(1,1) NOT NULL,
	[car_bbnumber] [int] NOT NULL,
	[car_grz] [varchar](50) NULL,
	[car_vin] [varchar](17) NULL,
	[car_year] [int] NULL,
	[car_enumber] [varchar](50) NULL,
	[car_bodynumber] [varchar](50) NULL,
	[pts_id] [int] NULL,
	[sts_id] [int] NULL,
	[grade_id] [int] NULL,
	[color_id] [int] NULL,
	[car_lisingDate] [datetime] NULL,
	[car_invertoryNumber] [varchar](50) NULL,
 CONSTRAINT [PK_Car] PRIMARY KEY CLUSTERED 
(
	[car_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Car]  WITH CHECK ADD  CONSTRAINT [FK_Car_Color] FOREIGN KEY([color_id])
REFERENCES [dbo].[Color] ([color_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Car] CHECK CONSTRAINT [FK_Car_Color]
GO

ALTER TABLE [dbo].[Car]  WITH CHECK ADD  CONSTRAINT [FK_Car_Grade] FOREIGN KEY([grade_id])
REFERENCES [dbo].[Grade] ([grade_id])
GO

ALTER TABLE [dbo].[Car] CHECK CONSTRAINT [FK_Car_Grade]
GO
