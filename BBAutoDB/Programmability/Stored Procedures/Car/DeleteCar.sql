create procedure [dbo].[DeleteCar]
  @idCar int
as
begin
  delete from Car where Id = @idCar
end
