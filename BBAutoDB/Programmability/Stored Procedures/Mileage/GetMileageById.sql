create procedure [dbo].[GetMileageById]
  @id int
as
 select
    m.Id,
    m.CarId,
    m.Date,
    m.Count
  from
    Mileage m
  where
    m.Id = @id
