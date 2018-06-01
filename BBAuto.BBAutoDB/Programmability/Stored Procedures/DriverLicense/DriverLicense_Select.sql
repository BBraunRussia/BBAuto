CREATE PROCEDURE [dbo].[DriverLicense_Select]
AS
BEGIN
	SELECT DriverLicense_id, driver_id, DriverLicense_number, DriverLicense_dateBegin, DriverLicense_dateEnd,
		DriverLicense_file, DriverLicense_notificationSent
	FROM DriverLicense
END
GO
