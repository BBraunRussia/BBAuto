CREATE PROCEDURE [dbo].[Fuel_Select]
AS
BEGIN
	SELECT fuel_id, fuelCard_id, fuel_date, fuel_count, engineType_id FROM Fuel
END
GO
