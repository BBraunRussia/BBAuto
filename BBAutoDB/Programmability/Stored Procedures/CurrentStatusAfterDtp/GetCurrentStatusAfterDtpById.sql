create procedure [dbo].[GetCurrentStatusAfterDtpById]
  @id int
as
  select
    Id,
    [Name]
  from
    CurrentStatusAfterDTP
  where
    Id = @id
