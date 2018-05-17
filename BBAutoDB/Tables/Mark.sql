create table [dbo].[Mark] (
  [Id] [INT] identity (1, 1) not null,
  [Name] nvarchar(50) not null,
  constraint [PK_Mark] primary key clustered
  (
  [Id] asc
  ) with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on [PRIMARY]
) on [PRIMARY]
go
