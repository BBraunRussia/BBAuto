create procedure [dbo].[UpsertDiagCard]
  @id int,
  @idCar int,
  @number nvarchar(50),
  @dateEnd datetime,
  @file nvarchar(200),
  @notificationSent int
as
begin
  if (@id = 0)
  begin
    insert into DiagCard values(@idCar, @number, @dateEnd, @file, @notificationSent)

    set @id = scope_identity()
  end
  else
    update diagCard
    set Number = @number,
        DateEnd = @dateEnd,
        [File] = @file,
        NotificationSent = @notificationSent
    where Id = @id

  select @id
end
