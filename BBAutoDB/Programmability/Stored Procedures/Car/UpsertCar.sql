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
  @InvertoryNumber nvarchar(50)
as
begin
  if (@idColor = 0)
    set @idColor = 1

  if (@isLising = 0)
    set @LisingDate = null

  if (@idCar = 0)
  begin
    insert into Car(bbnumber, grz, vin, year, enumber, bodynumber, ptsId, stsId, gradeId, colorId, LisingDate, InvertoryNumber)
      values(@bbNumber, @grz, @vin, @year, @eNumber, @bodyNumber, null, null, @idGrade, @idColor, @LisingDate, @InvertoryNumber)
    set @idCar = scope_identity()
  end
  else
  begin
    update Car
    set grz = @grz,
        vin = @vin,
        [year] = @year,
        enumber = @eNumber,
        bodynumber = @bodyNumber,
        gradeId = @idGrade,
        colorId = @idColor,
        LisingDate = @LisingDate,
        InvertoryNumber = @InvertoryNumber
    where Id = @idCar
  end

  select @idCar
end
