CREATE TABLE [dbo].[Policy](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CarId] [int] NOT NULL,
	[PolicyTypeId] [int] NOT NULL,
	[OwnerId] [int] NOT NULL,
	[CompId] [int] NOT NULL,
	[Number] NVARCHAR(50) NOT NULL,
	[DateBegin] [datetime] NOT NULL,
	[DateEnd] [datetime] NOT NULL,
	[Pay1] [float] NULL,
	[LimitCost] [float] NULL,
	[Pay2] [float] NULL,
	[Pay2Date] [datetime] NULL,
	[File] NVARCHAR(100) NULL,
	[AccountId] [int] NULL,
	[AccountId2] [int] NULL,
	[NotificationSent] BIT NULL,
	[Comment] NVARCHAR(100) NULL,
	[DateCreate] [datetime] NOT NULL,
 CONSTRAINT [PK_Kasko] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Policy]  WITH CHECK ADD  CONSTRAINT [FK_Kasko_Car] FOREIGN KEY([CarId])
REFERENCES [dbo].[Car] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Policy] CHECK CONSTRAINT [FK_Kasko_Car]
GO

ALTER TABLE [dbo].[Policy]  WITH CHECK ADD  CONSTRAINT [FK_Kasko_Comp] FOREIGN KEY([CompId])
REFERENCES [dbo].[Comp] ([Id])
GO

ALTER TABLE [dbo].[Policy] CHECK CONSTRAINT [FK_Kasko_Comp]
GO

ALTER TABLE [dbo].[Policy]  WITH CHECK ADD  CONSTRAINT [FK_Kasko_Owner] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[Owner] ([Id])
GO

ALTER TABLE [dbo].[Policy] CHECK CONSTRAINT [FK_Kasko_Owner]
GO

ALTER TABLE [dbo].[Policy]  WITH CHECK ADD  CONSTRAINT [FK_Policy_policyType] FOREIGN KEY([PolicyTypeId])
REFERENCES [dbo].[policyType] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Policy] CHECK CONSTRAINT [FK_Policy_policyType]
GO
