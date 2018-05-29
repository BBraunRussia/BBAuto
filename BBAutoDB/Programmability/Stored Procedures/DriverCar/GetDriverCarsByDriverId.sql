create procedure [dbo].[GetDriverCarsByDriverId]
  @driverId int,
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
    DriverId = @driverId and
    @date between Date1 and Date2
  order by Date2 desc
