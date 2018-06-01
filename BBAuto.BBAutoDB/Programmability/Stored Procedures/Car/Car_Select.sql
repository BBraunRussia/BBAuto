CREATE PROCEDURE [dbo].[Car_Select]
AS
BEGIN
	SELECT c.car_id, car_bbnumber, car_grz, car_vin, car_year, car_enumber, car_bodynumber,
		mark_id, g.model_id, c.grade_id, color_id,
		owner_id, region_id_buy, region_id_using,
		driver_id, carBuy_dateOrder, carBuy_isGet, carBuy_dateGet, carBuy_cost, carBuy_dop,
		carBuy_events, diller_id, car_lisingDate, car_InvertoryNumber
	FROM Car c
		join Grade g ON g.grade_id=c.grade_id
		join Model ON Model.model_id=g.model_id
		join CarBuy cb ON cb.car_id=c.car_id
	ORDER BY car_bbnumber
END
GO
