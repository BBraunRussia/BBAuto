CREATE TABLE [dbo].[FuelCardDriver](
	[FuelCardDriver_id] [int] IDENTITY(1,1) NOT NULL,
	[FuelCard_id] [int] NOT NULL,
	[driver_id] [int] NOT NULL,
	[FuelCardDriver_dateBegin] [datetime] NOT NULL,
	[FuelCardDriver_dateEnd] [datetime] NULL,
 CONSTRAINT [PK_FuelCardDriver] PRIMARY KEY CLUSTERED 
(
	[FuelCardDriver_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
