create procedure [dbo].[GetFuelCards]
as
begin
  select
    Id,
    FuelCardTypeId,
    Number,
    DateEnd,
    RegionId,
    Pin,
    Lost,
    Comment
  from
    FuelCard
end
