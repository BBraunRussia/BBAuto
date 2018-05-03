create procedure [dbo].[GetMyPoints]
as
begin
  select mypoint_id, region_id, mypoint_name from MyPoint
end
