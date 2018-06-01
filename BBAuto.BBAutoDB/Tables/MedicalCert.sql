CREATE TABLE [dbo].[MedicalCert](
	[MedicalCert_id] [int] IDENTITY(1,1) NOT NULL,
	[MedicalCert_number] [varchar](50) NOT NULL,
	[MedicalCert_dateBegin] [datetime] NOT NULL,
	[MedicalCert_dateEnd] [datetime] NOT NULL,
	[driver_id] [int] NOT NULL,
	[MedicalCert_file] [varchar](500) NULL,
	[MedicalCert_notificationSent] [int] NULL,
 CONSTRAINT [PK_MedicalCert] PRIMARY KEY CLUSTERED 
(
	[MedicalCert_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MedicalCert]  WITH CHECK ADD  CONSTRAINT [FK_MedicalCert_Driver] FOREIGN KEY([driver_id])
REFERENCES [dbo].[Driver] ([driver_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[MedicalCert] CHECK CONSTRAINT [FK_MedicalCert_Driver]
GO
