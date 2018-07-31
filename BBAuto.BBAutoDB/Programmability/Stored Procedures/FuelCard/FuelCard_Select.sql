create procedure [dbo].[FuelCard_Select]
as
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
  order by
    FuelCard_lost, FuelCard_number
