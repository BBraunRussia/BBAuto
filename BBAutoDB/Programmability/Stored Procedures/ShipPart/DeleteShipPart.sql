create procedure [dbo].[DeleteShipPart]
  @idShipPart int
as
begin
  delete from ShipPart
  where shipPart_id = @idShipPart
end
