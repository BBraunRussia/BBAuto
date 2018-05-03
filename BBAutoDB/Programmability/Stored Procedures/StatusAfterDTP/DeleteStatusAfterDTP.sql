create procedure [dbo].[DeleteStatusAfterDTP]
  @id int
as
begin
  delete from StatusAfterDTP
  where StatusAfterDTP_id = @id
end
