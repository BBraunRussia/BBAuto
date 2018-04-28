create procedure [dbo].[UpsertCarBuy]
  @idCar int,
  @idOwner int,
  @idRegionBuy int,
  @idRegionUsing int,
  @idDriver int,
  @dateOrder datetime,
  @isGet bit,
  @dateGet nvarchar(50),
  @cost float,
  @dop nvarchar(100),
  @events nvarchar(500),
  @idDealer int
as
begin
  if (@idDealer = 0)
    set @idDealer = 1

  if (not exists (select * from CarBuy where CarId = @idCar))
  begin
    insert into CarBuy(CarId, OwnerId, RegionIdBuy, RegionIdUsing, DriverId, DateOrder, IsGet, DateGet, Cost, Dop, [Events], DealerId)
    values(@idCar, @idOwner, @idRegionBuy, @idRegionUsing, @idDriver, @dateOrder, @isGet, @dateGet, @cost, @dop, @events, @idDealer)
  end
  else
  begin
    update CarBuy
    set OwnerId = @idOwner,
        RegionIdBuy = @idRegionBuy,
        RegionIdUsing = @idRegionUsing,
        DriverId = @idDriver,
        DateOrder = @dateOrder,
        IsGet = @isGet,
        DateGet = @dateGet,
        Cost = @cost,
        Dop = @dop,
        [Events] = @events,
        DealerId = @idDealer
    where CarId = @idCar
  end
end
