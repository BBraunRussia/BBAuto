create procedure [dbo].[GetEngineTypes]
as
begin
  select
    Id,
    [Name],
    ShortName
  from
    EngineType
end
