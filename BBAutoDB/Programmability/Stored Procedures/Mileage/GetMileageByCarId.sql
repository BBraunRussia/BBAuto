create procedure [dbo].[GetMileageByCarId]
  @idCar int
as
  select
    m.Id,
    m.CarId,
    m.Date,
    m.Count
  from
    Mileage m
  where
    m.carId = @idCar
