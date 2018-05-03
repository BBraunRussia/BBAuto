create procedure [dbo].[GetFuelCardTypes]
as
begin
  select FuelCardType_id, FuelCardType_name 'Название' from FuelCardType
end
