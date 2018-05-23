create procedure [dbo].[GetFuelCardTypes]
as
begin
  select Id, [Name] from FuelCardType
end
