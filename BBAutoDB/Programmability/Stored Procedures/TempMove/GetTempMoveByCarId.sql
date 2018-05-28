create procedure [dbo].[GetTempMoveByCarId]
  @carId int,
  @date datetime
as
  select
    Id,
    CarId,
    DriverId,
    DateBegin,
    DateEnd
  from
    TempMove
  where
    CarId = @carId and
    @date between DateBegin and DateEnd