create procedure [dbo].[DeletePTS]
  @idCar int
as
begin
  delete from PTS
  where car_id = @idCar
end
