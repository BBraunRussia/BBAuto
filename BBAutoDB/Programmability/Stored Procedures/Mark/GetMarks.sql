create procedure [dbo].[GetMarks]
  @all int = 0
as
begin
  select
    mark_id,
    mark_name 'Название'
  from
    Mark
  order by
    mark_name
end
