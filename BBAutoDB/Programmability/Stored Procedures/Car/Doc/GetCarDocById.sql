create procedure dbo.GetCarDocById
  @id int
as
  select
    Id,
    CarId,
    [Name],
    [File]
  from
    CarDoc
  where
    Id = @id
