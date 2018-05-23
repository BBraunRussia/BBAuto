create procedure [dbo].[GetCurrentStatusAfterDTPs]
as
begin
  select
    Id,
    [Name]
  from
    CurrentStatusAfterDTP
end
