create procedure [dbo].[GetWayBillRoutes]
as
begin
  select
    Id,
    WayBillDayId,
    MyPointId1,
    MyPointId2,
    Distance
  from
    WayBillRoute
end
