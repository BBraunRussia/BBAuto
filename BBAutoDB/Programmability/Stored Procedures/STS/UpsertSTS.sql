create procedure [dbo].[UpsertSTS]
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
    STS
  where
    car_id = @idCar

  if (@count = 0)
    insert into STS values(@idCar, @number, @date, @giveOrg, @file)
  else
    update STS
    set sts_number = @number,
        sts_date = @date,
        sts_giveOrg = @giveOrg,
        sts_file = @file
    where car_id = @idCar
end
