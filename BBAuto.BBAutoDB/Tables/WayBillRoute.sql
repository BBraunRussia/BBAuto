CREATE TABLE [dbo].[WayBillRoute](
	[wayBillRoute_id] [int] IDENTITY(1,1) NOT NULL,
	[wayBillDay_id] [int] NOT NULL,
	[myPoint1_id] [int] NOT NULL,
	[myPoint2_id] [int] NOT NULL,
	[wayBillRoute_distance] [int] NOT NULL,
 CONSTRAINT [PK_WayBillRoute] PRIMARY KEY CLUSTERED 
(
	[wayBillRoute_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
