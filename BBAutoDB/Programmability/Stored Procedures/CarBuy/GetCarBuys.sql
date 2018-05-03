create procedure [dbo].[GetCarBuys]
  @idCar int
as
begin
  select
    owner_id,
    region_id_buy,
    region_id_using,
    driver_id,
    carBuy_dateOrder,
    carBuy_isGet,
    carBuy_dateGet,
    carBuy_cost,
    carBuy_dop,
    carBuy_events,
    dealerId
  from
    CarBuy
  where
    car_id = @idCar
end
