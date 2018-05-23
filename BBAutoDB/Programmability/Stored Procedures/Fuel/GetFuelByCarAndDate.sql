create procedure [dbo].[GetFuelByCarAndDate]
  @idCar int,
  @date datetime
as
begin
  select
    Fuel.Id
  from
    Fuel
    join FuelCardDriver fcd
      on Fuel.fuelCardId = fcd.FuelCardId
    join Function_DriverCar_Select() dc
      on fcd.DriverId = dc.DriverId
  where
    CarId = @idCar
    and @date > date1
    and @date <= date2
    and year([Date]) = year(@date)
    and month([Date]) = month(@date)
end
