CREATE PROCEDURE [dbo].[Driver_Insert]
@idDriver int,
@fio varchar(100),
@idRegion int,
@dateBirthText varchar(50),
@mobile varchar(10),
@email varchar(100),
@fired int,
@ExpSince int,
@idPosition int,
@idDept int,
@login varchar(8),
@idOwner int,
@suppyAddress varchar(500),
@sex int,
@decret int,
@dateStopNotificationText datetime,
@number varchar(50),
@isDriver int,
@from1C int
AS
BEGIN
	Declare @dateBirth datetime	
	if (@dateBirthText = '')
		SET @dateBirth = null
	else
		SET @dateBirth = CAST(@dateBirthText as datetime)
	
	Declare @dateStopNotification datetime
	if (@dateStopNotificationText = '')
		SET @dateStopNotification = null
	else
		SET @dateStopNotification = CAST(@dateStopNotificationText as datetime)
	
	if ((@idDriver = 0) and (@number <> ''))
	begin
		SELECT @idDriver=driver_id FROM Driver WHERE driver_number=@number
		
		if (@idDriver is null)
			SET @idDriver = 0
	end
	
	if (@idDriver = 0)
	begin
		INSERT INTO Driver VALUES(@fio, @idRegion, @dateBirth, @mobile, @email, @fired, @ExpSince,
			@idPosition, @idDept, @login, @idOwner, @suppyAddress, @sex, @decret,
			@dateStopNotification, @number, @isDriver, @from1C)
		SET @idDriver = SCOPE_IDENTITY()
	end
	else if (@idPosition = 47 and @login = 'petumiru')
		UPDATE Driver
		SET driver_fio=@fio, region_id=@idRegion, driver_dateBirth=@dateBirth,
			driver_mobile='', driver_email='', driver_fired=@fired,
			driver_expSince=@ExpSince, position_id=@idPosition, dept_id=@idDept,
			driver_login=@login, owner_id=@idOwner, driver_suppyAddress=@suppyAddress,
			driver_sex=@sex, driver_decret=@decret,
			driver_dateStopNotification=@dateStopNotification,
			driver_number=@number, driver_isDriver=0,
			driver_from1C=@from1C
		WHERE driver_id=@idDriver

	else
		UPDATE Driver
		SET driver_fio=@fio, region_id=@idRegion, driver_dateBirth=@dateBirth,
			driver_mobile=@mobile, driver_email=@email, driver_fired=@fired,
			driver_expSince=@ExpSince, position_id=@idPosition, dept_id=@idDept,
			driver_login=@login, owner_id=@idOwner, driver_suppyAddress=@suppyAddress,
			driver_sex=@sex, driver_decret=@decret,
			driver_dateStopNotification=@dateStopNotification,
			driver_number=@number, driver_isDriver=@isDriver,
			driver_from1C=@from1C
		WHERE driver_id=@idDriver
		
	SELECT @idDriver
END
GO
