create procedure [dbo].[UpsertCarSale]
  @idCar int,
  @comm nvarchar(100) = '',
  @date nvarchar(50) = ''
as
begin
  declare @count int
  select
    @count = count(car_id)
  from
    CarSale
  where
    car_id = @idCar

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
    set carSale_date = @date,
        carSale_comm = @comm
    where car_id = @idCar
  end
end
