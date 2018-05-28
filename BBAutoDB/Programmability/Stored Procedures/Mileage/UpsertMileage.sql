create procedure [dbo].[UpsertMileage]
  @id int,
  @CarId int,
  @date datetime,
  @count int
as
  if (@id = 0)
  begin
    insert into Mileage values(@CarId, @date, @count)

    set @id = scope_identity()
  end
  else
    update
      Mileage
    set
      [Date] = @date,
      count = @count
    where
      id = @id

  exec dbo.GetMileageById @id
