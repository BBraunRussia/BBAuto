create procedure [dbo].[GetStatusAfterDTPs]
as
  select
    Id,
    [Name]
  from
    StatusAfterDTP
  order by
    [Name]
