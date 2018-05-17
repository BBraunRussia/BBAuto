create procedure [dbo].[GetMarks]
  @all int = 0
as
begin
  select
    Id,
    [Name]
  from
    dbo.Mark
  order by
    [Name]
end
