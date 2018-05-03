CREATE TABLE [dbo].[Account](
	[account_id] [int] IDENTITY(1,1) NOT NULL,
	[account_number] NVARCHAR(50) NOT NULL,
	[account_agreed] [int] NOT NULL,
	[policyType_id] [int] NOT NULL,
	[owner_id] [int] NOT NULL,
	[account_paymentNumber] [int] NOT NULL,
	[account_businessTrip] [int] NOT NULL,
	[account_file] NVARCHAR(100) NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
