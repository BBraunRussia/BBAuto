create procedure [dbo].[GetCarSales]
as
begin
  select car_id, carSale_date, carSale_comm from CarSale
end
