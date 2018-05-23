create procedure [dbo].[GetDepts]
  @actual int = 0
as
begin
  select
    Id,
    [Name]
  from
    Dept
  order by
    [Name]
end
