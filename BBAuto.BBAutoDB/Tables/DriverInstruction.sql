create table [dbo].[DriverInstruction] (
  [Id] [int] identity (1, 1) not null,
  [DocumentId] int not null,
  [DriverId] [int] not null,
  [Date] [datetime] null,
  constraint [PK_DriverInstraction] primary key clustered
  (
  [Id] asc
  ) with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on [PRIMARY]
) on [PRIMARY]
go

alter table [dbo].[DriverInstruction] with check add constraint [FK_DriverInstruction_Driver] foreign key ([DriverId])
references [dbo].[Driver] ([driver_id])
go

alter table [dbo].[DriverInstruction] check constraint [FK_DriverInstruction_Driver]
go
