create procedure [dbo].[GetTempMoves]
as
  select
    Id,
    CarId,
    DriverId,
    DateBegin,
    DateEnd
  from
    TempMove
