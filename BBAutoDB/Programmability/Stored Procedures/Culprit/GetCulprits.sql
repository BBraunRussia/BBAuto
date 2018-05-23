create procedure [dbo].[GetCulprits]
  @actual int = 0
as
begin
  select
    Id,
    [Name]
  from
    Culprit
  order by
    [Name]
end
