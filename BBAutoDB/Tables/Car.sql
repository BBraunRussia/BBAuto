create table [dbo].[Car] (
  [Id] [INT] identity (1, 1) not null,
  [bbnumber] [INT] not null,
  [grz] nvarchar(50) null,
  [vin] nvarchar(17) null,
  [year] [INT] null,
  [enumber] nvarchar(50) null,
  [bodynumber] nvarchar(50) null,
  [ptsId] [INT] null,
  [stsId] [INT] null,
  [gradeId] [INT] null,
  [colorId] [INT] null,
  [LisingDate] [DATETIME] null,
  [InvertoryNumber] nvarchar(50) null,
  constraint [PK_Car] primary key clustered
  (
  [Id] asc
  ) with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on [PRIMARY]
) on [PRIMARY]
go

alter table [dbo].[Car] with check add constraint [FK_Car_Color] foreign key ([colorId])
references [dbo].[Color] ([color_id])
on update cascade
on delete cascade
go

alter table [dbo].[Car] check constraint [FK_Car_Color]
go

alter table [dbo].[Car] with check add constraint [FK_Car_Grade] foreign key ([gradeId])
references [dbo].[Grade] ([grade_Id])
go

alter table [dbo].[Car] check constraint [FK_Car_Grade]
go
