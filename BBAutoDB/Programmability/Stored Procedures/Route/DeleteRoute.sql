create procedure [dbo].[DeleteRoute]
  @id int
as
begin
  delete from Route
  where route_id = @id
end
