create procedure [dbo].[UpsertMileage]
  @idMileage int,
  @idCar int,
  @date datetime,
  @count int
as
  if (@idMileage = 0)
  begin
    insert into Mileage values(@idCar, @date, @count)

    set @idMileage = scope_identity()
  end
  else
    update Mileage
    set DATE = @date,
        count = @count
    where id = @idMileage

  exec dbo.GetMileageById @idMileage
