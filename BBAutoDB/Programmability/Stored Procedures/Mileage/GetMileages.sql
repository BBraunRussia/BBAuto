create procedure [dbo].[GetMileages]
as
  select
    m.Id,
    m.CarId,
    m.Date,
    m.Count
  from
    Mileage m
