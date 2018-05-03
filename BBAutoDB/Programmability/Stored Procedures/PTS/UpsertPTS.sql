create procedure [dbo].[UpsertPTS]
  @idCar int,
  @number nvarchar(50),
  @date datetime,
  @giveOrg nvarchar(100),
  @file nvarchar(200)
as
begin
  if (@idCar = 0)
    return

  declare @count int
  select
    @count = count(car_id)
  from
    PTS
  where
    car_id = @idCar

  if (@count = 0)
  begin
    insert into PTS values(@idCar, @number, @date, @giveOrg, @file)
  end
  else
  begin
    update PTS
    set pts_number = @number,
        pts_date = @date,
        pts_giveOrg = @giveOrg,
        pts_file = @file
    where car_id = @idCar
  end
end
