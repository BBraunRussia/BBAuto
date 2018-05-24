create procedure [dbo].[GetViolationTypes]
as
  select
    Id,
    [Name]
  from
    ViolationType
  order by
    [Name]
