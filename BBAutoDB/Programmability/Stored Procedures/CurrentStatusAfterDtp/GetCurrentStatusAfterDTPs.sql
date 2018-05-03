create procedure [dbo].[GetCurrentStatusAfterDTPs]
as
begin
  select
    CurrentStatusAfterDTP_id,
    CurrentStatusAfterDTP_name 'Название'
  from
    CurrentStatusAfterDTP
end
