CREATE TABLE [dbo].[Fuel](
	[fuel_id] [int] IDENTITY(1,1) NOT NULL,
	[fuelCard_id] [int] NOT NULL,
	[fuel_date] [datetime] NOT NULL,
	[fuel_count] [float] NOT NULL,
	[engineType_id] [int] NOT NULL,
 CONSTRAINT [PK_Fuel] PRIMARY KEY CLUSTERED 
(
	[fuel_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
