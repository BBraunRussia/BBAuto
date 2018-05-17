create procedure dbo.GetCarDocByCarId
  @carId int
as
  select
    id,
    CarId,
    [Name],
    [File]
  from
    CarDoc
  where
    carId = @carId
