CREATE PROCEDURE [dbo].[FuelCardDriver_Delete]
@idFuelCardDriver int
AS
BEGIN
	DELETE FROM FuelCardDriver WHERE FuelCardDriver_id=@idFuelCardDriver
END
GO
