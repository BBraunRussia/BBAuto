create procedure [dbo].[GetStatuses]
  @all int = 0
as
begin
  select
    Id,
    [Name]
  from
    Status
  order by
    StatusSeq
end
