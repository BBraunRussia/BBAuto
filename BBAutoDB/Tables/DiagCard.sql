create table [dbo].[DiagCard] (
  [Id] [INT] identity (1, 1) not null,
  [CarId] [INT] not null,
  [Number] nvarchar(50) not null,
  [DateEnd] [DATETIME] not null,
  [File] nvarchar(200) null,
  [NotificationSent] bit null,
  constraint [PK_diagCard] primary key clustered
  (
  [Id] asc
  ) with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on [PRIMARY]
) on [PRIMARY]
go

alter table [dbo].[diagCard] with check add constraint [FK_diagCard_Car] foreign key ([CarId])
references [dbo].[Car] ([Id])
go

alter table [dbo].[diagCard] check constraint [FK_diagCard_Car]
go
