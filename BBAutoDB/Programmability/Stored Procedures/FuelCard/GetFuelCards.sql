create procedure [dbo].[GetFuelCards]
as
begin
  select
    FuelCard_id,
    FuelCardType_id,
    FuelCard_number,
    FuelCard_dateEnd,
    region_id,
    FuelCard_pin,
    FuelCard_lost,
    FuelCard_comment
  from
    FuelCard
end
