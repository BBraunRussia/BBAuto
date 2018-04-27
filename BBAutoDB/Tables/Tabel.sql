CREATE TABLE [dbo].[Tabel](
	[driver_id] [int] NOT NULL,
	[tabel_date] [datetime] NOT NULL,
	[tabel_comment] NVARCHAR(50) NULL,
 CONSTRAINT [PK_Tabel_1] PRIMARY KEY CLUSTERED 
(
	[driver_id] ASC,
	[tabel_date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Tabel]  WITH CHECK ADD  CONSTRAINT [FK_Tabel_Driver] FOREIGN KEY([driver_id])
REFERENCES [dbo].[Driver] ([Id])
GO

ALTER TABLE [dbo].[Tabel] CHECK CONSTRAINT [FK_Tabel_Driver]
GO
