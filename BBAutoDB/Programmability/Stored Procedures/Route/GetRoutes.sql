create procedure [dbo].[GetRoutes]
as
begin
  select
    route_id,
    mypoint1_id,
    mypoint2_id,
    route_distance
  from
    Route
end
