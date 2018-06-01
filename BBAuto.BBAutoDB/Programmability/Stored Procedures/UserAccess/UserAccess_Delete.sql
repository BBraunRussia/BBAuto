CREATE PROCEDURE [dbo].[UserAccess_Delete]
@idDriver int,
@idRole int
AS
BEGIN
	DELETE FROM UserAccess WHERE driver_id=@idDriver and role_id=@idRole
END
GO
