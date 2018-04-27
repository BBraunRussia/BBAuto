create procedure [dbo].[GetMileageById]
  @idMileage int
as
 select
    m.id,
    m.carId,
    m.Date,
    m.Count
  from
    Mileage m
  where
    m.Id = @idMileage
