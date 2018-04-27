CREATE TABLE [dbo].[Instraction](
	[Instraction_id] [int] IDENTITY(1,1) NOT NULL,
	[Instraction_number] NVARCHAR(50) NOT NULL,
	[Instraction_date] [datetime] NOT NULL,
	[driver_id] [int] NOT NULL,
	[instraction_file] NVARCHAR(100) NULL,
 CONSTRAINT [PK_Instraction] PRIMARY KEY CLUSTERED 
(
	[Instraction_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Instraction]  WITH CHECK ADD  CONSTRAINT [FK_Instraction_Driver] FOREIGN KEY([driver_id])
REFERENCES [dbo].[Driver] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Instraction] CHECK CONSTRAINT [FK_Instraction_Driver]
GO
