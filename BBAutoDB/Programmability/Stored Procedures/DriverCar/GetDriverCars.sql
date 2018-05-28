create procedure [dbo].[GetDriverCars]
as
  select
    CarId,
    DriverId,
    date1,
    date2,
    number
  from
    Function_DriverCar_Select()
