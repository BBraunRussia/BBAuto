create procedure [dbo].[DeleteCulprit]
  @id int
as
begin
  delete from Culprit
  where culprit_id = @id
end
