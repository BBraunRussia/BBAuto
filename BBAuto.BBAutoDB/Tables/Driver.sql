CREATE TABLE [dbo].[Driver](
	[driver_id] [int] IDENTITY(1,1) NOT NULL,
	[driver_fio] [varchar](100) NOT NULL,
	[region_id] [int] NOT NULL,
	[driver_dateBirth] [datetime] NULL,
	[driver_mobile] [varchar](10) NULL,
	[driver_email] [varchar](100) NULL,
	[driver_fired] [int] NULL,
	[driver_expSince] [int] NULL,
	[position_id] [int] NOT NULL,
	[dept_id] [int] NULL,
	[driver_login] [varchar](8) NULL,
	[owner_id] [int] NULL,
	[driver_suppyAddress] [varchar](500) NULL,
	[driver_sex] [int] NULL,
	[driver_decret] [int] NULL,
	[driver_dateStopNotification] [datetime] NULL,
	[driver_number] [varchar](50) NULL,
	[driver_isDriver] [int] NULL,
	[driver_from1C] [int] NULL,
 CONSTRAINT [PK_Driver] PRIMARY KEY CLUSTERED 
(
	[driver_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Driver]  WITH CHECK ADD  CONSTRAINT [FK_Driver_Position] FOREIGN KEY([position_id])
REFERENCES [dbo].[Position] ([position_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Driver] CHECK CONSTRAINT [FK_Driver_Position]
GO

ALTER TABLE [dbo].[Driver]  WITH CHECK ADD  CONSTRAINT [FK_Driver_Region] FOREIGN KEY([region_id])
REFERENCES [dbo].[Region] ([region_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Driver] CHECK CONSTRAINT [FK_Driver_Region]
GO
