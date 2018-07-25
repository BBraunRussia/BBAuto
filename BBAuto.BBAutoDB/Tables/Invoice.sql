create table [dbo].[Invoice] (
  [invoice_id] [int] identity (1, 1) not null,
  [car_id] [int] not null,
  [invoice_number] [int] not null,
  [driver_id_From] [int] not null,
  [driver_id_To] [int] not null,
  [invoice_date] [datetime] null,
  [invoice_dateMove] [datetime] null,
  [region_id_From] [int] null,
  [region_id_To] [int] null,
  [invoice_file] [varchar](500) null,
  [IsMain] bit null,
  constraint [PK_Invoice] primary key clustered
  (
  [invoice_id] asc
  ) with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on [PRIMARY]
) on [PRIMARY]
go
