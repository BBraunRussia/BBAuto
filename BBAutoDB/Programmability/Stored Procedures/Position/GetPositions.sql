create procedure [dbo].[GetPositions]
  @actual int = 0
as
begin
  select
    position_id,
    position_name 'Название'
  from
    Position
  order by
    position_name
end
