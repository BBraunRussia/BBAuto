create procedure [dbo].[GetTempMoveById]
  @id int
AS
  select
    Id,
    CarId,
    DriverId,
    DateBegin,
    DateEnd
  from
    TempMove
  where
    Id = @id
