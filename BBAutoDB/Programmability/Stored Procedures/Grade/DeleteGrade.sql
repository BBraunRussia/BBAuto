create procedure [dbo].[DeleteGrade]
  @id int
as
begin
  delete from Grade
  where Id = @id
  select 'Удален'
end
