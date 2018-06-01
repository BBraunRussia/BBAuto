CREATE TABLE [dbo].[Policy](
	[policy_id] [int] IDENTITY(1,1) NOT NULL,
	[car_id] [int] NOT NULL,
	[policyType_id] [int] NOT NULL,
	[owner_id] [int] NOT NULL,
	[comp_id] [int] NOT NULL,
	[policy_number] [varchar](50) NOT NULL,
	[policy_dateBegin] [datetime] NOT NULL,
	[policy_dateEnd] [datetime] NOT NULL,
	[policy_pay1] [float] NULL,
	[policy_limitCost] [float] NULL,
	[policy_pay2] [float] NULL,
	[policy_pay2Date] [datetime] NULL,
	[policy_file] [varchar](100) NULL,
	[account_id] [int] NULL,
	[account_id2] [int] NULL,
	[policy_notificationSent] [int] NULL,
	[policy_comment] [varchar](100) NULL,
	[policy_dateCreate] [datetime] NOT NULL,
 CONSTRAINT [PK_Kasko] PRIMARY KEY CLUSTERED 
(
	[policy_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Policy]  WITH CHECK ADD  CONSTRAINT [FK_Kasko_Car] FOREIGN KEY([car_id])
REFERENCES [dbo].[Car] ([car_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Policy] CHECK CONSTRAINT [FK_Kasko_Car]
GO

ALTER TABLE [dbo].[Policy]  WITH CHECK ADD  CONSTRAINT [FK_Kasko_Comp] FOREIGN KEY([comp_id])
REFERENCES [dbo].[Comp] ([comp_id])
GO

ALTER TABLE [dbo].[Policy] CHECK CONSTRAINT [FK_Kasko_Comp]
GO

ALTER TABLE [dbo].[Policy]  WITH CHECK ADD  CONSTRAINT [FK_Kasko_Owner] FOREIGN KEY([owner_id])
REFERENCES [dbo].[Owner] ([owner_id])
GO

ALTER TABLE [dbo].[Policy] CHECK CONSTRAINT [FK_Kasko_Owner]
GO

ALTER TABLE [dbo].[Policy]  WITH CHECK ADD  CONSTRAINT [FK_Policy_policyType] FOREIGN KEY([policyType_id])
REFERENCES [dbo].[policyType] ([policyType_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Policy] CHECK CONSTRAINT [FK_Policy_policyType]
GO
