create procedure [dbo].[GetDriverCarsByCarId]
  @carId int
as
  select
    CarId,
    DriverId,
    Date1,
    Date2,
    Number
  from
    Function_DriverCar_Select()
  where
    CarId = @carId
  order by Date2 desc, Number desc
