create procedure [dbo].[GetDrivers]
as
  select
    dr.Id,
    Fio,
    RegionId,
    DateBirth,
    Mobile,
    Email,
    Fired,
    ExpSince,
    dr.PositionId,
    dr.DeptId,
    Login,
    OwnerId,
    SuppyAddress,
    Sex,
    Decret,
    DateStopNotification,
    Number,
    IsDriver,
    From1C
  from
    Driver dr
  order by
    Fio
