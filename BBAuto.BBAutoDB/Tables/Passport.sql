CREATE TABLE [dbo].[Passport](
	[passport_id] [int] IDENTITY(1,1) NOT NULL,
	[driver_id] [int] NOT NULL,
	[passport_firstName] [varchar](50) NOT NULL,
	[passport_lastName] [varchar](50) NOT NULL,
	[passport_secondName] [varchar](50) NOT NULL,
	[passport_number] [varchar](12) NOT NULL,
	[passport_GiveOrg] [varchar](200) NOT NULL,
	[passport_GiveDate] [datetime] NOT NULL,
	[passport_address] [varchar](200) NOT NULL,
	[passport_file] [varchar](100) NULL,
 CONSTRAINT [PK_Passport] PRIMARY KEY CLUSTERED 
(
	[passport_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Passport]  WITH CHECK ADD  CONSTRAINT [FK_Passport_Driver] FOREIGN KEY([driver_id])
REFERENCES [dbo].[Driver] ([driver_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Passport] CHECK CONSTRAINT [FK_Passport_Driver]
GO
