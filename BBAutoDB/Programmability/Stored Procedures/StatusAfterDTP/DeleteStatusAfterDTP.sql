create procedure [dbo].[DeleteStatusAfterDTP]
  @id int
as
begin
  delete from StatusAfterDTP
  where Id = @id
end
