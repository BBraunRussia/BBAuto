create procedure [dbo].[GetViolationTypes]
  @actual int = 0
as
begin
  select
    violationType_id,
    violationType_name 'Название'
  from
    ViolationType
  order by
    violationType_name
end
