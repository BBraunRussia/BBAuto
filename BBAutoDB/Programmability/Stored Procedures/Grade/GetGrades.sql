create procedure [dbo].[GetGrades]
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
