create procedure [dbo].[GetRoles]
as
begin
  select role_id, role_name from Role
end
