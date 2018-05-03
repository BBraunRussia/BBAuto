create procedure [dbo].[GetCarBuyInfoByCarId]
  @idCar int
as
  select
    cb.car_id,
    cb.owner_id,
    cb.region_id_buy,
    cb.region_id_using,
    cb.driver_id,
    cb.carBuy_dateOrder,
    cb.carBuy_isGet,
    cb.carBuy_dateGet,
    cb.carBuy_cost,
    cb.carBuy_dop,
    cb.carBuy_events,
    cb.dealerId
  from
    CarBuy cb
  where
    cb.car_id = @idCar
