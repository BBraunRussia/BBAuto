create procedure [dbo].[GetMarkByGradeId]
  @idGrade int
as
begin
  select
    Mark.mark_id,
    mark_name
  from
    Grade g
    join Model m
      on m.model_id = g.model_id
    join Mark
      on Mark.mark_id = m.mark_id
  where
    grade_Id = @idGrade
end
