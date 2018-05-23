create procedure [dbo].[GetStatusAfterDTPs]
  @actual int = 0
as
begin
  select
    Id,
    [Name]
  from
    StatusAfterDTP
  order by
    [Name]
end
