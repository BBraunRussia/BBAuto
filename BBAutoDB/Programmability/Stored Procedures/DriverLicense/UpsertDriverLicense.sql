create procedure [dbo].[UpsertDriverLicense]
  @idDriverLicense int,
  @idDriver int,
  @number nvarchar(50),
  @dateBegin datetime,
  @dateEnd datetime,
  @file nvarchar(100),
  @notificationSent int
as
begin
  if (@idDriverLicense = 0)
  begin
    insert into DriverLicense values(@number, @dateBegin, @dateEnd, @idDriver, @file, 0)

    set @idDriverLicense = scope_identity()
  end
  else
    update DriverLicense
    set DriverLicense_number = @number,
        DriverLicense_dateBegin = @dateBegin,
        DriverLicense_dateEnd = @dateEnd,
        DriverLicense_file = @file,
        DriverLicense_notificationSent = @notificationSent
    where DriverLicense_id = @idDriverLicense

  select @idDriverLicense
end
