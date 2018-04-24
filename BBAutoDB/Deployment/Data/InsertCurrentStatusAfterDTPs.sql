create procedure [dbo].[InsertCurrentStatusAfterDTPs]
as
  insert into dbo.CurrentStatusAfterDTP values(N'В ремонте')
  insert into dbo.CurrentStatusAfterDTP values(N'Направление на ремонт получено')
  insert into dbo.CurrentStatusAfterDTP values(N'Ожидание документов из ГИБДД')
  insert into dbo.CurrentStatusAfterDTP values(N'Ожидание направления на ремонт')
  insert into dbo.CurrentStatusAfterDTP values(N'Ожидание осмотра')
  insert into dbo.CurrentStatusAfterDTP values(N'Отремонтирован')
  insert into dbo.CurrentStatusAfterDTP values(N'ТОТАЛ')
  insert into dbo.CurrentStatusAfterDTP values(N'Ожидание запчастей')
  insert into dbo.CurrentStatusAfterDTP values(N'Денежное возмещение')
  insert into dbo.CurrentStatusAfterDTP values(N'Отказ')
