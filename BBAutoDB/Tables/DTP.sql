CREATE TABLE [dbo].[DTP](
	[dtp_id] [int] IDENTITY(1,1) NOT NULL,
	[car_id] [int] NOT NULL,
	[dtp_number] [int] NOT NULL,
	[dtp_date] [datetime] NULL,
	[region_id] [int] NOT NULL,
	[dtp_dateCallInsure] [datetime] NULL,
	[culprit_id] [int] NULL,
	[StatusAfterDTP_id] [int] NOT NULL,
	[dtp_numberLoss] NVARCHAR(50) NULL,
	[dtp_sum] [float] NULL,
	[dtp_damage] NVARCHAR(300) NULL,
	[dtp_facts] NVARCHAR(500) NULL,
	[dtp_comm] NVARCHAR(100) NULL,
	[CurrentStatusAfterDTP_id] [int] NULL,
 CONSTRAINT [PK_dtp] PRIMARY KEY CLUSTERED 
(
	[dtp_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DTP]  WITH CHECK ADD  CONSTRAINT [FK_dtp_Car] FOREIGN KEY([car_id])
REFERENCES [dbo].[Car] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[DTP] CHECK CONSTRAINT [FK_dtp_Car]
GO

ALTER TABLE [dbo].[DTP]  WITH CHECK ADD  CONSTRAINT [FK_DTP_Culprit] FOREIGN KEY([culprit_id])
REFERENCES [dbo].[Culprit] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[DTP] CHECK CONSTRAINT [FK_DTP_Culprit]
GO

ALTER TABLE [dbo].[DTP]  WITH CHECK ADD  CONSTRAINT [FK_dtp_Region] FOREIGN KEY([region_id])
REFERENCES [dbo].[Region] ([Id])
GO

ALTER TABLE [dbo].[DTP] CHECK CONSTRAINT [FK_dtp_Region]
GO

ALTER TABLE [dbo].[DTP]  WITH CHECK ADD  CONSTRAINT [FK_dtp_StatusAfterDTP] FOREIGN KEY([StatusAfterDTP_id])
REFERENCES [dbo].[StatusAfterDTP] ([Id])
GO

ALTER TABLE [dbo].[DTP] CHECK CONSTRAINT [FK_dtp_StatusAfterDTP]
GO
