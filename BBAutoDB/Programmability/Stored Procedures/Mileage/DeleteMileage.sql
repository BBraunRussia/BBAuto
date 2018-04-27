create procedure [dbo].[DeleteMileage]
  @id int
as
begin
  delete from Mileage
  where id = @id
end
