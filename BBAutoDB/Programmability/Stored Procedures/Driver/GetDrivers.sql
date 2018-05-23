create procedure [dbo].[GetDrivers]
as
begin
  select
    dr.id,
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
    left join Position pos
      on pos.Id = dr.PositionId
    left join Dept
      on Dept.Id = dr.DeptId
  order by
    Fio
end
