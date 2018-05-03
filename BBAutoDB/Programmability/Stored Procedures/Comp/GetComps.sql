create procedure [dbo].[GetComps]
as
begin
  select
    comp_id,
    comp_name 'Название'
  from
    Comp
  order by
    comp_name
end
