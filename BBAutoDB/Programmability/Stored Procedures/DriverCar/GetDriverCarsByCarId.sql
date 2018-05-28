create procedure [dbo].[GetDriverCarsByCarId]
  @carId int
as
  select
    CarId,
    DriverId,
    date1,
    date2,
    number
  from
    Function_DriverCar_Select()
  where
    CarId = @carId
