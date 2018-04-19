CREATE TABLE [dbo].[ViolationType](
	[violationType_id] [int] IDENTITY(1,1) NOT NULL,
	[violationType_name] NVARCHAR(50) NOT NULL,
 CONSTRAINT [PK_violationType] PRIMARY KEY CLUSTERED 
(
	[violationType_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
