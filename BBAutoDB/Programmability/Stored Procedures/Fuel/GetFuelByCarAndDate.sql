create procedure [dbo].[GetFuelByCarAndDate]
  @idCar int,
  @date datetime
as
begin
  select
    fuel_id
  from
    Fuel
    join FuelCardDriver fcd
      on Fuel.fuelCard_id = fcd.fuelCard_id
    join Function_DriverCar_Select() dc
      on fcd.driver_id = dc.DriverId
  where
    CarId = @idCar
    and @date > date1
    and @date <= date2
    and year(fuel_date) = year(@date)
    and month(fuel_date) = month(@date)
end
