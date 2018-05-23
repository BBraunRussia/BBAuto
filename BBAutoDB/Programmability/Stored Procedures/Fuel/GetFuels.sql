create procedure [dbo].[GetFuels]
as
begin
  select
    Id,
    fuelCardId,
    [Date],
    [Count],
    EngineTypeId
  from
    Fuel
end
