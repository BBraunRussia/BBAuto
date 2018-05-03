create procedure [dbo].[DeleteFuelCard]
  @idFuelCard int
as
begin
  delete from FuelCardDriver
  where FuelCard_id = @idFuelCard
  delete from FuelCard
  where FuelCard_id = @idFuelCard
end
