CREATE PROCEDURE [dbo].[CarBuy_Select]
@idCar int
AS
BEGIN
	SELECT owner_id, region_id_buy, region_id_using, driver_id, carBuy_dateOrder, carBuy_isGet,
		carBuy_dateGet, carBuy_cost, carBuy_dop, carBuy_events, diller_id
	FROM CarBuy
	WHERE car_id=@idCar
END
GO
