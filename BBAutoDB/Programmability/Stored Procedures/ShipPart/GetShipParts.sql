create procedure [dbo].[GetShipParts]
as
begin
  select
    shipPart_id,
    car_id,
    driver_id,
    shipPart_name,
    shipPart_dateRequest,
    shipPart_dateSent,
    shipPart_file
  from
    ShipPart
end
