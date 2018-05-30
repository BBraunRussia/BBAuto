create procedure [dbo].[GetDriverByLogin]
  @login nvarchar(8)
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
  where
    dr.[Login] = @login
