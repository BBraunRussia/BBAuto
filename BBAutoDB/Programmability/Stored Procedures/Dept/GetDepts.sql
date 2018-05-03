create procedure [dbo].[GetDepts]
  @actual int = 0
as
begin
  select
    dept_id,
    dept_name 'Название'
  from
    Dept
  order by
    dept_name
end
