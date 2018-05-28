create procedure [dbo].[GetTempMoveById]
  @id int
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
    Id = @id
