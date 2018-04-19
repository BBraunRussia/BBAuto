CREATE TABLE [dbo].[dtpFile](
	[dtpFile_id] [int] IDENTITY(1,1) NOT NULL,
	[dtp_id] [int] NOT NULL,
	[dtpFile_name] NVARCHAR(100) NULL,
	[dtpFile_file] NVARCHAR(300) NOT NULL,
 CONSTRAINT [PK_dtpFile] PRIMARY KEY CLUSTERED 
(
	[dtpFile_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[dtpFile]  WITH CHECK ADD  CONSTRAINT [FK_dtpFile_DTP] FOREIGN KEY([dtp_id])
REFERENCES [dbo].[DTP] ([dtp_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[dtpFile] CHECK CONSTRAINT [FK_dtpFile_DTP]
GO
