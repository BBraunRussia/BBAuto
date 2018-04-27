create procedure [dbo].[GetInvoices]
as
  select
    i.Id,
    i.carId,
    i.number,
    i.driverIdFrom,
    i.driverIdTo,
    i.[date],
    i.DateMove,
    i.regionIdFrom,
    i.regionIdTo,
    i.[file]
  from
    Invoice i
    join Driver d1
      on d1.Id = driverIdFrom
    join Driver d2
      on d2.Id = driverIdTo
  order by
    i.Id
