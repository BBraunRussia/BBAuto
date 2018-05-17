create procedure [dbo].[GetMarkByGradeId]
  @idGrade int
as
  select
    Mark.Id,
    Mark.[Name]
  from
    Grade g
    join Model m
      on m.Id = g.ModelId
    join Mark
      on Mark.Id = m.MarkId
  where
    g.Id = @idGrade
