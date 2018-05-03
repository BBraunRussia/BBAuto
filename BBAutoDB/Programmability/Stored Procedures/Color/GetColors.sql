create procedure [dbo].[GetColors]
as
begin
  select
    color_id,
    color_name 'Название'
  from
    Color
  order by
    color_name
end
