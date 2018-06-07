create procedure [dbo].[GetCarSaleByCarId]
  @carId int
as
  select
    car_id as carId,
    carSale_date as [date],
    carSale_comm as comment,
    CustomerId
  from
    CarSale
  where
    car_id = @carId
