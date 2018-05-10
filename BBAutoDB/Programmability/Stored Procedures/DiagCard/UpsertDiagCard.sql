create procedure [dbo].[UpsertDiagCard]
  @id int,
  @idCar int,
  @number nvarchar(50),
  @date datetime,
  @file nvarchar(200),
  @notificationSent int
as
begin
  if (@id = 0)
  begin
    insert into DiagCard values(@idCar, @number, @date, @file, @notificationSent)

    set @id = scope_identity()
  end
  else
    update diagCard
    set Number = @number,
        [Date] = @date,
        [File] = @file,
        NotificationSent = @notificationSent
    where Id = @id

  select @id
end
