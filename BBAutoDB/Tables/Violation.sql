create table dbo.Violation (
  [Id] [INT] identity (1, 1) not null,
  [CarId] [INT] not null,
  [Date] [DATETIME] not null,
  [Number] NVARCHAR(50) not null,
  [File] NVARCHAR(200) null,
  [DatePay] [DATETIME] null,
  [FilePay] NVARCHAR(200) null,
  [ViolationTypeId] [INT] null,
  [Sum] [INT] null,
  [Sent] bit NOT null,
  [NoDeduction] BIT NOT null,
  [Agreed] BIT NOT null,
  [DateCreate] [DATETIME] not null,
  constraint [PK_Violation] primary key clustered (Id),
  constraint FK_Violation_Car foreign key (CarId) references dbo.Car ([Id])
)
