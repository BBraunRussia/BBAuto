create table [dbo].[Grade] (
  [Id] [INT] identity (1, 1) not null,
  [Name] nvarchar(50) not null,
  [Epower] [INT] not null,
  [Evol] [INT] not null,
  [MaxLoad] [INT] not null,
  [NoLoad] [INT] not null,
  [EngineTypeId] [INT] not null,
  [ModelId] [INT] not null,
  constraint [PK_Grade] primary key clustered
  (
  [Id] asc
  ) with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on [PRIMARY]
) on [PRIMARY]
go

alter table [dbo].[Grade] with check add constraint [FK_Grade_engineType] foreign key ([EngineTypeId])
references [dbo].[engineType] ([Id])
go

alter table [dbo].[Grade] check constraint [FK_Grade_engineType]
go

alter table [dbo].[Grade] with check add constraint [FK_Grade_Model] foreign key ([ModelId])
references [dbo].[model] ([Id])
go

alter table [dbo].[Grade] check constraint [FK_Grade_Model]
go
