create table [dbo].[SaleCar] (
  [CarId] [INT] not null,
  [Date] [DATETIME] null,
  [Comment] nvarchar(100) null,
  constraint [PK_SaleCar] primary key clustered
  (
  [CarId] asc
  ) with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on [PRIMARY]
) on [PRIMARY]
go

alter table [dbo].[SaleCar] with check add constraint [FK_SaleCar_Car] foreign key ([CarId])
references [dbo].[Car] ([id])
go

alter table [dbo].[SaleCar] check constraint [FK_SaleCar_Car]
go
