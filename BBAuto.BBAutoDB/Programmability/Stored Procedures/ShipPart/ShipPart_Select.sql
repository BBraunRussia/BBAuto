CREATE PROCEDURE [dbo].[ShipPart_Select]
AS
BEGIN
	SELECT shippart_id, car_id, driver_id, shipPart_name, shipPart_dateRequest,
		shipPart_dateSent, shipPart_file
	FROM ShipPart
END
GO
