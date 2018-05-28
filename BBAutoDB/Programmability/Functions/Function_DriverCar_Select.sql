create function [dbo].[Function_DriverCar_Select] ()
returns table
as
  return
  (
  select
    CarId,
    DriverIdTo DriverId,
    DateMove Date1,
    case when Date2 is not null then Date2 else current_timestamp end Date2,
    Number
  from
    (select
        CarId,
        DriverIdTo,
        DateMove,
        (select
            min(DateMove)
          from
            Invoice i2
          where
            i.CarId = i2.CarId
            and i.Id < i2.Id
            and i.DateMove <= i2.DateMove
            and i.DateMove <= i2.DateMove)
        Date2,
        Number
      from
        Invoice i) tb
  where
    DateMove is not null
  )
