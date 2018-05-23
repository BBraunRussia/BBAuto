create procedure [dbo].[CulpritSelectWithUser]
  @idCar int,
  @date datetime
as
begin
  select * into #table1 from GetDriverCars()

  declare @DriverId int

  select
    @DriverId = driverId
  from
    #table1
  where
    car_id = @idCar
    and @date >= date1
    and @date < date2

  select id, [Name] from Culprit where id != 4
  union
  select 4, Fio from Driver where id = @DriverId
end
go
