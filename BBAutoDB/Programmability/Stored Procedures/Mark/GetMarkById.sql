create procedure dbo.GetMarkById
  @id int
as
  select
    Id,
    [Name]
  from
    dbo.Mark
  where
    Id = @id
