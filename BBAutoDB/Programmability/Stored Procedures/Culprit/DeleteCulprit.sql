create procedure [dbo].[DeleteCulprit]
  @id int
as
begin
  delete from Culprit
  where Id = @id
end
