create procedure [dbo].[UpsertCar]
  @id int,
  @bbNumber int,
  @grz nvarchar(50),
  @vin nvarchar(17),
  @year int,
  @eNumber nvarchar(50),
  @bodyNumber nvarchar(50),
  @GradeId int,
  @ColorId int,
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
  if (@ColorId = 0)
    set @ColorId = 1

  if (@id = 0)
  begin
    insert into Car(BBNumber, Grz, Vin, Year, Enumber, Bodynumber, PtsId, StsId, GradeId, ColorId, LisingDate, InvertoryNumber, OwnerId,
                    RegionIdBuy, RegionIdUsing, DriverId, DateOrder, IsGet, DateGet, Cost, Dop, Events, DealerId)
    values(@bbNumber, @grz, @vin, @year, @eNumber, @bodyNumber, null, null, @GradeId, @ColorId, @LisingDate, @InvertoryNumber, @OwnerId,
                    @RegionIdBuy, @RegionIdUsing, @DriverId, @dateOrder, @isGet, @dateGet, @cost, @dop, @events, @DealerId)

    set @id = scope_identity()
  end
  else
  begin
    update
      Car
    set
      Grz = @grz,
      Vin = @vin,
      [Year] = @year,
      Enumber = @eNumber,
      Bodynumber = @bodyNumber,
      GradeId = @GradeId,
      ColorId = @ColorId,
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
      Id = @id
  end

  exec dbo.GetCarById @id
