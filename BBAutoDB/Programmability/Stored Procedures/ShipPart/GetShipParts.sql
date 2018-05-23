create procedure [dbo].[GetShipParts]
as
begin
  select
    Id,
    CarId,
    DriverId,
    [Name],
    DateRequest,
    DateSent,
    [File]
  from
    ShipPart
end
