create procedure [dbo].[GetEngineTypeById]
  @id int
as
begin
  select
    Id,
    [Name],
    ShortName
  from
    EngineType
  where
    Id = @id
end
