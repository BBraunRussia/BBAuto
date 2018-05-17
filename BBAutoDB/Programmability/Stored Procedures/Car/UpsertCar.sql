create procedure [dbo].[UpsertCar]
  @idCar int,
  @bbNumber int,
  @grz nvarchar(50),
  @vin nvarchar(17),
  @year int,
  @eNumber nvarchar(50),
  @bodyNumber nvarchar(50),
  @idGrade int,
  @idColor int,
  @isLising int,
  @LisingDate datetime,
  @InvertoryNumber nvarchar(50),
  @OwnerId int,
  @RegionIdBuy int,
  @RegionIdUsing int,
  @DriverId int,
  @dateOrder datetime,
  @isGet bit,
  @dateGet nvarchar(50),
  @cost decimal(20, 2),
  @dop nvarchar(100),
  @events nvarchar(500),
  @DealerId int

as
begin
  if (@idColor = 0)
    set @idColor = 1

  if (@isLising = 0)
    set @LisingDate = null

  if (@idCar = 0)
  begin
    insert into Car(bbnumber, grz, vin, year, enumber, bodynumber, ptsId, stsId, gradeId, colorId, LisingDate, InvertoryNumber, OwnerId,
                    RegionIdBuy, RegionIdUsing, DriverId, DateOrder, IsGet, DateGet, Cost, Dop, Events, DealerId)
    values(@bbNumber, @grz, @vin, @year, @eNumber, @bodyNumber, null, null, @idGrade, @idColor, @LisingDate, @InvertoryNumber, @OwnerId,
                    @RegionIdBuy, @RegionIdUsing, @DriverId, @dateOrder, @isGet, @dateGet, @cost, @dop, @events, @DealerId)

    set @idCar = scope_identity()
  end
  else
  begin
    update
      Car
    set
      grz = @grz,
      vin = @vin,
      [year] = @year,
      enumber = @eNumber,
      bodynumber = @bodyNumber,
      gradeId = @idGrade,
      colorId = @idColor,
      LisingDate = @LisingDate,
      InvertoryNumber = @InvertoryNumber,
      OwnerId = @OwnerId,
      RegionIdBuy = @RegionIdBuy,
      RegionIdUsing = @RegionIdUsing,
      DriverId = @DriverId,
      DateOrder = @dateOrder,
      IsGet = @isGet,
      DateGet = @dateGet,
      Cost = @cost,
      Dop = @dop,
      Events = @events,
      DealerId = @DealerId
    where
      id = @idCar
  end

  select @idCar
end
