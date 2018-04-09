CREATE TABLE [dbo].[engineType](
	[engineType_id] [int] IDENTITY(1,1) NOT NULL,
	[engineType_name] [varchar](50) NOT NULL,
	[engineType_shortName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_engineType] PRIMARY KEY CLUSTERED 
(
	[engineType_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
