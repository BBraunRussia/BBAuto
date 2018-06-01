CREATE TABLE [dbo].[Tabel_test](
	[driver_id] [int] NOT NULL,
	[tabel_date] [datetime] NOT NULL,
	[tabel_comment] [varchar](50) NULL,
 CONSTRAINT [PK_Tabel_test] PRIMARY KEY CLUSTERED 
(
	[driver_id] ASC,
	[tabel_date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
