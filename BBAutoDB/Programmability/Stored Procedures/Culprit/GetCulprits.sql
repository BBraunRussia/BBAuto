create procedure [dbo].[GetCulprits]
  @actual int = 0
as
begin
  select
    culprit_id,
    culprit_name 'Название'
  from
    Culprit
  order by
    culprit_name
end
