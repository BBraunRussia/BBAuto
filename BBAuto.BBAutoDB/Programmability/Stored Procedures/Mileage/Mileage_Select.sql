CREATE PROCEDURE [dbo].[Mileage_Select]
AS
BEGIN
	SELECT mileage_id, car_id, mileage_date, mileage_count FROM Mileage
END
GO
