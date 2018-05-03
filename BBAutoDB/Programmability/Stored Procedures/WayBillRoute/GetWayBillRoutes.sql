create procedure [dbo].[GetWayBillRoutes]
as
begin
  select
    wayBillRoute_id,
    wayBillDay_id,
    myPoint1_id,
    myPoint2_id,
    wayBillRoute_distance
  from
    WayBillRoute
end
