create procedure [dbo].[GetFuelCardDrivers]
as
begin
  select
    Id,
    FuelCardId,
    DriverId,
    DateBegin,
    DateEnd
  from
    FuelCardDriver
end
