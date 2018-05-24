create procedure [dbo].[GetFuelCardTypeById]
  @id int
as
  select
    Id,
    [Name]
  from
    FuelCardType
  where
    Id = @id
