create procedure [dbo].[GetReportTransponderList]
as
  select
    t.Id,
    d.driver_id driverId,
    t.Number,
    r.region_name RegionName,
    d.driver_fio DriverFio
  from DriverTransponder dt
    join Transponder t on t.Id = dt.TransponderId
    join Region r on r.region_id = t.RegionId
    join Driver d on d.driver_id = dt.DriverId
  where
    dt.DateEnd is null
