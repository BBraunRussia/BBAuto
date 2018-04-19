create procedure dbo.InsertDrivers
as
  declare @regionId int
  select top 1 @regionId = region_id from dbo.Region

  declare @positionId int
  select top 1 @positionId = position_id from dbo.Position

  insert into dbo.Driver(driver_fio, region_id, driver_dateBirth, driver_mobile, driver_email, driver_fired, driver_expSince, position_id, dept_id, driver_login, owner_id, driver_suppyAddress, driver_sex, driver_decret, driver_dateStopNotification, driver_number, driver_isDriver, driver_from1C)
    values (N'Masliaev', @regionId, getdate(), '', '', 0, 0, @positionId, 0, N'O137HI7', 0, '', 0, 0, getdate(), '', 0, 0);

  declare @roleId int
  select top 1 @roleId = role_id from dbo.Role

  insert into dbo.UserAccess(driver_id, role_id)
    values (scope_identity(), @roleId)
