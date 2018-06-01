CREATE PROCEDURE [dbo].[ShipPart_Delete]
@idShipPart int
AS
BEGIN
	DELETE FROM ShipPart WHERE shipPart_id=@idShipPart
END
GO
