create procedure [dbo].[GetCulpritById]
  @id int
as
  select
    Id,
    [Name]
  from
    Culprit
  where
    Id = @id
