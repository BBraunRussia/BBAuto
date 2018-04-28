create procedure [dbo].[GetCarBuy]
  @idCar int
as
begin
  select
    CarId,
    RegionIdBuy,
    RegionIdUsing,
    DriverId,
    DateOrder,
    IsGet,
    DateGet,
    Cost,
    Dop,
    [Events],
    dealerId
  from
    CarBuy
  where
    CarId = @idCar
end
