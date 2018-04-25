CREATE TABLE [dbo].[Grade](
	[grade_Id] [int] IDENTITY(1,1) NOT NULL,
	[grade_name] NVARCHAR(50) NOT NULL,
	[grade_epower] [int] NOT NULL,
	[grade_evol] [int] NOT NULL,
	[grade_maxLoad] [int] NOT NULL,
	[grade_noLoad] [int] NOT NULL,
	[engineType_id] [int] NOT NULL,
	[model_id] [int] NOT NULL,
 CONSTRAINT [PK_Grade] PRIMARY KEY CLUSTERED 
(
	[grade_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Grade]  WITH CHECK ADD  CONSTRAINT [FK_Grade_engineType] FOREIGN KEY([engineType_id])
REFERENCES [dbo].[engineType] ([engineType_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Grade] CHECK CONSTRAINT [FK_Grade_engineType]
GO

ALTER TABLE [dbo].[Grade]  WITH CHECK ADD  CONSTRAINT [FK_Grade_Model] FOREIGN KEY([model_id])
REFERENCES [dbo].[Model] ([model_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Grade] CHECK CONSTRAINT [FK_Grade_Model]
GO
