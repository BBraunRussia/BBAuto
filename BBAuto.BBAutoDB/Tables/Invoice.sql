CREATE TABLE [dbo].[Invoice](
	[invoice_id] [int] IDENTITY(1,1) NOT NULL,
	[car_id] [int] NOT NULL,
	[invoice_number] [int] NOT NULL,
	[driver_id_From] [int] NOT NULL,
	[driver_id_To] [int] NOT NULL,
	[invoice_date] [datetime] NULL,
	[invoice_dateMove] [datetime] NULL,
	[region_id_From] [int] NULL,
	[region_id_To] [int] NULL,
	[invoice_file] [varchar](500) NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[invoice_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
