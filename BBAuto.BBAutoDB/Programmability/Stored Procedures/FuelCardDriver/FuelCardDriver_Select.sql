CREATE PROCEDURE [dbo].[FuelCardDriver_Select]
AS
BEGIN
	SELECT FuelCardDriver_id, FuelCard_id, driver_id, FuelCardDriver_dateBegin,
		FuelCardDriver_dateEnd FROM FuelCardDriver
END
GO
