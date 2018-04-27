CREATE TABLE [dbo].[DriverLicense](
	[DriverLicense_id] [int] IDENTITY(1,1) NOT NULL,
	[DriverLicense_number] NVARCHAR(50) NOT NULL,
	[DriverLicense_dateBegin] [datetime] NULL,
	[DriverLicense_dateEnd] [datetime] NULL,
	[driver_id] [int] NOT NULL,
	[DriverLicense_file] NVARCHAR(100) NULL,
	[DriverLicense_notificationSent] [int] NULL,
 CONSTRAINT [PK_License] PRIMARY KEY CLUSTERED 
(
	[DriverLicense_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DriverLicense]  WITH CHECK ADD  CONSTRAINT [FK_License_Driver] FOREIGN KEY([driver_id])
REFERENCES [dbo].[Driver] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[DriverLicense] CHECK CONSTRAINT [FK_License_Driver]
GO
