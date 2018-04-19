CREATE TABLE [dbo].[carDoc](
	[carDoc_id] [int] IDENTITY(1,1) NOT NULL,
	[car_id] [int] NOT NULL,
	[carDoc_name] NVARCHAR(50) NOT NULL,
	[carDoc_file] NVARCHAR(200) NOT NULL,
 CONSTRAINT [PK_carDoc] PRIMARY KEY CLUSTERED 
(
	[carDoc_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
