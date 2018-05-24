create procedure [dbo].[GetStatusAfterDtpById]
  @id int
as
  select
    Id,
    [Name]
  from
    StatusAfterDTP
  where
    Id = @id
