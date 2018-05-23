create procedure [dbo].[GetRoutes]
as
begin
  select
    Id,
    MypointId1,
    MypointId2,
    Distance
  from
    Route
end
