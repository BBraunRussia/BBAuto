create procedure [dbo].[UpsertCarBuy]
  @idCar int,
  @idOwner int,
  @idRegionBuy int,
  @idRegionUsing int,
  @idDriver int,
  @dateOrder datetime,
  @isGet int,
  @dateGet nvarchar(50),
  @cost float,
  @dop nvarchar(100),
  @events nvarchar(500),
  @idDealer int
as
begin
  if (@idDealer = 0)
    set @idDealer = 1

  declare @count int
  select
    @count = count(car_id)
  from
    CarBuy
  where
    car_id = @idCar

  if (@count = 0)
  begin
    insert into CarBuy values(@idCar, @idOwner, @idRegionBuy, @idRegionUsing, @idDriver, @dateOrder, @isGet, @dateGet, @cost, @dop, @events, @idDealer)
  end
  else
  begin
    update CarBuy
    set owner_id = @idOwner,
        region_id_buy = @idRegionBuy,
        region_id_using = @idRegionUsing,
        driver_id = @idDriver,
        carBuy_dateOrder = @dateOrder,
        carBuy_isGet = @isGet,
        carBuy_dateGet = @dateGet,
        carBuy_cost = @cost,
        carBuy_dop = @dop,
        carBuy_events = @events,
        dealerId = @idDealer
    where car_id = @idCar
  end
end
