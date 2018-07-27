create table [dbo].[DriverTransponder] (
  [Id] int not null primary key,
  [TransponderId] int not null,
  [DriverId] int not null,
  [DateBegin] datetime not null,
  [DateEnd] datetime null
)
