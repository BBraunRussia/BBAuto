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
      on pos.position_id = dr.PositionId
    left join Dept
      on Dept.dept_id = dr.DeptId
  order by
    Fio
end
