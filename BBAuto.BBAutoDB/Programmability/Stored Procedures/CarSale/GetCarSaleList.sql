create procedure [dbo].[GetCarSaleList]
as
  select
    car_id as carId,
    carSale_date as [date],
    carSale_comm as comment,
    CustomerId
  from
    CarSale
