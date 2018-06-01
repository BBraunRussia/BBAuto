CREATE TABLE [dbo].[Repair](
	[repair_id] [int] IDENTITY(1,1) NOT NULL,
	[car_id] [int] NOT NULL,
	[repairType_id] [int] NOT NULL,
	[ServiceStantion_id] [int] NOT NULL,
	[repair_date] [datetime] NOT NULL,
	[repair_cost] [float] NOT NULL,
	[repair_file] [varchar](200) NULL,
 CONSTRAINT [PK_Repair] PRIMARY KEY CLUSTERED 
(
	[repair_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
