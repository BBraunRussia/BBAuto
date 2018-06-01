CREATE PROCEDURE [dbo].[UserAccess_Insert]
@idDriver int,
@idRole int
AS
BEGIN
	Declare @count int
	SELECT @count=COUNT(*) FROM UserAccess WHERE driver_id=@idDriver
	
	if (@count = 0)
		INSERT INTO UserAccess VALUES(@idDriver, @idRole)
	else
		UPDATE UserAccess SET role_id=@idRole WHERE driver_id=@idDriver
END
GO
