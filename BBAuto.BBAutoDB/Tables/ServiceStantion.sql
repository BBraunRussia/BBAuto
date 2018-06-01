CREATE TABLE [dbo].[ServiceStantion](
	[ServiceStantion_id] [int] IDENTITY(1,1) NOT NULL,
	[ServiceStantion_name] [varchar](200) NOT NULL,
 CONSTRAINT [PK_ServiceStantion] PRIMARY KEY CLUSTERED 
(
	[ServiceStantion_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
