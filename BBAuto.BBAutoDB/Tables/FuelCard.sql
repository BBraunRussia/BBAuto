CREATE TABLE [dbo].[FuelCard](
	[FuelCard_id] [int] IDENTITY(1,1) NOT NULL,
	[FuelCardType_id] [int] NOT NULL,
	[FuelCard_number] [varchar](50) NOT NULL,
	[FuelCard_dateEnd] [datetime] NULL,
	[region_id] [int] NOT NULL,
	[FuelCard_pin] [varchar](4) NOT NULL,
	[FuelCard_lost] [int] NOT NULL,
	[FuelCard_comment] [varchar](100) NOT NULL,
 CONSTRAINT [PK_FuelCard] PRIMARY KEY CLUSTERED 
(
	[FuelCard_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
