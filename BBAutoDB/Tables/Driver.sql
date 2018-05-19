create table [dbo].[Driver] (
  [Id] [INT] identity (1, 1) not null,
  [Fio] nvarchar(100) not null,
  [RegionId] [INT] not null,
  [DateBirth] [DATETIME] null,
  [Mobile] nvarchar(10) null,
  [Email] nvarchar(100) null,
  [Fired] bit null,
  [ExpSince] [INT] null,
  [PositionId] [INT] not null,
  [DeptId] [INT] null,
  [Login] nvarchar(8) null,
  [OwnerId] [INT] null,
  [SuppyAddress] nvarchar(500) null,
  [Sex] bit null,
  [Decret] bit null,
  [DateStopNotification] [DATETIME] null,
  [Number] nvarchar(50) null,
  [IsDriver] bit null,
  [From1C] bit null,
  constraint [PK_Driver] primary key clustered
  (
  [Id] asc
  ) with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on [PRIMARY]
) on [PRIMARY]
go

alter table [dbo].[Driver] with check add constraint [FK_Driver_Position] foreign key ([positionId])
references [dbo].[Position] ([position_id])
on update cascade
on delete cascade
go

alter table [dbo].[Driver] check constraint [FK_Driver_Position]
go

alter table [dbo].[Driver] with check add constraint [FK_Driver_Region] foreign key ([regionId])
references [dbo].[Region] ([Id])
on update cascade
on delete cascade
go

alter table [dbo].[Driver] check constraint [FK_Driver_Region]
go
