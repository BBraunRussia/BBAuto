create procedure [dbo].[DeleteSaleCar]
  @id int
as
begin
  delete from CarSale
  where CarId = @id
end
