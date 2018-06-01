CREATE TABLE [dbo].[ShipPart](
	[shipPart_id] [int] IDENTITY(1,1) NOT NULL,
	[car_id] [int] NOT NULL,
	[driver_id] [int] NOT NULL,
	[shipPart_name] [varchar](50) NOT NULL,
	[shipPart_dateRequest] [datetime] NULL,
	[shipPart_dateSent] [datetime] NULL,
	[shipPart_file] [varchar](500) NULL,
 CONSTRAINT [PK_ShipParts] PRIMARY KEY CLUSTERED 
(
	[shipPart_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
