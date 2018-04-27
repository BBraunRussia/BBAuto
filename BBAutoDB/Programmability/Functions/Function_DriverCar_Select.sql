create function [dbo].[Function_DriverCar_Select] ()
returns table
as
  return
  (
  select
    CarId,
    DriverIdTo DriverId,
    DateMove date1,
    case when date2 is not null then date2 else current_timestamp end date2,
    number
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
        date2,
        number
      from
        Invoice i) tb
  where
    DateMove is not null
  )
