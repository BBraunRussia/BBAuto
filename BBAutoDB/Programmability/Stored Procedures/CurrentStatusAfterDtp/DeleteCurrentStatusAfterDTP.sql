create procedure [dbo].[DeleteCurrentStatusAfterDTP]
  @id int
as
begin
  delete from CurrentStatusAfterDTP
  where CurrentStatusAfterDTP_id = @id
end
