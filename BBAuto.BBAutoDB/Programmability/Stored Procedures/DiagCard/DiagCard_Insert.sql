CREATE PROCEDURE [dbo].[DiagCard_Insert]
@idDiagCard int,
@idCar int,
@number varchar(50),
@date datetime,
@file varchar(200),
@notificationSent int
AS
BEGIN
	if (@idDiagCard = 0)
	begin
		INSERT INTO DiagCard VALUES(@idCar, @number, @date, @file, @notificationSent)
		
		SET @idDiagCard = SCOPE_IDENTITY()
	end
	else
		UPDATE DiagCard
		SET diagCard_number=@number, diagCard_date=@date, diagCard_file=@file,
			diagCard_notificationSent=@notificationSent
		WHERE diagCard_id=@idDiagCard
	
	SELECT @idDiagCard
END
GO
