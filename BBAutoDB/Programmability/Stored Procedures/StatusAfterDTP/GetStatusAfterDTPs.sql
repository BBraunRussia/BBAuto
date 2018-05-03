create procedure [dbo].[GetStatusAfterDTPs]
  @actual int = 0
as
begin
  select
    StatusAfterDTP_id,
    StatusAfterDTP_name 'Название'
  from
    StatusAfterDTP
  order by
    StatusAfterDTP_name
end
