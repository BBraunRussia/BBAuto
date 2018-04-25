create procedure [dbo].[InsertViolationTypes]
as
  insert into dbo.ViolationType values(N'Превышение скорости')
  insert into dbo.ViolationType values(N'Нарушение правил остановки тс')
  insert into dbo.ViolationType values(N'Нарушение правил парковки')
  insert into dbo.ViolationType values(N'Пересечение стоп-линии на перекрестке')
  insert into dbo.ViolationType values(N'Проезд на запрещающий сигнал светофора')
  insert into dbo.ViolationType values(N'Движение по полосе предназначенная для маршрутных ')
  insert into dbo.ViolationType values(N'Парковка тс на платной стоянке без оплаты')
  insert into dbo.ViolationType values(N'Движение по обочине')
  insert into dbo.ViolationType values(N'Парковка на тротуаре')
  insert into dbo.ViolationType values(N'Перестроение в нарушении ПДД')
  insert into dbo.ViolationType values(N'Движение в нарушение требований дорожных знаков')
  insert into dbo.ViolationType values(N'Постановление было оспорено в суде')
  insert into dbo.ViolationType values(N'Нарушение движения на нерегулируемом пешеходном пе')