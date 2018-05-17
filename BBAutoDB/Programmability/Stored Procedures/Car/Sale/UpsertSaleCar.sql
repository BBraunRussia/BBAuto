create procedure [dbo].[UpsertSaleCar]
  @idCar int,
  @comm nvarchar(100) = '',
  @date nvarchar(50) = ''
as
begin
  declare @count int
  select
    @count = count(CarId)
  from
    CarSale
  where
    CarId = @idCar

  if (@count = 0)
  begin
    insert into CarSale values(@idCar, null, null)
  end
  else
  begin
    if (@date = '')
      set @date = null
    else
      set @date = cast(@date as datetime)
    update CarSale
    set Date = @date,
        Comment = @comm
    where CarId = @idCar
  end
end
