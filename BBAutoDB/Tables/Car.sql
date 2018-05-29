create table [dbo].[Car] (
  [Id] [INT] identity (1, 1) not null,
  [BBNumber] [INT] not null,
  [Grz] nvarchar(50) null,
  [Vin] nvarchar(17) null,
  [Year] [INT] null,
  [Enumber] nvarchar(50) null,
  [Bodynumber] nvarchar(50) null,
  [PtsId] [INT] null,
  [StsId] [INT] null,
  [GradeId] [INT] null,
  [ColorId] [INT] null,
  [LisingDate] [DATETIME] null,
  [InvertoryNumber] nvarchar(50) null,
  [OwnerId] [INT] null,
  [RegionIdBuy] [INT] null,
  [RegionIdUsing] [INT] null,
  [DriverId] [INT] null,
  [DateOrder] [DATETIME] null,
  [IsGet] bit null,
  [DateGet] [DATETIME] null,
  [Cost] decimal(20, 2) null,
  [Dop] nvarchar(100) null,
  [Events] nvarchar(500) null,
  [DealerId] [INT] null,
  constraint [PK_Car] primary key clustered
  (
  [Id] asc
  ) with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on [PRIMARY]
) on [PRIMARY]
go

alter table [dbo].[Car] with check add constraint [FK_Car_Color] foreign key ([ColorId])
references [dbo].[Color] ([Id])
go

alter table [dbo].[Car] check constraint [FK_Car_Color]
go

alter table [dbo].[Car] with check add constraint [FK_Car_Grade] foreign key ([GradeId])
references [dbo].[Grade] ([Id])
go

alter table [dbo].[Car] check constraint [FK_Car_Grade]
go
