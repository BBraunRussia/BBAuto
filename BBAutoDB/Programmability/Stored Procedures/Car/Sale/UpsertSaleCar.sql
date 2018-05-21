create procedure [dbo].[UpsertSaleCar]
  @idCar int,
  @comm nvarchar(100) = '',
  @date nvarchar(50) = ''
as
  declare @count int
  select
    @count = count(*)
  from
    SaleCar
  where
    CarId = @idCar

  if (@count = 0)
  begin
    insert into SaleCar values(@idCar, null, null)
  end
  else
  begin
    if (@date = '')
      set @date = null
    else
      set @date = cast(@date as datetime)
    update
      SaleCar
    set
      [Date] = @date,
      Comment = @comm
    where
      CarId = @idCar
  end
