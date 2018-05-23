create procedure [dbo].[GetPositions]
  @actual int = 0
as
begin
  select
    Id,
    [Name]
  from
    Position
  order by
    [Name]
end
