create procedure [dbo].[GetUserAccesses]
as
begin
  select driver_id, role_id from UserAccess
end
