create procedure [dbo].[GetMarkByGradeId]
  @gradeId int
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
    g.Id = @gradeId
