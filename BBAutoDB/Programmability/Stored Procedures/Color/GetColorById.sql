create procedure [dbo].[GetColorById]
  @id int
as
  select
    Id,
    [Name]
  from
    Color
  where
    Id = @id
