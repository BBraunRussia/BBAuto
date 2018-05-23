CREATE TABLE [dbo].[MileageMonth](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CarId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Count] [float] NOT NULL,
	[psn_count] [float] NOT NULL,
	[psk_count] [float] NOT NULL,
	[gas_count] [float] NOT NULL,
	[gas_begin] [float] NOT NULL,
	[gas_end] [float] NOT NULL,
	[gas_norm] [float] NOT NULL,
 CONSTRAINT [PK_MileageMonth] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
