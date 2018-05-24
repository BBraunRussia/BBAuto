create procedure [dbo].[GetFuelCardTypes]
as
  select
    Id,
    [Name]
  from
    FuelCardType
  order by
    [Name]
