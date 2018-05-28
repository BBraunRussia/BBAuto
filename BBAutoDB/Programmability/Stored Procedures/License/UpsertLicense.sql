create procedure [dbo].[UpsertLicense]
  @id int,
  @DriverId int,
  @number nvarchar(50),
  @dateBegin datetime,
  @dateEnd datetime,
  @file nvarchar(100),
  @notificationSent bit
as
begin
  if (@id = 0)
  begin
    insert into License values(@number, @dateBegin, @dateEnd, @DriverId, @file, 0)

    set @id = scope_identity()
  end
  else
    update License
    set Number = @number,
        DateBegin = @dateBegin,
        DateEnd = @dateEnd,
        [File] = @file,
        NotificationSent = @notificationSent
    where Id = @id

  select @id
end
