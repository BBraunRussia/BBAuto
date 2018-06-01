CREATE PROCEDURE [dbo].[MedicalCert_Insert]
@id int,
@idDriver int,
@number varchar(50),
@dateBegin datetime,
@dateEnd datetime,
@file varchar(500),
@notificationSent int
AS
BEGIN
	if (@id = 0)
	begin
		INSERT INTO MedicalCert VALUES(@number, @dateBegin, @dateEnd, @idDriver, @file, 0)
		
		SET @id = SCOPE_IDENTITY()
	end
	else
		UPDATE MedicalCert SET MedicalCert_number=@number, MedicalCert_dateBegin=@dateBegin,
			MedicalCert_dateEnd=@dateEnd, driver_id=@idDriver, MedicalCert_file=@file,
			MedicalCert_notificationSent=@notificationSent
		WHERE MedicalCert_id=@id
		
	SELECT @id
END
GO
