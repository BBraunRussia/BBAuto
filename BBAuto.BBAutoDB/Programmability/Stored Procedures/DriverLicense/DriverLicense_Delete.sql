CREATE PROCEDURE [dbo].[DriverLicense_Delete]
@idLicense int
AS
BEGIN
	DELETE FROM DriverLicense WHERE DriverLicense_id=@idLicense
END
GO
