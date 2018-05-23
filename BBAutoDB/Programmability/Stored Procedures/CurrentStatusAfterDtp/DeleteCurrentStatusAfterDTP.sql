create procedure [dbo].[DeleteCurrentStatusAfterDTP]
  @id int
as
begin
  delete from CurrentStatusAfterDTP
  where Id = @id
end
