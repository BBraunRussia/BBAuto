create procedure [dbo].[GetDriversByRoleId]
  @roleId int
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
  join UserAccess ua
    on ua.DriverId = dr.Id  
  where
    ua.RoleId = @roleId
