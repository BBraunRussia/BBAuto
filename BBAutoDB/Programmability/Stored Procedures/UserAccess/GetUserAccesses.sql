create procedure [dbo].[GetUserAccesses]
as
begin
  select DriverId, RoleId from UserAccess
end
