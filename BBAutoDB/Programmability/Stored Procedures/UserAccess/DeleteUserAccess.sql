create procedure [dbo].[DeleteUserAccess]
  @DriverId int,
  @RoleId int
as
begin
  delete from UserAccess
  where DriverId = @DriverId
    and RoleId = @RoleId
end
