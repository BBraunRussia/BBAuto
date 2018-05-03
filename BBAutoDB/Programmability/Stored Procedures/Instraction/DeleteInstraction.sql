create procedure [dbo].[DeleteInstraction]
  @id int
as
begin
  delete from Instraction
  where instraction_id = @id
end
