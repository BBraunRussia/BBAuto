create procedure [dbo].[GetFuels]
as
begin
  select
    fuel_id,
    fuelCard_id,
    fuel_date,
    fuel_count,
    engineType_id
  from
    Fuel
end
