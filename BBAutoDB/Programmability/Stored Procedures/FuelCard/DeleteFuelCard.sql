create procedure [dbo].[DeleteFuelCard]
  @id int
as
begin
  delete from FuelCardDriver
  where FuelCardId = @id
  delete from FuelCard
  where Id = @id
end
