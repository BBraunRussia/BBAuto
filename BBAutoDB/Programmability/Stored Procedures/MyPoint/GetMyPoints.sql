create procedure [dbo].[GetMyPoints]
as
begin
  select Id, RegionId, [Name] from MyPoint
end
