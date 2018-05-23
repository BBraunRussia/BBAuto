create procedure [dbo].[GetWayBillDays]
as
begin
  select
    Id,
    CarId,
    DriverId,
    [Date]
  from
    WayBillDay
end
