create table [dbo].[Model] (
  [Id] [INT] identity (1, 1) not null,
  [Name] nvarchar(50) not null,
  [MarkId] [INT] not null,
  constraint [PK_Model] primary key clustered
  (
  [Id] asc
  ) with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on [PRIMARY]
) on [PRIMARY]
go

alter table [dbo].[Model] with check add constraint [FK_Model_Mark] foreign key ([MarkId])
references [dbo].[Mark] ([Id])
go

alter table [dbo].[Model] check constraint [FK_Model_Mark]
go
