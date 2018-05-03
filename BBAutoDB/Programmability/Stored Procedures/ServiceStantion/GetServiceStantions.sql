create procedure [dbo].[GetServiceStantions]
  @actual int = 0
as
begin
  select
    ServiceStantion_id,
    ServiceStantion_name 'Название'
  from
    ServiceStantion
  order by
    ServiceStantion_name
end
