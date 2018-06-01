CREATE PROCEDURE [dbo].[FuelCard_Delete]
@idFuelCard int
AS
BEGIN
	DELETE FROM FuelCardDriver WHERE FuelCard_id=@idFuelCard
	DELETE FROM FuelCard WHERE FuelCard_id=@idFuelCard
END
GO
