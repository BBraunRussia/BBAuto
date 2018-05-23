create procedure [dbo].[DeleteRoute]
  @id int
as
begin
  delete from Route
  where Id = @id
end
