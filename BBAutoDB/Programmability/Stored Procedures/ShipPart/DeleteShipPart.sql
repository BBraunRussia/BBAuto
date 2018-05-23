create procedure [dbo].[DeleteShipPart]
  @id int
as
begin
  delete from ShipPart
  where Id = @id
end
