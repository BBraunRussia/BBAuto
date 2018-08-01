create procedure [dbo].[GetCarSaleList]
as
  select
    car_id as carId,
    carSale_date as [date],
    carSale_comm as comment,
    CustomerId
  from
    CarSale
  order by
    case when carSale_date is null then 0 else 1 end,
    carSale_date desc
