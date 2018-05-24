create procedure [dbo].[GetViolationTypeById]
  @id int
as
  select
    Id,
    [Name]
  from
    ViolationType
  where
    Id = @id
