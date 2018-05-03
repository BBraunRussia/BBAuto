create procedure [dbo].[DeleteFuelCardDriver]
  @idFuelCardDriver int
as
begin
  delete from FuelCardDriver
  where FuelCardDriver_id = @idFuelCardDriver
end
