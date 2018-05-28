create procedure [dbo].[UpsertDriver]
  @id int,
  @fio nvarchar(100),
  @RegionId int,
  @dateBirthText nvarchar(50),
  @mobile nvarchar(10),
  @email nvarchar(100),
  @fired bit,
  @ExpSince int,
  @PositionId int,
  @DeptId int,
  @login nvarchar(8),
  @OwnerId int,
  @suppyAddress nvarchar(500),
  @sex bit,
  @decret bit,
  @dateStopNotificationText datetime,
  @number nvarchar(50),
  @isDriver bit,
  @from1C bit
as
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

  if ((@id = 0)
    and (@number <> ''))
  begin
    select
      @id = Id
    from
      Driver
    where
      number = @number

    if (@id is null)
      set @id = 0
  end

  if (@id = 0)
  begin
    insert into Driver values(@fio, @RegionId, @dateBirth, @mobile, @email, @fired, @ExpSince, @PositionId, @DeptId, @login, @OwnerId, @suppyAddress, @sex, @decret, @dateStopNotification, @number, @isDriver, @from1C)
    set @id = scope_identity()
  end
  else
  if (@PositionId = 47
    and @login = 'petumiru')
    update Driver
    set Fio = @fio,
        RegionId = @RegionId,
        DateBirth = @dateBirth,
        Mobile = '',
        Email = '',
        Fired = @fired,
        ExpSince = @ExpSince,
        PositionId = @PositionId,
        DeptId = @DeptId,
        Login = @login,
        OwnerId = @OwnerId,
        SuppyAddress = @suppyAddress,
        Sex = @sex,
        Decret = @decret,
        DateStopNotification = @dateStopNotification,
        Number = @number,
        IsDriver = 0,
        From1C = @from1C
    where Id = @id

  else
    update Driver
    set Fio = @fio,
        RegionId = @RegionId,
        DateBirth = @dateBirth,
        Mobile = @mobile,
        Email = @email,
        Fired = @fired,
        ExpSince = @ExpSince,
        PositionId = @PositionId,
        DeptId = @DeptId,
        login = @login,
        OwnerId = @OwnerId,
        SuppyAddress = @suppyAddress,
        Sex = @sex,
        Decret = @decret,
        DateStopNotification = @dateStopNotification,
        number = @number,
        IsDriver = @isDriver,
        From1C = @from1C
    where Id = @id

  exec dbo.GetDriverById @id
