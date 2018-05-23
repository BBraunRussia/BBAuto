create procedure [dbo].[DeleteInstraction]
  @id int
as
begin
  delete from Instraction
  where Id = @id
end
