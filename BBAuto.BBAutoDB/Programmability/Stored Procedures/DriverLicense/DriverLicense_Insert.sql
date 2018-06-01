CREATE PROCEDURE [dbo].[DriverLicense_Insert]
@idDriverLicense int,
@idDriver int,
@number varchar(50),
@dateBegin datetime,
@dateEnd datetime,
@file varchar(100),
@notificationSent int
AS
BEGIN
	if (@idDriverLicense = 0)
	begin
		INSERT INTO DriverLicense Values(@number, @dateBegin, @dateEnd, @idDriver, @file, 0)
		
		SET @idDriverLicense=SCOPE_IDENTITY()
	end
	else
		UPDATE DriverLicense SET DriverLicense_number=@number, DriverLicense_dateBegin=@dateBegin,
			DriverLicense_dateEnd=@dateEnd, DriverLicense_file=@file,
			DriverLicense_notificationSent=@notificationSent
		WHERE DriverLicense_id=@idDriverLicense
		
	SELECT @idDriverLicense
END
GO
