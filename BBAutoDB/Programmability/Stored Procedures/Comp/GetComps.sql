create procedure [dbo].[GetComps]
as
begin
  select
    Id,
    [Name]
  from
    Comp
  order by
    [Name]
end
