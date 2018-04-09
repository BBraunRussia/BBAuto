CREATE TABLE [dbo].[proxyType](
	[proxyType_id] [int] IDENTITY(1,1) NOT NULL,
	[proxyType_name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_proxyType] PRIMARY KEY CLUSTERED 
(
	[proxyType_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
