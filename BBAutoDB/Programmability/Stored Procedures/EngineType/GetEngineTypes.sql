create procedure [dbo].[GetEngineTypes]
as
begin
  select
    engineType_id,
    engineType_name 'Название',
    engineType_shortName
  from
    EngineType
end
