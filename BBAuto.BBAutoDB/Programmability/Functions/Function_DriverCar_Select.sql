create function [dbo].[Function_DriverCar_Select] ()
returns table
as
  return (select
      car_id,
      driver_id_To driver_id,
      invoice_dateMove date1,
      case when date2 is not null then date2 else CURRENT_TIMESTAMP end date2,
      invoice_number,
      IsMain,
      region_id_To RegionId
    from
      (select
          car_id,
          driver_id_To,
          invoice_dateMove,
          (select
              min(invoice_dateMove)
            from
              Invoice i2
            where
              i.car_id = i2.car_id
              and i.invoice_id <> i2.invoice_id
              and i.invoice_dateMove <= i2.invoice_dateMove)
          date2,
          invoice_number,
          IsMain,
          region_id_To
        from
          Invoice i) tb
    where
      invoice_dateMove is not null)
go
