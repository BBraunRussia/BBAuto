create procedure [dbo].[UpsertDriver]
  @idDriver int,
  @fio nvarchar(100),
  @idRegion int,
  @dateBirthText nvarchar(50),
  @mobile nvarchar(10),
  @email nvarchar(100),
  @fired int,
  @ExpSince int,
  @idPosition int,
  @idDept int,
  @login nvarchar(8),
  @idOwner int,
  @suppyAddress nvarchar(500),
  @sex int,
  @decret int,
  @dateStopNotificationText datetime,
  @number nvarchar(50),
  @isDriver int,
  @from1C int
as
begin
  declare @dateBirth datetime
  if (@dateBirthText = '')
    set @dateBirth = null
  else
    set @dateBirth = cast(@dateBirthText as datetime)

  declare @dateStopNotification datetime
  if (@dateStopNotificationText = '')
    set @dateStopNotification = null
  else
    set @dateStopNotification = cast(@dateStopNotificationText as datetime)

  if ((@idDriver = 0)
    and (@number <> ''))
  begin
    select
      @idDriver = id
    from
      Driver
    where
      number = @number

    if (@idDriver is null)
      set @idDriver = 0
  end

  if (@idDriver = 0)
  begin
    insert into Driver values(@fio, @idRegion, @dateBirth, @mobile, @email, @fired, @ExpSince, @idPosition, @idDept, @login, @idOwner, @suppyAddress, @sex, @decret, @dateStopNotification, @number, @isDriver, @from1C)
    set @idDriver = scope_identity()
  end
  else
  if (@idPosition = 47
    and @login = 'petumiru')
    update Driver
    set Fio = @fio,
        RegionId = @idRegion,
        DateBirth = @dateBirth,
        Mobile = '',
        Email = '',
        Fired = @fired,
        ExpSince = @ExpSince,
        PositionId = @idPosition,
        DeptId = @idDept,
        login = @login,
        OwnerId = @idOwner,
        SuppyAddress = @suppyAddress,
        Sex = @sex,
        Decret = @decret,
        DateStopNotification = @dateStopNotification,
        number = @number,
        IsDriver = 0,
        From1C = @from1C
    where id = @idDriver

  else
    update Driver
    set Fio = @fio,
        RegionId = @idRegion,
        DateBirth = @dateBirth,
        Mobile = @mobile,
        Email = @email,
        Fired = @fired,
        ExpSince = @ExpSince,
        PositionId = @idPosition,
        DeptId = @idDept,
        login = @login,
        OwnerId = @idOwner,
        SuppyAddress = @suppyAddress,
        Sex = @sex,
        Decret = @decret,
        DateStopNotification = @dateStopNotification,
        number = @number,
        IsDriver = @isDriver,
        From1C = @from1C
    where id = @idDriver

  select @idDriver
end
