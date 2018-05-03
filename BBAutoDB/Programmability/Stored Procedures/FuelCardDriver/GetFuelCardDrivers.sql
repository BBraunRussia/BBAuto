create procedure [dbo].[GetFuelCardDrivers]
as
begin
  select
    FuelCardDriver_id,
    FuelCard_id,
    driver_id,
    FuelCardDriver_dateBegin,
    FuelCardDriver_dateEnd
  from
    FuelCardDriver
end
