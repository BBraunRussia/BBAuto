CREATE TABLE [dbo].[CurrentStatusAfterDTP](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
 CONSTRAINT [PK_CurrentStatusAfterDTP] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
