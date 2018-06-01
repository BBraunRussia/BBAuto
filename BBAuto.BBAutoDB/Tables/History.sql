CREATE TABLE [dbo].[History](
	[his_id] [int] IDENTITY(1,1) NOT NULL,
	[his_comment] [varchar](50) NOT NULL,
	[id] [int] NOT NULL,
	[his_date] [datetime] NOT NULL,
	[his_event] [varchar](50) NOT NULL,
	[his_file] [varchar](max) NULL,
 CONSTRAINT [PK_History] PRIMARY KEY CLUSTERED 
(
	[his_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
