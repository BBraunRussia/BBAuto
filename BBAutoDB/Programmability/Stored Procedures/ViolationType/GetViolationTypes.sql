create procedure [dbo].[GetViolationTypes]
  @actual int = 0
as
begin
  select
    Id,
    [Name]
  from
    ViolationType
  order by
    [Name]
end
