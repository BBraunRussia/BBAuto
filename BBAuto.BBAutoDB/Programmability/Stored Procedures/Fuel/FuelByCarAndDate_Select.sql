CREATE PROCEDURE [dbo].[FuelByCarAndDate_Select]
@idCar int,
@date datetime
AS
BEGIN
	SELECT fuel_id FROM Fuel
		join FuelCardDriver fcd ON fuel.fuelCard_id=fcd.fuelCard_id
		join Function_DriverCar_Select() dc ON fcd.driver_id=dc.driver_id
	WHERE car_id=@idCar and @date > date1 and @date <= date2
		and year(fuel_date)=year(@date) and month(fuel_date)=month(@date)
END
GO
