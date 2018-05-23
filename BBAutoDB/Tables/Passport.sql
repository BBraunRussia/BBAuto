CREATE TABLE [dbo].[Passport](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DriverId] [int] NOT NULL,
	[FirstName] NVARCHAR(50) NOT NULL,
	[LastName] NVARCHAR(50) NOT NULL,
	[SecondName] NVARCHAR(50) NOT NULL,
	[Number] NVARCHAR(12) NOT NULL,
	[GiveOrg] NVARCHAR(200) NOT NULL,
	[GiveDate] [datetime] NOT NULL,
	[Address] NVARCHAR(200) NOT NULL,
	[File] NVARCHAR(100) NULL,
 CONSTRAINT [PK_Passport] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Passport]  WITH CHECK ADD  CONSTRAINT [FK_Passport_Driver] FOREIGN KEY([DriverId])
REFERENCES [dbo].[Driver] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Passport] CHECK CONSTRAINT [FK_Passport_Driver]
GO
