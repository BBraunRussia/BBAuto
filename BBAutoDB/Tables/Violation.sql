create table dbo.Violation (
  [violation_id] [INT] identity (1, 1) not null,
  [car_id] [INT] not null,
  [violation_date] [DATETIME] not null,
  [violation_number] NVARCHAR(50) not null,
  [violation_file] NVARCHAR(200) null,
  [violation_datePay] [DATETIME] null,
  [violation_filePay] NVARCHAR(200) null,
  [violationType_id] [INT] null,
  [violation_sum] [INT] null,
  [violation_sent] [INT] null,
  [violation_noDeduction] [INT] null,
  [violation_agreed] NVARCHAR(5) null,
  [violation_dateCreate] [DATETIME] not null,
  constraint [PK_Violation] primary key clustered (violation_id),
  constraint FK_Violation_Car foreign key (car_id) references dbo.Car ([Id])
)
