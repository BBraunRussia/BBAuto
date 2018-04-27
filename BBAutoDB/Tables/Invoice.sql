create table [dbo].[Invoice] (
  [Id] [INT] identity (1, 1) not null,
  [CarId] [INT] not null,
  [Number] [INT] not null,
  [DriverIdFrom] [INT] not null,
  [DriverIdTo] [INT] not null,
  [Date] [DATETIME] null,
  [DateMove] [DATETIME] null,
  [RegionIdFrom] [INT] null,
  [RegionIdTo] [INT] null,
  [File] nvarchar(500) null,
  constraint [PK_Invoice] primary key clustered
  (
  [Id] asc
  ) with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on [PRIMARY]
) on [PRIMARY]
go
