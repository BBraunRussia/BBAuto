create procedure [dbo].[GetModelByGradeId]
  @idGrade int
as
begin
  select
    m.Id,
    m.[Name]
  from
    Grade g
    join Model m
      on m.Id = g.ModelId
  where
    g.Id = @idGrade
end
