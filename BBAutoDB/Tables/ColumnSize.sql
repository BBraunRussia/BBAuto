CREATE TABLE [dbo].[ColumnSize](
	[DriverId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[column0] [int] NOT NULL,
	[column1] [int] NOT NULL,
	[column2] [int] NOT NULL,
	[column3] [int] NOT NULL,
	[column4] [int] NOT NULL,
	[column5] [int] NOT NULL,
	[column6] [int] NOT NULL,
	[column7] [int] NOT NULL,
	[column8] [int] NOT NULL,
	[column9] [int] NOT NULL,
	[column10] [int] NOT NULL,
	[column11] [int] NOT NULL,
	[column12] [int] NOT NULL,
	[column13] [int] NOT NULL,
	[column14] [int] NOT NULL,
	[column15] [int] NOT NULL,
	[column16] [int] NOT NULL,
 CONSTRAINT [PK_ColumnSize] PRIMARY KEY CLUSTERED 
(
	[DriverId] ASC,
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
