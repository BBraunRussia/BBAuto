create procedure [dbo].[GetMileageByCarId]
  @idCar int
as
  select
    m.id,
    m.carId,
    m.Date,
    m.Count
  from
    Mileage m
  where
    m.carId = @idCar
