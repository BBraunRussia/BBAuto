CREATE TABLE [dbo].[diagCard](
	[diagCard_id] [int] IDENTITY(1,1) NOT NULL,
	[car_id] [int] NOT NULL,
	[diagCard_number] [varchar](50) NOT NULL,
	[diagCard_date] [datetime] NOT NULL,
	[diagCard_file] [varchar](200) NULL,
	[diagCard_notificationSent] [int] NULL,
 CONSTRAINT [PK_diagCard] PRIMARY KEY CLUSTERED 
(
	[diagCard_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[diagCard]  WITH CHECK ADD  CONSTRAINT [FK_diagCard_Car] FOREIGN KEY([car_id])
REFERENCES [dbo].[Car] ([car_id])
GO

ALTER TABLE [dbo].[diagCard] CHECK CONSTRAINT [FK_diagCard_Car]
GO
