create procedure [dbo].[GetMileageByCarId]
  @CarId int
as
  select
    m.Id,
    m.CarId,
    m.Date,
    m.Count
  from
    Mileage m
  where
    m.CarId = @CarId
