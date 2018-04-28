create procedure [dbo].[GetCarBuyInfoByCarId]
  @idCar int
as
  select
    cb.CarId,
    cb.OwnerId,
    cb.RegionIdBuy,
    cb.RegionIdUsing,
    cb.DriverId,
    cb.DateOrder,
    cb.IsGet,
    cb.DateGet,
    cb.Cost,
    cb.Dop,
    cb.[Events],
    cb.dealerId
  from
    CarBuy cb
  where
    cb.CarId = @idCar
