create procedure [dbo].[GetServiceStantions]
  @actual int = 0
as
begin
  select
    Id,
    [Name]
  from
    ServiceStantion
  order by
    [Name]
end
