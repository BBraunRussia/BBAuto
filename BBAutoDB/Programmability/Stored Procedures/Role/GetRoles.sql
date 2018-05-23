create procedure [dbo].[GetRoles]
as
begin
  select Id, [Name] from [Role]
end
