create procedure [dbo].[GetOwnerById]
  @id int
as
  select
    Id,
    [Name]
  from
    [Owner]
  where
    Id = @id
