create procedure [dbo].[GetTempMoves]
as
begin
  select
    tempMove_id,
    car_id,
    driver_id,
    tempMove_dateBegin,
    tempMove_dateEnd
  from
    TempMove
end
