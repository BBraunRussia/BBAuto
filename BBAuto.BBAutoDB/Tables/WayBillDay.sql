CREATE TABLE [dbo].[WayBillDay](
	[wayBillDay_id] [int] IDENTITY(1,1) NOT NULL,
	[car_id] [int] NOT NULL,
	[driver_id] [int] NOT NULL,
	[wayBillDay_date] [datetime] NOT NULL,
 CONSTRAINT [PK_WayBillDay] PRIMARY KEY CLUSTERED 
(
	[wayBillDay_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
