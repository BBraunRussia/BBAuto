CREATE PROCEDURE [dbo].[FuelCard_Select]
AS
BEGIN
	SELECT FuelCard_id, FuelCardType_id, FuelCard_number, FuelCard_dateEnd,
		region_id, FuelCard_pin, FuelCard_lost, FuelCard_comment FROM FuelCard
END
GO
