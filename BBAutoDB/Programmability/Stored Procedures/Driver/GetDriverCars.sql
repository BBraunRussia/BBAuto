create procedure [dbo].[GetDriverCars]
as
begin
  select
    CarId,
    DriverId,
    date1,
    date2,
    number
  from
    Function_DriverCar_Select()
end
