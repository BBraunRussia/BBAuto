create procedure [dbo].[GetModelByGradeId]
  @GradeId int
as
  select
    m.Id,
    m.[Name],
    MarkId
  from
    Grade g
    join Model m
      on m.Id = g.ModelId
  where
    g.Id = @GradeId
