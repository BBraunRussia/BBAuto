create procedure [dbo].[GetTempMoves]
as
begin
  select
    Id,
    CarId,
    DriverId,
    DateBegin,
    DateEnd
  from
    TempMove
end
