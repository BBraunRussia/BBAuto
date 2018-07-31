create procedure [dbo].[GetDriverTranspondersByTransponderId]
  @transponderId int
as
  select
    dt.Id,
    dt.TransponderId,
    dt.DriverId,
    d.driver_fio driverFio,
    dt.DateBegin,
    dt.DateEnd
  from
    DriverTransponder dt
    join Driver d on d.driver_id = dt.DriverId
  where
    dt.TransponderId = @transponderId
  order by
    dt.DateBegin desc, dt.DateEnd desc
