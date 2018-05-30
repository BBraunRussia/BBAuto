create procedure [dbo].[GetModelById]
  @id int
as
  select
    Id,
    [Name],
    MarkId
  from
    Model
  where
    Id = @id
