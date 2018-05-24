create procedure [dbo].[GetCompById]
  @id int
as
  select
    id,
    [Name]
  from
    Comp
  where
    id = @id
