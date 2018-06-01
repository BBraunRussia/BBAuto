CREATE PROCEDURE [dbo].[CarSale_Select]
AS
BEGIN
	SELECT car_id, carSale_date, carSale_comm FROM CarSale
END
GO
