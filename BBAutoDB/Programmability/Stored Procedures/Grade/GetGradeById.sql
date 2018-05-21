create procedure dbo.GetGradeById
  @id int
as
  select
    Id,
    [Name],
    Epower,
    Evol,
    MaxLoad,
    NoLoad,
    EngineTypeId,
    ModelId
  from
    Grade
  where
    Id = @id
