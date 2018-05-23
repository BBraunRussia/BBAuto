create procedure [dbo].[DeleteFuelCardDriver]
  @id int
as
begin
  delete from FuelCardDriver
  where id = @id
end
