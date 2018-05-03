create procedure [dbo].[DeleteCarSale]
  @id int
as
begin
  delete from CarSale
  where car_id = @id
end
