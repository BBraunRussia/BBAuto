create procedure [dbo].[GetStatuses]
  @all int = 0
as
begin
  select
    Status_id,
    Status_name 'Название'
  from
    Status
  order by
    Status_seq
end
