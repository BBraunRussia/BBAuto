CREATE TABLE [dbo].[Violation](
	[violation_id] [int] IDENTITY(1,1) NOT NULL,
	[car_id] [int] NOT NULL,
	[violation_date] [datetime] NOT NULL,
	[violation_number] [varchar](50) NOT NULL,
	[violation_file] [varchar](200) NULL,
	[violation_datePay] [datetime] NULL,
	[violation_filePay] [varchar](200) NULL,
	[violationType_id] [int] NULL,
	[violation_sum] [int] NULL,
	[violation_sent] [int] NULL,
	[violation_noDeduction] [int] NULL,
	[violation_agreed] [varchar](5) NULL,
	[violation_dateCreate] [datetime] NOT NULL,
 CONSTRAINT [PK_Violation] PRIMARY KEY CLUSTERED 
(
	[violation_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Violation]  WITH CHECK ADD  CONSTRAINT [FK_Violation_Car] FOREIGN KEY([car_id])
REFERENCES [dbo].[Car] ([car_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Violation] CHECK CONSTRAINT [FK_Violation_Car]
GO
