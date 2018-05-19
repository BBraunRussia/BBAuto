create procedure dbo.InsertDrivers
as
  declare @regionId int
  select top 1 @regionId = Id from dbo.Region

  declare @positionId int
  select top 1 @positionId = position_id from dbo.Position

  insert into dbo.Driver(Fio, RegionId, DateBirth, Mobile, Email, Fired, ExpSince, PositionId, DeptId, Login, OwnerId, SuppyAddress, Sex, Decret, DateStopNotification, Number, IsDriver, From1C)
    values (N'Masliaev', @regionId, getdate(), '', '', 0, 0, @positionId, 0, N'Next', 0, '', 0, 0, getdate(), '', 0, 0);

  declare @roleId int
  select top 1 @roleId = role_id from dbo.Role

  insert into dbo.UserAccess(driver_id, role_id)
    values (scope_identity(), @roleId)
