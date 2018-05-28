create procedure [dbo].[GetDriverCarsByCarIdAndDate]
  @carId int,
  @date datetime
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
    CarId = @carId and
    @date between Date1 and Date2
  order by Date2 desc, Number desc
