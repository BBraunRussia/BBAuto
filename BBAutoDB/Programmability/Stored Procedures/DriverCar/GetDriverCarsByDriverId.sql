create procedure [dbo].[GetDriverCarsByDriverId]
  @driverId int
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
    DriverId = @driverId
