create table dbo.CarDoc (
  [Id] [INT] identity (1, 1) not null,
  [CarId] [INT] not null,
  [Name] nvarchar(50) not null,
  [File] nvarchar(200) not null,
  constraint [PK_carDoc] primary key clustered
  (
  [Id] asc
  ) with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on [PRIMARY]
) on [PRIMARY]
go
