create procedure [dbo].[DeleteSTS]
  @idCar int
as
begin
  delete from STS
  where car_id = @idCar
end
