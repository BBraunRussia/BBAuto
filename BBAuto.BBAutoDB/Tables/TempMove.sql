CREATE TABLE [dbo].[TempMove](
	[tempMove_id] [int] IDENTITY(1,1) NOT NULL,
	[car_id] [int] NOT NULL,
	[driver_id] [int] NOT NULL,
	[tempMove_dateBegin] [datetime] NOT NULL,
	[tempMove_dateEnd] [datetime] NOT NULL,
 CONSTRAINT [PK_TempMove] PRIMARY KEY CLUSTERED 
(
	[tempMove_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
