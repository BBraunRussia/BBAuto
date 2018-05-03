create procedure [dbo].[DeleteGrade]
  @id int
as
begin
  delete from Grade
  where grade_id = @id
  select 'Удален'
end
