create procedure [dbo].[GetDriverById]
  @id int
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
    login,
    OwnerId,
    SuppyAddress,
    Sex,
    Decret,
    DateStopNotification,
    number,
    IsDriver,
    From1C
  from
    Driver dr
  where
    dr.Id = @id
  order by
    Fio
