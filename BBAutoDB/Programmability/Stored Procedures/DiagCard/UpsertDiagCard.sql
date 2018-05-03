create procedure [dbo].[UpsertDiagCard]
  @idDiagCard int,
  @idCar int,
  @number nvarchar(50),
  @date datetime,
  @file nvarchar(200),
  @notificationSent int
as
begin
  if (@idDiagCard = 0)
  begin
    insert into diagCard values(@idCar, @number, @date, @file, @notificationSent)

    set @idDiagCard = scope_identity()
  end
  else
    update diagCard
    set diagCard_number = @number,
        diagCard_date = @date,
        diagCard_file = @file,
        diagCard_notificationSent = @notificationSent
    where diagCard_id = @idDiagCard

  select @idDiagCard
end
