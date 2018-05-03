create procedure [dbo].[DeleteUserAccess]
  @idDriver int,
  @idRole int
as
begin
  delete from UserAccess
  where driver_id = @idDriver
    and role_id = @idRole
end
