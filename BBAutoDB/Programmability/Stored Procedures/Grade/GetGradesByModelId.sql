create procedure [dbo].[GetGradesByModelId]
  @modelId int
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
    ModelId = @modelId
