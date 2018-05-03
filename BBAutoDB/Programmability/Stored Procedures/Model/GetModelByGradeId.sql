create procedure [dbo].[GetModelByGradeId]
  @idGrade int
as
begin
  select
    m.model_id,
    model_name
  from
    Grade g
    join Model m
      on m.model_id = g.model_id
  where
    grade_Id = @idGrade
end
